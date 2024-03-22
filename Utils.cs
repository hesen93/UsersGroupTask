namespace Example2
{
    public static class Utils
    {
        public static int Id { get; set; }
        public static int StId { get; set; }

        public static int GenerateId(int defaultId = 1)
        {

            Id += defaultId;
            return Id;
           
        }
        public static int GenerateIdStudent(int defaultIdStudent = 1)
        {

            StId += defaultIdStudent;
            return StId;
        }


        public static void ShowUserInfo(this User user)
        {
            Console.WriteLine("Id nomresi:" + user.Id);
            Console.WriteLine("Fullname:" + user.Username);
            Console.WriteLine("Password:" + user.Password);
            Console.WriteLine("Email:" + user.Email);
        }
        
    }
}
