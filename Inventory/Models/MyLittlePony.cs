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

    public Pony(string newName, string newType, string newCutieMark, string newProductType, int newId = 0)
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

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO pony (Name, Type, CutieMark, ProductType) VALUES (@PonyName, @PonyType, @PonyCutieMark, @PonyProductType);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@PonyName";
      name.Value = _name;
      cmd.Parameters.Add(name);

      MySqlParameter type = new MySqlParameter();
      type.ParameterName = "@PonyType";
      type.Value = _type;
      cmd.Parameters.Add(type);

      MySqlParameter cutieMark = new MySqlParameter();
      cutieMark.ParameterName = "@PonyCutieMark";
      cutieMark.Value = _cutieMark;
      cmd.Parameters.Add(cutieMark);

      MySqlParameter productType = new MySqlParameter();
      productType.ParameterName = "@PonyProductType";
      productType.Value = _productType;
      cmd.Parameters.Add(productType);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;  // Notice the slight update to this line of code!

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
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
        Pony newPony = new Pony(ponyName, ponyType, ponyCutieMark, ponyProductType, ponyId);
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
      cmd.CommandText = @"TRUNCATE TABLE pony;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public override bool Equals(System.Object otherPony)
    {
      if (!(otherPony is Pony))
      {
        return false;
      }
      else
      {
        Pony newPony = otherPony as Pony;
        bool nameEquality = (this.GetName() == newPony.GetName());
        bool typeEquality = (this.GetPonyType() == newPony.GetPonyType());
        bool cutieMarkEquality = (this.GetCutieMark() == newPony.GetCutieMark());
        bool productTypeEquality = (this.GetProductType() == newPony.GetProductType());
        bool allEquality;

        if (nameEquality && typeEquality && cutieMarkEquality && productTypeEquality)
        {
          allEquality = true;
        }
        else
        {
          allEquality = false;
        }

        return (allEquality);
      }
    }

    public override int GetHashCode()
    {
      string toHash = this.GetName() + this.GetPonyType() + this.GetCutieMark() + this.GetProductType();
      return toHash.GetHashCode();
    }


  }
}
