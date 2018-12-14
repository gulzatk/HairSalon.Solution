using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
    public class Stylist
    {
      private string _name;
      private string _description;
      private int _id;

      public Stylist(string name, string description, int id = 0)
      {
        _name = name;
        _description = description;
        _id = id;
      }

      public string GetName()
      {
        return _name;
      }

      public string GetDescription()
      {
        return _description;
      }

      public int GetId()
      {
        return _id;
      }

      public static List<Stylist> GetAll()
      {
        List<Stylist> allStylists = new List<Stylist> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM stylists;";
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
          int stylistId = rdr.GetInt32(0);
          string stylistName = rdr.GetString(1);
          string stylistDescription = rdr.GetString(2);
          Stylist newStylist = new Stylist( stylistName, stylistDescription, stylistId);
          allStylists.Add(newStylist);
        }
          conn.Close();
         if (conn != null)
         {
           conn.Dispose();
         }
         return allStylists;
        }


        public static void ClearAll()
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"DELETE FROM stylists;";
          cmd.ExecuteNonQuery();
          conn.Close();
          if (conn != null)
          {
            conn.Dispose();
          }
        }
        public static Stylist Find(int id)
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT * FROM stylists WHERE id = (@searchId);";
          MySqlParameter searchId = new MySqlParameter();
          searchId.ParameterName = "@searchId";
          searchId.Value = id;
          cmd.Parameters.Add(searchId);
          var rdr = cmd.ExecuteReader() as MySqlDataReader;
          int stylistId = 0;
          string stylistName = "";
          string stylistDescription = "";
          while(rdr.Read())
          {
           stylistId = rdr.GetInt32(0);
           stylistName = rdr.GetString(1);
           stylistDescription = rdr.GetString(2);
          }
          Stylist newStylist = new Stylist(stylistName, stylistDescription, stylistId);
          conn.Close();
          if (conn != null)
          {
           conn.Dispose();
          }
          return newStylist;
        }

        public List<Client> GetClients()
        {
          List<Client> allClients = new List<Client> {};
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT * FROM clients WHERE stylistId = @stylistId;";
          MySqlParameter stylistId = new MySqlParameter();
          stylistId.ParameterName = "@stylistId";
          stylistId.Value = this._id;
          cmd.Parameters.Add(stylistId);
          var rdr = cmd.ExecuteReader() as MySqlDataReader;
          while(rdr.Read())
          {
            int clientId = rdr.GetInt32(0);
            string clientName = rdr.GetString(1);
            int clientStylistId = rdr.GetInt32(2);
            Client newClient = new Client(clientName, clientStylistId, clientId);
            allClients.Add(newClient);
          }
            conn.Close();
            if (conn != null)
            {
              conn.Dispose();
            }
            return allClients;
          }

  public override bool Equals(System.Object otherStylist)
  {
    if (!(otherStylist is Stylist))
    {
      return false;
    }
    else
    {
      Stylist newStylist = (Stylist) otherStylist;
      bool idEquality = this.GetId().Equals(newStylist.GetId());
      bool nameEquality = this.GetName().Equals(newStylist.GetName());
      bool descriptionEquality = this.GetDescription().Equals(newStylist.GetDescription());
      return (idEquality && nameEquality && descriptionEquality);
    }
  }

  public void Save()
   {
     MySqlConnection conn = DB.Connection();
     conn.Open();
     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"INSERT INTO stylists (name, description) VALUES (@name, @description);";
     MySqlParameter name = new MySqlParameter();
     name.ParameterName = "@name";
     name.Value = this._name;
     cmd.Parameters.Add(name);
     MySqlParameter description = new MySqlParameter();
     description.ParameterName = "@description";
     description.Value = this._description;
     cmd.Parameters.Add(description);
     cmd.ExecuteNonQuery();
     _id = (int) cmd.LastInsertedId;
     conn.Close();
     if (conn != null)
     {
       conn.Dispose();
     }
   }

        public static void DeleteStylist(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists WHERE id = (@thisId);";

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

    public void Edit(string newStylistName, string newdescription)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE stylists SET name = @newStylistName, description = @newdescription WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter stylistName = new MySqlParameter();
      stylistName.ParameterName = "@newStylistName";
      stylistName.Value = newStylistName;
      cmd.Parameters.Add(stylistName);

      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@newdescription";
      description.Value = newdescription;
      cmd.Parameters.Add(description);


      cmd.ExecuteNonQuery();
      _name = newStylistName;
      _description = newdescription;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

  }
}
