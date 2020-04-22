using IO = System.IO;

namespace FaustVX.Temp
{
    public sealed class TemporaryDirectory : Disposable
    {
        /// <summary>
        /// The wrapped directory.
        /// </summary>
        public IO.DirectoryInfo Path { get; }

        public TemporaryDirectory(string directoryPath, bool setCurrentDirectory)
            : this(new IO.DirectoryInfo(directoryPath), setCurrentDirectory)
        { }

        public TemporaryDirectory(IO.DirectoryInfo directoryPath, bool setCurrentDirectory)
            : base(OnDispose(setCurrentDirectory, System.Environment.CurrentDirectory, directoryPath))
        {
            Path = directoryPath;
            Path.Create();

            if (setCurrentDirectory)
                System.Environment.CurrentDirectory = this;
        }

        private static System.Action OnDispose(bool setCurrentDirectory, string previousDirectory, IO.DirectoryInfo currentDirectory)
            => () =>
            {
                if (setCurrentDirectory)
                    System.Environment.CurrentDirectory = previousDirectory;
                currentDirectory.Delete(true);
            };

        public static TemporaryDirectory CreateTemporaryDirectory(bool setCurrentDirectory)
        {
            var tempPath = IO.Path.GetTempPath();
            var tempDirectoryName = IO.Path.GetRandomFileName();
            var tempDirectoryFullPath = IO.Path.Combine(tempPath, tempDirectoryName);

            return new TemporaryDirectory(tempDirectoryFullPath, setCurrentDirectory);
        }

        public static TemporaryDirectory CreateLocalTemporaryDirectory(bool setCurrentDirectory)
            => new TemporaryDirectory(IO.Path.GetRandomFileName(), setCurrentDirectory);

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
            => new TemporaryDirectory(directoryInfo.FullName, false);
    }
}
