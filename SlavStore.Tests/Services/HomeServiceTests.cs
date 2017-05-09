using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlavStore.Data;
using SlavStore.Data.Mocks;
using SlavStore.Models;
using SlavStore.Models.ViewModels;
using SlavStore.Services;

namespace SlavStore.Tests.Services
{
    [TestClass]
    public class HomeServiceTests
    {
        private IHomeService service;

        [TestInitialize]
        public void InitializeTest()
        {
            this.InitializeMapping();

            FakeDbContext context = new FakeDbContext();
            this.service = new HomeService(context);
        }

        private void InitializeMapping()
        {
            Mapper.Initialize(p =>
            {
                p.CreateMap<Item, HomeViewModel>(); 
            });
        }


        [TestMethod]
        public void GetHomeItems_EmptySet_CountShouldBeZero()
        {
            int result = 0;

            var homeItems = service.GetHomeItems();

            Assert.IsNotNull(homeItems);
            Assert.AreEqual(result, homeItems.Count);
        }

    }
}
