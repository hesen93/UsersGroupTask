using Example2;





bool exitCondition = false;
List<User> users = new List<User>();
while (exitCondition == false)
{
    Console.Clear();
    Console.WriteLine("===== Istifadeci yaratma formu ====\n");
    var errorList = new Dictionary<int, string>();
    var errorEmail = new Dictionary<int, string>();
    Console.WriteLine("Adi daxil edin");
    string name = Console.ReadLine()!;
    Console.WriteLine("Email daxil edin");
    string email = Console.ReadLine()!;
    Console.WriteLine("Sifreni daxil edin");
    string password = Console.ReadLine()!;
    Console.Clear();

    var user = User.CreateUser(name, password, email, out errorList, out errorEmail);
    if (user is not null)
    {
        users.Add(user);
        Console.Clear();
        Console.WriteLine("======= USER CREATED =======");
        Console.WriteLine("1.Show info");
        Console.WriteLine("2.Create new group");
        string choise1= Console.ReadLine()!;
        if (choise1== "1")
            user.ShowUserInfo();
        else if (choise1== "2")
        {
            bool condition3 = false;
            while (condition3 == false)
            {
                Console.WriteLine("Yeni bir qrup yaratmaq ucun qrupun adini qeyd edin");
                string groupNo = Console.ReadLine();
                Console.WriteLine("Limit teyin edin");
                int studentLimit = int.Parse(Console.ReadLine());
                Group group = Group.CreateGroup(groupNo, studentLimit, out Dictionary<int, string> errorGroupno);
                bool condition2 = false;
                while (condition2 == false)
                {
                    if (group != null)
                    {

                        Console.WriteLine("===========");
                        Console.WriteLine("1.Show all students");
                        Console.WriteLine("2.Get Student by ID");
                        Console.WriteLine("3.Add Student");
                        Console.WriteLine("0.Quit");
                        string choice = Console.ReadLine();
                        if (choice == "1")
                        {
                            Console.Clear();
                            if (group.GetAllStudents().Count() == 0)
                            {
                                Console.WriteLine("Hec bir telebe qeydiyyati aparilmayib");
                            }
                            foreach (Student student in group.GetAllStudents())
                            {
                                Console.WriteLine(student.GetStudentInfo(student));
                            }
                        }
                        else if (choice == "3")
                        {
                            Console.Clear();
                            Console.WriteLine("Telebenin adini qeyd edin");
                            string studentName = Console.ReadLine();
                            Console.WriteLine("Telebenin balini qeyd edin");
                            string studentPoint = Console.ReadLine();
                            Student student = new Student(studentName, studentPoint, out Dictionary<int, string> errorNameFormat);
                            if (errorNameFormat.Count == 0)
                            {
                                var isAdded = group.AddStudents(student);
                                if (isAdded == true)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Yeni telebe elave edildi");
                                    Console.WriteLine(student.GetStudentInfo(student));
                                    Console.ResetColor();
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Grupda yerler artiq bitmisdir :(");
                                    Console.ResetColor();
                                }
                            }
                            else
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                foreach (var item in errorNameFormat)
                                {
                                    Console.WriteLine(item.Value);
                                }
                                Console.ResetColor();
                            }
                        }
                        else if (choice == "2")
                        {
                            Console.Clear();
                            Console.WriteLine("Melumat elde etmek istediyiniz telebenin 'ID' nomresini daxil edin");
                            int studentId = int.Parse(Console.ReadLine());
                            var student = group.GetStudent(studentId);
                            if (student != null)
                            {
                                Console.WriteLine(student.GetStudentInfo(student));
                            }
                            else
                                Console.WriteLine("Qeyd edilen id-ye uygun telebe tapilmadi");
                        }
                        else if (choice == "0")
                        {
                            condition2 = true;
                            condition3 = true;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        foreach (var item in errorGroupno)
                        {
                            Console.WriteLine(item);
                        }
                        Console.ResetColor();
                        break;
                    }
                }
            }
        }
    }
    else
    {
        Console.Clear();
        Console.WriteLine("======= FAILED =======");
        foreach (var item in errorList)
        {
            Console.WriteLine(item);
        }
        foreach (var item in errorEmail)
        {
            Console.WriteLine(item);
        }
    }

    Console.WriteLine("\nButun Istifadecileri elde etmek ucun - 5 basin");
    int choice2= int.Parse(Console.ReadLine()!);
    if (choice2== 5)
    {
        // get my users list inside foreach
        Console.Clear();
        foreach (var item in users)
        {
            item.ShowUserInfo();
            Console.WriteLine("=======================================");
        }
    }

    Console.WriteLine("Cixmaq ucun -1 basin");
    int eded = int.Parse(Console.ReadLine()!);
    if (eded == 1)
    {
        exitCondition = true;
    }

}





