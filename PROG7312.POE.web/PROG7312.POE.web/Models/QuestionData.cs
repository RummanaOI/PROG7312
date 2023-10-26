using System;
using System.Collections.Generic;
using System.Linq;

namespace PROG7312.POE.web.Models
{
    public class QuestionData
    {
        public List<string> columnA { get; set; }
        public List<string> columnB { get; set; }
        public Dictionary<string, string> correctAnswers { get; set; }

        public QuestionData()
        {
            // Initialize variables (Jamro, 2018)
            columnA = new List<string>();
            columnB = new List<string>();
            correctAnswers = new Dictionary<string, string>();
            //create random object 
            Random random = new Random();

            // Randomly decide between categories and call numbers for columnA
            bool isCategoryInColumnA = random.Next(2) == 0;

            if (isCategoryInColumnA)
            {
                // Select 4 random categories for columnA
                columnA = DDCategories.Categories.Values.OrderBy(x => Guid.NewGuid()).Take(4).ToList();

                // Populate columnB with corresponding call numbers (Jamro, 2018)
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

                // Populate columnB with corresponding categories (Jamro, 2018)
                foreach (var key in columnA)
                {
                    string value;
                    DDCategories.Categories.TryGetValue(key, out value);
                    columnB.Add(value);
                }
            }

            // Add 3 random items to columnB (Jamro, 2018)
            var remainingItems = DDCategories.Categories.Where(x => !columnB.Contains(x.Key) && !columnB.Contains(x.Value)).ToList();
            var randomItems = remainingItems.OrderBy(x => Guid.NewGuid()).Take(3).ToList();
            foreach (var item in randomItems)
            {
                columnB.Add(isCategoryInColumnA ? item.Key : item.Value);
            }

            // Randomize the order of columnB
            columnB = columnB.OrderBy(x => Guid.NewGuid()).ToList();

            //populate correct answers dictionary (Jamro, 2018)
            for (int i = 0; i < columnA.Count; i++)
            {
                int colBIndex;
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

                string colAIndexStr = (i + 1).ToString();
                string colBIndexStr = ((char)(96 + colBIndex)).ToString(); // Convert index to letter (a, b, c, ...)

                correctAnswers.Add(colAIndexStr, colBIndexStr);
            }
        }
    }

    //static class to store dictionary of categories and call numbers 
    public static class DDCategories
    {
        //Store top level of dewey decimal call numbers and their respective category (Jamro, 2018)
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
}
//Reference list
//Jamro, M. 2018. C# Data Structures and Algorithms. Packt Publishing.