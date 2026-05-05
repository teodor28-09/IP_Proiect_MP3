// ============================================================
// Fisier:          PlaylistManager.cs
// Autor:           Membru 3 - [Nume Prenume]
// Data:            2025
// Functionalitate: Gestioneaza colectia de playlist-uri,
//                  navigatia intre melodii si cautarea.
//                  Implementeaza sablonul Observer prin evenimente.
// ============================================================

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MP3Player.Models;
using NAudio.Wave;

namespace MP3Player.Core
{
    /// <summary>
    /// Gestioneaza toate playlist-urile si melodia activa.
    /// Foloseste sablonul de proiectare Observer (events) pentru
    /// a notifica UI-ul la schimbarea starii.
    /// </summary>
    public class PlaylistManager
    {
        private readonly List<Playlist> _playlists = new List<Playlist>();
        private Playlist _activePlaylist = null;
        private int _currentIndex = -1;

        // Evenimente (Observer Pattern) 
        /// <summary>Se declanseaza cand melodia activa s-a schimbat.</summary>
        public event EventHandler<Song> CurrentSongChanged;

        /// <summary>Se declanseaza cand continutul playlist-ului s-a modificat.</summary>
        public event EventHandler PlaylistChanged;

        /// <summary>Melodia activa in acest moment.</summary>
        public Song CurrentSong =>
            _activePlaylist?.GetSong(_currentIndex);

        /// <summary>Indexul melodiei active.</summary>
        public int CurrentIndex => _currentIndex;

        /// <summary>Playlist-ul activ.</summary>
        public Playlist ActivePlaylist => _activePlaylist;

        /// <summary>Lista tuturor playlist-urilor.</summary>
        public IReadOnlyList<Playlist> Playlists => _playlists.AsReadOnly();

        /// <summary>
        /// Constructorul initializeaza managerul de playlist-uri cu un playlist implicit gol.
        /// </summary>
        public PlaylistManager()
        {
            var defaultPlaylist = new Playlist("Favorite");
            _playlists.Add(defaultPlaylist);
            _activePlaylist = defaultPlaylist;
        }

        /// <summary>Creeaza un playlist nou cu numele dat.</summary>
        /// <exception cref="ArgumentException">Numele este gol sau deja exista.</exception>
        public Playlist CreatePlaylist(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Numele playlist-ului nu poate fi gol.", nameof(name));

            if (_playlists.Any(p => p.Name == name))
                throw new ArgumentException($"Exista deja un playlist cu numele '{name}'.");

            var playlist = new Playlist(name);
            _playlists.Add(playlist);
            return playlist;
        }

        /// <summary>Seteaza playlist-ul activ dupa nume.</summary>
        public bool SetActivePlaylist(string name)
        {
            var playlist = _playlists.FirstOrDefault(p => p.Name == name);
            if (playlist == null) return false;

            _activePlaylist = playlist;
            _currentIndex = -1;
            PlaylistChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }


        /// <summary>
        /// Adauga un fisier MP3 in playlist-ul activ.
        /// Citeste automat tag-urile ID3 si durata.
        /// </summary>
        public Song AddSong(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Fisierul MP3 nu a fost gasit.", filePath);

            if (Path.GetExtension(filePath).ToLower() != ".mp3")
                throw new ArgumentException("Fisierul nu este in format MP3.");

            string title = Path.GetFileNameWithoutExtension(filePath);
            string artist = "Necunoscut";
            string duration = "--:--";

            try
            {
                //citire durata
                using (var reader = new AudioFileReader(filePath))
                {
                    duration = reader.TotalTime.ToString(@"mm\:ss");
                }

                //utilizam ID3 folosind TagLib#
                using (var tagFile = TagLib.File.Create(filePath))
                {
                    if (!string.IsNullOrWhiteSpace(tagFile.Tag.Title))
                        title = tagFile.Tag.Title;

                    // FirstPerformer extrage primul artist găsit în listă
                    if (!string.IsNullOrWhiteSpace(tagFile.Tag.FirstPerformer))
                        artist = tagFile.Tag.FirstPerformer;
                }
            }
            catch
            {
                // Dacă citirea tag-urilor eșuează (fișier corupt/blocat), 
                // funcția continuă cu valorile default setate mai sus.
            }

            var song = new Song(filePath, title, artist, duration);

            _activePlaylist.AddSong(song);

            // Notificăm UI-ul că playlist-ul s-a schimbat
            PlaylistChanged?.Invoke(this, EventArgs.Empty);

            return song;
        }

        /// <summary>Elimina melodia la indexul dat din playlist-ul activ.</summary>
        public bool RemoveSong(int index)
        {
            bool removed = _activePlaylist?.RemoveSong(index) ?? false;
            if (removed)
            {
                if (_currentIndex >= _activePlaylist.Count)
                    _currentIndex = _activePlaylist.Count - 1;
                PlaylistChanged?.Invoke(this, EventArgs.Empty);
            }
            return removed;
        }

        /// <summary>Goleste playlist-ul activ.</summary>
        public void ClearPlaylist()
        {
            _activePlaylist?.Clear();
            _currentIndex = -1;
            PlaylistChanged?.Invoke(this, EventArgs.Empty);
        }

        // ── Navigatie ────────────────────────────────────────────

        /// <summary>Seteaza melodia activa dupa index si notifica observatorii.</summary>
        public Song SelectSong(int index)
        {
            if (_activePlaylist == null || index < 0 || index >= _activePlaylist.Count)
                return null;

            _currentIndex = index;
            var song = _activePlaylist.GetSong(_currentIndex);
            CurrentSongChanged?.Invoke(this, song);
            return song;
        }

        /// <summary>Trece la melodia urmatoare (ciclic).</summary>
        public Song Next()
        {
            if (_activePlaylist == null || _activePlaylist.Count == 0) return null;
            int next = (_currentIndex + 1) % _activePlaylist.Count;
            return SelectSong(next);
        }

        /// <summary>Trece la melodia anterioara (ciclic).</summary>
        public Song Previous()
        {
            if (_activePlaylist == null || _activePlaylist.Count == 0) return null;
            int prev = _currentIndex <= 0
                ? _activePlaylist.Count - 1
                : _currentIndex - 1;
            return SelectSong(prev);
        }
    }
}