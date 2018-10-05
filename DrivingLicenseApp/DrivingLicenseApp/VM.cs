using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DrivingLicenseApp
{
    class VM : INotifyPropertyChanged
    {
        const int MULT_FACTOR = 5;  // constant to take the mark into a base of 100 %
        const int MIN_MARK = 75; // constant that indicates the minimum mark required to pass
        //Array Declarations (correct result of the test and actual result that is the one read from the file)
        public string[] CorrectResult = new string[] { "B", "D", "A", "A", "C", "A", "B", "A", "C", "D", "B", "C", "D", "A", "D", "C", "C", "B", "D", "A" };
        public string[] actualResult;
        public string[] ActualResult { get { return actualResult; } set { actualResult = value; NotifyChange(); } }
        //Variable and Properties Declarations
        int mark;
        public int Mark { get { return mark; } set { mark = value; NotifyChange(); } }

        int sumCorrect;
        public int SumCorrect { get { return sumCorrect; } set { sumCorrect = value; NotifyChange(); } }

        int numberOfCorrect;
        public int NumberOfCorrect { get { return numberOfCorrect; } set { numberOfCorrect = value; NotifyChange(); } }

        string message;
        public string Message { get { return message; } set { message = value; NotifyChange(); } }
        List<Answer> wrongAnswers;
        public List<Answer> WrongAnswers { get { return wrongAnswers; } set { wrongAnswers = value; NotifyChange(); } }

        //Method used to read the File
        public void ReadFile()
        {
            string inputText = File.ReadAllText("Input.txt");
            string[] lines = inputText.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                //The line.Split put every char in the line inside the file into an array
                ActualResult = line.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }
        // Method to Calculate
        public void CalculateMark()
        {
            WrongAnswers = new List<Answer>(); // initialize the List WrongAnswers
            SumCorrect = 0; // Initializes the sum of correct answers
            NumberOfCorrect = 0; //Initializes the sum of incorrect answers
            //for Loop to compare every item inside the array ActualResult with the array CorrectResult  
            for (int i = 0; i < 20; i++)
            {
                if (ActualResult[i] == CorrectResult[i]) SumCorrect++;
                else
                {
                    NumberOfCorrect++;
                    Answer thing = new Answer
                    {
                        Question = i + 1 // The items of the arrays starts in zero but the number of question starts in one
                    };
                    WrongAnswers.Add(thing);
                }
            }
            //Calculation of the Mark in the base of 100%
            Mark = SumCorrect * MULT_FACTOR;
            // Showing the Message if pass or fail
            if (Mark >= MIN_MARK)
            {
                Message = "Congratulations! You have passed";
            }
            else
            {
                Message = "Sorry! You have failed";
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyChange([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
