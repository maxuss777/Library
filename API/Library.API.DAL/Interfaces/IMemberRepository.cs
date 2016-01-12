namespace Library.API.DataAccess.Interfaces
{
    using Library.API.Model;

    public interface IMemberRepository
    {
        Member Get(string email);

        Member Create(Member user);
    }
}