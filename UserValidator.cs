namespace Parks_LabAssignment8
{
    public static class UserValidator
    {
        public static bool ValidateNewUser(string username, string password, string confirmPassword, out string message)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                message = "Please enter a username.";
                return false;
            }
            if (username.Trim().Length < 3 || username.Trim().Length > 50)
            {
                message = "Username must be between 3 and 50 characters.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                message = "Please enter a password.";
                return false;
            }
            if (password.Length < 8)
            {
                message = "Password must be at least 8 characters long.";
                return false;
            }
            if (password != confirmPassword)
            {
                message = "Password and confirmation password do not match.";
                return false;
            }

            message = "";
            return true;
        }

        public static bool ValidateLogin(string username, string password, out string message)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                message = "Please enter your username.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                message = "Please enter your password.";
                return false;
            }

            message = "";
            return true;
        }
    }
}
