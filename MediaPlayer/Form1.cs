using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibVLCSharp.Shared;
using LibVLCSharp.Shared.Structures;

namespace MediaPlayer
{
	public partial class Form1 : Form
	{
		// jaspreet bath - core engine and main player fields
		internal LibVLC _libVLC;
		internal LibVLCSharp.Shared.MediaPlayer _mediaPlayer;

		// justin chik - dual player and track detection
		internal LibVLCSharp.Shared.MediaPlayer _playerMic;
		internal Media media1;
		internal Media media2;
		private int _sysTrackId = -1;
		private int _micTrackId = -1;
		private bool _isDualTrack = false;

		public Form1()
		{
			// jaspreet bath - ui and vlc core initialization
			InitializeComponent();
			Core.Initialize();

			_libVLC = new LibVLC("--aout=directx", "--audio");

			_mediaPlayer = new LibVLCSharp.Shared.MediaPlayer(_libVLC);
			_playerMic = new LibVLCSharp.Shared.MediaPlayer(_libVLC);

			videoView1.MediaPlayer = _mediaPlayer;

			// jaspreet bath - trackbar event subscriptions
			BindEvents();

			// justin chik - sync events for dual playback
			_mediaPlayer.Playing += (s, e) => { if (_isDualTrack && !_playerMic.IsPlaying) _playerMic.Play(); };
			_mediaPlayer.Paused += (s, e) => { if (_isDualTrack) _playerMic.Pause(); };
			_mediaPlayer.Stopped += (s, e) => { if (_playerMic != null) _playerMic.Stop(); };
		}

		// jaspreet bath - helper to bind vlc events
		private void BindEvents()
		{
			_mediaPlayer.LengthChanged += MediaPlayer_LengthChanged;
			_mediaPlayer.TimeChanged += MediaPlayer_TimeChanged;
		}

		// jaspreet bath - helper to unbind vlc events to prevent deadlocks
		private void UnbindEvents()
		{
			_mediaPlayer.LengthChanged -= MediaPlayer_LengthChanged;
			_mediaPlayer.TimeChanged -= MediaPlayer_TimeChanged;
		}

		// justin chik - open file with async cleanup and event unbinding
		private async void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.Filter = "Video Files|*.mp4;*.wmv;*.avi;*.mkv|All Files|*.*";
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					string newPath = openFileDialog.FileName;

					// justin chik - immediate state reset
					_isDualTrack = false;
					UnbindEvents(); // jaspreet bath - kill events first to stop deadlocks

					AudioTracksPanel.Controls.Clear();

					// jaspreet bath - async stop and dispose
					await Task.Run(() => {
						_mediaPlayer.Stop();
						_playerMic.Stop();
						_mediaPlayer.Media = null;
						_playerMic.Media = null;
						media1?.Dispose();
						media2?.Dispose();
					});

					media1 = new Media(_libVLC, newPath, FromType.FromPath);
					_mediaPlayer.Media = media1;

					// jaspreet bath - re-bind events for the new file
					BindEvents();
					_mediaPlayer.Play();

					// justin chik - delay for track identification
					await Task.Delay(500);
					_mediaPlayer.Volume = 100;

					var tracks = _mediaPlayer.AudioTrackDescription;
					int count = 0;
					_sysTrackId = -1;
					_micTrackId = -1;

					foreach (var t in tracks)
					{
						if (t.Id == -1) continue;
						if (count == 0) _sysTrackId = t.Id;
						if (count == 1) _micTrackId = t.Id;
						count++;
					}

					// justin chik - initialize background mic player if second track exists
					if (count >= 2 && _micTrackId != -1)
					{
						_isDualTrack = true;
						media2 = new Media(_libVLC, newPath, FromType.FromPath);
						media2.AddOption(":no-video");
						media2.AddOption($":audio-track-id={_micTrackId}");

						_playerMic.Media = media2;
						_playerMic.Play();
						_playerMic.Volume = 100;
					}

					if (_sysTrackId != -1) CreateTrackControl("System Audio", _mediaPlayer);
					if (_isDualTrack) CreateTrackControl("Microphone", _playerMic);
				}
			}
		}

		// justin chik - dynamic slider generation and independent volume control
		private void CreateTrackControl(string name, LibVLCSharp.Shared.MediaPlayer player)
		{
			FlowLayoutPanel container = new FlowLayoutPanel { AutoSize = true, Padding = new Padding(5) };
			Label lbl = new Label { Text = name, AutoSize = true, Width = 80, Margin = new Padding(0, 8, 0, 0) };
			TrackBar bar = new TrackBar { Minimum = 0, Maximum = 100, Value = 100, Width = 150 };

			bar.Scroll += (s, ev) => { player.Volume = bar.Value; };

			container.Controls.Add(lbl);
			container.Controls.Add(bar);
			AudioTracksPanel.Controls.Add(container);

			player.Volume = bar.Value;
		}

		// justin chik - play button with forced dual reset for replay
		private async void btnPlay_Click(object sender, EventArgs e)
		{
			if (_mediaPlayer == null) return;

			if (_mediaPlayer.State == VLCState.Ended || _mediaPlayer.State == VLCState.Stopped)
			{
				_mediaPlayer.Stop();
				_playerMic.Stop();

				await Task.Delay(50);

				_mediaPlayer.Play();
				if (_isDualTrack) _playerMic.Play();
			}
			else if (!_mediaPlayer.IsPlaying)
			{
				_mediaPlayer.Play();
				if (_isDualTrack) _playerMic.Play();
			}
		}

		// jaspreet bath - pausing logic for primary and secondary players
		private void btnPause_Click(object sender, EventArgs e)
		{
			_mediaPlayer?.Pause();
			if (_isDualTrack) _playerMic?.Pause();
		}

		// justin chik - synchronized muting
		private void btnMute_Click(object sender, EventArgs e)
		{
			bool muteState = !_mediaPlayer.Mute;
			_mediaPlayer.Mute = muteState;
			if (_isDualTrack) _playerMic.Mute = muteState;
		}

		// jaspreet bath - master volume trackbar listener
		private void trackBarVolume_Scroll(object sender, EventArgs e)
		{
			TrackBar bar = (TrackBar)sender;
			if (_mediaPlayer != null) _mediaPlayer.Volume = bar.Value;

			// justin chik - master volume synchronization
			if (_isDualTrack) _playerMic.Volume = bar.Value;
		}

		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			// jaspreet bath - clean up main player and event handlers
			UnbindEvents();
			_mediaPlayer?.Dispose();

			// justin chik - dual track resource cleanup
			_playerMic?.Dispose();
			_libVLC?.Dispose();
			media1?.Dispose();
			media2?.Dispose();
			base.OnFormClosed(e);
		}

		// jaspreet bath - standard seek logic with dual track support
		private void trackBar_Scroll(object sender, EventArgs e)
		{
			if (_mediaPlayer != null)
			{
				long newTime = trackBar1.Value;
				_mediaPlayer.Time = newTime;

				// justin chik - synchronization of audio tracks during seek
				if (_isDualTrack && _playerMic != null)
				{
					_playerMic.Time = newTime;
				}
			}
		}

		// jaspreet bath - application exit handler
		private void quitProgram(object sender, EventArgs e)
		{
			this.Close();
		}

		// jaspreet bath - update slider maximum based on media length
		private void MediaPlayer_LengthChanged(object sender, MediaPlayerLengthChangedEventArgs e)
		{
			this.Invoke(new Action(() =>
			{
				if (e.Length > 0)
				{
					trackBar1.Minimum = 0;
					trackBar1.Maximum = (int)e.Length;
				}
			}));
		}

		// jaspreet bath - update slider position based on current time
		private void MediaPlayer_TimeChanged(object sender, MediaPlayerTimeChangedEventArgs e)
		{
			this.Invoke(new Action(() =>
			{
				if (trackBar1.Capture == false)
				{
					int value = (int)e.Time;
					if (value > trackBar1.Maximum) value = trackBar1.Maximum;
					if (value < 0) value = 0;

					trackBar1.Value = value;
				}
			}));
		}

		// jaspreet bath - redundancy seeker logic
		private void trackBar4_Scroll(object sender, EventArgs e)
		{
			trackBar_Scroll(sender, e);
		}
	}
}