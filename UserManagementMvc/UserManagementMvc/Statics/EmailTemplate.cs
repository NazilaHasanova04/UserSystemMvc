namespace UserManagementMvc.Statics
{
    public static class EmailTemplate
    {
        private const string ConfirmTemplate = @"
                            <p>Hello,</p>
                            <p>Click the link below to confirm your registration:</p>
                            <p><a href='{confirmLink}' target='_blank'>Confirm</a></p>
                        ";

        private const string ResetPasswordTemplate = @"
                            <p>Hello,</p>
                            <p>Click the link below to reset your password:</p>
                            <p><a href='{confirmLink}' target='_blank'>Reset Password</a></p>
                            <p>If you did not request this, please ignore this email.</p>
                        ";

        public static string BuildConfirmLink(IConfiguration configuration, string userId, string token)
        {
            string baseUrl = configuration["ConfirmRegister"];
            return $"{baseUrl}?userId={Uri.EscapeDataString(userId)}&token={Uri.EscapeDataString(token)}";
        }

        public static string GetConfirmEmailBody(string confirmLink)
        {
            return ConfirmTemplate.Replace("{confirmLink}", confirmLink);
        }

        public static string GetConfirmEmailBodyForResetPassword(string confirmLink)
        {
            return ResetPasswordTemplate.Replace("{confirmLink}", confirmLink);
        }
    }
}
