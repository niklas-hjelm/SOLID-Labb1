namespace Shared
{
    public class User
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public string Password { get; set; }
        
        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}