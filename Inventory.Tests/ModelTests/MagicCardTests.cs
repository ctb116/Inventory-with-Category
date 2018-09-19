using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Inventory.Models;

namespace Inventory.Tests
{

  [TestClass]
  public class MagicCardTests : IDisposable
  {
    public void Dispose()
    {
      MagicCard.DeleteAll();
      Category.DeleteAll();
    }

    public MagicCardTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=inventory_test;";
    }

    [TestMethod]
    public void GetAll_DbStartsEmpty_0()
    {
      //Arrange
      //Act
      int result = MagicCard.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfCardsAreTheSame_Card()
    {
      // Arrange, Act
      MagicCard firstMagicCard = new MagicCard("1", "1", "1", "1", "1", 1);
      MagicCard secondMagicCard = new MagicCard("1", "1", "1", "1", "1", 1);
      bool isSame = firstMagicCard.Equals(secondMagicCard);
      // Assert
      Assert.AreEqual(isSame, true);
    }

    [TestMethod]
    public void Save_SavesCardToDatabase_MagicCardList()
    {
      MagicCard testCard = new MagicCard("1", "1", "1", "1", "1", 1);
      testCard.Save();

      List<MagicCard> result = MagicCard.GetAll();
      List<MagicCard> testList = new List<MagicCard>{testCard};

      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToObject_Id()
    {
      //Arrange
      MagicCard testMagicCard = new MagicCard("1", "1", "1", "1", "1", 1);
      MagicCard testMagicCard2 = new MagicCard("1", "1", "1", "1", "1", 1);

      //Act
      testMagicCard.Save();
      MagicCard savedCard = MagicCard.GetAll()[0];

      int result = savedCard.GetId();
      int testId = testMagicCard.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_FindMagicCardDatabase_MagicCard()
    {
      //Arrange
      MagicCard testMagicCard = new MagicCard("1", "2", "2", "2", "2", 1);
      testMagicCard.Save();

      //Act
      MagicCard foundMagicCard = MagicCard.Find(testMagicCard.GetId());

      //Assert
      Assert.AreEqual(testMagicCard, foundMagicCard);
    }
  }
}
