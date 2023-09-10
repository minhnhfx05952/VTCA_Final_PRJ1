using MySql.Data.MySqlClient;
using System;
using DAL;

namespace BL.Services
{
    internal class HiringDecisionsService
    {
        public void AddHiringDecision(string employeeUsername, string candidateUsername, DateTime decisionDate, string decisionDescription)
        {
            MySqlConnection connection = DbContext.GetConnection();

            try
            {
                connection.Open();

                // Trước tiên, tìm cv_id tương ứng với candidateUsername
                string cvIdQuery = "SELECT cv_id FROM CV WHERE username = @candidateUsername";
                MySqlCommand cvIdCommand = new MySqlCommand(cvIdQuery, connection);
                cvIdCommand.Parameters.AddWithValue("@candidateUsername", candidateUsername);

                int cvId = Convert.ToInt32(cvIdCommand.ExecuteScalar());

                // Thêm dữ liệu vào bảng Hiring_Decisions
                string query = "INSERT INTO Hiring_Decisions (employee_username, cv_id, decision_date, decision_description) " +
                               "VALUES (@employeeUsername, @cvId, @decisionDate, @decisionDescription)";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@employeeUsername", employeeUsername);
                command.Parameters.AddWithValue("@cvId", cvId);
                command.Parameters.AddWithValue("@decisionDate", decisionDate);
                command.Parameters.AddWithValue("@decisionDescription", decisionDescription);

                command.ExecuteNonQuery();
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
    }
}
