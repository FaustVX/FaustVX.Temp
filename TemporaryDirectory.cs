using IO = System.IO;

namespace FaustVX.Temp
{
    public sealed class TemporaryDirectory : Disposable
    {
        /// <summary>
        /// The wrapped directory.
        /// </summary>
        public IO.DirectoryInfo Path { get; }

        public TemporaryDirectory(string directoryPath)
            : this(new IO.DirectoryInfo(directoryPath))
        { }

        public TemporaryDirectory(IO.DirectoryInfo directoryPath)
            : base(() => directoryPath.Delete(true))
        {
            Path = directoryPath;
            Path.Create();
        }

        public static TemporaryDirectory CreateTemporaryDirectory()
        {
            var tempPath = IO.Path.GetTempPath();
            var tempDirectoryName = IO.Path.GetRandomFileName();
            var tempDirectoryFullPath = IO.Path.Combine(tempPath, tempDirectoryName);

            return new TemporaryDirectory(tempDirectoryFullPath);
        }

        /// <summary>
        /// Implicit conversion to <see cref="IO.DirectoryInfo"/> for easy use as method parameter, etc.
        /// <remarks>Remember to keep a reference to the <see cref="TemporaryDirectory"/>, otherwise it might get finalized and disposed before you planned to :)</remarks>
        /// </summary>
        public static implicit operator IO.DirectoryInfo(TemporaryDirectory temporaryDirectory)
            => temporaryDirectory.Path;

        /// <summary>
        /// Implicit conversion to <see cref="string"/> for easy use as method parameter, etc.
        /// <remarks>Remember to keep a reference to the <see cref="TemporaryDirectory"/>, otherwise it might get finalized and disposed before you planned to :)</remarks>
        /// </summary>
        public static implicit operator string(TemporaryDirectory temporaryDirectory)
            => temporaryDirectory.Path.FullName;

        /// <summary>
        /// Explicit conversion from <see cref="IO.DirectoryInfo"/> for easy wrapping.
        /// </summary>
        public static explicit operator TemporaryDirectory(IO.DirectoryInfo directoryInfo)
            => new TemporaryDirectory(directoryInfo.FullName);
    }
}
