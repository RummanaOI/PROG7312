using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace PROG7312.POE.web.Controllers
{
    public class ReplaceBooksController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<string> callNumbers = GenerateRandomCallNumbers();
            return View(callNumbers);
        }

        [HttpPost]
        public JsonResult ValidateCallNumbers(List<string> reorderedList, List<string> originalList)
        {
            bool isValid = ValidateOrder(reorderedList, originalList);
            return Json(isValid);
        }

        private bool ValidateOrder(List<string> reorderedList, List<string> originalList)
        {
            // Sort the original list using QuickSort
            QuickSort(originalList, 0, originalList.Count - 1);

            // Compare the sorted original list with the reordered list
            for (int i = 0; i < originalList.Count; i++)
            {
                if (originalList[i] != reorderedList[i])
                {
                    return false;
                }
            }
            return true;
        }

        private List<string> GenerateRandomCallNumbers()
        {
            //List to store call numbers 
            List<string> callNumbers = new List<string>(); //(Jamro, 2018)
            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                // Generate a random number between 000 and 999 (inclusive)
                int randomNumber = random.Next(0, 1000);  // Random number between 000 and 999
                string formattedNumber = randomNumber.ToString("D3");  // Format to 3 digits

                // Generate a random surname by selecting 3 random letters
                char letter1 = (char)random.Next('A', 'Z' + 1);
                char letter2 = (char)random.Next('A', 'Z' + 1);
                char letter3 = (char)random.Next('A', 'Z' + 1);
                //combine letters to make random surname
                string randomSurname = $"{letter1}{letter2}{letter3}";

                // Combine the number and surname to form the call number
                string callNumber = $"{formattedNumber}.{random.Next(10, 99)} {randomSurname}";

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

