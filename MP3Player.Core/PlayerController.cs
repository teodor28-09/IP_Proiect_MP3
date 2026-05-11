/**************************************************************************
 *                                                                        *
 *  File:        PlaylistController.cs                                    *
 *  Copyright:   (c) 2025-2026 Aparaschivei Teodor,                       *
 *                  Munteanu Alin Constantin,                             *
 *                  Marguta Dan Alexandru                                 *
 *  E-mail:      teodor.aparaschivei@student.tuiasi.ro                    *
 *               alin-constantin.munteanu@student.tuiasi.ro               *
 *               dan-alexandru.marguta@student.tuiasi.ro                  *                        
 *  Description: Implementeaza sablonul de proiectare Command             *
 *                  pentru operatiile playerului                          *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/


using System;
using MP3Player.Models;

namespace MP3Player.Core
{
    // ══════════════════════════════════════════════════════════
    //  COMMAND PATTERN  –  Interfata de baza
    // ══════════════════════════════════════════════════════════

    /// <summary>
    /// Interfata Command - fiecare operatie a playerului
    /// este o comanda independenta si reversibila.
    /// </summary>
    public interface ICommand
    {
        void Execute();
    }

    // ══════════════════════════════════════════════════════════
    //  Comenzi concrete
    // ══════════════════════════════════════════════════════════

    /// <summary>Comanda Play - porneste sau reia redarea.</summary>
    public class PlayCommand : ICommand
    {
        private readonly IAudioPlayer _player;
        private readonly PlaylistManager _manager;

        public PlayCommand(IAudioPlayer player, PlaylistManager manager)
        {
            _player = player;
            _manager = manager;
        }

        public void Execute()
        {
            if (_player.IsPaused)
            {
                _player.Resume();
            }
            else
            {
                Song song = _manager.CurrentSong;
                if (song != null)
                    _player.Play(song.FilePath);
            }
        }
    }

    /// <summary>Comanda Pause - pune redarea pe pauza.</summary>
    public class PauseCommand : ICommand
    {
        private readonly IAudioPlayer _player;
        public PauseCommand(IAudioPlayer player) => _player = player;
        public void Execute() => _player.Pause();
    }

    /// <summary>Comanda Stop - opreste complet redarea.</summary>
    public class StopCommand : ICommand
    {
        private readonly IAudioPlayer _player;
        public StopCommand(IAudioPlayer player) => _player = player;
        public void Execute() => _player.Stop();
    }

    /// <summary>Comanda Next - trece la melodia urmatoare.</summary>
    public class NextCommand : ICommand
    {
        private readonly IAudioPlayer _player;
        private readonly PlaylistManager _manager;

        public NextCommand(IAudioPlayer player, PlaylistManager manager)
        {
            _player = player;
            _manager = manager;
        }

        public void Execute()
        {
            Song next = _manager.Next();
            if (next != null)
                _player.Play(next.FilePath);
        }
    }

    /// <summary>Comanda Previous - trece la melodia anterioara.</summary>
    public class PreviousCommand : ICommand
    {
        private readonly IAudioPlayer _player;
        private readonly PlaylistManager _manager;

        public PreviousCommand(IAudioPlayer player, PlaylistManager manager)
        {
            _player = player;
            _manager = manager;
        }

        public void Execute()
        {
            Song prev = _manager.Previous();
            if (prev != null)
                _player.Play(prev.FilePath);
        }
    }

    // ══════════════════════════════════════════════════════════
    //  Invoker - leaga butoanele de comenzi
    // ══════════════════════════════════════════════════════════

    /// <summary>
    /// Invoker-ul din Command Pattern.
    /// Form1 apeleaza doar metodele acestei clase,
    /// fara sa stie nimic despre AudioPlayer sau PlaylistManager.
    /// </summary>
    public class PlayerController
    {
        private readonly ICommand _playCmd;
        private readonly ICommand _pauseCmd;
        private readonly ICommand _stopCmd;
        private readonly ICommand _nextCmd;
        private readonly ICommand _prevCmd;

        private readonly IAudioPlayer _player;
        private readonly PlaylistManager _manager;

        /// <summary>
        /// Expune PlayerManager pentru a permite UI-ului sa
        /// acceseze datele (lista melodii, cautare etc.)
        /// </summary>
        public PlaylistManager PlaylistManager => _manager;

        /// <summary>Expune playerul pentru a citi starea (pozitie, durata).</summary>
        public IAudioPlayer AudioPlayer => _player;

        public PlayerController(IAudioPlayer player, PlaylistManager manager)
        {
            _player = player ?? throw new ArgumentNullException(nameof(player));
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));

            _playCmd = new PlayCommand(player, manager);
            _pauseCmd = new PauseCommand(player);
            _stopCmd = new StopCommand(player);
            _nextCmd = new NextCommand(player, manager);
            _prevCmd = new PreviousCommand(player, manager);

            // Cand o melodie se termina, trecem automat la urmatoarea
            _player.SongFinished += (s, e) => _nextCmd.Execute();
        }

        public void Play() => _playCmd.Execute();
        public void Pause() => _pauseCmd.Execute();
        public void Stop() => _stopCmd.Execute();
        public void Next() => _nextCmd.Execute();
        public void Previous() => _prevCmd.Execute();

        /// <summary>Selecteaza si porneste melodia la indexul dat.</summary>
        public void PlayAt(int index)
        {
            Song song = _manager.SelectSong(index);
            if (song != null)
                _player.Play(song.FilePath);
        }

        /// <summary>Seteaza volumul (0-100).</summary>
        public void SetVolume(int value) =>
            _player.SetVolume(value / 100f);

        /// <summary>Seek la pozitia data in secunde.</summary>
        public void Seek(double seconds) =>
            _player.Seek(seconds);
    }
}