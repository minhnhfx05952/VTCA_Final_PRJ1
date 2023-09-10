using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DAL;
using BL.Business;
using MySql.Data.MySqlClient;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace asm
{
    internal class Program
    {
        // Hàm Main
        private static void Main(string[] args)
        {
            leader leaderOBJ = new leader();
            employee employeeOBJ = new employee();
            candidate candidateOBJ = new candidate();
           
            string asciiArt = @"
╔═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗


 _ _                         ___                                     __ __                                             _   
| | | _ _ ._ _ _  ___ ._ _  | . \ ___  ___ ___  _ _  _ _  ___  ___  |  \  \ ___ ._ _  ___  ___  ___ ._ _ _  ___ ._ _ _| |_ 
|   || | || ' ' |<_> || ' | |   // ._><_-</ . \| | || '_>/ | '/ ._> |     |<_> || ' |<_> |/ . |/ ._>| ' ' |/ ._>| ' | | |  
|_|_|`___||_|_|_|<___||_|_| |_\_\\___./__/\___/`___||_|  \_|_.\___. |_|_|_|<___||_|_|<___|\_. |\___.|_|_|_|\___.|_|_| |_|  
                                                                                          <___'                            


╚═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝";
            Console.WriteLine(asciiArt);
            // Tạo kết nối đến database
            MySqlConnection connection = DbContext.GetConnection();
            //tạo đối tượng login
            login login = new login();
            int input = 3;
            input:
            // Nhập thông tin người dùng
            Console.WriteLine("╔═════════");
            Console.Write(" Username: ");
            string username = Console.ReadLine();
            Console.WriteLine("╚═════════");

            Console.WriteLine("╔══════════");
            Console.Write(" Password: ");           
            string password = Console.ReadLine();
            Console.WriteLine("╚══════════");

            // Thực hiện xác minh người dùng
            bool isAuthenticated = login.AuthenticateUser(username, password);

            if (isAuthenticated)
            {
                Console.WriteLine("Success!");
            }
            else
            {
                Console.WriteLine("Wrong username or password, input again");
                if(input<=0)
                {
                    Console.WriteLine("You have input wrong username or password 3 times, please try again later");
                    return;
                }
                else
                {
                    input--;
                    goto input;
                }
            }
            //role user
            #region check role
            string userRole = login.GetUserRole(username);

            if (userRole == "admin")
            {
                Console.WriteLine("==============");
                Console.WriteLine("Welcome Admin");
                Console.WriteLine("==============");
            }
            else if (userRole == "user")
            {
                Console.WriteLine("==============");
                Console.WriteLine("Welcome Staff");
                Console.WriteLine("==============");
            }
            else
            {
                Console.WriteLine("==================");
                Console.WriteLine("Welcome Candidate");
                Console.WriteLine("==================");
            }
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine(asciiArt);
            #endregion
            //id user
            /*#region check id
            string userid = login.GetUserID(username);

            if (userid == "1")
            {
                Console.WriteLine("Chào admin!");
                Console.WriteLine(userid);
            }
            else if (userid == "2")
            {
                Console.WriteLine("Chào người dùng!");
            }
            else
            {
                Console.WriteLine("Vai trò không xác định!");
                
            }
            #endregion*/
            int sw;//phân quyền
            if (userRole == "admin")
            {
                sw = 1;
            }
            else if (userRole == "user")
            {
                sw = 2;
            }
            else
            {
                sw = 3;
            }
            //switch case
            switch (sw)
            {
                case 1:
                    Console.WriteLine("==================");
                    Console.WriteLine("Admin Portal");
                    Console.WriteLine("==================");
                    leaderOBJ.menuLeaderBl(username);
                    break;
                case 2:
                    Console.WriteLine("==================");
                    Console.WriteLine("Employee Portal");
                    Console.WriteLine("==================");
                    employeeOBJ.menuEmployeeBl(username);
                    break;
                case 3:
                    Console.WriteLine("==================");
                    Console.WriteLine("Candidate Portal");
                    Console.WriteLine("==================");
                    candidateOBJ.menuCandidateBl(username);

                    break;
                default:
                    break;
            }


        }
    }
}
