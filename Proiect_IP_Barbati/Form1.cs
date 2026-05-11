/**************************************************************************
 *                                                                        *
 *  File:        Form1.cs                                                 *
 *  Copyright:   (c) 2025-2026 Aparaschivei Teodor,                       *
 *                  Munteanu Alin Constantin,                             *
 *                  Marguta Dan Alexandru                                 *
 *  E-mail:      teodor.aparaschivei@student.tuiasi.ro                    *
 *               alin-constantin.munteanu@student.tuiasi.ro               *
 *               dan-alexandru.marguta@student.tuiasi.ro                  *                        
 *  Description: UI si callback-uri pentru butoane                        *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/

using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using MP3Player.Core;
using MP3Player.Models;

namespace Proiect_IP_Barbati
{
    public partial class Form1 : Form
    {
        // ── Dependente injectate ──────────────────────────────────
        private readonly PlayerController _controller;

        // ── Timer pentru bara de progres ─────────────────────────
        private Timer _progressTimer;

        private PlaylistRepository _repository;

        // ── Constructor ──────────────────────────────────────────
        public Form1()
        {
            InitializeComponent();

            // Compunem dependentele aici (Composition Root)
            var audioPlayer = new AudioPlayer();
            var playlistManager = new PlaylistManager();

            _repository = new PlaylistRepository();
            _controller = new PlayerController(audioPlayer, playlistManager);

            // Abonam UI-ul la evenimentele din Core (Observer)
            _controller.PlaylistManager.CurrentSongChanged += OnCurrentSongChanged;
            _controller.PlaylistManager.PlaylistChanged += OnPlaylistChanged;

            SetupControls();
            SetupTimer();
            SetupDragDrop();
            LoadLibrary();
        }

        // ── Initializare controale ────────────────────────────────
        private void SetupControls()
        {

            listViewSongs.HeaderStyle = ColumnHeaderStyle.Nonclickable; // Sau setări de culori dacă e DataGridView
            listViewSongs.FullRowSelect = true;
            listViewSongs.OwnerDraw = false;
            listBoxPlaylist.BorderStyle = BorderStyle.None;

            menuStrip.BackColor = Color.FromArgb(25, 20, 20);
            menuStrip.ForeColor = Color.White;
            // ListView melodii
            listViewSongs.GridLines = false; 
            listViewSongs.BackColor = Color.FromArgb(25, 20, 20);
            listViewSongs.BorderStyle = BorderStyle.None;

            listViewSongs.View = View.Details;
            listViewSongs.FullRowSelect = true;
            listViewSongs.Columns.Add("#", 35);
            listViewSongs.Columns.Add("Titlu", 180);
            listViewSongs.Columns.Add("Artist", 120);
            listViewSongs.Columns.Add("Durata", 60);
            listViewSongs.DoubleClick += (s, e) =>
            {
                if (listViewSongs.SelectedIndices.Count > 0)
                    PlayAt(listViewSongs.SelectedIndices[0]);
            };

            // ListView drag & drop
            listViewAddNew.View = View.Details;
            listViewAddNew.Columns.Add("Trage fisiere MP3 aici pentru a le adauga...", 380);

            // TrackBar volum
            trackBarVolum.Minimum = 0;
            trackBarVolum.Maximum = 100;
            trackBarVolum.Value = 70;
            trackBarVolum.ValueChanged += (s, e) => _controller.SetVolume(trackBarVolum.Value);

            // TrackBar progres
            trackBarSong.Minimum = 0;
            trackBarSong.Maximum = 100;
            trackBarSong.Value = 0;
            trackBarSong.ValueChanged += (s, e) =>
                _controller.Seek(trackBarSong.Value);

            // Butoane
            buttonPlay.Click += (s, e) => OnPlayPauseClicked();
            buttonStop.Click += (s, e) => OnStopClicked();
            buttonNext.Click += (s, e) => { _controller.Next(); UpdatePlayButton(); };
            buttonPrev.Click += (s, e) => { _controller.Previous(); UpdatePlayButton(); };

            // Meniu Fisier
            var deschideItem = new ToolStripMenuItem("Deschide fisiere MP3");
            var iesireItem = new ToolStripMenuItem("Iesire");
            deschideItem.Click += MenuDeschide_Click;
            iesireItem.Click += (s, e) => Application.Exit();
            fisierToolStripMenuItem.DropDownItems.Add(deschideItem);
            fisierToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
            fisierToolStripMenuItem.DropDownItems.Add(iesireItem);

            // Meniu Playlist
            var newPlaylistItem = new ToolStripMenuItem("Playlist nou");
            var removeItem = new ToolStripMenuItem("Elimina melodia selectata");
            var clearItem = new ToolStripMenuItem("Goleste playlist");
            newPlaylistItem.Click += MenuNewPlaylist_Click;
            removeItem.Click += MenuRemoveSong_Click;
            clearItem.Click += MenuClearPlaylist_Click;
            playlistToolStripMenuItem.DropDownItems.Add(newPlaylistItem);
            playlistToolStripMenuItem.DropDownItems.Add(removeItem);
            playlistToolStripMenuItem.DropDownItems.Add(clearItem);

            // Meniu Help
            var aboutItem = new ToolStripMenuItem("Despre aplicatie");
            aboutItem.Click += (s, e) => MessageBox.Show(
                "MP3 Player v1.0\nProiect Ingineria Programarii\n\n" +
                "Dublu click pe melodie = redeaza\n" +
                "Drag & Drop = adauga fisiere\n" +
                "Bara de progres = seek in melodie",
                "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
            helpToolStripMenuItem.DropDownItems.Add(aboutItem);

            // Selectare playlist din listbox
            listBoxPlaylist.SelectedIndexChanged += (s, e) =>
            {
                if (listBoxPlaylist.SelectedItem != null)
                {
                    _controller.Stop();
                    _controller.PlaylistManager.SetActivePlaylist(
                        listBoxPlaylist.SelectedItem.ToString());
                    RefreshSongList();
                }
            };

            this.Text = "MP3 Player";
        }

        private void SetupTimer()
        {
            _progressTimer = new Timer();
            _progressTimer.Interval = 500;
            _progressTimer.Tick += (s, e) => UpdateProgressBar();
        }

        private void SetupDragDrop()
        {
            listViewAddNew.DragEnter += (s, e) =>
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop)
                    ? DragDropEffects.Copy : DragDropEffects.None;

            listViewAddNew.DragDrop += (s, e) =>
            {
                try
                {
                    var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                    foreach (string file in files)
                    {
                        if (Path.GetExtension(file).ToLower() == ".mp3")
                            _controller.PlaylistManager.AddSong(file);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Eroare la adaugarea fisierelor: " + ex.Message,
                                    "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
        }

        // ── Logica butoane ────────────────────────────────────────
        private void OnPlayPauseClicked()
        {
            try
            {
                if (_controller.AudioPlayer.IsPlaying)
                {
                    _controller.Pause();
                    _progressTimer.Stop();
                    buttonPlay.Text = "Play";
                }
                else if (_controller.AudioPlayer.IsPaused)
                {
                    _controller.Play();
                    _progressTimer.Start();
                    buttonPlay.Text = "Pause";
                }
                else
                {
                    // Nicio melodie activa - pornim prima / selectata
                    int index = listViewSongs.SelectedIndices.Count > 0
                        ? listViewSongs.SelectedIndices[0] : 0;
                    PlayAt(index);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la redare: " + ex.Message, "Eroare",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnStopClicked()
        {
            _controller.Stop();
            _progressTimer.Stop();
            trackBarSong.Value = 0;
            buttonPlay.Text = "Play";
            this.Text = "MP3 Player";
            ClearSongHighlight();
        }

        private void PlayAt(int index)
        {
            try
            {
                _controller.PlayAt(index);
                _progressTimer.Start();
                UpdatePlayButton();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare: " + ex.Message, "Eroare",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdatePlayButton()
        {
            buttonPlay.Text = _controller.AudioPlayer.IsPlaying ? "Pause" : "Play";
        }

        // ── Actualizare UI ────────────────────────────────────────
        private void UpdateProgressBar()
        {
            if (!_controller.AudioPlayer.IsPlaying) return;

            double total = _controller.AudioPlayer.TotalDuration;
            double current = _controller.AudioPlayer.CurrentPosition;

            if (total > 0)
            {
                trackBarSong.Maximum = (int)total;
                if ((int)current <= trackBarSong.Maximum)
                    trackBarSong.Value = (int)current;
            }
        }

        // ── Observer callbacks ────────────────────────────────────
        private void OnCurrentSongChanged(object sender, Song song)
        {
            // Evenimentul poate veni din alt thread
            if (InvokeRequired) { Invoke(new Action(() => OnCurrentSongChanged(sender, song))); return; }

            this.Text = song != null ? $"MP3 Player - {song.Title}" : "MP3 Player";
            HighlightCurrentSong(_controller.PlaylistManager.CurrentIndex);
            UpdatePlayButton();

            textBoxMusicName.Text = song?.Title ?? "";
        }

        private void OnPlaylistChanged(object sender, EventArgs e)
        {
            if (InvokeRequired) { Invoke(new Action(() => OnPlaylistChanged(sender, e))); return; }
            RefreshSongList();
        }

        // ── Refresh lista melodii ─────────────────────────────────

        private void AddSongToUI(Song song)
        {
            if (song == null) return;

            int index = listViewSongs.Items.Count + 1;
            var item = new ListViewItem(index.ToString());
            item.SubItems.Add(song.Title);
            item.SubItems.Add(song.Artist ?? "Necunoscut");
            item.SubItems.Add(song.Duration);

            // Setăm culorile explicit pe rând
            item.BackColor = Color.FromArgb(25, 20, 20);
            item.ForeColor = Color.White;
            item.UseItemStyleForSubItems = true;

            // Forțăm fiecare sub-element să aibă aceeași culoare (uneori UseItemStyle e ignorat)
            foreach (ListViewItem.ListViewSubItem sub in item.SubItems)
            {
                sub.BackColor = Color.FromArgb(25, 20, 20);
                sub.ForeColor = Color.White;
            }

            listViewSongs.Items.Add(item);
        }

        private void RefreshSongList()
        {
            listViewSongs.Items.Clear();
            var playlist = _controller.PlaylistManager.ActivePlaylist;
            if (playlist == null) return;

            for (int i = 0; i < playlist.Count; i++)
            {
                Song song = playlist.GetSong(i);
                var item = new ListViewItem((i + 1).ToString());
                item.SubItems.Add(song.Title);
                item.SubItems.Add(song.Artist ?? "Necunoscut");
                item.SubItems.Add(song.Duration);

                // stilizare
                item.BackColor = Color.FromArgb(25, 20, 20);
                item.ForeColor = Color.White;
                item.UseItemStyleForSubItems = true;

                listViewSongs.Items.Add(item);
            }
        }

        private void HighlightCurrentSong(int index)
        {
            ClearSongHighlight();
            if (index >= 0 && index < listViewSongs.Items.Count)
            {
                listViewSongs.Items[index].BackColor = Color.FromArgb(80, 0, 80);
                listViewSongs.Items[index].ForeColor = Color.White;
                listViewSongs.Items[index].EnsureVisible();
            }
        }

        private void ClearSongHighlight()
        {
            foreach (ListViewItem item in listViewSongs.Items)
            {
                item.BackColor = Color.FromArgb(25, 20, 20);
                item.ForeColor = Color.White;
            }
        }

        private void RefreshPlaylistBox()
        {
            // Curățăm lista existentă
            listBoxPlaylist.Items.Clear();

            // Luăm toate playlist-urile din manager și le adăugăm numele în listă
            foreach (var pl in _controller.PlaylistManager.Playlists)
            {
                listBoxPlaylist.Items.Add(pl.Name);
            }

            // Dacă avem cel puțin un playlist, îl selectăm pe primul automat
            if (listBoxPlaylist.Items.Count > 0)
            {
                listBoxPlaylist.SelectedIndex = 0;
            }
        }

        // ── Meniu handlers ────────────────────────────────────────
        private void MenuDeschide_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "Selecteaza fisiere MP3";
                dlg.Filter = "Fisiere MP3 (*.mp3)|*.mp3";
                dlg.Multiselect = true;

                if (dlg.ShowDialog() != DialogResult.OK) return;

                foreach (string file in dlg.FileNames)
                {
                    try
                    {
                        Song song = _controller.PlaylistManager.AddSong(file);
                        AddSongToUI(song);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Nu s-a putut adauga {Path.GetFileName(file)}: {ex.Message}",
                                        "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void MenuNewPlaylist_Click(object sender, EventArgs e)
        {
            string name = Microsoft.VisualBasic.Interaction.InputBox(
                "Nume playlist nou:", "Playlist nou",
                "Playlist " + (_controller.PlaylistManager.Playlists.Count + 1));

            //tratare separat cazul in care avem spatiu in loc de un nume valid
            if (string.IsNullOrWhiteSpace(name)) return;

            try
            {
                _controller.PlaylistManager.CreatePlaylist(name);
                listBoxPlaylist.Items.Add(name);
                listBoxPlaylist.SelectedItem = name;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void MenuRemoveSong_Click(object sender, EventArgs e)
        {
            if (listViewSongs.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Selecteaza o melodie pentru a o elimina.",
                                "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int idx = listViewSongs.SelectedIndices[0];
            if (idx == _controller.PlaylistManager.CurrentIndex)
                OnStopClicked();

            _controller.PlaylistManager.RemoveSong(idx);
        }

        private void MenuClearPlaylist_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esti sigur?", "Confirmare",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                OnStopClicked();
                _controller.PlaylistManager.ClearPlaylist();
            }
        }

        //cand se inchide aplicatia, salvam playlist-urile in JSON si eliberam resursele audio
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                _repository.Save(_controller.PlaylistManager);  // salveaza JSON
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nu s-au putut salva playlist-urile:\n" + ex.Message,
                                "Avertisment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            _controller.Stop();
            (_controller.AudioPlayer as IDisposable)?.Dispose();
            _progressTimer?.Dispose();
            base.OnFormClosing(e);
        }

        /// ── Load library la start ─────────────────────────────────
        private void LoadLibrary()
        {
            try
            {
                string activeName = _repository.Load(_controller.PlaylistManager);
                RefreshPlaylistBox();

                // Selectam playlist-ul care era activ la ultima inchidere
                if (!string.IsNullOrEmpty(activeName))
                {
                    int idx = listBoxPlaylist.FindString(activeName);
                    if (idx >= 0)
                    {
                        listBoxPlaylist.SelectedIndex = idx;
                        _controller.PlaylistManager.SetActivePlaylist(activeName);
                        RefreshSongList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nu s-au putut incarca playlist-urile:\n" + ex.Message,
                                "Avertisment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                RefreshPlaylistBox(); // continuam cu playlist gol
            }
        }

    }
}