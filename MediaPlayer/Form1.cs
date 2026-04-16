using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibVLCSharp.Shared;

/**
 * Name: Dual Media Player
 * Group: Jaspreet Bath, Justin Chik
 * 
 * Description: This application is a media player that uses
 * the LibVLCSharp library and play media files.
 */

namespace MediaPlayer
{

	public partial class Form1 : Form
    {
		internal LibVLC _libVLC;
		internal Media media;
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
            InitializeComponent();

            Core.Initialize();

			_libVLC = new LibVLC("--aout=directx", "--audio");

			_playerSystem = new LibVLCSharp.Shared.MediaPlayer(_libVLC);
			_playerMic = new LibVLCSharp.Shared.MediaPlayer(_libVLC);

			videoView1.MediaPlayer = _playerSystem;

			// justin chik - sync events for dual playback
			_playerSystem.Playing += (s, e) => { if (_isDualTrack && !_playerMic.IsPlaying) _playerMic.Play(); };
			_playerSystem.Paused += (s, e) => { if (_isDualTrack) _playerMic.Pause(); };
			_playerSystem.Stopped += (s, e) => { if (_playerMic != null) _playerMic.Stop(); };
        }

		// justin chik - open file with immediate buffer clearing to prevent ghost audio
		private async void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.Filter = "Video Files|*.mp4;*.wmv;*.avi;*.mkv|All Files|*.*";
				openFileDialog.Title = "Open Media File";

				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					string newPath = openFileDialog.FileName;

					// justin chik - reset state immediately to stop ghosting
					_isDualTrack = false;
					_playerSystem.Stop();
					_playerMic.Stop();
					_playerSystem.Media = null; // force clear buffer
					_playerMic.Media = null;    // force clear buffer
					AudioTracksPanel.Controls.Clear();

					await Task.Run(() => {
						media1?.Dispose();
						media2?.Dispose();
					});

					media1 = new Media(_libVLC, newPath, FromType.FromPath);
					_playerSystem.Media = media1;
					_playerSystem.Play();

					// justin chik - delay for track identification
					await Task.Delay(500);
					_playerSystem.Volume = 100;

					var tracks = _playerSystem.AudioTrackDescription;
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
					});

					if (_sysTrackId != -1) CreateTrackControl("System Audio", _playerSystem);
					if (_isDualTrack) CreateTrackControl("Microphone", _playerMic);
				}
			}
			//doesnt work but im keeping this here just in case
			//_mediaPlayer.SetAudioOutput(_mediaPlayer.AudioOutputDeviceEnum[0].DeviceIdentifier);
			//_mediaPlayer.SetAudioTrack(0);
			//_mediaPlayer.Volume = 100;
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
			if (_playerSystem == null) return;

			if (_playerSystem.State == VLCState.Ended || _playerSystem.State == VLCState.Stopped)
            {
				_playerSystem.Stop();
				_playerMic.Stop();

				await Task.Delay(50);

				_playerSystem.Play();
				if (_isDualTrack) _playerMic.Play();
			}
			else if (!_playerSystem.IsPlaying)
                {
				_playerSystem.Play();
				if (_isDualTrack) _playerMic.Play();
				}
                _mediaPlayer.Play();
            }
        }

        // Method for handling button clicks to pause media.
        private void btnPause_Click(object sender, EventArgs e)
        {
			_playerSystem?.Pause();
			if (_isDualTrack) _playerMic?.Pause();
			}
		}

		// justin chik - synchronized muting
        private void btnMute_Click(object sender, EventArgs e)
        {
			bool muteState = !_playerSystem.Mute;
			_playerSystem.Mute = muteState;
			if (_isDualTrack) _playerMic.Mute = muteState;
            }
        }

		// justin chik - master volume synchronization
        private void trackBarVolume_Scroll(object sender, EventArgs e)
        {
			TrackBar bar = (TrackBar)sender;
			_playerSystem.Volume = bar.Value;
			if (_isDualTrack) _playerMic.Volume = bar.Value;
        }

		// Method for keeping resources clean when the form is closed.
		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			_playerSystem?.Dispose();
			_playerMic?.Dispose();
			_libVLC?.Dispose();
			media1?.Dispose();
			media2?.Dispose();
			base.OnFormClosed(e);
		}

		private void trackBar4_Scroll(object sender, EventArgs e) { }
	}
}