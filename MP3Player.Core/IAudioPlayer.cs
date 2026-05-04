// ============================================================
// Fisier:          IAudioPlayer.cs
// Autor:           Membru 2 - [Nume Prenume]
// Data:            2025
// Functionalitate: Interfata pentru redarea audio.
//                  Permite decuplarea implementarii de UI.
// ============================================================

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