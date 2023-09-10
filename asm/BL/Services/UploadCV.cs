using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BL.Services
{
    internal class UploadCV
    {
        MySqlConnection connection = DbContext.GetConnection();

        public void UpCV(string username, string cvName, string cvFilePath)
        {
            try
            {
                connection.Open();

                // Kiểm tra xem có CV nào cho người dùng đã cho chưa
                if (IsCVExists(username))
                {
                    Console.WriteLine("CV for this user already exists. Updating the CV path.");
                    UpdateCV(username, cvName, cvFilePath);
                }
                else
                {
                    // Chưa có CV cho người dùng, thêm một bản ghi mới
                    InsertNewCV(username, cvName, cvFilePath);
                }

                Console.WriteLine("CV uploaded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private bool IsCVExists(string username)
        {
            // Kiểm tra xem có bản ghi CV nào cho người dùng đã cho chưa
            string query = "SELECT COUNT(*) FROM CV WHERE username = @username";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);

            int count = Convert.ToInt32(command.ExecuteScalar());

            return count > 0;
        }

        private void InsertNewCV(string username, string cvName, string cvFilePath)
        {
            // Thêm một bản ghi mới cho CV của người dùng
            string insertQuery = "INSERT INTO CV (username, cv_name, cv_file_path) VALUES (@username, @cvName, @cvFilePath)";
            MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
            insertCommand.Parameters.AddWithValue("@username", username);
            insertCommand.Parameters.AddWithValue("@cvName", cvName);
            insertCommand.Parameters.AddWithValue("@cvFilePath", cvFilePath);

            insertCommand.ExecuteNonQuery();
        }

        private void UpdateCV(string username, string cvName, string cvFilePath)
        {
            // Cập nhật đường dẫn tệp CV cho người dùng
            string updateQuery = "UPDATE CV SET cv_name = @cvName, cv_file_path = @cvFilePath WHERE username = @username";
            MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
            updateCommand.Parameters.AddWithValue("@username", username);
            updateCommand.Parameters.AddWithValue("@cvName", cvName);
            updateCommand.Parameters.AddWithValue("@cvFilePath", cvFilePath);

            updateCommand.ExecuteNonQuery();
        }
    }
}
