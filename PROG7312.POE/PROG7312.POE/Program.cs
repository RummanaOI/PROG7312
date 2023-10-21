using System;

namespace PROG7312.POE;

class Program
{
    static int userPoints = 0;

    static void Main(string[] args)
    {


        bool gameplay = true;
        Console.WriteLine("\n------------------------------------------------------------------------------------------------------------------------------\n\nWELCOME to DEWEYDISCOVER!");
        Console.WriteLine("DeweyDiscover enables users to engage in tasks that help them learn about the Dewey Decimal System in libraries!!\n");

        while (gameplay)
        {
            // Display the startup menu
            Console.WriteLine($"Points: {userPoints}\n");
            Console.WriteLine("Please choose a task to perform by entering the corresponding number to the task:\n");
            Console.WriteLine("\n1. Replacing books");
            Console.WriteLine("\n2. Identifying areas (Disabled)");
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
                        PerformReplacingBooksTask();
                        break;
                    }
                case "2":
                    Console.WriteLine("\nThis feature is currently disabled.\n");
                    break;
                case "3":
                    Console.WriteLine("\nThis feature is currently disabled.\n");
                    break;
                case "4":
                    Console.WriteLine($"\nGAME OVER.\nTOTAL POINTS EARNED: {userPoints}");
                    gameplay = false;
                    break;
                default:
                    Console.WriteLine("\nInvalid option. Please choose a valid task.\n");
                    break;
            }
        }


        //REPLACING BOOKS LOGIC
        static void PerformReplacingBooksTask()
        {

            //try and catch statement for validation 
            try
            {
                //generate random call numbers and store in list (Jamro, 2018)
                List<string> randomCallNumbers = GenerateRandomCallNumbers();

                //display the list to the user
                Console.WriteLine("\n------------------------------------------------------------------------------------------------------------------------------\n");
                Console.WriteLine("\nPlease sort the following call numbers in ascending order (numerical then alphabetical):\n");
                for (int i = 0; i < randomCallNumbers.Count; i++)
                {
                    Console.WriteLine($"\n{i + 1}. {randomCallNumbers[i]}");
                }

                //get user input
                Console.WriteLine("\nEnter the numbers corresponding to the sorted call numbers, separated by commas (no spaces):\n");
                string userInput = Console.ReadLine();
                string[] userSortedIndices = userInput.Split(',');

                //convert user input to call numbers
                List<string> userSortedNumbers = new List<string>();
                foreach (string index in userSortedIndices)
                {
                    int newIndex = int.Parse(index.Trim()) - 1; // Convert to zero-based index
                    if (newIndex < 0 || newIndex >= randomCallNumbers.Count) //check that new index is within range
                    {
                        throw new ArgumentOutOfRangeException("Index out of range.");
                    }
                    userSortedNumbers.Add(randomCallNumbers[newIndex]);
                }

                // Sort the list using the quick sort
                QuickSort(randomCallNumbers, 0, randomCallNumbers.Count - 1);

                // Compare the user answer to the correct answer and give user points
                if (string.Join(",", randomCallNumbers) == string.Join(",", userSortedNumbers))
                {
                    Console.WriteLine("\nCorrect! You sorted the call numbers accurately.\n");
                    Console.WriteLine($"WELL DONE.\nYou have earned 10 POINTS!.\nPoints: {userPoints} + 10 = {userPoints + 10}");
                    userPoints += 10;
                }
                else
                {
                    Console.WriteLine("\nIncorrect. The correct sorted order is:");
                    foreach (string number in randomCallNumbers)
                    {
                        Console.WriteLine(number);
                    }
                    Console.WriteLine($"\nSorry that means you have received 0 points for this task.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format. Please enter numbers separated by commas.");
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Index out of range. Please enter valid indices.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("\n------------------------------------------------------------------------------------------------------------------------------\n");
            }
        }


        //GENERATE RANDOM CALL NUMEBRS
        static List<string> GenerateRandomCallNumbers()
        {
            //List to store call numbers 
            List<string> callNumbers = new List<string>(); //(Jamro, 2018)

            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                // Generate a random number between 100 and 999 (inclusive)
                int randomNumber = random.Next(100, 1000);

                // Generate a random surname by selecting 3 random letters
                char letter1 = (char)random.Next('A', 'Z' + 1);
                char letter2 = (char)random.Next('A', 'Z' + 1);
                char letter3 = (char)random.Next('A', 'Z' + 1);
                //combine letters to make random surname
                string randomSurname = $"{letter1}{letter2}{letter3}";

                // Combine the number and surname to form the call number
                string callNumber = $"{randomNumber}.{random.Next(10, 99)} {randomSurname}";

                // Add the call number to the list of stored call numbers
                callNumbers.Add(callNumber);
            }

            //return list of call numbers 
            return callNumbers;
        }


        //QUICK SORT CODE
        //primary method for sorting the list of call numbers (Jamro, 2018)
        static void QuickSort(List<string> arr, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(arr, low, high);
                QuickSort(arr, low, pi - 1);
                QuickSort(arr, pi + 1, high);
            }
        }

        //method to return the index of the pivot element after partitioning
        static int Partition(List<string> arr, int low, int high)
        {
            string pivot = arr[high];
            int i = low - 1;

            for (int j = low; j <= high - 1; j++)
            {
                if (CompareCallNumbers(arr[j], pivot) <= 0)
                {
                    i++;
                    Swap(arr, i, j);
                }
            }
            Swap(arr, i + 1, high);
            return i + 1;
        }

        //method to swap elements (Jamro, 2018)
        static void Swap(List<string> arr, int i, int j)
        {
            string temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        //method to compare numerical part of call number
        static int CompareCallNumbers(string a, string b)
        {
            // Extract numerical parts
            double numA = double.Parse(a.Split(' ')[0]);
            double numB = double.Parse(b.Split(' ')[0]);

            // Compare numerical parts
            if (numA < numB) return -1;
            if (numA > numB) return 1;

            // Compare alphabetical parts if numerical parts are equal
            return string.Compare(a.Split(' ')[1], b.Split(' ')[1]);
        }

    }
}

/*REFERENCE LIST
Jamro, M. 2018. C# Data Structures and Algorithms. Packt Publishing.
 */
