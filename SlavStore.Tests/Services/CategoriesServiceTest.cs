using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlavStore.Data.Mocks;
using SlavStore.Models;
using SlavStore.Services;
using SlavStore.Services.Interfaces;

namespace SlavStore.Tests.Services
{
    [TestClass]
    public class CategoriesServiceTest
    {
        private const int ValidId = 666;
        private const string ValidName = "testCategory";
        private const string ValidDescription = "testCategoryDescription";


        private ICategoriesService service;

        [TestInitialize]
        public void InitializeTest()
        {
            FakeDbContext context = new FakeDbContext();
            this.service = new CategoriesService(context);
        }


        [TestMethod]
        public void GetCategories_EmptySet_CountShouldBeZero()
        {
            int result = 0;

            var categories = this.service.GetCategories();

            Assert.IsNotNull(categories);
            Assert.AreEqual(result,categories.Count);
        }


        [TestMethod]
        public void GetCategories_EmptySet_CountShouldBeOne()
        {
            int result = 1;

            this.CreateCategory();
            var categories = this.service.GetCategories();

            Assert.AreEqual(result, categories.Count);
        }

        [TestMethod]
        public void RemoveCategory_ValidId_CountShouldBeZero()
        {
            int result = 0;

            this.CreateCategory();
            this.service.Delete(ValidId);
            var categories = this.service.GetCategories();

            Assert.AreEqual(result, categories.Count);
        }

        private void CreateCategory()
        {
            Category category = new Category()
            {
                Id = ValidId,
                Name = ValidName,
                Description = ValidDescription,
            };
            this.service.Create(category);
        }
    }
}
