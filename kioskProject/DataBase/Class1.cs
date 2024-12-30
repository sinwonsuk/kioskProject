using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        public void test()
        {
            try
            {
                // MySQL 연결 명령어
                MySqlConnection connection = new MySqlConnection("Server=localhost;Database=sys;Uid=root;Pwd=a1357900");
                // MySQL 서버 연결 유지
                connection.Open();

                // MySQL로 보낼 문자열 Query 변수 선언
                string Query = "SELECT * FROM sys.test WHERE name = 'kabul';";
                // MySqlCommand 클래스를 사용해 쿼리문을 MySQL로 전송
                MySqlCommand command = new MySqlCommand(Query, connection);

                // MySqlDataReader 클래스와 ExecuteReader() 함수를 이용해,
                // 받아온 정보를 reader에 저장
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // 받아온 reader의 정보 중, name 열만 출력
                    Console.WriteLine((string)reader["name"]);
                }
                // MySQL 서버 연결 종료
                connection.Close();
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message.ToString()); }
        }


        public void tttt(Dictionary<string, Dictionary<string, string>> info)
        {
            List<string> list = new List<string>(); 

            Dictionary<string,string> keyValuePairs = new Dictionary<string,string>();

            try
            {
                MySqlConnection mysql = new MySqlConnection("Server=localhost;Database=sys;Uid=root;Pwd=a1357900");
                
                    mysql.Open();


                //for (int i = 0; i < info.Count; i++)
                //{
                //    for (int j = 0; j < info[i].Count; j++)
                //    {
                //        list.Add(info[i][j]);
                //    }

                //    string insertQuery = string.Format("INSERT INTO 목록 (name, Price,Quant,TotalPrice) VALUES ('{0}', '{1}', '{2}' ,'{3}');", list[0], list[1], list[2], list[3]);
                //    MySqlCommand command = new MySqlCommand(insertQuery, mysql);
                //    command.ExecuteNonQuery();

                //    list.Clear();
                //}






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

    }
}
