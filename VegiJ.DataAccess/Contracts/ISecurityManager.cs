namespace VegiJ.DataAccess.Contracts
{
    public interface ISecurityManager
    {
        bool LogIn(string username, string password);
        User GetCurrentUser();
        void LogOut();
    }
}
