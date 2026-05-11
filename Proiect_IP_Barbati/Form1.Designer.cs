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
            this.buttonPlay = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonPrev = new System.Windows.Forms.Button();
            this.listViewSongs = new System.Windows.Forms.ListView();
            this.listViewAddNew = new System.Windows.Forms.ListView();
            this.progressTimer = new System.Windows.Forms.Timer(this.components);
            this.labelVolum = new System.Windows.Forms.Label();
            this.textBoxMusicName = new System.Windows.Forms.TextBox();
            this.trackBarVolum = new ModernSlider();
            this.trackBarSong = new ModernSlider();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fisierToolStripMenuItem,
            this.playlistToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip.Size = new System.Drawing.Size(788, 31);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fisierToolStripMenuItem
            // 
            this.fisierToolStripMenuItem.Name = "fisierToolStripMenuItem";
            this.fisierToolStripMenuItem.Size = new System.Drawing.Size(66, 27);
            this.fisierToolStripMenuItem.Text = "Fisier";
            // 
            // playlistToolStripMenuItem
            // 
            this.playlistToolStripMenuItem.Name = "playlistToolStripMenuItem";
            this.playlistToolStripMenuItem.Size = new System.Drawing.Size(81, 27);
            this.playlistToolStripMenuItem.Text = "Playlist";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(62, 27);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // listBoxPlaylist
            // 
            this.listBoxPlaylist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.listBoxPlaylist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxPlaylist.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxPlaylist.ForeColor = System.Drawing.Color.White;
            this.listBoxPlaylist.FormattingEnabled = true;
            this.listBoxPlaylist.ItemHeight = 23;
            this.listBoxPlaylist.Location = new System.Drawing.Point(12, 40);
            this.listBoxPlaylist.Name = "listBoxPlaylist";
            this.listBoxPlaylist.Size = new System.Drawing.Size(120, 301);
            this.listBoxPlaylist.TabIndex = 1;
            // 
            // buttonPlay
            // 
            this.buttonPlay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.buttonPlay.FlatAppearance.BorderSize = 0;
            this.buttonPlay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Purple;
            this.buttonPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPlay.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPlay.Location = new System.Drawing.Point(180, 126);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(113, 40);
            this.buttonPlay.TabIndex = 3;
            this.buttonPlay.Text = "Play";
            this.buttonPlay.UseVisualStyleBackColor = false;
            // 
            // buttonStop
            // 
            this.buttonStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.buttonStop.FlatAppearance.BorderSize = 0;
            this.buttonStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Purple;
            this.buttonStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStop.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStop.Location = new System.Drawing.Point(331, 126);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(124, 41);
            this.buttonStop.TabIndex = 4;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = false;
            // 
            // buttonNext
            // 
            this.buttonNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.buttonNext.FlatAppearance.BorderSize = 0;
            this.buttonNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Purple;
            this.buttonNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNext.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNext.Location = new System.Drawing.Point(628, 126);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(95, 40);
            this.buttonNext.TabIndex = 5;
            this.buttonNext.Text = "Next";
            this.buttonNext.UseVisualStyleBackColor = false;
            // 
            // buttonPrev
            // 
            this.buttonPrev.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.buttonPrev.FlatAppearance.BorderSize = 0;
            this.buttonPrev.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Purple;
            this.buttonPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPrev.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPrev.Location = new System.Drawing.Point(489, 126);
            this.buttonPrev.Name = "buttonPrev";
            this.buttonPrev.Size = new System.Drawing.Size(104, 40);
            this.buttonPrev.TabIndex = 6;
            this.buttonPrev.Text = "Previous";
            this.buttonPrev.UseVisualStyleBackColor = false;
            // 
            // listViewSongs
            // 
            this.listViewSongs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.listViewSongs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewSongs.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewSongs.ForeColor = System.Drawing.Color.White;
            this.listViewSongs.HideSelection = false;
            this.listViewSongs.Location = new System.Drawing.Point(192, 197);
            this.listViewSongs.Name = "listViewSongs";
            this.listViewSongs.Size = new System.Drawing.Size(560, 162);
            this.listViewSongs.TabIndex = 8;
            this.listViewSongs.UseCompatibleStateImageBehavior = false;
            // 
            // listViewAddNew
            // 
            this.listViewAddNew.AllowDrop = true;
            this.listViewAddNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.listViewAddNew.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewAddNew.Font = new System.Drawing.Font("Segoe UI Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewAddNew.ForeColor = System.Drawing.Color.White;
            this.listViewAddNew.HideSelection = false;
            this.listViewAddNew.Location = new System.Drawing.Point(192, 390);
            this.listViewAddNew.Name = "listViewAddNew";
            this.listViewAddNew.Size = new System.Drawing.Size(389, 38);
            this.listViewAddNew.TabIndex = 9;
            this.listViewAddNew.UseCompatibleStateImageBehavior = false;
            // 
            // labelVolum
            // 
            this.labelVolum.AutoSize = true;
            this.labelVolum.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVolum.Location = new System.Drawing.Point(675, 371);
            this.labelVolum.Name = "labelVolum";
            this.labelVolum.Size = new System.Drawing.Size(61, 23);
            this.labelVolum.TabIndex = 12;
            this.labelVolum.Text = "Volum";
            // 
            // textBoxMusicName
            // 
            this.textBoxMusicName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.textBoxMusicName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxMusicName.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMusicName.ForeColor = System.Drawing.Color.White;
            this.textBoxMusicName.Location = new System.Drawing.Point(312, 49);
            this.textBoxMusicName.Name = "textBoxMusicName";
            this.textBoxMusicName.Size = new System.Drawing.Size(299, 30);
            this.textBoxMusicName.TabIndex = 13;
            this.textBoxMusicName.Text = "None";
            this.textBoxMusicName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // trackBarVolum
            // 
            this.trackBarVolum.Cursor = System.Windows.Forms.Cursors.Hand;
            this.trackBarVolum.Location = new System.Drawing.Point(652, 390);
            this.trackBarVolum.Maximum = 100;
            this.trackBarVolum.Minimum = 0;
            this.trackBarVolum.Name = "trackBarVolum";
            this.trackBarVolum.ProgressColor = System.Drawing.Color.Purple;
            this.trackBarVolum.Size = new System.Drawing.Size(100, 38);
            this.trackBarVolum.TabIndex = 11;
            this.trackBarVolum.Text = "modernSlider1";
            this.trackBarVolum.TrackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.trackBarVolum.Value = 50;
            // 
            // trackBarSong
            // 
            this.trackBarSong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.trackBarSong.Location = new System.Drawing.Point(249, 85);
            this.trackBarSong.Maximum = 100;
            this.trackBarSong.Minimum = 0;
            this.trackBarSong.Name = "trackBarSong";
            this.trackBarSong.ProgressColor = System.Drawing.Color.Purple;
            this.trackBarSong.Size = new System.Drawing.Size(417, 23);
            this.trackBarSong.TabIndex = 10;
            this.trackBarSong.Text = "modernSlider1";
            this.trackBarSong.TrackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.trackBarSong.Value = 50;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(788, 452);
            this.Controls.Add(this.textBoxMusicName);
            this.Controls.Add(this.labelVolum);
            this.Controls.Add(this.trackBarVolum);
            this.Controls.Add(this.trackBarSong);
            this.Controls.Add(this.listViewAddNew);
            this.Controls.Add(this.listViewSongs);
            this.Controls.Add(this.buttonPrev);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.listBoxPlaylist);
            this.Controls.Add(this.menuStrip);
            this.ForeColor = System.Drawing.Color.White;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Form1";
            this.Text = "MP3";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fisierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playlistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ListBox listBoxPlaylist;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonPrev;
        private System.Windows.Forms.ListView listViewSongs;
        private System.Windows.Forms.ListView listViewAddNew;
        private System.Windows.Forms.Timer progressTimer;
        private ModernSlider trackBarSong;
        private ModernSlider trackBarVolum;
        private System.Windows.Forms.Label labelVolum;
        private System.Windows.Forms.TextBox textBoxMusicName;
    }
}

