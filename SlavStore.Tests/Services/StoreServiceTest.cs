using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlavStore.Data.Mocks;
using SlavStore.Models;
using SlavStore.Services;
using SlavStore.Services.Interfaces;

namespace SlavStore.Tests.Services
{
    [TestClass]
    public class StoreServiceTest
    {
        private const int ValidId = 666;
        private const string ValidName = "testCategory";
        private const string ValidUserId = "testUserId";



        private IStoresService service;

        [TestInitialize]
        public void InitializeTest()
        {
            FakeDbContext context = new FakeDbContext();
            this.service = new StoresService(context);
        }


        [TestMethod]
        public void GetStores_EmptySet_CountShouldBeZero()
        {
            int result = 0;

            var stores = this.service.GetStores();

            Assert.IsNotNull(stores);
            Assert.AreEqual(result, stores.Count);
        }


        [TestMethod]
        public void GetStores_EmptySet_CountShouldBeOne()
        {
            int result = 1;

            this.CreateStore();
            var stores = this.service.GetStores();

            Assert.AreEqual(result, stores.Count);
        }

        [TestMethod]
        public void RemoveStore_ValidId_CountShouldBeZero()
        {
            int result = 0;

            this.CreateStore();
            this.service.Delete(ValidId);
            var stores = this.service.GetStores();

            Assert.AreEqual(result, stores.Count);
        }

        [TestMethod]
        public void IsCurrentUserStore_ValidUserId_AssertShouldBeTrue()
        {
            bool result = true;

            this.CreateStore();
            var check = this.service.IsCurrentUserStore(ValidId, ValidUserId);

            Assert.AreEqual(result, check);
        }

        [TestMethod]
        public void IsStoreNull_ValidStoreId_AssertShouldBeFalse()
        {
            bool result = false;

            this.CreateStore();
            var check = this.service.IsStoreNull(ValidId);

            Assert.AreEqual(result, check);
        }

        private void CreateStore()
        {
            Store store = new Store()
            {
                Id = ValidId,
                Name = ValidName,
            };
            this.service.Create(store,ValidUserId);
        }
    }
}
