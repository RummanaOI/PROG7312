using System;
namespace PROG7312.POE
{
	public class ReplacingBooksTask
	{
		public static void ReplaceBooks()
		{
            //try and catch statement for validation 
            try
            {
                //generate random call numbers and store in list (Jamro, 2018)
                List<string> randomCallNumbers = Utilities.GenerateRandomCallNumbers();

                //display the list to the user
                Console.WriteLine("\n------------------------------------------------------------------------------------------------------------------------------\n");
                Console.WriteLine("\nPlease sort the following call numbers in ascending order (numerical then alphabetical):\n");
                for (int i = 0; i < randomCallNumbers.Count; i++)
                {
                    Console.WriteLine($"\n{i + 1}. {randomCallNumbers[i]}");
                }

                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Sort the list using the quick sort
                //create list for sorted numbers
                List<string> sortedNumbers = new List<string>(randomCallNumbers);
                //sort numbers
                Utilities.QuickSort(sortedNumbers, 0, sortedNumbers.Count - 1);
                //create dictionary for correct answer
                Dictionary<string, string> correctAnswer = new Dictionary<string, string>();
                //populate and display correct answer 
                for(int i =0; i < sortedNumbers.Count; i++)
                {
                    for(int j =0; j<randomCallNumbers.Count; j++)
                    {
                        if (sortedNumbers[i] == randomCallNumbers[j])
                        {
                            correctAnswer.Add((j+1).ToString(), sortedNumbers[i]);
                            Console.WriteLine($"{(j+1).ToString()}. {sortedNumbers[i]}");
                        }
                    }
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

                

                // Compare the user answer to the correct answer and give user points
                if (string.Join(",", sortedNumbers) == string.Join(",", userSortedNumbers))
                {
                    Console.WriteLine("\nCorrect! You sorted the call numbers accurately.\n");
                    Console.WriteLine($"WELL DONE.\nYou have earned 10 POINTS!.\nPoints: {UserPointsManager.Instance.UserPoints} + 10 = {UserPointsManager.Instance.UserPoints + 10}");
                    UserPointsManager.Instance.AddPoints(10);
                }
                else
                {
                    Console.WriteLine("\nIncorrect. Here's how your answer compares to the correct answer:");
                    Console.WriteLine("Your Answer\t\tCorrect Answer");
                    for (int i=0; i < sortedNumbers.Count; i++)
                    {
                        string userAnswer = i < userSortedNumbers.Count ? userSortedNumbers[i] : "N/A";
                        Console.WriteLine($"{userAnswer}\t\t{sortedNumbers[i]}");
                        
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
	}
}

