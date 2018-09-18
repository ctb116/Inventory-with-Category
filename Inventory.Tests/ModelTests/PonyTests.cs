using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Inventory.Models;

namespace Inventory.Tests
{
  [TestClass]
  public class PonyTests : IDisposable
  {
    public void Dispose()
    {
      Pony.DeleteAll();
    }

    public PonyTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=inventory_test;";
    }

    [TestMethod]
    public void GetAll_DbStartsEmpty_0()
    {
      //Arrange
      //Act
      int result = Pony.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfPoniesAreTheSame_Pony()
    {
      // Arrange, Act
      Pony firstPony = new Pony("1", "1", "1", "1");
      Pony secondPony = new Pony("1", "1", "1", "1");
      bool isSame = firstPony.Equals(secondPony);
      // Assert
      Assert.AreEqual(isSame, true);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      Pony testPony = new Pony("1", "1", "1", "1");
      Pony testPony2 = new Pony("1", "1", "1", "1");

      //Act
      testPony.Save();
      Pony savedCard = Pony.GetAll()[0];

      int result = savedCard.GetId();
      int testId = testPony.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }
  }
}
