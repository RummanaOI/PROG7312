using System;

//SIngleton pattern to store user points - this makes sure there is only one instance of the object 
namespace PROG7312.POE
{
    public class UserPointsManager
    {
        private static UserPointsManager? _instance;

        public static UserPointsManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserPointsManager();
                }
                return _instance;
            }
        }

        public int UserPoints { get; private set; }

        private UserPointsManager()
        {
            UserPoints = 0;
        }

        public void AddPoints(int points)
        {
            UserPoints += points;
        }

        public void ResetPoints()
        {
            UserPoints = 0;
        }
    }
}
