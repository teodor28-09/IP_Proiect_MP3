// ============================================================
// Fisier:          Song.cs
// Autor:           Membru 1 - [Nume Prenume]
// Data:            2025
// Functionalitate: Modelul de date pentru o melodie MP3.
//                  Contine proprietatile unui fisier audio.
// ============================================================

namespace MP3Player.Models
{
    /// <summary>
    /// Reprezinta o melodie din playlist.
    /// </summary>
    public class Song
    {
        /// <summary>Titlul melodiei (din tag ID3 sau din numele fisierului).</summary>
        public string Title { get; set; }

        /// <summary>Artistul melodiei (din tag ID3).</summary>
        public string Artist { get; set; }

        /// <summary>Durata melodiei formatata ca mm:ss.</summary>
        public string Duration { get; set; }

        /// <summary>Calea completa catre fisierul MP3 pe disc.</summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Constructorul principal al unui obiect Song.
        /// </summary>
        public Song(string filePath, string title, string artist = "Necunoscut",string duration = "--:--")
        {
            FilePath = filePath;
            Title = title;
            Artist = artist;
            Duration = duration;
        }

        /// <summary>
        /// Returneaza un string reprezentativ pentru afisare.
        /// </summary>
        public override string ToString() => $"{Artist} - {Title}";
    }
}