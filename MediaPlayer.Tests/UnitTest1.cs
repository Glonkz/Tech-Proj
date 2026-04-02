using System;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibVLCSharp.Shared;
using MediaPlayer;

namespace MediaPlayer.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private Form1 _form;

        // Runs exactly once before the test suite starts
        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            // crashes if called more than once per process.
            Core.Initialize();
        }

        [TestInitialize]
        public void Setup()
        {
            _form = new Form1();

            
            var dummyHandle = _form.Handle;
        }

        [TestMethod]
        public void Constructor_InitializesVlcComponents()
        {
            Assert.IsNotNull(_form._libVLC,
                "LibVLC core should be initialized.");
            Assert.IsNotNull(_form._mediaPlayer,
                "MediaPlayer instance should be initialized.");
        }

        [TestMethod]
        public void BtnMute_Click_TogglesMuteState()
        {
            _form.Show();

            // Loading test media to make sure media is valid
            using (var dummyMedia = new Media(_form._libVLC,
                new Uri("http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ElephantsDream.mp4")))
            {
                _form._mediaPlayer.Play(dummyMedia);

                // allowing audio to stabilize
                System.Threading.Thread.Sleep(500);


                _form._mediaPlayer.Mute = false;
                bool initialMuteState = _form._mediaPlayer.Mute;


                MethodInfo muteMethod = typeof(Form1).GetMethod("btnMute_Click",
                    BindingFlags.NonPublic | 
                    BindingFlags.Instance);

                muteMethod.Invoke(_form, new object[] { null, EventArgs.Empty });


                Assert.AreNotEqual(initialMuteState, _form._mediaPlayer.Mute, 
                    "The mute state did not toggle correctly.");


                _form._mediaPlayer.Stop();
            }
        }


        [TestMethod]
        public void TrackBarVolume_Scroll_UpdatesMediaPlayerVolume()
        {
            TrackBar mockTrackBar = new TrackBar();

            mockTrackBar.Maximum = 100;
            mockTrackBar.Value = 75;

            MethodInfo volumeMethod = typeof(Form1).GetMethod("trackBarVolume_Scroll",
                BindingFlags.NonPublic |
                BindingFlags.Instance);

            volumeMethod.Invoke(_form, new object[] { mockTrackBar, EventArgs.Empty });

            Assert.AreEqual(75, _form._mediaPlayer.Volume,
                "The media player volume was not updated to the trackbar value.");
        }

        [TestCleanup]
        public void Cleanup()
        {
            // clean LibVLC unmanaged resources first
            if (_form != null)
            {
                _form._mediaPlayer?.Dispose();
                _form._libVLC?.Dispose();
                _form.Dispose();
            }
        }
    }
}