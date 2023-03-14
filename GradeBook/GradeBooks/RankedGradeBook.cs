using System;
using System.Collections.Generic;
using System.Linq;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5) 
                throw new InvalidOperationException();

            List<double> averages = Students.Select(s => s.AverageGrade).ToList();
            averages.Sort();

            int numberOfStudentsGradeA_shouldPass = Students.Count - (int)(Students.Count * 0.2);
            int numberOfStudentsGradeB_shouldPass = Students.Count - (int)(Students.Count * 0.4);
            int numberOfStudentsGradeC_shouldPass = Students.Count - (int)(Students.Count * 0.6);
            int numberOfStudentsGradeD_shouldPass = Students.Count - (int)(Students.Count * 0.8);

            double gradeA_threshold = averages[numberOfStudentsGradeA_shouldPass];
            double gradeB_threshold = averages[numberOfStudentsGradeB_shouldPass];
            double gradeC_threshold = averages[numberOfStudentsGradeC_shouldPass];
            double gradeD_threshold = averages[numberOfStudentsGradeD_shouldPass];

            if(averageGrade >= gradeA_threshold) 
                return 'A';
            if(averageGrade >= gradeB_threshold) 
                return 'B';
            if(averageGrade >= gradeC_threshold) 
                return 'C';
            if(averageGrade >= gradeD_threshold) 
                return 'D';

            return 'F';
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5){
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }

            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5){
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            
            base.CalculateStudentStatistics(name);
        }
    }
}