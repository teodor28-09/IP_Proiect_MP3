// ============================================================
// Fisier:          AudioPlayer.cs
// Autor:           Membru 2 - [Nume Prenume]
// Data:            2025
// Functionalitate: Implementarea concreta a playerului audio
//                  folosind libraria NAudio pentru redarea MP3.
//                  Thread-safe: dispose se face pe thread-ul corect.
// ============================================================

using System;
using System.Threading;
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
        private bool _isStopping = false; // previne double-dispose

        // ── Sincronizare thread ──────────────────────────────────
        private readonly object _lock = new object();

        // ── Evenimente ──────────────────────────────────────────
        /// <summary>Se declanseaza cand melodia activa s-a terminat natural.</summary>
        public event EventHandler SongFinished;

        // ── Proprietati ──────────────────────────────────────────
        public double CurrentPosition
        {
            get { lock (_lock) { return _audioReader?.CurrentTime.TotalSeconds ?? 0; } }
        }

        public double TotalDuration
        {
            get { lock (_lock) { return _audioReader?.TotalTime.TotalSeconds ?? 0; } }
        }

        public bool IsPlaying
        {
            get { lock (_lock) { return _waveOut?.PlaybackState == PlaybackState.Playing; } }
        }

        public bool IsPaused
        {
            get { lock (_lock) { return _waveOut?.PlaybackState == PlaybackState.Paused; } }
        }

        // ── Metode publice ───────────────────────────────────────

        /// <summary>
        /// Incarca fisierul de la filePath si incepe redarea.
        /// </summary>
        public void Play(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath));

            if (!System.IO.File.Exists(filePath))
                throw new System.IO.FileNotFoundException("Fisierul MP3 nu a fost gasit.", filePath);

            // Oprim ce era in redare INAINTE de a crea resurse noi
            StopAndDispose();

            lock (_lock)
            {
                _isStopping = false;
                _audioReader = new AudioFileReader(filePath) { Volume = _volume };
                _waveOut = new WaveOutEvent();
                _waveOut.Init(_audioReader);
                _waveOut.PlaybackStopped += OnPlaybackStopped;
                _waveOut.Play();
            }
        }

        /// <summary>Pune redarea pe pauza.</summary>
        public void Pause()
        {
            lock (_lock)
            {
                if (_waveOut?.PlaybackState == PlaybackState.Playing)
                    _waveOut.Pause();
            }
        }

        /// <summary>Continua redarea dupa pauza.</summary>
        public void Resume()
        {
            lock (_lock)
            {
                if (_waveOut?.PlaybackState == PlaybackState.Paused)
                    _waveOut.Play();
            }
        }

        /// <summary>Opreste redarea si elibereaza resursele audio.</summary>
        public void Stop()
        {
            StopAndDispose();
        }

        /// <summary>Seteaza volumul intre 0.0 si 1.0.</summary>
        public void SetVolume(float volume)
        {
            _volume = Math.Max(0f, Math.Min(1f, volume));
            lock (_lock)
            {
                if (_audioReader != null)
                    _audioReader.Volume = _volume;
            }
        }

        /// <summary>Sare la pozitia data in secunde.</summary>
        public void Seek(double seconds)
        {
            lock (_lock)
            {
                if (_audioReader == null) return;
                double clamped = Math.Max(0, Math.Min(seconds, TotalDuration));
                _audioReader.CurrentTime = TimeSpan.FromSeconds(clamped);
            }
        }

        // ── Metode private ───────────────────────────────────────

        /// <summary>
        /// Opreste si distruge resursele audio in mod sigur.
        /// Apelata atat de Stop() cat si inainte de Play() nou.
        /// </summary>
        private void StopAndDispose()
        {
            WaveOutEvent oldWaveOut;
            AudioFileReader oldReader;

            lock (_lock)
            {
                if (_isStopping) return; // deja in proces de oprire
                _isStopping = true;

                oldWaveOut = _waveOut;
                oldReader = _audioReader;
                _waveOut = null;
                _audioReader = null;
            }

            // Dispose AFARA din lock, pe thread-ul curent (nu pe cel audio)
            if (oldWaveOut != null)
            {
                try
                {
                    // Deatasam evenimentul INAINTE de Stop pentru a evita
                    // re-intrarea in OnPlaybackStopped
                    oldWaveOut.PlaybackStopped -= OnPlaybackStopped;
                    oldWaveOut.Stop();

                    // Asteptam putin pentru ca thread-ul audio sa termine
                    Thread.Sleep(50);

                    oldWaveOut.Dispose();
                }
                catch { /* ignoram erorile la dispose */ }
            }

            if (oldReader != null)
            {
                try { oldReader.Dispose(); }
                catch { }
            }
        }

        /// <summary>
        /// Callback de la NAudio cand redarea s-a oprit.
        /// Ruleza pe thread-ul audio al NAudio - nu facem Dispose aici!
        /// </summary>
        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            // Verificam daca oprirea a fost intentionata (Stop/Next/Prev)
            // sau naturala (melodia s-a terminat)
            bool wasStopping;
            lock (_lock) { wasStopping = _isStopping; }

            if (!wasStopping && e.Exception == null)
            {
                // Melodia s-a terminat natural - notificam pe un thread nou
                // pentru a nu face dispose pe thread-ul audio NAudio
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    Thread.Sleep(100); // mic delay de siguranta
                    SongFinished?.Invoke(this, EventArgs.Empty);
                });
            }
        }

        // ── IDisposable ──────────────────────────────────────────
        public void Dispose()
        {
            if (!_disposed)
            {
                StopAndDispose();
                _disposed = true;
            }
        }
    }
}