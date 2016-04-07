namespace VegiJ.DataAccess.Contracts
{
    public interface ISecurityProvider
    {
        // TODO: Login()[Authenticate], LogOut(), Authorize()?
        
        bool LogIn(string username, string password);
        void LogOut();
    }
}
