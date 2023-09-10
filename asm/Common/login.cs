using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using MySql.Data.MySqlClient;

namespace Common
{
    public class login
    {
        MySqlConnection connection = DbContext.GetConnection();

        public bool AuthenticateUser(string username, string password)
        {
/*            Console.Write("Nhập tên người dùng: ");
            string username = Console.ReadLine();

            Console.Write("Nhập mật khẩu: ");
            string password = Console.ReadLine();*/
            try
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM users WHERE username = @username AND password = @password";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                int userCount = Convert.ToInt32(cmd.ExecuteScalar());

                return userCount > 0;

            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu cần
                Console.WriteLine("Cannot found users. Error: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
        //lấy role
        public string GetUserRole(string username)
        {
            try
            {
                connection.Open();

                string query = "SELECT role FROM users WHERE username = @username";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@username", username);

                object roleObj = cmd.ExecuteScalar();

                if (roleObj != null)
                {
                    return roleObj.ToString();
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu cần
                Console.WriteLine("Lỗi xác định vai trò người dùng: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            // Trả về một giá trị mặc định nếu không tìm thấy vai trò
            return "unknown";
        }
        // Lấy id của người dùng
        public string GetUserID(string username)
        {
            try
            {
                connection.Open();

                string query = "SELECT id FROM users WHERE username = @username";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@username", username);

                object idObj = cmd.ExecuteScalar();

                if (idObj != null)
                {
                    return idObj.ToString();
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu cần
                Console.WriteLine("Lỗi xác định vai trò người dùng: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            // Trả về một giá trị mặc định nếu không tìm thấy id
            return "unknown";
        }



    }
}
