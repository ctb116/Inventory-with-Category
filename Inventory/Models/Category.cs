using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace Inventory.Models
{
    public class Category
    {
        private string _name;
        private int _id;
        private List<MagicCard> _cards;

        public Category(string name, int id = 0)
        {
            _name = name;
            _id = id;
            _cards = new List<MagicCard>{};
        }
        public override bool Equals(System.Object otherCategory)
        {
            if (!(otherCategory is Category))
            {
                return false;
            }
            else
            {
                Category newCategory = (Category) otherCategory;
                return this.GetId().Equals(newCategory.GetId());
            }
        }
        public override int GetHashCode()
        {
            return this.GetId().GetHashCode();
        }
        public string GetName()
        {
            return _name;
        }
        public int GetId()
        {
          return _id;
        }

        public void AddCard(MagicCard cardList)
        {
          _cards.Add(cardList);
        }
        public void Save()
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();

          var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO category (name) VALUES (@name);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this._name;
            cmd.Parameters.Add(name);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

        }
        public static List<Category> GetAll()
        {
            List<Category> allCategories = new List<Category> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM category;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int CategoryId = rdr.GetInt32(0);
              string CategoryName = rdr.GetString(1);
              Category newCategory = new Category(CategoryName, CategoryId);
              allCategories.Add(newCategory);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allCategories;
        }
        public static Category Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM category WHERE id = (@searchId);";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int CategoryId = 0;
            string CategoryName = "";

            while(rdr.Read())
            {
              CategoryId = rdr.GetInt32(0);
              CategoryName = rdr.GetString(1);
            }
            Category newCategory = new Category(CategoryName, CategoryId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newCategory;
        }
        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"TRUNCATE TABLE category;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
              conn.Dispose();
            }
          }

          public List<MagicCard> GetMagicCards()
          {
            List<MagicCard> allCategoryMagicCards = new List<MagicCard> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM magic_card WHERE category_id = @category_id;";

            MySqlParameter categoryId = new MySqlParameter();
            categoryId.ParameterName = "@category_id";
            categoryId.Value = this._id;
            cmd.Parameters.Add(categoryId);


            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int magicCardId = rdr.GetInt32(0);
              string magicCardName = rdr.GetString(1);
              string magicCardColor = rdr.GetString(2);
              string magicCardRarity = rdr.GetString(3);
              string magicCardType = rdr.GetString(4);
              string magicCardSet = rdr.GetString(5);
              int magicCardCategoryId = rdr.GetInt32(6);
              MagicCard newMagicCard = new MagicCard(magicCardName, magicCardColor, magicCardRarity, magicCardType, magicCardSet, magicCardCategoryId, magicCardId);
              allCategoryMagicCards.Add(newMagicCard);
            }
            conn.Close();
            if (conn != null)
            {
              conn.Dispose();
            }
            return allCategoryMagicCards;
          }
        }
      }
