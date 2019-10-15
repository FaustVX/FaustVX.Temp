using System.IO;

namespace FaustVX.Temp
{
    /// <summary>
    /// A simple wrapper which deletes a file on dispose (or in finalizer).
    /// </summary>
    public sealed class TemporaryFile : Disposable
    {
        /// <summary>
        /// The wrapped file.
        /// </summary>
        public FileInfo FileInfo { get; }

        /// <summary>
        /// Wraps an existing file as a temporary file.
        /// </summary>
        /// <param name="path">Name of file to wrap.</param>
        public TemporaryFile(string fileName)
            : this(new FileInfo(fileName))
        { }

        /// <summary>
        /// Wraps an existing file as a temporary file.
        /// </summary>
        /// <param name="file">File to wrap.</param>
        public TemporaryFile(FileInfo temporaryFile)
            : base(temporaryFile.Delete)
            => FileInfo = temporaryFile;

        /// <summary>
        /// Creates and wraps a new temporary file using <see cref="Path.GetTempFileName"/>.
        /// </summary>
        public static TemporaryFile CreateTemporaryFile()
            => new TemporaryFile(Path.GetRandomFileName());

        /// <summary>
        /// Implicit conversion to <see cref="FileInfo"/> for easy use as method parameter, etc.
        /// <remarks>Remember to keep a reference to the <see cref="TemporaryFile"/>, otherwise it might get finalized and disposed before you planned to :)</remarks>
        /// </summary>
        public static implicit operator FileInfo(TemporaryFile temporaryFile)
            => temporaryFile.FileInfo;

        /// <summary>
        /// Implicit conversion to <see cref="string"/> for easy use as method parameter, etc.
        /// <remarks>Remember to keep a reference to the <see cref="TemporaryFile"/>, otherwise it might get finalized and disposed before you planned to :)</remarks>
        /// </summary>
        public static implicit operator string(TemporaryFile temporaryFile)
            => temporaryFile.FileInfo.FullName;

        /// <summary>
        /// Explicit conversion from <see cref="FileInfo"/> for easy wrapping.
        /// </summary>
        public static explicit operator TemporaryFile(FileInfo temporaryFile)
            => new TemporaryFile(temporaryFile);
    }
}
