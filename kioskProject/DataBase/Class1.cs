using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace DataBase
{
    public class DbProject
    {
        public int asdasd = 0;



        public DbProject()
        {
            //MySQL 연결을 위한 주소 형식
            //_connectionAddress = string.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4}", _server, _port, _database, _id, _pw);
        }

        public void SendOrder(Dictionary<string, Dictionary<string, string>> info)
        {
            List<string> list = new List<string>();

            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

            try
            {
                MySqlConnection mysql = new MySqlConnection("Server=localhost;Database=sys;Uid=root;Pwd=a1357900");

                mysql.Open();

                foreach (var name in info)
                {
                    keyValuePairs.Clear();

                    foreach (var test in name.Value)
                    {
                        keyValuePairs.Add(test.Key, test.Value);
                    }
                    string insertQuery = string.Format("INSERT INTO 목록 (name, Quant,Price,TotalPrice) VALUES ('{0}', '{1}', '{2}' ,'{3}');", name.Key, keyValuePairs["수량"], keyValuePairs["가격"], keyValuePairs["총가격"]);
                    MySqlCommand command = new MySqlCommand(insertQuery, mysql);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(asdasd);
            }
        }

        public bool tttt(string id, string password)
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

        public bool Adadadad(string id, string password)
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
                    MessageBox.Show("등록된 정보 입니다");
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
                    MessageBox.Show("가입되었습니다");
                    return true;
                }

            }

            catch (Exception exc)
            {
                Console.WriteLine(asdasd);
            }

            return false;
        }
        public void adasdadad(List<MenuInfo> menuItems)
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
    }

    public class MenuInfo
    {
       
        public string Name { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public string Page { get; set; }
    }
}
