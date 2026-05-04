namespace Proiect_IP_Barbati
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fisierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playlistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listBoxPlaylist = new System.Windows.Forms.ListBox();
            this.trackBarSong = new System.Windows.Forms.TrackBar();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonPrev = new System.Windows.Forms.Button();
            this.trackBarVolum = new System.Windows.Forms.TrackBar();
            this.listViewSongs = new System.Windows.Forms.ListView();
            this.listViewAddNew = new System.Windows.Forms.ListView();
            this.progressTimer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolum)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fisierToolStripMenuItem,
            this.playlistToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 28);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fisierToolStripMenuItem
            // 
            this.fisierToolStripMenuItem.Name = "fisierToolStripMenuItem";
            this.fisierToolStripMenuItem.Size = new System.Drawing.Size(57, 24);
            this.fisierToolStripMenuItem.Text = "Fisier";
            // 
            // playlistToolStripMenuItem
            // 
            this.playlistToolStripMenuItem.Name = "playlistToolStripMenuItem";
            this.playlistToolStripMenuItem.Size = new System.Drawing.Size(69, 24);
            this.playlistToolStripMenuItem.Text = "Playlist";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // listBoxPlaylist
            // 
            this.listBoxPlaylist.FormattingEnabled = true;
            this.listBoxPlaylist.ItemHeight = 16;
            this.listBoxPlaylist.Location = new System.Drawing.Point(12, 40);
            this.listBoxPlaylist.Name = "listBoxPlaylist";
            this.listBoxPlaylist.Size = new System.Drawing.Size(120, 308);
            this.listBoxPlaylist.TabIndex = 1;
            // 
            // trackBarSong
            // 
            this.trackBarSong.Location = new System.Drawing.Point(239, 74);
            this.trackBarSong.Name = "trackBarSong";
            this.trackBarSong.Size = new System.Drawing.Size(316, 56);
            this.trackBarSong.TabIndex = 2;
            // 
            // buttonPlay
            // 
            this.buttonPlay.Location = new System.Drawing.Point(180, 136);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(75, 23);
            this.buttonPlay.TabIndex = 3;
            this.buttonPlay.Text = "Play";
            this.buttonPlay.UseVisualStyleBackColor = true;
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(281, 135);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(92, 23);
            this.buttonStop.TabIndex = 4;
            this.buttonStop.Text = "Stop/Pause";
            this.buttonStop.UseVisualStyleBackColor = true;
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(394, 135);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(75, 23);
            this.buttonNext.TabIndex = 5;
            this.buttonNext.Text = "Next";
            this.buttonNext.UseVisualStyleBackColor = true;
            // 
            // buttonPrev
            // 
            this.buttonPrev.Location = new System.Drawing.Point(506, 135);
            this.buttonPrev.Name = "buttonPrev";
            this.buttonPrev.Size = new System.Drawing.Size(75, 23);
            this.buttonPrev.TabIndex = 6;
            this.buttonPrev.Text = "Previous";
            this.buttonPrev.UseVisualStyleBackColor = true;
            // 
            // trackBarVolum
            // 
            this.trackBarVolum.Location = new System.Drawing.Point(732, 55);
            this.trackBarVolum.Name = "trackBarVolum";
            this.trackBarVolum.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarVolum.Size = new System.Drawing.Size(56, 104);
            this.trackBarVolum.TabIndex = 7;
            // 
            // listViewSongs
            // 
            this.listViewSongs.HideSelection = false;
            this.listViewSongs.Location = new System.Drawing.Point(192, 197);
            this.listViewSongs.Name = "listViewSongs";
            this.listViewSongs.Size = new System.Drawing.Size(389, 162);
            this.listViewSongs.TabIndex = 8;
            this.listViewSongs.UseCompatibleStateImageBehavior = false;
            // 
            // listViewAddNew
            // 
            this.listViewAddNew.AllowDrop = true;
            this.listViewAddNew.HideSelection = false;
            this.listViewAddNew.Location = new System.Drawing.Point(192, 390);
            this.listViewAddNew.Name = "listViewAddNew";
            this.listViewAddNew.Size = new System.Drawing.Size(389, 38);
            this.listViewAddNew.TabIndex = 9;
            this.listViewAddNew.UseCompatibleStateImageBehavior = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listViewAddNew);
            this.Controls.Add(this.listViewSongs);
            this.Controls.Add(this.trackBarVolum);
            this.Controls.Add(this.buttonPrev);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.trackBarSong);
            this.Controls.Add(this.listBoxPlaylist);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fisierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playlistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ListBox listBoxPlaylist;
        private System.Windows.Forms.TrackBar trackBarSong;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonPrev;
        private System.Windows.Forms.TrackBar trackBarVolum;
        private System.Windows.Forms.ListView listViewSongs;
        private System.Windows.Forms.ListView listViewAddNew;
        private System.Windows.Forms.Timer progressTimer;
    }
}

