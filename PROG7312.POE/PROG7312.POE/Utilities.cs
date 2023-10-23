using System;
using System.Collections.Generic;
using System.Linq;
using PROG7312.POE;

namespace PROG7312.POE
{
	public class Utilities
	{
		public Utilities()
		{
		}


        //P1
        //GENERATE RANDOM CALL NUMEBRS
        public static List<string> GenerateRandomCallNumbers()
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
        public static void QuickSort(List<string> arr, int low, int high)
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

//P2

public static class DDCategories
{
    public static Dictionary<string, string> Categories = new Dictionary<string, string>
        {
            {"000", "Generalities"},
            {"100", "Philosophy & Psychology"},
            {"200", "Religion"},
            {"300", "Social Sciences"},
            {"400", "Language"},
            {"500", "Natural Sciences & Mathematics"},
            {"600", "Technology (Applied Sciences)"},
            {"700", "The Arts"},
            {"800", "Literature & Rhetoric"},
            {"900", "Geography & History"}
        };
}


public class CorrectAnswer
{
    public int ColAIndex { get; set; }
    public int ColBIndex { get; set; }
    public string ColBContent { get; set; }
}

//Data class for generating and passing the question
public class QuestionData
{
    public List<string> columnA { get; set; }
    public List<string> columnB { get; set; }
    public List<CorrectAnswer> correctAnswers { get; set; }

    public QuestionData()
    {
        columnA = new List<string>();
        columnB = new List<string>();
        correctAnswers = new List<CorrectAnswer>();
        Random random = new Random();

        // Randomly decide between categories and call numbers for columnA
        bool isCategoryInColumnA = random.Next(2) == 0;

        if (isCategoryInColumnA)
        {
            // Select 4 random categories for columnA
            columnA = DDCategories.Categories.Values.OrderBy(x => Guid.NewGuid()).Take(4).ToList();

            // Populate columnB with corresponding call numbers
            foreach (var item in columnA)
            {
                string key = DDCategories.Categories.FirstOrDefault(x => x.Value == item).Key;
                columnB.Add(key);
            }
        }
        else
        {
            // Select 4 random call numbers for columnA
            columnA = DDCategories.Categories.Keys.OrderBy(x => Guid.NewGuid()).Take(4).ToList();

            // Populate columnB with corresponding categories
            foreach (var key in columnA)
            {
                string value;
                DDCategories.Categories.TryGetValue(key, out value);
                columnB.Add(value);
            }
        }

        // Add 3 random items to columnB
        var remainingItems = DDCategories.Categories.Where(x => !columnB.Contains(x.Key) && !columnB.Contains(x.Value)).ToList();
        var randomItems = remainingItems.OrderBy(x => Guid.NewGuid()).Take(3).ToList();
        foreach (var item in randomItems)
        {
            columnB.Add(isCategoryInColumnA ? item.Key : item.Value);
        }

        // Randomize the order of columnB
        columnB = columnB.OrderBy(x => Guid.NewGuid()).ToList();



        //populate correct answers
        for (int i = 0; i < columnA.Count; i++)
        {
            int colBIndex; // Declare it here so you can use it later for ColBContent
            if (isCategoryInColumnA)
            {
                string key = DDCategories.Categories.FirstOrDefault(x => x.Value == columnA[i]).Key;
                colBIndex = columnB.IndexOf(key) + 1;
            }
            else
            {
                string value;
                DDCategories.Categories.TryGetValue(columnA[i], out value);
                colBIndex = columnB.IndexOf(value) + 1;
            }

            var answer = new CorrectAnswer
            {
                ColAIndex = i + 1,
                ColBIndex = colBIndex,
                ColBContent = columnB[colBIndex - 1]
            };
            correctAnswers.Add(answer);
        }
    }
}