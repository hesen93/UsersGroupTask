using Example2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace Example2
{
   
    public class Student
    {
        public int StId { get; set; }
        public string FullName { get; set; }
        public string Point { get; set; }
        

        //refactor by Farid
        public string GetStudentInfo(Student student)
        {
            return $"Id nomresi: {student.StId}\n" +
                   $"Fullname: {student.FullName}\n" +
                   $"Point: {student.Point}\n";
        }
        public Student(string fullName, string point, out Dictionary<int, string> errorNameformat)
        {
            errorNameformat = new Dictionary<int, string>();
            // Name Validation ( Advance ==> Future )
            var isdDigitlinqCheck = fullName.Any(x => Char.IsDigit(x));
            if (isdDigitlinqCheck)
                errorNameformat.TryAdd(69, "Telebe adindan yalniz herflerden istifade edile biler");

            // Name Validation Normal  - 1
            foreach (var item in fullName)
            {
                if (Char.IsNumber(item))
                {
                    errorNameformat.TryAdd(0, "Telebe adindan yalniz herflerden istifade edile biler");
                    break;
                }
            }

            // Point Validation  - 2
            var isNumber = int.TryParse(point, out int number);
            if (!isNumber)
                errorNameformat.TryAdd(1, "Duzgu qiymetlendirme aparilmadi");

            // Result   - 3
            if (errorNameformat.Count == 0)
            {
                Point = point;
                FullName = fullName;
                StId = Utils.GenerateIdStudent();
            }
        }

        public override string ToString()
        {
            return $"Id nomresi: {this.StId}\n" +
                   $"Fullname: {this.FullName}\n" +
                   $"Point: {this.Point}\n";
        }
    }
   
}
