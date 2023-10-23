using System;
using System.Collections.Generic;
using System.Linq;
using static PROG7312.POE.Utilities;

namespace PROG7312.POE
{
	public class IdentifyingAreasTask
	{
		public static void IdentifyingAreas()
		{
            QuestionData questionData = new QuestionData();
            List<string> columnA = questionData.columnA;
            List<string> columnB = questionData.columnB;
            List<CorrectAnswer> correctAnswers = questionData.correctAnswers;

            // Continue with your logic

            Console.WriteLine("\n------------------------------------------------------------------------------------------------------------------------------\n");
            Console.WriteLine("\nPlease match the relevant block in column A with column B.\nPlease format your answer in the following way:" +
                "\n(cA:cB)(cA:cb)(cA:cB)(cA:cB)" +
                "Since there are only 4 required answers please only enter 4 options\n");

            Console.WriteLine("");

        }
        

    }

    

   
}

