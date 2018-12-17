using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{

  [TestClass]
  public class StylistTest : IDisposable
  {

  public StylistTest()
   {
     DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=gulzat_karimova_tests;";
   }

   public void Dispose()
    {
      Stylist.ClearAll();
      Client.ClearAll();
    }

    [TestMethod]
   public void StylistConstructor_CreatesInstanceOfStylist_Stylist()
   {
     Stylist newStylist = new Stylist("gulzat", "cut");
     Assert.AreEqual(typeof(Stylist), newStylist.GetType());
   }

   [TestMethod]
   public void GetName_ReturnsName_String()
   {
     //Arrange
     string name = "gulzat";
     Stylist newStylist = new Stylist(name, "description", 2);

     //Act
     string result = newStylist.GetName();

     //Assert
     Assert.AreEqual(name, result);
   }

   [TestMethod]
   public void GetDescription_ReturnsDescription_String()
   {
     //Arrange
     string name = "gulzat";
     string description = "cut";
     Stylist newStylist = new Stylist(name, description, 1);

     //Act
     string result = newStylist.GetDescription();

     //Assert
     Assert.AreEqual(description, result);
   }

   [TestMethod]
   public void GetId_ReturnStylistId_Int()
   {
     //Arrange
      string name = "Test Stylist";
      string description = "cut";
      int id = 4;
      Stylist newStylist = new Stylist(name, description, id);

      //Act
      int result = newStylist.GetId();

      //Assert
      Assert.AreEqual(id, result);
   }

   [TestMethod]
    public void GetAll_ReturnsAllStylistObjects_StylistList()
    {
      //Arrange
      string name = "Anna";
      string description = "cut";
      string name2 = "Anna";
      string description2 = "cut";
      Stylist newStylist1 = new Stylist(name, description);
      newStylist1.Save();
      Stylist newStylist2 = new Stylist(name2, description2);
      newStylist2.Save();
      List<Stylist> newList = new List<Stylist> { newStylist1, newStylist2 };

      //Act
      List<Stylist> result = Stylist.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
       public void Find_ReturnsStylistInDatabase_Stylist()
       {
         //Arrange
         Stylist testStylist = new Stylist("anna", "cut");
         testStylist.Save();

         //Act
         Stylist foundStylist = Stylist.Find(testStylist.GetId());

         //Assert
         Assert.AreEqual(testStylist, foundStylist);
       }


    [TestMethod]
    public void GetStylist_ReturnsUpdatedList_AfterEdit()
    {
      //Arrange
      string name = "jibek";
      string description = "Cut";
      string name2 = "gulzat";
      string description2 = "color";
      Stylist newStylist = new Stylist(name, description);
      newStylist.Save();

      newStylist.Edit(name2, description2);

      //Act
      string result = newStylist.GetName();

      //Assert
     Assert.AreEqual(name2, result);
    }



      [TestMethod]
      public void GetStylist_ReturnsEmptyList_AfterDelete()
      {
        //Arrange
        string name = "gulzat";
        string description = "cut";
        Stylist newStylist = new Stylist(name, description);
        newStylist.Save();

        //Act
        Stylist.DeleteStylist(newStylist.GetId());

        //Assert
        List<Stylist> result = Stylist.GetAll();
        List<Stylist> newList = new List<Stylist> {};
        CollectionAssert.AreEqual(newList, result);
      }

      [TestMethod]
     public void GetClient_ReturnStylistClient_ListOfClient()
     {
       //Arrange
       Stylist newStylist = new Stylist("gulzat", "cut");
       newStylist.Save();

       Client newClient = new Client("gulzat", newStylist.GetId());
       newClient.Save();

       //Act
       List<Client> result = newStylist.GetClients();
       List<Client> newList = new List<Client> {newClient};

       //Assert
       CollectionAssert.AreEqual(newList, result);
     }

     [TestMethod]
     public void Save_SavesStylistToDatabase_StylistList()
     {
       //Arrange
       Stylist testStylist = new Stylist("jibek", "cut");

       //Act
       testStylist.Save();
       List<Stylist> result = Stylist.GetAll();
       List<Stylist> testList = new List<Stylist>{testStylist};

       //Assert
      CollectionAssert.AreEqual(testList, result);
     }

      [TestMethod]
       public void Save_DatabaseAssignsIdToStylist_Id()
       {
         //Arrange
         Stylist testStylist = new Stylist("gulzat", "color");
         testStylist.Save();

         //Act
         Stylist savedStylist = Stylist.GetAll()[0];

         int result = savedStylist.GetId();
         int testId = testStylist.GetId();

         //Assert
         Assert.AreEqual(testId, result);
       }

       [TestMethod]
        public void Test_AddSpecialty_AddsSpecialtyToStylist()
        {
          //Arrange
          Stylist testStylist = new Stylist("Gulzat", "cuts");
          testStylist.Save();
          Specialty testSpecialty = new Specialty("kids cut");
          testSpecialty.Save();
          Specialty testSpecialty2 = new Specialty("coloring");
          testSpecialty2.Save();

          //Act
          testStylist.AddSpecialty(testSpecialty);
          testStylist.AddSpecialty(testSpecialty2);
          List<Specialty> result = testStylist.GetSpecialties();
          List<Specialty> testList = new List<Specialty>{testSpecialty, testSpecialty2};

          //Assert
          CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void GetSpecialty_ReturnsAllStylistSpecialty_SpecialtyList()
        {
          //Arrange
          Stylist testStylist = new Stylist("gulzat", "cuts");
          testStylist.Save();
          Specialty testSpecialty1 = new Specialty("color");
          testSpecialty1.Save();
          Specialty testSpecialty2 = new Specialty("cut");
          testSpecialty2.Save();

          //Act
          testStylist.AddSpecialty(testSpecialty1);
          List<Specialty> savedSpecialty = testStylist.GetSpecialties();
          List<Specialty> testList = new List<Specialty> {testSpecialty1};

          //Assert
          CollectionAssert.AreEqual(testList, savedSpecialty);
        }

  }
}
