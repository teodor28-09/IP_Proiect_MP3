/**************************************************************************
 *                                                                        *
 *  File:        PlaylistManagerTests.cs                                  *
 *  Copyright:   (c) 2025-2026 Aparaschivei Teodor,                       *
 *                  Munteanu Alin Constantin,                             *
 *                  Marguta Dan Alexandru                                 *
 *  E-mail:      teodor.aparaschivei@student.tuiasi.ro                    *
 *               alin-constantin.munteanu@student.tuiasi.ro               *
 *               dan-alexandru.marguta@student.tuiasi.ro                  *                        
 *  Description: Teste unitare pentru PlaylistManager                     *
 *                  din MP3Player.Core                                    *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/


using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MP3Player.Core;
using MP3Player.Models;

namespace MP3Player.Tests
{
    [TestClass]
    public class PlaylistManagerTests
    {
        private PlaylistManager _manager;

        [TestInitialize]
        public void Setup()
        {
            _manager = new PlaylistManager();
        }

        [TestMethod]
        public void CreatePlaylist_CreazaPlaylistNou()
        {
            // Act
            Playlist result = _manager.CreatePlaylist("Workout");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Workout", result.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void CreatePlaylist_NumeDuplicat_AruncaExceptie()
        {
            // Arrange
            _manager.CreatePlaylist("Workout");

            // Act 
            _manager.CreatePlaylist("Workout");
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void CreatePlaylist_NumeGol_AruncaExceptie()
        {
            // Act
            _manager.CreatePlaylist("");
        }

        [TestMethod]
        public void SetActivePlaylist_SchimbaPlaylistulActiv()
        {
            // Arrange
            _manager.CreatePlaylist("Workout");

            // Act
            bool result = _manager.SetActivePlaylist("Workout");

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("Workout", _manager.ActivePlaylist.Name);
        }

        [TestMethod]
        public void SetActivePlaylist_NumeInexistent_ReturneazaFalse()
        {
            // Act
            bool result = _manager.SetActivePlaylist("NumeCarenuExista");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Next_TreceCorrectLaUrmatoarea()
        {
            // Arrange 
            _manager.ActivePlaylist.AddSong(new Song("C:\\a.mp3", "Melodie A"));
            _manager.ActivePlaylist.AddSong(new Song("C:\\a.mp3", "Melodie B"));
            _manager.SelectSong(0);

            // Act
            Song result = _manager.Next();

            // Assert
            Assert.AreEqual("Melodie B", result.Title);
        }

        [TestMethod]
        public void Next_DeLaUltima_MergeInapoisLaPrima()
        {
            // Arrange
            _manager.ActivePlaylist.AddSong(new Song("C:\\a.mp3", "Melodie A"));
            _manager.ActivePlaylist.AddSong(new Song("C:\\b.mp3", "Melodie B"));
            _manager.SelectSong(1); 

            // Act
            Song result = _manager.Next();

            // Assert 
            Assert.AreEqual("Melodie A", result.Title);
        }

        [TestMethod]
        public void Previous_TreceCorrectLaAnterioara()
        {
            // Arrange
            _manager.ActivePlaylist.AddSong(new Song("C:\\a.mp3", "Melodie A"));
            _manager.ActivePlaylist.AddSong(new Song("C:\\b.mp3", "Melodie B"));
            _manager.SelectSong(1); 

            // Act
            Song result = _manager.Previous();

            // Assert
            Assert.AreEqual("Melodie A", result.Title);
        }

        [TestMethod]
        public void Previous_DelaPrima_MergelaUltima()
        {
            // Arrange
            _manager.ActivePlaylist.AddSong(new Song("C:\\a.mp3", "Melodie A"));
            _manager.ActivePlaylist.AddSong(new Song("C:\\b.mp3", "Melodie B"));
            _manager.SelectSong(0); 

            // Act
            Song result = _manager.Previous();

            // Assert 
            Assert.AreEqual("Melodie B", result.Title);
        }

        [TestMethod]
        public void Next_PlaylistGol_ReturneazaNull()
        {
            // Act
            Song result = _manager.Next();

            // Assert
            Assert.IsNull(result);
        }


        [TestMethod]
        public void RemoveSong_EliminaCorrect()
        {
            // Arrange
            _manager.ActivePlaylist.AddSong(new Song("C:\\a.mp3", "Melodie A"));
            _manager.ActivePlaylist.AddSong(new Song("C:\\b.mp3", "Melodie B"));

            // Act
            bool result = _manager.RemoveSong(0);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _manager.ActivePlaylist.Count);
        }

        [TestMethod]
        public void SelectSong_DeclanseazaEvenimentulCurrentSongChanged()
        {
            // Arrange
            _manager.ActivePlaylist.AddSong(new Song("C:\\a.mp3", "Melodie A"));
            Song songDinEveniment = null;
            _manager.CurrentSongChanged += (s, song) => songDinEveniment = song;

            // Act
            _manager.SelectSong(0);

            // Assert - evenimentul a fost declansat cu melodia corecta
            Assert.IsNotNull(songDinEveniment);
            Assert.AreEqual("Melodie A", songDinEveniment.Title);
        }

        [TestMethod]
        public void ClearPlaylist_DeclanseazaEvenimentulPlaylistChanged()
        {
            // Arrange
            bool evenimentDeclansat = false;
            _manager.PlaylistChanged += (s, e) => evenimentDeclansat = true;

            // Act
            _manager.ClearPlaylist();

            // Assert
            Assert.IsTrue(evenimentDeclansat);
        }
    }
}