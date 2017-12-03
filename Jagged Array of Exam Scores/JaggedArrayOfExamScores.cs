using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;

namespace Jagged_Array_of_Exam_Scores
{
    public enum ExamScoreType
    {
        LOWEST,
        HIGHEST
    }

    public class JaggedArrayOfExamScores : INotifyPropertyChanged
    {
        const int MAX_SECTIONS = 3;
        const int MIN_STARTING_INDEX = 0;
        const int ARRAY_INDEX_OFFSET = 1;
        const decimal MIN_SCORE = 0.0m;
        const int LEVEL_1_INDEX = 0;
        const int LEVEL_2_INDEX = 1;
        const int LEVEL_3_INDEX = 2;
        const string LEVEL_1_FILE_PATH = "..\\..\\Exam Scores\\Section1.txt";
        const string LEVEL_2_FILE_PATH = "..\\..\\Exam Scores\\Section2.txt";
        const string LEVEL_3_FILE_PATH = "..\\..\\Exam Scores\\Section3.txt";
        const int DECIMAL_PLACES = 1;

        private List<DataModel> dataModels;
        private decimal[][] examScores;
        private decimal averageExamScorePerSection1, averageExamScorePerSection2, averageExamScorePerSection3;
        private decimal averageExamScoreOverall;
        private int highestExamScoreSection, lowestExamScoreSection;
        private decimal highestExamScoreOverall, lowstExamScoreOverall;
        private bool noError;

        public List<DataModel> DataModels
        {
            set { dataModels = value; NotifyPropertyChanged(); }
            get { return dataModels; }
        }

        public decimal AverageExamScorePerSection1
        {
            set { averageExamScorePerSection1 = value; NotifyPropertyChanged(); }
            get { return averageExamScorePerSection1; }
        }

        public decimal AverageExamScorePerSection2
        {
            set { averageExamScorePerSection2 = value; NotifyPropertyChanged(); }
            get { return averageExamScorePerSection2; }
        }

        public decimal AverageExamScorePerSection3
        {
            set { averageExamScorePerSection3 = value; NotifyPropertyChanged(); }
            get { return averageExamScorePerSection3; }
        }

        public decimal AverageExamScoreOverallResult
        {
            set { averageExamScoreOverall = value; NotifyPropertyChanged(); }
            get { return averageExamScoreOverall; }
        }

        public int HighestExamScoreSection
        {
            set { highestExamScoreSection = value; NotifyPropertyChanged(); }
            get { return highestExamScoreSection; }
        }

        public int LowestExamScoreSection
        {
            set { lowestExamScoreSection = value; NotifyPropertyChanged(); }
            get { return lowestExamScoreSection; }
        }

        public decimal HighestExamScoreOverall
        {
            set { highestExamScoreOverall = value; NotifyPropertyChanged(); }
            get { return highestExamScoreOverall; }
        }

        public decimal LowestExamScoreOverall
        {
            set { lowstExamScoreOverall = value; NotifyPropertyChanged(); }
            get { return lowstExamScoreOverall; }
        }

        public JaggedArrayOfExamScores()
        {
            examScores = new decimal[MAX_SECTIONS][];
        }

        public void RefreshExamScores()
        {
            List<DataModel> dms = new List<DataModel>();
            int greatestInnerArrayLength = 0;

            for (int i = MIN_STARTING_INDEX; i < MAX_SECTIONS; i++)
            {
                if (i == MIN_STARTING_INDEX || examScores[i].Length > greatestInnerArrayLength)
                {
                    greatestInnerArrayLength = examScores[i].Length;
                }
            }

            for (int i = MIN_STARTING_INDEX; i < greatestInnerArrayLength; i++)
            {
                decimal[] sectionScores = new decimal[MAX_SECTIONS];

                for (int j = MIN_STARTING_INDEX; j < MAX_SECTIONS; j++)
                {
                    if (i < examScores[j].Length)
                    {
                        sectionScores[j] = examScores[j][i];
                    }
                }

                DataModel dataModel = new DataModel(sectionScores[LEVEL_1_INDEX], sectionScores[LEVEL_2_INDEX], sectionScores[LEVEL_3_INDEX]);

                dms.Add(dataModel);
            }

            DataModels = dms;

            AverageExamScorePerSection();
            AverageExamScoreOverall();
            OverallExamScore(ExamScoreType.HIGHEST);
            OverallExamScore(ExamScoreType.LOWEST);
        }

        public bool ReadExamScoresFromFile()
        {
            noError = true;

            try
            {
                string[] lines = File.ReadAllLines(LEVEL_1_FILE_PATH);
                examScores[LEVEL_1_INDEX] = new decimal[lines.Length];
                ParseStringsIntoExamScores(lines, LEVEL_1_INDEX);

                lines = File.ReadAllLines(LEVEL_2_FILE_PATH);
                examScores[LEVEL_2_INDEX] = new decimal[lines.Length];
                ParseStringsIntoExamScores(lines, LEVEL_2_INDEX);

                lines = File.ReadAllLines(LEVEL_3_FILE_PATH);
                examScores[LEVEL_3_INDEX] = new decimal[lines.Length];
                ParseStringsIntoExamScores(lines, LEVEL_3_INDEX);
            }
            catch
            {
                noError = false;
            }

            return noError;
        }

        private void ParseStringsIntoExamScores(string[] lines, int index)
        {
            if (noError)
            {
                // pass in exam score index for row, array of lines from read text file and use it to determine which column to 
                // assign each element of string lines to exam scores
                for (int i = MIN_STARTING_INDEX; i < examScores[index].Length; i++)
                {
                    if (noError)
                    {
                        if (decimal.TryParse(lines[i], out decimal result))
                        {
                            if (result >= MIN_SCORE)
                            {
                                examScores[index][i] = result;
                                noError = true;
                            }
                            else
                            {
                                noError = false;
                            }
                        }
                        else
                        {
                            noError = false;
                        }
                    }
                }
            }
        }
        
        private void AverageExamScorePerSection()
        {
            for (int i = MIN_STARTING_INDEX; i < MAX_SECTIONS; i++)
            {
                decimal result = 0.0m;

                for (int j = MIN_STARTING_INDEX; j < examScores[i].Length; j++)
                {
                    result += examScores[i][j];
                }

                result /= examScores[i].Length;
                result = Math.Round(result, DECIMAL_PLACES);

                switch (i)
                {
                    case LEVEL_1_INDEX:
                        {
                            AverageExamScorePerSection1 = result;
                            break;
                        }
                    case LEVEL_2_INDEX:
                        {
                            AverageExamScorePerSection2 = result;
                            break;
                        }
                    case LEVEL_3_INDEX:
                        {
                            AverageExamScorePerSection3 = result;
                            break;
                        }
                }
            }
        }

        private void AverageExamScoreOverall()
        {
            decimal result = 0.0m;
            int overallLength = 0;

            for (int i = MIN_STARTING_INDEX; i < MAX_SECTIONS; i++)
            {
                for (int j = MIN_STARTING_INDEX; j < examScores[i].Length; j++)
                {
                    result += examScores[i][j];
                    overallLength++;
                }
            }

            result /= overallLength;
            AverageExamScoreOverallResult = Math.Round(result, DECIMAL_PLACES);
        }

        private void OverallExamScore(ExamScoreType examScoreType)
        {
            decimal result = 0.0m;
            int sectionFound = 0;

            for (int i = MIN_STARTING_INDEX; i < MAX_SECTIONS; i++)
            {
                for (int j = MIN_STARTING_INDEX; j < examScores[i].Length; j++)
                {
                    if (i == MIN_STARTING_INDEX && j == MIN_STARTING_INDEX)
                    {
                        result = examScores[i][j];
                        sectionFound = i + ARRAY_INDEX_OFFSET;
                    }
                    else if (examScoreType == ExamScoreType.HIGHEST)
                    {
                        if (examScores[i][j] > result)
                        {
                            result = examScores[i][j];
                            sectionFound = i + ARRAY_INDEX_OFFSET;
                        }
                    }
                    else if (examScoreType == ExamScoreType.LOWEST)
                    {
                        if (examScores[i][j] < result)
                        {
                            result = examScores[i][j];
                            sectionFound = i + ARRAY_INDEX_OFFSET;
                        }
                    }
                }
            }

            if (examScoreType == ExamScoreType.HIGHEST)
            {
                HighestExamScoreOverall = result;
                HighestExamScoreSection = sectionFound;
            }
            else if (examScoreType == ExamScoreType.LOWEST)
            {
                LowestExamScoreOverall = result;
                LowestExamScoreSection = sectionFound;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
