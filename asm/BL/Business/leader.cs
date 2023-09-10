using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BL.Services;



namespace BL.Business
{

    public class leader
    {
        public string username { get; set; }
        MySqlConnection connection = DbContext.GetConnection();
        //AddUserToDatabase()
       
        //see member and employee
        public void seeMember(string username)
        {
            try
            {
                connection.Open();

                string query = "SELECT * FROM users";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
        public void menuLeaderBl(string username)
        {

            bool quit = true;

            do
            {
                Console.WriteLine("╔═══════════════════════════════════════════╗");
                Console.WriteLine("║              FUNCTION                     ║");
                Console.WriteLine("╠════════════════════╦══════════════════════╣");
                Console.WriteLine("║ 1. Add users       ║ 2.View users         ║");
                Console.WriteLine("║ 3. Modify users    ║ 4.Remove users       ║");
                Console.WriteLine("║ 5. Search username ║ 6.Quit               ║");
                Console.WriteLine("╚════════════════════╩══════════════════════╝");
                Console.Write("Your choice :");
                int choose = int.Parse(Console.ReadLine());
                switch (choose)
                {
                    case 1:
                        Console.WriteLine("╔═══════════════════════════════════════════╗");
                        Console.WriteLine("Number of users want to add:");
                        int number = int.Parse(Console.ReadLine());
                        for (int i = 0; i < number; i++)
                        {
                            addUsers add = new addUsers();
                            add.AddUserToDatabase();
                        }
                        Console.WriteLine("╚═══════════════════════════════════════════╝");
                        break;
                    case 2:
                        Console.WriteLine("╔═══════════════════════════════════════════╗");
                        ViewMemInfo viewMemInfo = new ViewMemInfo();
                        viewMemInfo.ViewEmployeesWithDepartments();
                        Console.WriteLine("╚═══════════════════════════════════════════╝");
                        break;
                    case 3:
                        Console.WriteLine("╔═══════════════════════════════════════════╗");
                        modifyUser modifyUser = new modifyUser();
                        Console.WriteLine("input username you want modify:");
                        string userMod=Console.ReadLine();
                        modifyUser.ModifyUserInformation(userMod);
                        Console.WriteLine("╚═══════════════════════════════════════════╝");

                        break;
                    case 4:
                        removeUsers removeUsers = new removeUsers();
                        Console.WriteLine("input username you want remove:");
                        string userDel = Console.ReadLine();
                        removeUsers.DeleteUser(userDel);
                        Console.WriteLine("╚═══════════════════════════════════════════╝");
                        break;
                    case 5:
                        view1mem view1Mem = new view1mem();
                        Console.WriteLine("input username you want view:");
                        string userview = Console.ReadLine();
                        view1Mem.ViewUserInfoByUsername(userview);
                        Console.WriteLine("╚═══════════════════════════════════════════╝");
                        break;
                    case 6:
                        quit = false;
                        break;
                    default:
                        quit = false;
                        break;
                }
            } while (quit);
        }
    }
}
