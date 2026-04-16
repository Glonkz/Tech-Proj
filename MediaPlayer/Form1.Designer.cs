namespace MediaPlayer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.menuStrip2 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.videoView1 = new LibVLCSharp.WinForms.VideoView();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.trackBar2 = new System.Windows.Forms.TrackBar();
			this.button1 = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.AudioTracksPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.menuStrip2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.videoView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip2
			// 
			this.menuStrip2.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
			this.menuStrip2.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
			this.menuStrip2.Location = new System.Drawing.Point(0, 0);
			this.menuStrip2.Name = "menuStrip2";
			this.menuStrip2.Size = new System.Drawing.Size(1713, 40);
			this.menuStrip2.TabIndex = 1;
			this.menuStrip2.Text = "menuStrip2";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(71, 36);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new System.Drawing.Size(206, 44);
			this.openToolStripMenuItem.Text = "Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// videoView1
			// 
			this.videoView1.AllowDrop = true;
			this.videoView1.BackColor = System.Drawing.Color.Black;
			this.videoView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.videoView1.Location = new System.Drawing.Point(0, 40);
			this.videoView1.Margin = new System.Windows.Forms.Padding(4);
			this.videoView1.MediaPlayer = null;
			this.videoView1.Name = "videoView1";
			this.videoView1.Size = new System.Drawing.Size(1713, 723);
			this.videoView1.TabIndex = 2;
			this.videoView1.Text = "videoView1";
			// 
			// trackBar1
			// 
			this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.trackBar1.BackColor = System.Drawing.SystemColors.Control;
			this.trackBar1.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.trackBar1.Location = new System.Drawing.Point(212, 19);
			this.trackBar1.Margin = new System.Windows.Forms.Padding(4);
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Size = new System.Drawing.Size(1249, 90);
			this.trackBar1.TabIndex = 4;
			this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBar1.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.button2.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.button2.Location = new System.Drawing.Point(86, 19);
			this.button2.Margin = new System.Windows.Forms.Padding(4);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(96, 71);
			this.button2.TabIndex = 6;
			this.button2.Text = "Pause";
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Click += new System.EventHandler(this.btnPause_Click);
			// 
			// button3
			// 
			this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button3.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.button3.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.button3.Location = new System.Drawing.Point(1469, 17);
			this.button3.Margin = new System.Windows.Forms.Padding(4);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(98, 71);
			this.button3.TabIndex = 7;
			this.button3.Text = "Mute";
			this.button3.UseVisualStyleBackColor = false;
			this.button3.Click += new System.EventHandler(this.btnMute_Click);
			// 
			// trackBar2
			// 
			this.trackBar2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.trackBar2.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.trackBar2.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.trackBar2.Location = new System.Drawing.Point(1583, 8);
			this.trackBar2.Margin = new System.Windows.Forms.Padding(4);
			this.trackBar2.Maximum = 100;
			this.trackBar2.Name = "trackBar2";
			this.trackBar2.Size = new System.Drawing.Size(126, 90);
			this.trackBar2.TabIndex = 8;
			this.trackBar2.MouseCaptureChanged += new System.EventHandler(this.trackBarVolume_Scroll);
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.button1.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.button1.Location = new System.Drawing.Point(12, 19);
			this.button1.Margin = new System.Windows.Forms.Padding(4);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(74, 71);
			this.button1.TabIndex = 5;
			this.button1.Text = "Play";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.btnPlay_Click);
			// 
			// panel1
			// 
			this.panel1.AutoSize = true;
			this.panel1.Controls.Add(this.AudioTracksPanel);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.trackBar1);
			this.panel1.Controls.Add(this.button2);
			this.panel1.Controls.Add(this.button3);
			this.panel1.Controls.Add(this.trackBar2);
			this.panel1.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 763);
			this.panel1.Margin = new System.Windows.Forms.Padding(4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1713, 232);
			this.panel1.TabIndex = 16;
			// 
			// AudioTracksPanel
			// 
			this.AudioTracksPanel.Location = new System.Drawing.Point(3, 114);
			this.AudioTracksPanel.Name = "AudioTracksPanel";
			this.AudioTracksPanel.Size = new System.Drawing.Size(1710, 115);
			this.AudioTracksPanel.TabIndex = 15;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1713, 995);
			this.Controls.Add(this.videoView1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.menuStrip2);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "Form1";
			this.Text = "Form1";
			this.menuStrip2.ResumeLayout(false);
			this.menuStrip2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.videoView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private LibVLCSharp.WinForms.VideoView videoView1;
		private System.Windows.Forms.TrackBar trackBar1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.TrackBar trackBar2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.FlowLayoutPanel AudioTracksPanel;
	}
}

