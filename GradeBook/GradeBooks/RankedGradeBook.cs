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
            int numberOfStudents = Students.Count;

            if (numberOfStudents < 5)
            {
                throw new InvalidOperationException();
            }

            List<double> averages = Students.Select(s => s.AverageGrade).ToList();
            averages.Sort();

            int numberOfStudentsGradeA_shouldPass = numberOfStudents - (int)(numberOfStudents * 0.2);
            int numberOfStudentsGradeB_shouldPass = numberOfStudents - (int)(numberOfStudents * 0.4);
            int numberOfStudentsGradeC_shouldPass = numberOfStudents - (int)(numberOfStudents * 0.6);
            int numberOfStudentsGradeD_shouldPass = numberOfStudents - (int)(numberOfStudents * 0.8);

            double gradeA_threshold = averages[numberOfStudentsGradeA_shouldPass];
            double gradeB_threshold = averages[numberOfStudentsGradeB_shouldPass];
            double gradeC_threshold = averages[numberOfStudentsGradeC_shouldPass];
            double gradeD_threshold = averages[numberOfStudentsGradeD_shouldPass];

            if(averageGrade >= gradeA_threshold) return 'A';
            if(averageGrade >= gradeB_threshold) return 'B';
            if(averageGrade >= gradeC_threshold) return 'C';
            if(averageGrade >= gradeD_threshold) return 'D';

            return 'F';
        }

    }
}