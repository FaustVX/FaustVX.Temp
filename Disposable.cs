using System;

namespace FaustVX.Temp
{
    public abstract class Disposable : IDisposable
    {
        private volatile bool _disposed;
        private readonly Action _delete;

        public Disposable(Action delete)
        {
            _delete = delete;
        }

        /// <summary>
        /// Silently (exceptions swallowed) try to delete the temporary resource.
        /// </summary>
        public void Dispose()
        {
            try
            {
                _delete();
                _disposed = true;
            }
            catch (Exception) { } // Ignore
        }

        /// <summary>
        /// Call <see cref="Dispose"/> if nobody else has.
        /// </summary>
        ~Disposable()
        {
            if (!_disposed)
                Dispose();
        }
    }
}
