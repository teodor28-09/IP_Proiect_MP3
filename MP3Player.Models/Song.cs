/**************************************************************************
 *                                                                        *
 *  File:        Song.cs                                                  *
 *  Copyright:   (c) 2025-2026 Aparaschivei Teodor,                       *
 *                  Munteanu Alin Constantin,                             *
 *                  Marguta Dan Alexandru                                 *
 *  E-mail:      teodor.aparaschivei@student.tuiasi.ro                    *
 *               alin-constantin.munteanu@student.tuiasi.ro               *
 *               dan-alexandru.marguta@student.tuiasi.ro                  *                        
 *  Description: contine proprietatile unui fisier audio                  *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/

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