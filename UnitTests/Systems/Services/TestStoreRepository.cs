using System. Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PizzaOrderAPI.Controllers;
using PizzaOrderAPI.Data;
using PizzaOrderAPI.Data.Mock;
using PizzaOrderAPI.Models;
using Xunit;
using FluentAssertions;
using UnitTests.Fixtures;

namespace UnitTests.Systems.Services
{
    public class TestStoreRepository
    {
        [Fact]
        public async Task GetAllStores_WhenCalled_ReturnsListOfUsers()
        {
            //Arrange
            var sut = new MockStoreRepository();

            //Act
            var result = await sut.GetAllStores();

            //Assert
            result.Should().BeOfType<List<Store>>();
        }
    }
}
