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

            int numberOfStudentsGradeA_ShouldPass = numberOfStudents - (int)(numberOfStudents * 0.2);
            int numberOfStudentsGradeB_ShouldPass = numberOfStudents - (int)(numberOfStudents * 0.4);
            int numberOfStudentsGradeC_ShouldPass = numberOfStudents - (int)(numberOfStudents * 0.6);
            int numberOfStudentsGradeD_ShouldPass = numberOfStudents - (int)(numberOfStudents * 0.8);

            double gradeA_threshold = averages[numberOfStudentsGradeA_ShouldPass];
            double gradeB_threshold = averages[numberOfStudentsGradeB_ShouldPass];
            double gradeC_threshold = averages[numberOfStudentsGradeC_ShouldPass];
            double gradeD_threshold = averages[numberOfStudentsGradeD_ShouldPass];

            if(averageGrade > gradeA_threshold) return 'A';
            if(averageGrade > gradeB_threshold) return 'B';
            if(averageGrade > gradeC_threshold) return 'C';
            if(averageGrade > gradeD_threshold) return 'D';

            return 'F';
        }

    }
}