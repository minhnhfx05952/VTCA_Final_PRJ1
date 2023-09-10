using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Services;
namespace BL.Business
{
    public class candidate
    {
        public string username { get; set; }

        public void menuCandidateBl(string username)
        {
            bool quit = true;
            do
            {
                Console.WriteLine("╔══════════════════════════════════════════════════╗");
                Console.WriteLine("║                     FUNCTION                     ║");
                Console.WriteLine("╠════════════════════╦═════════════════════════════╣");
                Console.WriteLine("║ 1. Upload CV       ║ 2.Update CV                 ║");
                Console.WriteLine("║ 3. View CV         ║ 4.Delete CV                 ║");
                Console.WriteLine("║ 5. Quit Job        ║ 6.Quit                      ║");
                Console.WriteLine("╚════════════════════╩═════════════════════════════╝");
                Console.Write("Your choice :");
                int choose = int.Parse(Console.ReadLine());
                switch (choose)
                {
                    case 1:
                        UploadCV uploadCV = new UploadCV();
                        Console.WriteLine("Enter CV Name: ");
                        string cvName = Console.ReadLine();
                        Console.WriteLine("Enter CV File Path: ");
                        string cvFilePath = Console.ReadLine();
                        uploadCV.UpCV(username, cvName, cvFilePath);
                        break;
                    case 2:
                        modifyCV modifyCV = new modifyCV();
                        modifyCV.ModifyCVByUsername(username);
                        break;
                    case 3:
                        viewCV1 viewCV1 = new viewCV1();
                        viewCV1.ViewCVByUsername(username);
                        break;
                    case 4:
                        removeCVcs removeCVcs = new removeCVcs();
                        removeCVcs.DeleteCVByUsername(username);
                        break;
                    case 5:

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
