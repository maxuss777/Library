using Library.API.Common.MemberObjects;
using Library.API.Common.MemberObjects;

namespace Library.API.DAL.Abstract
{
    public interface IMemberRepository
    {
        Member Get(string email);
        Member Create(Member user);
    }
}