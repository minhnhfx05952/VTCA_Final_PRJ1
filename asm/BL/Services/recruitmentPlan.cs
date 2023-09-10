using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using DAL;

namespace BL.Services
{
    internal class RecruitmentPlanService
    {
        public void AddRecruitmentPlan(int departmentId, int jobId, DateTime planDate, string planDescription)
        {
            MySqlConnection connection = DbContext.GetConnection();

            try
            {
                connection.Open();

                string query = "INSERT INTO Recruitment_Plan (department_id, job_id, plan_date, plan_description) " +
                               "VALUES (@departmentId, @jobId, @planDate, @planDescription)";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@departmentId", departmentId);
                command.Parameters.AddWithValue("@jobId", jobId);
                command.Parameters.AddWithValue("@planDate", planDate);
                command.Parameters.AddWithValue("@planDescription", planDescription);

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
