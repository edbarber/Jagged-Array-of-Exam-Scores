namespace Jagged_Array_of_Exam_Scores
{
    public class DataModel
    {
        public decimal Section1Score
        {
            set; get;
        }

        public decimal Section2Score
        {
            set; get;
        }

        public decimal Section3Score
        {
            set; get;
        }

        public DataModel (decimal section1Score, decimal section2SCore, decimal section3Score)
        {
            Section1Score = section1Score;
            Section2Score = section2SCore;
            Section3Score = section3Score;
        }
    }
}
