// ============================================================
// Fisier:          Playlist.cs
// Autor:           Membru 1 - [Nume Prenume]
// Data:            2025
// Functionalitate: Modelul de date pentru un playlist.
//                  Contine o colectie de melodii si metadate.
// ============================================================

using System.Collections.Generic;

namespace MP3Player.Models
{
    /// <summary>
    /// Reprezinta un playlist care contine mai multe melodii.
    /// </summary>
    public class Playlist
    {
        /// <summary>Numele playlist-ului.</summary>
        public string Name { get; set; }

        /// <summary>Lista de melodii din playlist.</summary>
        public List<Song> Songs { get; private set; }

        /// <summary>Numarul de melodii din playlist.</summary>
        public int Count => Songs.Count;

        /// <summary>
        /// Constructorul unui playlist cu un nume dat.
        /// </summary>
        public Playlist(string name)
        {
            Name = name;
            Songs = new List<Song>();
        }

        /// <summary>Adauga o melodie in playlist.</summary>
        public void AddSong(Song song)
        {
            if (song != null)
                Songs.Add(song);
        }

        /// <summary>Elimina o melodie dupa index.</summary>
        public bool RemoveSong(int index)
        {
            if (index < 0 || index >= Songs.Count)
                return false;
            Songs.RemoveAt(index);
            return true;
        }

        /// <summary>Goleste toate melodiile din playlist.</summary>
        public void Clear() => Songs.Clear();

        /// <summary>Returneaza melodia de la indexul dat, sau null.</summary>
        public Song GetSong(int index)
        {
            if (index < 0 || index >= Songs.Count)
                return null;
            return Songs[index];
        }

        public override string ToString() => $"{Name} ({Count} melodii)";
    }
}