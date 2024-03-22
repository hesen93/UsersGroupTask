namespace Example2
{
    public class User : IAccount
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Id { get; set; }
        public string Email { get; set; }

        public User() { }

        public User(string email, string password)
        {
            Id = Utils.GenerateId();
            Email = email;
            Password = password;
        }

        public static User CreateUser(string username, string password, string email, out Dictionary<int, string> errorList, out Dictionary<int, string> errorEmail)
        {
            errorList = new Dictionary<int, string>();
            errorEmail = new Dictionary<int, string>();
            var checkPassword = PasswordChecker(password, out errorList);
            var checkEmail = EmailChecker(email, out errorEmail);
            // checkEmail = EmailChecker();
            if (checkPassword && checkEmail)
            {
                User user = new(email, password);
                user.Username = username;
                return user;
            }
            return null;
        }

        public static bool PasswordChecker(string password, out Dictionary<int, string> errorDict)
        {
            bool isValidPassword = false;
            errorDict = new();
            //Dictionary<int, string> errorDict = new();
            //0 => upperError, 1 => lowerError, 2 => numberError
            //errorDict.Add(0, "UpperError");
            //errorDict.ContainsKey(0);

            if (password.Length >= 8)
            {
                foreach (char item in password)
                {
                    isValidPassword = Char.IsUpper(item);
                    if (isValidPassword == true) break;
                    else if (item == password[password.Length - 1] && !errorDict.ContainsKey(0))
                        errorDict.Add(0, "UpperError");
                }
                foreach (char item in password)
                {
                    isValidPassword = Char.IsLower(item);
                    if (isValidPassword == true)
                        break;
                    else if (item == password[password.Length - 1] && !errorDict.ContainsKey(1))
                        errorDict.Add(1, "LowerError");
                }
                foreach (char item in password)
                {
                    isValidPassword = Char.IsNumber(item);
                    if (isValidPassword == true)
                        break;
                    else if (item == password[password.Length - 1] && !errorDict.ContainsKey(2))
                        errorDict.Add(2, "NumberError");
                }
            }
            else
                errorDict.Add(3, "Password not correct Size !");

            return isValidPassword && errorDict.Count == 0;
        }
        public static bool EmailChecker(string email, out Dictionary<int, string> errorEmail)
        {
            int pointCounter = 0;
            int loopCounter = 0;
            int pointPos = 0;
            bool atExists = false;
            int atCounter = 0;

            errorEmail = new Dictionary<int, string>();

            foreach (var item in email)
            {
                loopCounter++;
                // @farid..as.@bk.asd.asd
                if (!Char.IsLetter(email[0]))
                    errorEmail.TryAdd(6, "Email yalniz herfle bashlaya biler");
                if (item == '@' && (loopCounter == 1 || loopCounter == email.Length))
                {
                    errorEmail.TryAdd(1, "@ isaresi birinci veya sonuncu simvol ola bilmez");
                    atCounter++;
                }
                else if (item == '@')
                {
                    atExists = true;
                    atCounter++;
                    if (email[loopCounter] == '_')
                        errorEmail.TryAdd(5, "@ isaresinden sonra _ simvolu ola bilmez");
                }
                else if (atExists == false && loopCounter == email.Length)
                    errorEmail.TryAdd(0, "@ isaresi olmalidir");


                if (atExists && item == '.' && email.IndexOf('.') > email.IndexOf('@'))
                    pointCounter++;

                if (item == '.' && email.IndexOf('.') > email.IndexOf('@') && pointCounter > 2)
                    errorEmail.TryAdd(2, "@ dan sonra iki maksimum noqte qoyula biler");

                if (item == '.') // check points pos
                {
                    if (pointPos > 0 && loopCounter - pointPos == 1)
                    {
                        errorEmail.TryAdd(3, "Emailde yanasi iki noqte istifade edilisdir");
                    }
                    pointPos = loopCounter;
                }

                // Artirilacaq validasiyalar
                // mailde cemi bir defe @ ola biler
                // @ - dan sonra _ ola bilmez
                // mail reqemle, herhasni bir simvolla baslaya bilmez
            }
            if (atCounter > 1)
                errorEmail.TryAdd(4, "Yalnizca bir defe @ isaresi istifade edile biler.");
            return errorEmail.Count == 0;
        }




        //public void GetInfo(User user)
        //{
        //    Console.WriteLine("Id nomresi:" + user.Id);
        //    Console.WriteLine("Fullname:" + user.Username);
        //    Console.WriteLine("Password:" + user.Password);
        //    Console.WriteLine("Email:" + user.Email);
        //}

    }
}
