using Library.API.Common.Member;

namespace Library.API.DAL.Abstract
{
    public interface IMemberRepository
    {
        MemberObject Get(string email);
        MemberObject Create(MemberObject user);
    }
}