/****************************************************************
*   作者：李正浩
*   联系方式: QQ 1263212577
*   CLR版本：4.0.30319.42000
*   创建时间： 2018/9/23 9:25:43
*   描述说明：
*
*****************************************************************/
using System;
using MySql.Data.MySqlClient;


namespace Game
{
    public class AccountDAO
    {
        public Account VerifyUser(MySqlConnection conn, string username, string password)
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from account where name=@username and password=@password",
                    conn);
                
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Console.WriteLine("查询到有数据");
                    int id = reader.GetInt32("id");
                    string name = reader.GetString("name");
                    string pwd = reader.GetString("password");
                    int gold = reader.GetInt32("goldcount");
                    Account user = new Account() { id = id, goldcount = gold, password = pwd, username = name };
                    reader?.Close();
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("在VerifyUser的时候出现异常：" + e);
            }
            finally
            {
                DB.CloseConnection(conn);
            }

            return null;
        }

        public bool GetUserByUsername(MySqlConnection conn, string username)
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from account where name = @username", conn);
                cmd.Parameters.AddWithValue("username", username);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("在GetUserByUsername的时候出现异常：" + e);
                return false;
            }
            finally
            {
                if (reader != null) reader.Close();
                DB.CloseConnection(conn);
            }
        }

        public int AddUser(MySqlConnection conn, string username, string password)
        {
            var result = GetUserByUsername(conn, username);
            try
            {

                MySqlCommand cmd = new MySqlCommand("insert into account set name = @username , password = @password",
                    conn);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("在AddUser的时候出现异常：" + e);
                return 0;
            }
            finally
            {
                DB.CloseConnection(conn);
            }

        }
    }
}
