using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Inventory;
using System;

namespace Inventory.Models
{
  public class Pony
  {
    private int _id;
    private string _name;
    private string _type;
    private string _cutieMark;
    private string _productType;

    public Pony(int newId, string newName, string newType, string newCutieMark, string newProductType)
    {
      _id = newId;
      _name = newName;
      _type = newType;
      _cutieMark = newCutieMark;
      _productType = newProductType;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public void SetName(string newName)
    {
      _name = newName;
    }

    public string GetPonyType()
    {
      return _type;
    }

    public void SetType(string newType)
    {
      _type = newType;
    }

    public string GetCutieMark()
    {
      return _cutieMark;
    }

    public void SetCutieMark(string newCutieMark)
    {
      _cutieMark = newCutieMark;
    }

    public string GetProductType()
    {
      return _productType;
    }

    public void SetProductType(string newProductType)
    {
      _productType = newProductType;
    }

    public static List<Pony> GetAll()
    {
      List<Pony> allPonys = new List<Pony> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM pony;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int ponyId = rdr.GetInt32(0);
        string ponyName = rdr.GetString(1);
        string ponyType = rdr.GetString(2);
        string ponyCutieMark = rdr.GetString(3);
        string ponyProductType = rdr.GetString(4);
        Pony newPony = new Pony(ponyId, ponyName, ponyType, ponyCutieMark, ponyProductType);
        allPonys.Add(newPony);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allPonys;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM pony;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }


  }
}
