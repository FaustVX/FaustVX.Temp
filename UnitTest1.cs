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
            using (var dir = TemporaryDirectory.CreateTemporaryDirectory(false))
                Assert.AreEqual(new DirectoryInfo(System.IO.Path.GetTempPath()).FullName.TrimEnd('/', '\\'), dir.Path.Parent.FullName.TrimEnd('/', '\\'));
            
            using (var dir = TemporaryDirectory.CreateTemporaryDirectory(true))
                Assert.AreEqual(System.Environment.CurrentDirectory, dir);
        }
        
        [TestMethod]
        public void Local()
        {
            using (var dir = TemporaryDirectory.CreateLocalTemporaryDirectory(false))
                Assert.AreEqual(new DirectoryInfo("./").FullName.TrimEnd('/', '\\'), dir.Path.Parent.FullName.TrimEnd('/', '\\'));
            
            using (var dir = TemporaryDirectory.CreateLocalTemporaryDirectory(true))
                Assert.AreEqual(System.Environment.CurrentDirectory, dir);
        }
    }
}
#endif