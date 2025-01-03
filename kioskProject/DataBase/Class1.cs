using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace DataBase
{
    public class DbProject
    {
        public int asdasd = 0;



        public DbProject()
        {
          
        }

        public void SendOrder(OrderInfo orderInfo)
        {
            List<string> list = new List<string>();

            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

            try
            {
                MySqlConnection mysql = new MySqlConnection("Server=localhost;Database=sys;Uid=root;Pwd=a1357900");

                mysql.Open();

               
                string insertQuery = "INSERT INTO 주문목록 (name, Quant, Price, TotalPrice) VALUES (@name, @Quant, @Price, @TotalPrice)";

                MySqlCommand command = new MySqlCommand(insertQuery, mysql);

                command.Parameters.AddWithValue("@name", orderInfo.Name);
                command.Parameters.AddWithValue("@Quant", orderInfo.Quantity);
                command.Parameters.AddWithValue("@Price", orderInfo.Price);
                command.Parameters.AddWithValue("@TotalPrice", orderInfo.TotalPrice);

                command.ExecuteNonQuery();
               
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }

        public bool Login(string id, string password)
        {
           
            MySqlConnection mysql = new MySqlConnection("Server=localhost;Database=sys;Uid=root;Pwd=a1357900");

            mysql.Open();


            string selectQuery = "SELECT COUNT(*) FROM 회원정보 WHERE loginId = @id AND loginPassword = @password";

            MySqlCommand Selectcommand = new MySqlCommand(selectQuery, mysql);

            Selectcommand.Parameters.AddWithValue("@id", id);
            Selectcommand.Parameters.AddWithValue("@password", password);

            int userCount = Convert.ToInt32(Selectcommand.ExecuteScalar());


            if (userCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool register(string id, string password)
        {
            try
            {
                MySqlConnection mysql = new MySqlConnection("Server=localhost;Database=sys;Uid=root;Pwd=a1357900");

                mysql.Open();

                string selectQuery = "SELECT COUNT(*) FROM 회원정보 WHERE loginId = @id AND loginPassword = @password";

                MySqlCommand Selectcommand = new MySqlCommand(selectQuery, mysql);

                Selectcommand.Parameters.AddWithValue("@id", id);
                Selectcommand.Parameters.AddWithValue("@password", password);

                int userCount = Convert.ToInt32(Selectcommand.ExecuteScalar());

                if (userCount > 0)
                {
                   
                    return false;
                }
                else
                {
                    string newid = id;
                    string newpassword = password;

                    string insertQuery = "INSERT INTO 회원정보 (loginId, loginPassword) VALUES (@newid, @newpassword)";
                   
                    MySqlCommand command = new MySqlCommand(insertQuery, mysql);

                    command.Parameters.AddWithValue("@newid", newid);
                    command.Parameters.AddWithValue("@newpassword", newpassword);

                    command.ExecuteNonQuery();                  
                    return true;
                }

            }

            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }

            return false;
        }
        public void GiveFoodInfo(List<MenuInfo> menuItems)
        {
                 
            MySqlConnection mysql = new MySqlConnection("Server=localhost;Database=sys;Uid=root;Pwd=a1357900");

            mysql.Open();


            string selectQuery = "SELECT * FROM 음식정보";



            MySqlCommand Selectcommand = new MySqlCommand(selectQuery, mysql);


            MySqlDataReader reader = Selectcommand.ExecuteReader();

            while (reader.Read())
            {

               
                string name = reader["Name"].ToString();
                string page = reader["Page"].ToString();
                string price = reader["Price"].ToString();
                string image = reader["Image"].ToString();




                Dictionary<string, string> menu = new Dictionary<string, string>
                {
                    {"page",page},
                    {"Price",price},
                    {"Image",image},
                };


          

                List<string> lists = new List<string> {name,price,image,page };

                menuItems.Add(new MenuInfo { Name = name, Price = int.Parse(price), Image = image, Page = page });

            }         
        }

        public void ItemDelete(string name)
        {
            try
            {
                MySqlConnection mysql = new MySqlConnection("Server=localhost;Database=sys;Uid=root;Pwd=a1357900");

                mysql.Open();

                string selectQuery = "DELETE  FROM 음식정보 WHERE Name = @name";


                MySqlCommand command = new MySqlCommand(selectQuery, mysql);

                command.Parameters.AddWithValue("@name", name);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ItemUpdate(string originalName, string name, string price, string image)
        {
            try
            {
               

                MySqlConnection mysql = new MySqlConnection("Server=localhost;Database=sys;Uid=root;Pwd=a1357900");
                mysql.Open();

                string updateQuery = "UPDATE 음식정보 SET Name = @name, Price = @price, Image = @image WHERE Name = @originalName";

                MySqlCommand command = new MySqlCommand(updateQuery, mysql);

                string base64Image = image;


                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@image", image);
                command.Parameters.AddWithValue("@originalName", originalName);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void SendDB(string name,string price,string image,string page)
        {
           
            try
            {
                MySqlConnection mysql = new MySqlConnection("Server=localhost;Database=sys;Uid=root;Pwd=a1357900");

                mysql.Open();

                string insertQuery = "INSERT INTO 음식정보 (Name, Price, Image, Page) VALUES (@name, @price, @image, @page)";

                MySqlCommand command = new MySqlCommand(insertQuery, mysql);

                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@image", image);
                command.Parameters.AddWithValue("@page", page);

                command.ExecuteNonQuery();
               
            }
            catch (Exception exc)
            {
                Console.WriteLine(asdasd);
            }
        }

    }

    public class MenuInfo
    {    
        public string Name { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public string Page { get; set; }
    }

    public class OrderInfo
    {
        public string SeverID { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Quantity { get; set; }
        public string TotalPrice { get; set; }
    }
}
