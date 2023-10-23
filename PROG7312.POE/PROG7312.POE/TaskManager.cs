using System;
namespace PROG7312.POE
{
	public class TaskManager
	{
		public static void ShowMenu()
		{
            bool gameplay = true;
            Console.WriteLine("\n------------------------------------------------------------------------------------------------------------------------------\n\nWELCOME to DEWEYDISCOVER!");
            Console.WriteLine("DeweyDiscover enables users to engage in tasks that help them learn about the Dewey Decimal System in libraries!!\n");

            while (gameplay)
            {
                Console.WriteLine($"Points: {UserPointsManager.Instance.UserPoints}\n");
                Console.WriteLine("Please choose a task to perform by entering the corresponding number to the task:\n");
                Console.WriteLine("\n1. Replacing books");
                Console.WriteLine("\n2. Identifying areas");
                Console.WriteLine("\n3. Finding call numbers (Disabled)");
                Console.WriteLine("\n4. End game. (ALL POINTS WILL BE LOST)\n");

                // Get user input for menu selection
                string userInput = Console.ReadLine();

                // Activity selected based on input
                switch (userInput)
                {
                    case "1":
                        {
                            Console.WriteLine("\nYou have chosen 'Replacing books'.");
                            ReplacingBooksTask.ReplaceBooks();
                            break;
                        }
                    case "2":
                        {
                            Console.WriteLine("\nYou have chosen 'Identifying areas'.\n");
                            IdentifyingAreasTask.IdentifyingAreas();
                            break;
                        }
                    case "3":
                        Console.WriteLine("\nThis feature is currently disabled.\n");
                        break;
                    case "4":
                        Console.WriteLine($"\nGAME OVER.\nTOTAL POINTS EARNED: {UserPointsManager.Instance.UserPoints}");
                        gameplay = false;
                        break;
                    default:
                        Console.WriteLine("\nInvalid option. Please choose a valid task.\n");
                        break;
                }
            }
        }
	}
}

