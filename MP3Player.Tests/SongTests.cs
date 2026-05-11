/**************************************************************************
 *                                                                        *
 *  File:        SongTests.cs                                             *
 *  Copyright:   (c) 2025-2026 Aparaschivei Teodor,                       *
 *                  Munteanu Alin Constantin,                             *
 *                  Marguta Dan Alexandru                                 *
 *  E-mail:      teodor.aparaschivei@student.tuiasi.ro                    *
 *               alin-constantin.munteanu@student.tuiasi.ro               *
 *               dan-alexandru.marguta@student.tuiasi.ro                  *                        
 *  Description: Teste unitare pentru clasele Song si Playlist            *
 *                  din MP3Player.Models                                  *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/



using Microsoft.VisualStudio.TestTools.UnitTesting;
using MP3Player.Models;

namespace MP3Player.Tests
{
    [TestClass]
    public class SongTests
    {

        [TestMethod]
        public void Song_Constructor_SeteazaProprietatileCorect()
        {
            // Arrange + Act
            var song = new Song("C:\\muzica\\test.mp3", "Bohemian Rhapsody",
                                "Queen", "5:55");

            // Assert
            Assert.AreEqual("C:\\muzica\\test.mp3", song.FilePath);
            Assert.AreEqual("Bohemian Rhapsody", song.Title);
            Assert.AreEqual("Queen", song.Artist);
            Assert.AreEqual("5:55", song.Duration);
        }

        [TestMethod]
        public void Song_Constructor_ValoriImpliciteCorecte()
        {
            // Arrange + Act - fara artist/album/durata
            var song = new Song("C:\\test.mp3", "Test Song");

            // Assert
            Assert.AreEqual("Necunoscut", song.Artist);
            
            Assert.AreEqual("--:--", song.Duration);
        }

        [TestMethod]
        public void Song_ToString_ReturneazaFormatulCorect()
        {
            // Arrange
            var song = new Song("C:\\test.mp3", "Hotel California", "Eagles");

            // Act
            string result = song.ToString();

            // Assert
            Assert.AreEqual("Eagles - Hotel California", result);
        }
    }

    // ── Teste Playlist ───────────────────────────────────────────

    [TestClass]
    public class PlaylistTests
    {
        private Song CreateSong(string title) =>
            new Song($"C:\\{title}.mp3", title, "Artist Test");

        [TestMethod]
        public void Playlist_Constructor_EsteGolInitial()
        {
            // Arrange + Act
            var playlist = new Playlist("Favorite");

            // Assert
            Assert.AreEqual("Favorite", playlist.Name);
            Assert.AreEqual(0, playlist.Count);
        }

        [TestMethod]
        public void Playlist_AddSong_CresteNumarulDeMelodii()
        {
            // Arrange
            var playlist = new Playlist("Test");
            var song = CreateSong("Melodie1");

            // Act
            playlist.AddSong(song);

            // Assert
            Assert.AreEqual(1, playlist.Count);
        }

        [TestMethod]
        public void Playlist_AddSong_NullNuAdaugaNimic()
        {
            // Arrange
            var playlist = new Playlist("Test");

            // Act
            playlist.AddSong(null);

            // Assert
            Assert.AreEqual(0, playlist.Count);
        }

        [TestMethod]
        public void Playlist_RemoveSong_EliminaCorrect()
        {
            // Arrange
            var playlist = new Playlist("Test");
            playlist.AddSong(CreateSong("Melodie1"));
            playlist.AddSong(CreateSong("Melodie2"));

            // Act
            bool result = playlist.RemoveSong(0);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, playlist.Count);
        }

        [TestMethod]
        public void Playlist_RemoveSong_IndexInvalidReturneazaFalse()
        {
            // Arrange
            var playlist = new Playlist("Test");

            // Act
            bool result = playlist.RemoveSong(99);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Playlist_GetSong_ReturneazaMelodiaCorecta()
        {
            // Arrange
            var playlist = new Playlist("Test");
            var song = CreateSong("Melodie1");
            playlist.AddSong(song);

            // Act
            Song result = playlist.GetSong(0);

            // Assert
            Assert.AreEqual("Melodie1", result.Title);
        }

        [TestMethod]
        public void Playlist_GetSong_IndexInvalidReturneazaNull()
        {
            // Arrange
            var playlist = new Playlist("Test");

            // Act
            Song result = playlist.GetSong(99);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Playlist_Clear_GolesteToateMelodiile()
        {
            // Arrange
            var playlist = new Playlist("Test");
            playlist.AddSong(CreateSong("Melodie1"));
            playlist.AddSong(CreateSong("Melodie2"));
            playlist.AddSong(CreateSong("Melodie3"));

            // Act
            playlist.Clear();

            // Assert
            Assert.AreEqual(0, playlist.Count);
        }

        [TestMethod]
        public void Playlist_ToString_ContineNumeleSiCount()
        {
            // Arrange
            var playlist = new Playlist("Favorite");
            playlist.AddSong(CreateSong("Melodie1"));

            // Act
            string result = playlist.ToString();

            // Assert
            StringAssert.Contains(result, "Favorite");
            StringAssert.Contains(result, "1");
        }
    }
}