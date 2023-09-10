using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using BL.Services;

namespace BL.Business
{
    public class employee
    {
        public string username { get; set; }
        public void menuEmployeeBl(string username)
        {
            bool quit = true;
            do
            {
                Console.WriteLine("╔══════════════════════════════════════════════════╗");
                Console.WriteLine("║                     FUNCTION                     ║");
                Console.WriteLine("╠════════════════════╦═════════════════════════════╣");
                Console.WriteLine("║ 1. CV manage       ║ 2. View all candidate       ║");
                Console.WriteLine("║ 3. Hiring decisions║ 4. Recruitment plan manage  ║");
                Console.WriteLine("╚════════════════════╩═════════════════════════════╝");
                Console.Write("Your choice :");
                int choose = int.Parse(Console.ReadLine());
                switch (choose)
                {
                    case 1:
                        Console.WriteLine("╔═══════════════════════════════════════════╗");
                        cvManage cvManage = new cvManage();
                        Console.WriteLine("input username you want search CV:");
                        string userCV = Console.ReadLine();
                        cvManage.FindCVByUsername(userCV);
                        //Console.WriteLine( cvManage.CheckIfUsernameExistsInCV(userCV));
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
                        HiringDecisionsService hiringDecisionsService = new HiringDecisionsService();

                        Console.Write("Enter your username: ");
                        string employeeUsername = Console.ReadLine();

                        Console.Write("Enter candidate's username: ");
                        string candidateUsername = Console.ReadLine();

                        
                        Console.Write("Enter decision description: ");
                        string decisionDescription = Console.ReadLine();

                        hiringDecisionsService.AddHiringDecision(employeeUsername, candidateUsername, DateTime.Now, decisionDescription);
                        Console.WriteLine("Hiring decision added successfully.");
                        
                        Console.WriteLine("╚═══════════════════════════════════════════╝");
                        break;
                    case 4:
                        Console.WriteLine("╔═══════════════════════════════════════════╗");
                        RecruitmentPlanService recruitmentPlanService = new RecruitmentPlanService();

                        Console.Write("Enter department ID: ");
                        if (int.TryParse(Console.ReadLine(), out int departmentId))
                        {
                            Console.Write("Enter job ID: ");
                            if (int.TryParse(Console.ReadLine(), out int jobId))
                            {
                                Console.Write("Enter plan date (yyyy-MM-dd): ");
                                if (DateTime.TryParse(Console.ReadLine(), out DateTime planDate))
                                {
                                    Console.Write("Enter plan description: ");
                                    string planDescription = Console.ReadLine();

                                    recruitmentPlanService.AddRecruitmentPlan(departmentId, jobId, planDate, planDescription);
                                    Console.WriteLine("Recruitment plan added successfully.");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid date format. Please enter the date in yyyy-MM-dd format.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid job ID. Please enter a valid number.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid department ID. Please enter a valid number.");
                        }

                        Console.WriteLine("╚═══════════════════════════════════════════╝");
                        break;
                    default:
                        quit = false;
                        break;
                }
            } while (quit);
        }
    }
}
