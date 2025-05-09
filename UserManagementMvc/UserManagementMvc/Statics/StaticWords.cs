namespace UserManagementMvc.Statics
{
    public static class StaticWords
    {
        public static string SamePasswordCode = "SamePasswords";
        public static string SamePasswordDescription = "Passwords should be same.";

        public static string UpperCaseCode = "PasswordRequiresUppercase";
        public static string UpperCaseDescription = "Password must contain at least one uppercase letter.";

        public static string SymbolCode = "PasswordRequiresSymbol";
        public static string SymbolDescription = "Password must contain at least one symbol.";

        public static string UsernameNotFound = "Username doesnt exist!";
        public static string UsernameLockedOut = "Username locked out, please wait!";
        public static string IncorrectLogin = "Invalid username or password!";

        public static string SuccessfullLogin = "Successfull login!";
        public static string RegisterConfirmation = "Link is sent to your mail for registration";
        public static string ResetPasswordConfirmation = "Link is sent to your mail for reset password";

        public const string PasswordNotMatched = "Passwords do not match.";

        public const string NotSucceded = "Result is not succeeded.";
        public static string EmailConfirmation = "Email Confirmation";

        public static string IncorrectEmail = "No user found with this email address.";
        public static string ResetPassword = "ResetPassword";

    }
}
