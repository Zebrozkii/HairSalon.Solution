using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
    public class StylistControllerTest
    {
      [TestMethod]
      public void Index_ReturnsCorrectView_True()
      {
        //Arrange
        StylistController controller = new StylistController();

        //Act
        ActionResult indexView = controller.Index();

        //Assert
        Assert.IsInstanceOfType(indexView, typeof(ViewResult));
      }

      [TestMethod]
      public void Index_HasCorrectModelType_ItemList()
      {
        //Arrange
        ViewResult indexView = new StylistController().Index() as ViewResult;

        //Act
        var result = indexView.ViewData.Model;

        //Assert
        Assert.IsInstanceOfType(result, typeof(List<Stylist>));
      }
    }
}
