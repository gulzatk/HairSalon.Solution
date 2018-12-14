using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
    public class Specialty
    {
        private int _id;
        private string _name;

        public Specialty (string name, int id=0)
        {
            _name = name;
            _id = id;
        }

        public string GetName()
        {
          return _name;
        }
        public int GetId()
        {
          return _id;
        }

        public void SetId(int newId)
        {
          _id = newId;
        }

        public void Save()
        {
           MySqlConnection conn = DB.Connection();
           conn.Open();
           var cmd = conn.CreateCommand() as MySqlCommand;
           cmd.CommandText = @"INSERT INTO specialties (name) VALUES (@name);";
           cmd.Parameters.AddWithValue("@name", this._name);
           cmd.ExecuteNonQuery();
           _id = (int) cmd.LastInsertedId;
           conn.Close();
           if (conn != null)
           {
               conn.Dispose();
           }
         }

         public static List<Specialty> GetAll()
         {
             List<Specialty> allSpecialties = new List<Specialty>{};
             MySqlConnection conn = DB.Connection();
             conn.Open();
             var cmd = conn.CreateCommand() as MySqlCommand;
             cmd.CommandText = @"SELECT * FROM specialties;";
             MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
             while(rdr.Read())
             {
                 int specialtyId = rdr.GetInt32(0);
                 string specialtyName = rdr.GetString(1);
                 Specialty newSpecialty = new Specialty(specialtyName);
                 newSpecialty.SetId(specialtyId);
                 allSpecialties.Add(newSpecialty);
             }
             conn.Close();
             if (conn != null)
             {
                 conn.Dispose();
             }
             return allSpecialties;
         }


          public List<Stylist> GetStylists()
          {
              MySqlConnection conn = DB.Connection();
              conn.Open();
              MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
              cmd.CommandText = @"SELECT stylists.* FROM specialties
                  JOIN stylist-specialty ON (specialties.id = stylist-specialty.specialty_id)
                  JOIN stylists ON (stylist-specialty.stylist_id = stylists.id)
                  WHERE specialties.id = @SpecialtyId;";

              cmd.Parameters.AddWithValue("@SpecialtyId", this._id);
              MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
              List<Stylist> stylists = new List<Stylist>{};
              while(rdr.Read())
              {
              int stylistId = rdr.GetInt32(0);

              Stylist newStylist = Stylist.Find(stylistId);
              stylists.Add(newStylist);
              }
              conn.Close();
              if (conn != null)
              {
              conn.Dispose();
              }
              return stylists;
          }

          public static Specialty Find(int id)
          {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialties WHERE id = (@searchId);";
            cmd.Parameters.AddWithValue("@searchId", id);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int specialtyId = 0;
            string specialtyName = "";
            while(rdr.Read())
            {
             specialtyId = rdr.GetInt32(0);
             specialtyName = rdr.GetString(1);
            }
            Specialty newSpecialty = new Specialty(specialtyName, specialtyId);
            conn.Close();
            if (conn != null)
            {
             conn.Dispose();
            }
            return newSpecialty;
          }

          public static void ClearAll()
          {
              MySqlConnection conn = DB.Connection();
              conn.Open();
              var cmd = conn.CreateCommand() as MySqlCommand;
              cmd.CommandText = @"DELETE FROM specialties;";
              cmd.ExecuteNonQuery();
              conn.Close();
              if(conn != null)
              {
                  conn.Dispose();
              }
            }

            public override bool Equals(System.Object otherSpecialty)
            {
                if (!(otherSpecialty is Specialty))
                {
                    return false;
                }
                else
                {
                    Specialty newSpecialty = (Specialty) otherSpecialty;
                    bool areIdsEqual = (this.GetId() == newSpecialty.GetId());
                    bool areNameEqual = (this.GetName() == newSpecialty.GetName());
                    return (areIdsEqual && areNameEqual);
                }
            }
            public override int GetHashCode()
            {
                return this.GetName().GetHashCode();
            }
          }
        }
