using CatShelter.Controllers;
using CatShelter.Services;
using CatShelter.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
namespace TestCatShelter
{
    public class TestRegister
    {
        [Fact]
        public void Test()
        {
            var mockCatService = new Mock<ICatService>();
            var catController = new CatController(mockCatService.Object, null);
            var result = catController.Create(new CatShelter.ViewModels.CatViewModels.CreateViewModel
            {
                Id = 1,
            });
            Assert.IsType<RedirectResult>(result);
            RedirectResult redirectResult = result as RedirectResult;
            Assert.Equal("/cat/details/1", redirectResult.Url);
            mockCatService.Verify(service =>
                service.Insert(It.IsAny<Cat>()),
                Times.Once());                

        }
    }
}

