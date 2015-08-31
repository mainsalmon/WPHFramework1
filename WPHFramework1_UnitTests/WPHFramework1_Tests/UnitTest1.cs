using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPHFramework1;
using System.Runtime.Serialization;
using System.IO;
using Caliburn.Micro;
using System.Collections.Generic;
using System.Linq;

namespace WPHFramework1_Tests
{
    [TestClass]
    public class JsonRepositoryTests
    {
        // Test the repository and json backing
         
        [TestMethod]
        public void TestSave()
        {
            JsonRepository<TestEntity> repo = new JsonRepository<TestEntity>();
            int id = 7;
            TestEntity te = new TestEntity(id);
            repo.Save(te);

            Assert.IsTrue(File.Exists(repo.MakeFullPath(id)));
         
            Directory.Delete(repo.FolderPath, true);

        }

        [TestMethod]
        public void TestGetById()
        {
            JsonRepository<TestEntity> repo = new JsonRepository<TestEntity>();
            int id = 3;
            TestEntity te = new TestEntity(id);
            repo.Save(te);

            Album a3 = repo.GetById(id);
            Assert.IsNotNull(a3);

            Directory.Delete(repo.FolderPath, true);
        }

        [TestMethod]
        public void TestDeserialization()
        {
            JsonRepository<TestEntity> repo = new JsonRepository<TestEntity>();
            int id = 3;
            TestEntity te = new TestEntity(id);

            string testTitle = "Test Title";
            te.Title = testTitle;

            double testPrice = 7.15;
            te.UnitPrice = testPrice;

            AlbumCategory testCat = AlbumCategory.Pop;
            te.Category = testCat;

            int testQty = 7;
            te.Quantity = testQty;

            DateTime testDate = DateTime.Now.Date;
            te.ReleaseDate = testDate;

            repo.Save(te);

            Album a3 = repo.GetById(id);
            
            Assert.IsTrue(a3.Title == testTitle);
            Assert.IsTrue(a3.UnitPrice == testPrice);
            Assert.IsTrue(a3.Category == testCat);
            Assert.IsTrue(a3.Quantity == testQty);
            Assert.IsTrue(a3.ReleaseDate == testDate);

            Directory.Delete(repo.FolderPath, true);
        }


        [TestMethod]
        public void TestGetNextId()
        {
            JsonRepository<TestEntity> repo = new JsonRepository<TestEntity>();
            int id = 7;
            TestEntity te = new TestEntity(id);
            repo.Save(te);

            Assert.IsTrue(repo.GetNextId() == id + 1);

            Directory.Delete(repo.FolderPath, true);

        }

        [TestMethod]
        public void TestSearchFor()
        {
            JsonRepository<TestEntity> repo = new JsonRepository<TestEntity>();
            int id = 1;
            TestEntity te = new TestEntity(id);
            te.Quantity = 10;
            te.Category = AlbumCategory.Rock;
            te.VendorId = "FM";

            repo.Save(te);

            id = 2;
            te = new TestEntity(id);
            te.Quantity = 40;
            te.Category = AlbumCategory.Country;
            te.VendorId = "FM";
            repo.Save(te);

            id = 3;
            te = new TestEntity(id);
            te.Quantity = 10;
            te.Category = AlbumCategory.Pop;
            te.VendorId = "AMZ";
            repo.Save(te);

            repo = new JsonRepository<TestEntity>(); // start fresh
            
            var rslt = repo.SearchFor(a => a.VendorId == "FM").ToList();
            Assert.IsTrue(rslt.Count == 2);

            var rslt2 = repo.SearchFor(a => a.Category == AlbumCategory.Country).ToList();
            Assert.IsTrue(rslt2.Count == 1);

            var rslt3 = repo.SearchFor(a => a.Quantity > 5).ToList();
            Assert.IsTrue(rslt3.Count == 3);

            Directory.Delete(repo.FolderPath, true);

        }

     
        [TestMethod]
        public void TestGetAll()
        {
            JsonRepository<TestEntity> repo = new JsonRepository<TestEntity>();
            int id = 1;
            TestEntity te = new TestEntity(id);
            repo.Save(te);

            id = 2;
            te = new TestEntity(id);
            repo.Save(te);

            id = 3;
            te = new TestEntity(id);
            repo.Save(te);

            repo = new JsonRepository<TestEntity>();
            var fromFiles = repo.GetAll().ToList();

            Assert.IsTrue(fromFiles.Count == 3);

            Directory.Delete(repo.FolderPath, true);
        
        }
    }

    [DataContract]
    public class TestEntity : Album
    {
        public TestEntity(int id): base(id)
        {
        // wrapper so folder and files created by tests are kept separate from 'live' 
        // versions and can be easily deleted.
        }

    }
}
