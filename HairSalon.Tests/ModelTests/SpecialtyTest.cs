using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{

  [TestClass]
  public class SpecialtyTest : IDisposable
  {

  public SpecialtyTest()
   {
     DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=gulzat_karimova_tests;";
   }

   public void Dispose()
    {
      Stylist.ClearAll();
      Specialty.ClearAll();
      Client.ClearAll();
    }

    [TestMethod]
   public void SpecialtyConstructor_CreatesInstanceOfSpecialty_Specialty()
   {
     Specialty newSpecialty = new Specialty("coloring");
     Assert.AreEqual(typeof(Specialty), newSpecialty.GetType());
   }

   [TestMethod]
   public void GetName_ReturnsName_String()
   {
     //Arrange
     string name = "coloring";
     Specialty newSpecialty = new Specialty(name, 2);

     //Act
     string result = newSpecialty.GetName();

     //Assert
     Assert.AreEqual(name, result);
   }

   [TestMethod]
   public void GetId_ReturnSpecialtyId_Int()
   {
     //Arrange
      string name = "styling";
      int id = 4;
      Specialty newSpecialty = new Specialty(name, id);

      //Act
      int result = newSpecialty.GetId();

      //Assert
      Assert.AreEqual(id, result);
   }

   [TestMethod]
    public void GetAll_ReturnsAllSpecialtyObjects_SpecialtyList()
    {
      //Arrange
      string name = "kids cut";
      string name2 = "mens cut";
      Specialty newSpecialty1 = new Specialty(name);
      newSpecialty1.Save();
      Specialty newSpecialty2 = new Specialty(name2);
      newSpecialty2.Save();
      List<Specialty> newList = new List<Specialty> { newSpecialty1, newSpecialty2 };

      //Act
      List<Specialty> result = Specialty.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
       public void Find_ReturnsSpecialtyInDatabase_Specialty()
       {
         //Arrange
         Specialty testSpecialty = new Specialty("coloring");
         testSpecialty.Save();

         //Act
         Specialty foundSpecialty = Specialty.Find(testSpecialty.GetId());

         //Assert
         Assert.AreEqual(testSpecialty, foundSpecialty);
       }


           [TestMethod]
           public void GetStylisty_ReturnsAllSpecialtyStylist_StylistList()
           {
             //Arrange
             Specialty testSpecialty = new Specialty("coloring");
             testSpecialty.Save();
             Stylist testStylist1 = new Stylist("Anna", "cut");
             testStylist1.Save();
             Stylist testStylist2 = new Stylist("Emma", "color");
             testStylist2.Save();

             //Act
             testSpecialty.AddStylist(testStylist1);
             List<Stylist> result = testSpecialty.GetStylists();
             List<Stylist> testList = new List<Stylist> {testStylist1};

             //Assert
            CollectionAssert.AreEqual(testList, result);
           }

           [TestMethod]
           public void AddStylist_AddsStylistToSpecialty_StylistList()
           {
             // Arrange
             Specialty testSpecialty = new Specialty("cut");
             testSpecialty.Save();
             Stylist testStylist = new Stylist("Maria", "nails");
             testStylist.Save();
             // Act
             testSpecialty.AddStylist(testStylist);
             List<Stylist> result = testSpecialty.GetStylists();
             List<Stylist> testList = new List<Stylist>{testStylist};

             // Assert
             CollectionAssert.AreEqual(testList, result);
           }

           // [TestMethod]
           // public void Delete_DeleteSpecialtyFromDatabase_SpecialtyList()
           // {
           //   // Arrange
           //   Stylist testStylist = new Stylist("Maria","cut");
           //   testStylist.Save();
           //   string testName = "kids cut";
           //   Specialty testSpecialty = new Specialty(testName);
           //   testSpecialty.Save();
           //   // Act
           //   testSpecialty.AddStylist(testStylist);
           //   int id = testSpecialty.GetId();
           //   testSpecialty.DeleteSpecialty(int id);
           //   List<Specialty> result = testStylist.GetSpecialties();
           //   List<Specialty> testStylistSpecialties = new List<Specialty>{};
           //   // Asser
           //   CollectionAssert.AreEqual(testStylistSpecialties, result);
           // }


             [TestMethod]
             public void Save_SavesSpecialtyToDatabase_SpecialtyList()
             {
               //Arrange
               Specialty testSpecialty = new Specialty("cut");

               //Act
               testSpecialty.Save();
               List<Specialty> result = Specialty.GetAll();
               List<Specialty> testList = new List<Specialty>{testSpecialty};

               //Assert
              CollectionAssert.AreEqual(testList, result);
             }

              [TestMethod]
               public void Save_DatabaseAssignsIdToSpecialty_Id()
               {
                 //Arrange
                 Specialty testSpecialty = new Specialty("color");
                 testSpecialty.Save();

                 //Act
                 Specialty savedSpecialty = Specialty.GetAll()[0];

                 int result = savedSpecialty.GetId();
                 int testId = testSpecialty.GetId();

                 //Assert
                 Assert.AreEqual(testId, result);
               }
          }
        }
