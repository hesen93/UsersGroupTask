using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example2
{
    public class Group 
    {
        public string GroupNo { get; set; }

        private int _studentLimit;
        public int StundentLimit
        {
            get { return _studentLimit; }
            set
            {
                if (value < 5 || value > 18)
                    throw new StudentOverloadException("5 den kicik ve 18 den boyuk teyin oluna bilmez");
                else
                    _studentLimit = value;
            }
        }
        
        public Group(string groupNo, int studentLimit)
        {
            GroupNo = groupNo;
            StundentLimit = studentLimit;
        }

        public static Group CreateGroup(string groupNo, int studentLimit, out Dictionary<int, string> errorGroupno)
        {
            errorGroupno = new Dictionary<int, string>();
            try
            {
                var checkGroup = CheckGroupNo(groupNo, out errorGroupno);
                Group group = new Group(groupNo, studentLimit);
                if (checkGroup)
                    return group;
                else
                    return null; // properydeki validasyani triggerlemek ucun
            }
            catch (StudentOverloadException ex)
            {
                errorGroupno.TryAdd((int)GroupErrorKeys.STUDENT_LIMIT, ex.Message);
                return null;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        private List<Student> Students = new List<Student>();
        public static bool CheckGroupNo(string GroupNo, out Dictionary<int, string> errorGroupno)
        {
            errorGroupno = new Dictionary<int, string>();
            if (GroupNo.Length == 5)
            {
                if (Char.IsUpper(GroupNo[0]) && Char.IsUpper(GroupNo[1]) && Char.IsNumber(GroupNo[2]) && Char.IsNumber(GroupNo[3]) && Char.IsNumber(GroupNo[4]))
                {
                    return true;
                }    
                else
                    errorGroupno.TryAdd((int)GroupErrorKeys.NAME_SYMBOL_FORMAT, "Simvollar istenilen formatda deyil");

            }
            else 
              errorGroupno.TryAdd((int)GroupErrorKeys.NAME_SYMBOL_COUNT, "Qrup adi 5 simvoldan ibaret olmalidir.");
            return errorGroupno.Count == 0;
        }
        public bool AddStudents(Student newStudent)
        {
            if (Students.Count < StundentLimit)
            {
                Students.Add(newStudent);
                return true;
            }
            else
                return false;

        }

        public Student GetStudent(int id)
        {
            Student response = null;
            for (int i = 0; i < Students.Count; i++)
            {
               if(Students[i].StId == id)
                {
                    response = Students[i];
                }
            }
            return response;
        }

        public List<Student> GetAllStudents()
        {
            
            return Students;
        }
        

        
    }
}



public enum GroupErrorKeys
{
    NAME_SYMBOL_COUNT,
    NAME_SYMBOL_FORMAT,
    STUDENT_LIMIT
}

public class StudentOverloadException : Exception
{
    public StudentOverloadException(string message) : base(message)
    {
    }
}