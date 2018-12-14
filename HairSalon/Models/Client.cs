using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class Client
    {
      private string _clientName;
      private int _id;
      private int _stylistId;

      public Client(string name, int stylistId, int id = 0)
      {
        _clientName = name;
        _stylistId = stylistId;
        _id = id;
      }

      public string GetClientName()
      {
        return _clientName;
      }

      public void SetName(string newName)
      {
        _clientName = newName;
      }

      public int GetId()
      {
        return _id;
      }

      public int GetStylistId()
      {
        return _stylistId;
      }

      public static List<Client> GetAll()
   {
     List<Client> allClients = new List<Client> { };
     MySqlConnection conn = DB.Connection();
     conn.Open();
     MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"SELECT * FROM clients;";
     MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
     while(rdr.Read())
     {
       int clientId = rdr.GetInt32(0);
       string clientName = rdr.GetString(1);
       int stylistId = rdr.GetInt32(2);
       Client newClient = new Client(clientName, stylistId, clientId);
       allClients.Add(newClient);
     }
     conn.Close();
     if (conn != null)
     {
       conn.Dispose();
     }
     return allClients;
   }

   public static void ClearAll()
   {
     MySqlConnection conn = DB.Connection();
     conn.Open();
     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"DELETE FROM clients;";
     cmd.ExecuteNonQuery();
     conn.Close();
     if (conn != null)
     {
       conn.Dispose();
     }
   }

   public static List<Client> GetAllClients(int id)
   {
     List<Client> clients = new List<Client> {};
     MySqlConnection conn = DB.Connection();
     conn.Open();
     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"SELECT * FROM clients WHERE stylistId = '" + id + "';";
     var rdr = cmd.ExecuteReader() as MySqlDataReader;
     while(rdr.Read())
     {
       int clientId = rdr.GetInt32(0);
       string clientName = rdr.GetString(1);
       int stylistId = rdr.GetInt32(2);
       Client newClient = new Client(clientName, stylistId, clientId);
       clients.Add(newClient);
     }
     conn.Close();
     if (conn != null)
     {
       conn.Dispose();
     }
     return clients;
   }

   public static void DeleteClient(int id)
   {
     MySqlConnection conn = DB.Connection();
     conn.Open();
     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"DELETE FROM clients WHERE id = (@thisId);";

     MySqlParameter thisId = new MySqlParameter();
     thisId.ParameterName = "@thisId";
     thisId.Value = id;
     cmd.Parameters.Add(thisId);
     cmd.ExecuteNonQuery();
     conn.Close();
     if (conn != null)
     {
      conn.Dispose();
     }
   }

   public static Client Find(int id)
   {
     MySqlConnection conn = DB.Connection();
     conn.Open();
     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"SELECT * FROM clients WHERE id = (@searchId);";
     MySqlParameter searchId = new MySqlParameter();
     searchId.ParameterName = "@searchId";
     searchId.Value = id;
     cmd.Parameters.Add(searchId);
     var rdr = cmd.ExecuteReader() as MySqlDataReader;
     int clientId = 0;
     string clientName = "";
     int stylistId = 0;
     while(rdr.Read())
     {
       clientId = rdr.GetInt32(0);
       clientName = rdr.GetString(1);
       stylistId = rdr.GetInt32(2);
     }
     Client newClient = new Client(clientName, stylistId, clientId);
     conn.Close();
     if (conn != null)
     {
       conn.Dispose();
     }
     return newClient;
   }

   public override bool Equals(System.Object otherClient)
   {
     if (!(otherClient is Client))
     {
       return false;
     }
     else
     {
       Client newClient = (Client) otherClient;
       bool idEquality = this.GetId() == newClient.GetId();
       bool nameEquality = this.GetClientName() == newClient.GetClientName();
       bool stylistIdEquality = this.GetStylistId() == newClient.GetStylistId();
       return (idEquality && nameEquality && stylistIdEquality);
     }
   }

   public void Save()
   {
     MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"INSERT INTO clients (name, stylistId) VALUES (@name,  @stylistId);";
     MySqlParameter name = new MySqlParameter();
     name.ParameterName = "@name";
     name.Value = this._clientName;
     cmd.Parameters.Add(name);
     MySqlParameter stylistId = new MySqlParameter();
     stylistId.ParameterName = "@stylistId";
     stylistId.Value = this._stylistId;
     cmd.Parameters.Add(stylistId);
     cmd.ExecuteNonQuery();
     _id = (int) cmd.LastInsertedId;
     conn.Close();
     if (conn != null)
     {
       conn.Dispose();
     }
   }

   public void Edit(string newName)
   {
     MySqlConnection conn = DB.Connection();
     conn.Open();
     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"UPDATE clients SET name = @newName WHERE id = @searchId;";
     MySqlParameter searchId = new MySqlParameter();
     searchId.ParameterName = "@searchId";
     searchId.Value = _id;
     cmd.Parameters.Add(searchId);
     MySqlParameter name = new MySqlParameter();
     name.ParameterName = "@newName";
     name.Value = _clientName;
     cmd.Parameters.Add(name);
     cmd.ExecuteNonQuery();
     _clientName = newName;
     conn.Close();
     if (conn != null)
     {
       conn.Dispose();
     }
   }

 }
}
