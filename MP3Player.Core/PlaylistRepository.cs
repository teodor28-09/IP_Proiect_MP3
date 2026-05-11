/**************************************************************************
 *                                                                        *
 *  File:        PlaylistRepository.cs                                    *
 *  Copyright:   (c) 2025-2026 Aparaschivei Teodor,                       *
 *                  Munteanu Alin Constantin,                             *
 *                  Marguta Dan Alexandru                                 *
 *  E-mail:      teodor.aparaschivei@student.tuiasi.ro                    *
 *               alin-constantin.munteanu@student.tuiasi.ro               *
 *               dan-alexandru.marguta@student.tuiasi.ro                  *                        
 *  Description: Salvarea si incarcarea playlist-urilor                   *
 *                  intr-un fisier JSON pe disc.                          *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using MP3Player.Models;

namespace MP3Player.Core
{
    // DTO-uri pentru serializare JSON

    [DataContract]
    public class SongDto
    {
        [DataMember] public string FilePath { get; set; }
        [DataMember] public string Title { get; set; }
        [DataMember] public string Artist { get; set; }
        [DataMember] public string Duration { get; set; }
    }

    [DataContract]
    public class PlaylistDto
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public List<SongDto> Songs { get; set; }
    }

    [DataContract]
    public class LibraryDto
    {
        [DataMember] public List<PlaylistDto> Playlists { get; set; }
        [DataMember] public string ActivePlaylist { get; set; }
    }

    /// <summary>
    /// Salveaza si incarca toate playlist-urile intr-un fisier JSON.
    /// Fisierul e stocat in bin/debug
    /// </summary>
    public class PlaylistRepository
    {
        private readonly string _filePath;

        /// <summary>
        /// Calea completa catre fisierul JSON 
        /// </summary>
        public string FilePath => _filePath;

        /// <summary>
        /// Constructorul initializeaza calea catre fisierul JSON unde se vor salva playlist-urile.
        /// </summary>
        public PlaylistRepository()
        {
            string folder = AppDomain.CurrentDomain.BaseDirectory;
            _filePath = Path.Combine(folder, "library.json");
        }

        /// <summary>
        /// Serializeaza toate playlist-urile din PlaylistManager si le salveaza pe disc ca JSON.
        /// </summary>
        public void Save(PlaylistManager manager)
        {
            if (manager == null)
                throw new ArgumentNullException(nameof(manager));

            try
            {
                var library = new LibraryDto
                {
                    ActivePlaylist = manager.ActivePlaylist?.Name ?? "",
                    Playlists = new List<PlaylistDto>()
                };

                foreach (var playlist in manager.Playlists)
                {
                    var dto = new PlaylistDto
                    {
                        Name = playlist.Name,
                        Songs = new List<SongDto>()
                    };

                    for (int i = 0; i < playlist.Count; i++)
                    {
                        Song s = playlist.GetSong(i);
                        dto.Songs.Add(new SongDto
                        {
                            FilePath = s.FilePath,
                            Title = s.Title,
                            Artist = s.Artist,
                            Duration = s.Duration
                        });
                    }

                    library.Playlists.Add(dto);
                }

                // Serializam cu DataContractJsonSerializer
                var serializer = new DataContractJsonSerializer(typeof(LibraryDto));
                using (var stream = new MemoryStream())
                {
                    serializer.WriteObject(stream, library);
                    string json = Encoding.UTF8.GetString(stream.ToArray());

                    // Scriem cu indentare pentru lizibilitate
                    File.WriteAllText(_filePath, json, Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
                throw new IOException("Eroare la salvarea playlist-urilor: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Incarca playlist-urile din JSON si le aplica in PlaylistManager.
        /// Melodiile cu fisiere lipsa de pe disc sunt sarite automat.
        /// </summary>
        /// <returns>Numele playlist-ului care era activ la ultima salvare.</returns>
        public string Load(PlaylistManager manager)
        {
            if (manager == null)
                throw new ArgumentNullException(nameof(manager));

            if (!File.Exists(_filePath))
                return null; // Prima rulare - nu exista fisier inca

            try
            {
                string json = File.ReadAllText(_filePath, Encoding.UTF8);

                var serializer = new DataContractJsonSerializer(typeof(LibraryDto));
                LibraryDto library;

                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
                {
                    library = (LibraryDto)serializer.ReadObject(stream);
                }

                if (library?.Playlists == null) return null;

                foreach (var playlistDto in library.Playlists)
                {
                    Playlist playlist = null;
                    try
                    {
                        playlist = manager.CreatePlaylist(playlistDto.Name);
                    }
                    catch
                    {
                        // Playlist-ul exista deja (ex: "Favorite") - il luam pe cel existent
                        foreach (var p in manager.Playlists)
                            if (p.Name == playlistDto.Name) { playlist = p; break; }
                    }

                    if (playlist == null || playlistDto.Songs == null) continue;

                    foreach (var songDto in playlistDto.Songs)
                    {
                        // Sarim melodiile ale caror fisiere nu mai exista pe disc
                        if (!File.Exists(songDto.FilePath)) continue;

                        var song = new Song(
                            songDto.FilePath,
                            songDto.Title ?? Path.GetFileNameWithoutExtension(songDto.FilePath),
                            songDto.Artist ?? "Necunoscut",
                            songDto.Duration ?? "--:--");

                        playlist.AddSong(song);
                    }
                }

                return library.ActivePlaylist;
            }
            catch (Exception ex)
            {
                throw new IOException("Eroare la incarcarea playlist-urilor: " + ex.Message, ex);
            }
        }
    }
}