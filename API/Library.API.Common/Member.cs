namespace Library.API.Model
{
    using System;

    public class Member
    {
        public int Id { get; set; }
        
        public string Email { get; set; }
        
        public string MemberName { get; set; }
        
        public string Password { get; set; }
        
        public DateTime CreationDate { get; set; }
    }
}
