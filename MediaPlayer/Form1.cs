using System;
using System.Diagnostics;
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
        private LibVLC _libVLC;
        private LibVLCSharp.Shared.MediaPlayer _mediaPlayer;

        public Form1()
        {
            InitializeComponent();

            Core.Initialize();

            _libVLC = new LibVLC();
            _mediaPlayer = new LibVLCSharp.Shared.MediaPlayer(_libVLC);
            videoView1.MediaPlayer = _mediaPlayer;


        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Video Files|*.mp4;*.wmv;*.avi;*.mkv|All Files|*.*";
                openFileDialog.Title = "Open Media File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Stops currently playing video before loading a new one.
                    if (_mediaPlayer.IsPlaying)
                    {
                        _mediaPlayer.Stop();
                    }

                    // Creates media from the local file path.
                    using (var media = new Media(_libVLC, openFileDialog.FileName, FromType.FromPath))
                    {
                        _mediaPlayer.Play(media);
                    }
                }
            }
        }


        // Method for keeping resources clean when the form is closed.
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _mediaPlayer?.Dispose();
            _libVLC?.Dispose();
            base.OnFormClosed(e);
        }

        // Method for handling button clicks to play media.
        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (_mediaPlayer != null && !_mediaPlayer.IsPlaying)
            {
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
    }
}