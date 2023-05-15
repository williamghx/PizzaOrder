using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PizzaOrderAPI.Controllers;
using PizzaOrderAPI.Data;
using PizzaOrderAPI.Models;
using Xunit;
using FluentAssertions;
using UnitTests.Fixtures;

namespace UnitTests.Systems.Controllers
{
    public class TestStoresController
    {
        [Fact]
        public async Task Get_OnSuccess_InvokeStoreRepository()
        {
            //Arrange
            var mockStoreRepository = new Mock<IStoreRepository>();
            mockStoreRepository.Setup(service => service.GetAllStores()).ReturnsAsync(new List<Store>());

            var sut = new StoresController(mockStoreRepository.Object);

            //Act
            var result = await sut.GetStore();

            //Assert
            mockStoreRepository.Verify(service => service.GetAllStores(), Times.Once);
        }

        [Fact]
        public async Task Get_OnSuccess_ReturnStatusCode200()
        {
            //Arrange
            var mockStoreRepository = new Mock<IStoreRepository>();
            mockStoreRepository.Setup(service => service.GetAllStores()).ReturnsAsync(new List<Store>(StoresFixture.Stores));

            var sut = new StoresController(mockStoreRepository.Object);

            //Act
            var result = (await sut.GetStore()).Result as OkObjectResult;

            //Assert
     
            result?.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Get_OnSuccess_ReturnsListOfStores()
        {
            //Arrange
            var mockStoreRepository = new Mock<IStoreRepository>();
            mockStoreRepository.Setup(service => service.GetAllStores()).ReturnsAsync(new List<Store>(StoresFixture.Stores));

            var sut = new StoresController(mockStoreRepository.Object);

            //Act
            var result = (await sut.GetStore()).Result as OkObjectResult;

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            result?.Value.Should().BeOfType<List<Store>>();
            (result?.Value as List<Store>)?.Count.Should().Be(2);

        }
    }
}
