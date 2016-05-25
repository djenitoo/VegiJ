namespace VegiJ.DataAccess
{
    public class GlobalConstants
    {
        public static int UsernameMinLength => 4;
        public static int UsernameMaxLength => 20;
        public static int UsernamePasswordMinLength = 6;
        public static int UsernamePasswordMaxLength = 20;
        public static string DateTimeFormat = "dd/MM/yyyy H:mm";
        public static string DateFormat = "dd/MM/yyyy";
        public static string NoLoginDate = "No login date.";
        public static string UnknownString = "Unknown";
        public static int CategoryNameLength = 5;
        public static string CategoryLenErrorMessage = "Category name len should be at least {0} characters";
        public static string CannotBeEmptyErrorMessage = "{0} cannot be empty or whitespaces!";
    }
}
