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


        public Form1()
        {
            InitializeComponent();

            Core.Initialize();

            _libVLC = new LibVLC();
            _mediaPlayer = new LibVLCSharp.Shared.MediaPlayer(_libVLC);
            videoView1.MediaPlayer = _mediaPlayer;

			// Subscriber to the events of media player
            _mediaPlayer.LengthChanged += MediaPlayer_LengthChanged;
            _mediaPlayer.TimeChanged += MediaPlayer_TimeChanged;
        }

		private async void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.Filter = "Video Files|*.mp4;*.wmv;*.avi;*.mkv|All Files|*.*";
				openFileDialog.Title = "Open Media File";

				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					string newPath = openFileDialog.FileName;

					await Task.Run(() =>
					{
						if (_mediaPlayer != null && _mediaPlayer.IsPlaying)
						{
							_mediaPlayer.Stop();
						}

						if (media != null)
						{
							media.Dispose();
							media = null;
						}
					});

					media = new Media(_libVLC, newPath, FromType.FromPath);
					_mediaPlayer.Media = media;
					_mediaPlayer.Play();
				}
			}
			//doesnt work but im keeping this here just in case
			//_mediaPlayer.SetAudioOutput(_mediaPlayer.AudioOutputDeviceEnum[0].DeviceIdentifier);
			//_mediaPlayer.SetAudioTrack(0);
			//_mediaPlayer.Volume = 100;
		}


        // Method for handling button clicks to play media.
        private void btnPlay_Click(object sender, EventArgs e)
        {
			if (_mediaPlayer != null && !_mediaPlayer.IsPlaying)
            {
                if (!_mediaPlayer.WillPlay)
                {
                    _mediaPlayer.Play(_mediaPlayer.Media);
				}
                _mediaPlayer.Play();
            }
        }

        // Method for handling button clicks to pause media.
        private void btnPause_Click(object sender, EventArgs e)
        {
            if (_mediaPlayer != null && _mediaPlayer.IsPlaying)
            {
                _mediaPlayer.Pause();
			}
		}

        // Method for handling button clicks to stop media.
        private void btnMute_Click(object sender, EventArgs e)
        {
            if (_mediaPlayer != null)
            {
                _mediaPlayer.Mute = !_mediaPlayer.Mute;
            }
        }

        // Method for handling trackbar scroll events to adjust volume.
        private void trackBarVolume_Scroll(object sender, EventArgs e)
        {
            if (_mediaPlayer != null)
            {
                TrackBar volumeSlider = (TrackBar)sender;
                _mediaPlayer.Volume = volumeSlider.Value;
            }
        }

		// Method for keeping resources clean when the form is closed.
		protected override void OnFormClosed(FormClosedEventArgs e)
		{
            _mediaPlayer.LengthChanged -= MediaPlayer_LengthChanged;
            _mediaPlayer.TimeChanged -= MediaPlayer_TimeChanged;

            _mediaPlayer?.Dispose();
            _libVLC?.Dispose();
            media?.Dispose();
            base.OnFormClosed(e);
        }


		private void trackBar4_Scroll(object sender, EventArgs e)
		{
            if (_mediaPlayer != null)
            {
                // Set the video time to the trackbar value.
                _mediaPlayer.Time = trackBar1.Value;
            }
        }


		private void quitProgram(object sender, EventArgs e)
		{
			this.Close();
        }


        private void MediaPlayer_LengthChanged(object sender, MediaPlayerLengthChangedEventArgs e)
        {

            this.Invoke(new Action(() =>
            {
                // Only update if the length is a valid positive number
                if (e.Length > 0)
                {
                    trackBar1.Minimum = 0;
                    trackBar1.Maximum = (int)e.Length;
                }
            }));
        }

        private void MediaPlayer_TimeChanged(object sender, MediaPlayerTimeChangedEventArgs e)
        {
            
            Console.WriteLine($"Current Video Time: {e.Time} ms");

            this.Invoke(new Action(() =>
            {
                if (trackBar1.Capture == false)
                {
                    // Clamp value to ensure it doesn't crash if VLC reports 
                    // a time slightly higher than the length
                    int value = (int)e.Time;
                    if (value > trackBar1.Maximum) value = trackBar1.Maximum;
                    if (value < 0) value = 0;

                    trackBar1.Value = value;
                }
            }));
        }
    }
}