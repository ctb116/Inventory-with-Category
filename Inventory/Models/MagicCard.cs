using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Inventory;
using System;

namespace Inventory.Models
{
  public class MagicCard
  {
    private int _id;
    private string _name;
    private string _color;
    private string _rarity;
    private string _type;
    private string _set;

    public MagicCard(string newName, string newColor, string newRarity, string newType, string newSet, int newId = 0)
    {
      _id = newId;
      _name = newName;
      _color = newColor;
      _rarity = newRarity;
      _type = newType;
      _set = newSet;
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

    public string GetColor()
    {
      return _color;
    }

    public void SetColor(string newColor)
    {
      _color = newColor;
    }

    public string GetRarity()
    {
      return _rarity;
    }

    public void SetRarity(string newRarity)
    {
      _rarity = newRarity;
    }

    public string GetCardType()
    {
      return _type;
    }

    public void SetType(string newType)
    {
      _type = newType;
    }

    public string GetSet()
    {
      return _set;
    }

    public void SetSet(string newSet)
    {
      _set = newSet;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO magic_card (Name, Color, Rarity, Type, CardSet) VALUES (@MagicCardName, @MagicCardColor, @MagicCardRarity, @MagicCardType, @MagicCardSet);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@MagicCardName";
      name.Value = _name;
      cmd.Parameters.Add(name);

      MySqlParameter color = new MySqlParameter();
      color.ParameterName = "@MagicCardColor";
      color.Value = _color;
      cmd.Parameters.Add(color);

      MySqlParameter rarity = new MySqlParameter();
      rarity.ParameterName = "@MagicCardRarity";
      rarity.Value = _rarity;
      cmd.Parameters.Add(rarity);

      MySqlParameter type = new MySqlParameter();
      type.ParameterName = "@MagicCardType";
      type.Value = _type;
      cmd.Parameters.Add(type);

      MySqlParameter cardSet = new MySqlParameter();
      cardSet.ParameterName = "@MagicCardSet";
      cardSet.Value = _set;
      cmd.Parameters.Add(cardSet);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;  // Notice the slight update to this line of code!

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<MagicCard> GetAll()
    {
      List<MagicCard> allMagicCards = new List<MagicCard> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM magic_card;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int magicCardId = rdr.GetInt32(0);
        string magicCardName = rdr.GetString(1);
        string magicCardColor = rdr.GetString(2);
        string magicCardRarity = rdr.GetString(3);
        string magicCardType = rdr.GetString(4);
        string magicCardSet = rdr.GetString(5);
        MagicCard newMagicCard = new MagicCard(magicCardName, magicCardColor, magicCardRarity, magicCardType, magicCardSet, magicCardId);
        allMagicCards.Add(newMagicCard);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allMagicCards;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      // var cmd = conn.CreateCommand() as MySqlCommand;
      // cmd.CommandText = @"DELETE FROM magic_card;";
      //
      // var resetPrimaryKey = conn.CreateCommand() as MySqlCommand;
      // cmd.CommandText = @"TRUNCATE TABLE magic_card;";

      // ** Truncate deletes all data and resets auto-increment to 0. **
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"TRUNCATE TABLE magic_card;";

      cmd.ExecuteNonQuery();
      // resetPrimaryKey.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static MagicCard Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM magic_card WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int MagicCardId = 0;
      string MagicCardName = "";
      string MagicCardColor = "";
      string MagicCardRarity = "";
      string MagicCardType = "";
      string MagicCardSet = "";

      while (rdr.Read())
      {
        MagicCardId = rdr.GetInt32(0);
        MagicCardName = rdr.GetString(1);
        MagicCardColor = rdr.GetString(2);
        MagicCardRarity = rdr.GetString(3);
        MagicCardType = rdr.GetString(4);
        MagicCardSet = rdr.GetString(5);
      }

      MagicCard foundMagicCard = new MagicCard(MagicCardName, MagicCardColor, MagicCardRarity, MagicCardType, MagicCardSet, MagicCardId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      
      return foundMagicCard;
    }

    public override bool Equals(System.Object otherCard)
    {
      if (!(otherCard is MagicCard))
      {
        return false;
      }
      else
      {
        MagicCard newCard = otherCard as MagicCard;
        bool nameEquality = (this.GetName() == newCard.GetName());
        bool colorEquality = (this.GetColor() == newCard.GetColor());
        bool rarityEquality = (this.GetRarity() == newCard.GetRarity());
        bool typeEquality = (this.GetCardType() == newCard.GetCardType());
        bool setEquality = (this.GetSet() == newCard.GetSet());
        bool allEquality;

        if (nameEquality && colorEquality && rarityEquality && typeEquality && setEquality)
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
      string toHash = this.GetName() + this.GetColor() + this.GetRarity() + this.GetCardType() + this.GetSet();
      return toHash.GetHashCode();
    }

  }
}
