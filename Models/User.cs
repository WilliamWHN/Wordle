namespace Wordle.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Firstname { get; set; } = String.Empty;
        public string Lastname { get; set; } = String.Empty;
        public string Username { get; set; } = String.Empty;    
        public string Email { get; set; } = String.Empty;
        public Role Role { get; set; }
    }
}
