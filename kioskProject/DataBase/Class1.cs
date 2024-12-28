using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class DbProject
    {
        public int asdasd = 0;


        string _server = "localhost"; //DB 서버 주소, 로컬일 경우 localhost
        int _port = 3306; //DB 서버 포트
        string _database = "new_schema"; //DB 이름
        string _id = "root"; //계정 아이디
        string _pw = "root"; //계정 비밀번호
        string _connectionAddress = "";
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
                MySqlConnection connection = new MySqlConnection("Server=localhost;Database=mysql;Uid=root;Pwd=a1357900");
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


        public void tttt()
        {
            try
            {
                using (MySqlConnection mysql = new MySqlConnection("Server=localhost;Database=sys;Uid=root;Pwd=a1357900"))
                {
                    mysql.Open();
                    string insertQuery = string.Format("INSERT INTO test (name, phone) VALUES ('{0}', '{1}');", "1111", "1231225");
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
