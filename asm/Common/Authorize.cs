using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Common
{
    internal class Authorize
    {
        MySqlConnection connection = DbContext.GetConnection();
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

                string query = "SELECT id FROM users WHERE username = @username"; // Thay đổi thành tên cột phù hợp
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
                Console.WriteLine("Lỗi xác định ID người dùng: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            // Trả về một giá trị mặc định nếu không tìm thấy ID
            return "unknown";
        }

    }
}
