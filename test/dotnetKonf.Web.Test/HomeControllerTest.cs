using System;
using dotnetKonf.Web.Controllers;
using dotnetKonf.Web.Models;
using Xunit;

namespace dotnetKonf.Web.Test
{
    public class HomeControllerTest
    {
        [Fact]
        public void ConstructorShouldNotFail()
        {
            var homeController = new HomeController();
            Assert.NotNull(homeController);
        }

        [Fact]
        public void PricingModel_Should_Contain_3_Items()
        {
            var homeController = new HomeController();
            var pricingModel = new PriceModel();
            Assert.NotNull(pricingModel);
            Assert.InRange(homeController.PriceModel.Pricings.Count, 3, 3);
        }
    }
}
