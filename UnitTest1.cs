#if TEST
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace FaustVX.Temp.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Temp()
        {
            using var dir = TemporaryDirectory.CreateTemporaryDirectory();
            Assert.AreEqual(new DirectoryInfo(System.IO.Path.GetTempPath()).FullName.TrimEnd('/', '\\'), dir.Path.Parent.FullName.TrimEnd('/', '\\'));
        }
        
        [TestMethod]
        public void Local()
        {
            using var dir = TemporaryDirectory.CreateLocalTemporaryDirectory();
            Assert.AreEqual(new DirectoryInfo("./").FullName.TrimEnd('/', '\\'), dir.Path.Parent.FullName.TrimEnd('/', '\\'));
        }
    }
}
#endif