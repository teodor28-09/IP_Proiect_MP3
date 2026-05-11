/**************************************************************************
 *                                                                        *
 *  File:        IAudioPlayer.cs                                          *
 *  Copyright:   (c) 2025-2026 Aparaschivei Teodor,                       *
 *                  Munteanu Alin Constantin,                             *
 *                  Marguta Dan Alexandru                                 *
 *  E-mail:      teodor.aparaschivei@student.tuiasi.ro                    *
 *               alin-constantin.munteanu@student.tuiasi.ro               *
 *               dan-alexandru.marguta@student.tuiasi.ro                  *                        
 *  Description: interfata pentru redare audio                            *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/

using System;

namespace MP3Player.Core
{
    /// <summary>
    /// Interfata care defineste operatiile unui player audio.
    /// </summary>
    public interface IAudioPlayer
    {
        /// <summary>Se declanseaza cand melodia s-a terminat.</summary>
        event EventHandler SongFinished;

        /// <summary>Pozitia curenta in melodie (secunde).</summary>
        double CurrentPosition { get; }

        /// <summary>Durata totala a melodiei curente (secunde).</summary>
        double TotalDuration { get; }

        /// <summary>True daca playerul reda in acest moment.</summary>
        bool IsPlaying { get; }

        /// <summary>True daca playerul este pe pauza.</summary>
        bool IsPaused { get; }

        /// <summary>Incarca si reda un fisier audio de la calea data.</summary>
        void Play(string filePath);

        /// <summary>Pune pe pauza redarea curenta.</summary>
        void Pause();

        /// <summary>Continua redarea dupa pauza.</summary>
        void Resume();

        /// <summary>Opreste complet redarea.</summary>
        void Stop();

        /// <summary>Seteaza volumul (0.0 - 1.0).</summary>
        void SetVolume(float volume);

        /// <summary>Sare la pozitia data in secunde.</summary>
        void Seek(double seconds);
    }
}