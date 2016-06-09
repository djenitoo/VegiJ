namespace VegiJ.Web.MVC.Helpers
{
    using System.Web.UI.WebControls;

    public class ValidationError : CustomValidator
    {
        public ValidationError(string msg, string group = null)
        {
            base.ValidationGroup = group;
            base.ErrorMessage = msg;
            base.IsValid = false;
        }
    }
}