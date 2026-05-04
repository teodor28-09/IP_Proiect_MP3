// ============================================================
// Fisier:          AudioPlayer.cs
// Autor:           Membru 2 - [Nume Prenume]
// Data:            2025
// Functionalitate: Implementarea concreta a playerului audio
//                  folosind libraria NAudio pentru redarea MP3.
// ============================================================

using System;
using NAudio.Wave;

namespace MP3Player.Core
{
    /// <summary>
    /// Implementare concreta a IAudioPlayer folosind NAudio.
    /// </summary>
    public class AudioPlayer : IAudioPlayer, IDisposable
    {
        // ── Campuri private ──────────────────────────────────────
        private AudioFileReader _audioReader;
        private WaveOutEvent _waveOut;
        private float _volume = 0.7f;
        private bool _disposed = false;

        // ── Evenimente ──────────────────────────────────────────
        /// <summary>Se declanseaza cand melodia activa s-a terminat natural.</summary>
        public event EventHandler SongFinished;

        // ── Proprietati ──────────────────────────────────────────
        public double CurrentPosition =>
            _audioReader?.CurrentTime.TotalSeconds ?? 0;

        public double TotalDuration =>
            _audioReader?.TotalTime.TotalSeconds ?? 0;

        public bool IsPlaying =>
            _waveOut?.PlaybackState == PlaybackState.Playing;

        public bool IsPaused =>
            _waveOut?.PlaybackState == PlaybackState.Paused;

        // ── Metode publice ───────────────────────────────────────

        /// <summary>
        /// Incarca fisierul de la filePath si incepe redarea.
        /// </summary>
        /// <exception cref="ArgumentNullException">filePath este null sau gol.</exception>
        /// <exception cref="System.IO.FileNotFoundException">Fisierul nu exista.</exception>
        public void Play(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath), "Calea fisierului nu poate fi nula.");

            if (!System.IO.File.Exists(filePath))
                throw new System.IO.FileNotFoundException("Fisierul MP3 nu a fost gasit.", filePath);

            DisposeWaveOut();

            _audioReader = new AudioFileReader(filePath) { Volume = _volume };
            _waveOut = new WaveOutEvent();
            _waveOut.Init(_audioReader);
            _waveOut.PlaybackStopped += OnPlaybackStopped;
            _waveOut.Play();
        }

        /// <summary>Pune redarea pe pauza.</summary>
        public void Pause()
        {
            if (IsPlaying)
                _waveOut.Pause();
        }

        /// <summary>Continua redarea dupa pauza.</summary>
        public void Resume()
        {
            if (IsPaused)
                _waveOut.Play();
        }

        /// <summary>Opreste redarea si elibereaza resursele audio.</summary>
        public void Stop()
        {
            DisposeWaveOut();
        }

        /// <summary>
        /// Seteaza volumul.
        /// </summary>
        /// <param name="volume">Valoare intre 0.0 (mut) si 1.0 (maxim).</param>
        public void SetVolume(float volume)
        {
            _volume = Math.Max(0f, Math.Min(1f, volume));
            if (_audioReader != null)
                _audioReader.Volume = _volume;
        }

        /// <summary>
        /// Sare la o pozitie in melodie.
        /// </summary>
        /// <param name="seconds">Pozitia in secunde.</param>
        public void Seek(double seconds)
        {
            if (_audioReader == null) return;
            double clamped = Math.Max(0, Math.Min(seconds, TotalDuration));
            _audioReader.CurrentTime = TimeSpan.FromSeconds(clamped);
        }

        // ── Metode private ───────────────────────────────────────

        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (e.Exception != null)
                throw e.Exception;

            // Notificam UI-ul ca melodia s-a terminat
            SongFinished?.Invoke(this, EventArgs.Empty);
        }

        private void DisposeWaveOut()
        {
            _waveOut?.Stop();
            _waveOut?.Dispose();
            _waveOut = null;

            _audioReader?.Dispose();
            _audioReader = null;
        }

        // ── IDisposable ──────────────────────────────────────────
        public void Dispose()
        {
            if (!_disposed)
            {
                DisposeWaveOut();
                _disposed = true;
            }
        }
    }
}