using System;

namespace Library.API.Common.Member
{
    public class MemberObject
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string MemberName { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
