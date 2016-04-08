namespace VegiJ.DataAccess.Contracts
{
    public interface ISecurityProvider
    {
        bool LogIn(string username, string password);
        User GetCurrentUser();
        void LogOut();
    }
}
