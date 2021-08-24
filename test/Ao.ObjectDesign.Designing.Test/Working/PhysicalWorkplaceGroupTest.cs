using Ao.ObjectDesign.Designing.Working;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Designing.Test.Working
{
    [TestClass]
    public class PhysicalWorkplaceGroupTest
    {
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            var key = "k";
            var folder = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            var acceptExtensions = new ReadOnlyHashSet<string>(Enumerable.Empty<string>());
            Assert.ThrowsException<ArgumentNullException>(() => new ValuePhysicalWorkplaceGroup(null));
            Assert.ThrowsException<ArgumentNullException>(() => new ValuePhysicalWorkplaceGroup(null,acceptExtensions));
            Assert.ThrowsException<ArgumentNullException>(() => new ValuePhysicalWorkplaceGroup(folder,null));
            Assert.ThrowsException<ArgumentNullException>(() => new ValuePhysicalWorkplaceGroup(key, null));
        }
        
        private static readonly DirectoryInfo folder = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "physical"));

        [ClassInitialize]
        public static Task Initialize(TestContext context)
        {
            if (folder.Exists)
            {
                folder.Delete(true);
            }
            folder.Create();
            return Task.FromResult(0);
        }
        [ClassCleanup]
        public static void Cleanup()
        {
            folder.Delete(true);
        }

        [TestMethod]
        public void Init_AcceptAll_CanScanFiles()
        {
            var group = new ValuePhysicalWorkplaceGroup(folder);

            Assert.IsTrue(group.AcceptAllFile);
            Assert.IsNull(group.AcceptExtensions);
            Assert.AreEqual(folder, group.Folder);
            Assert.IsNull(group.Key);

            var fi = Path.Combine(folder.FullName, "a.txt");
            File.WriteAllText(fi, "hello");

            var wp = group.Get(string.Empty);

            Assert.IsNull(wp.GroupKey);
            Assert.AreEqual(group, wp.Group);

            var files = wp.Resources.ToArray();

            Assert.AreEqual(1, files.Length);
            Assert.AreEqual("a.txt", files[0]);

            var content=wp.Get("a.txt");

            Assert.AreEqual("hello", content);
        }
        [TestMethod]
        public void StoreFile_MustStoreToPhysicalFile()
        {
            var group = new ValuePhysicalWorkplaceGroup(folder);
            var wp = group.Get(string.Empty);

            var name = "store.txt";
            var path = Path.Combine(folder.FullName, name);

            wp.Store(name, "123");

            Assert.IsTrue(folder.GetFiles(name).Any());

            Assert.AreEqual("123", File.ReadAllText(path));

            wp.Store(name, "hello");

            Assert.IsTrue(folder.GetFiles(name).Any());

            Assert.AreEqual("hello", File.ReadAllText(path));
        }
        [TestMethod]
        public void CreateWithNotExists_MustCreatedFolder()
        {
            var group = new ValuePhysicalWorkplaceGroup(folder);

            var create1 = group.Get("create1");
            var create2 = group.Get("create2");

            Assert.IsTrue(folder.EnumerateDirectories().Any(x => x.Name == "create1"));
            Assert.IsTrue(folder.EnumerateDirectories().Any(x => x.Name == "create2"));

            create1.Store("a.txt", "123");
            create2.Store("b.txt", "123");

            group.Clear();

            Assert.AreEqual(0, folder.EnumerateDirectories().Count());
        }
        [TestMethod]
        public void ClearGrouopItems_FilesMustClean()
        {
            var group = new ValuePhysicalWorkplaceGroup(folder);

            var wp = group.Get(string.Empty);

            wp.Store("a.txt", "123");
            wp.Clear();

            Assert.AreEqual(0, wp.Resources.Count());
            Assert.AreEqual(0, folder.GetFiles().Length);

            wp.Store("a.txt", "123");
            wp.Store("b.txt", "456");
            wp.Clear();

            Assert.AreEqual(0, wp.Resources.Count());
            Assert.AreEqual(0, folder.GetFiles().Length);
        }
    }
}
