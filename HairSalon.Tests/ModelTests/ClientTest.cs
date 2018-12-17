using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using HairSalon.Models;

namespace HairSalon.Tests
{
 [TestClass]
  public class ClientTest : IDisposable
  {
    public ClientTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=gulzat_karimova_tests;";
    }

    public void Dispose()
    {
      Client.ClearAll();
    }

    [TestMethod]
     public void ClientConstructor_CreatesInstanceOfClient_Client()
     {
       Client newClient = new Client("gulzat", 2);
       Assert.AreEqual(typeof(Client), newClient.GetType());
     }

     [TestMethod]
     public void GetName_ReturnsName_String()
     {
       // Arrange
       string name = "gulzat";
       Client newClient = new Client(name, 2);
       // Act
       string result = newClient.GetClientName();
       // Assert
       Assert.AreEqual(name, result);
     }

     [TestMethod]
     public void GetId_ReturnClientId_Int()
     {
       // Arrange
       string name = "gulzat";
       int stylistId = 2;
       int id = 5;
       Client newClient = new Client(name, stylistId, id);
       // Act
       int result = newClient.GetId();
       // Assert
       Assert.AreEqual(id, result);
     }

     [TestMethod]
     public void GetAll_ReturnsAllClientObjects_ClientList()
     {
       // Arrange

       Stylist newStylist = new Stylist("Emma", "cut");
       newStylist.Save();
       int stylistId = newStylist.GetId();
       Client newClient = new Client("gulzat", stylistId);
       newClient.Save();
       Client newClient2 = new Client("jibek", stylistId);
       newClient2.Save();


       // Act
      List<Client> clients = new List<Client>{newClient, newClient2};
       List<Client> newList = Client.GetAll();
       // Assert
       CollectionAssert.AreEqual(clients, newList);
     }

     [TestMethod]
     public void Save_SavesClientToDatabase_ClientList()
     {
       //Arrange
       Stylist newStylist = new Stylist("Emma", "cut");
       newStylist.Save();
       int stylistId = newStylist.GetId();
       Client testClient = new Client("jibek", stylistId);

       //Act
       testClient.Save();
       List<Client> result = Client.GetAll();
       List<Client> testList = new List<Client>{testClient};

       //Assert
      CollectionAssert.AreEqual(testList, result);
     }

     [TestMethod]
      public void Save_DatabaseAssignsIdToClient_Id()
      {
        //Arrange
        Stylist newStylist = new Stylist("Emma", "cut");
        newStylist.Save();
        int stylistId = newStylist.GetId();
        Client testClient = new Client("jibek", stylistId);
        testClient.Save();

        //Act
        Client savedClient = Client.GetAll()[0];

        int result = savedClient.GetId();
        int testId = testClient.GetId();

        //Assert
        Assert.AreEqual(testId, result);
      }
  }
}
