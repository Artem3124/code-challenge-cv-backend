namespace Shared.Core.Constants
{
    public static class Errors
    {
        public static (string Code, string Message) CodeProblemNotFound => (
            Code: "code_problem_not_found",
            Message: "Code problem not found.");

        public static (string Code, string Message) CodeLanguageNotSupported => (
            Code: "code_language_not_supported",
            Message: "Code language not supported.");

        public static (string Code, string Message) UnexpectedError => (
            Code: "unexpected_error",
            Message: "Unexpected error.");

        public static (string Code, string Message) CodeRunNotFound => (
            Code: "code_run_not_found",
            Message: "Code run was not found.");

        public static (string Code, string Message) CredentialsInvalid => (
            Code: "credentials_invalid",
            Message: "Invalid email or password.");

        public static (string Code, string Message) UserExists => (
            Code: "user_exists",
            Message: "User with such email address already exists.");

        public static (string Code, string Message) EmailInvalid => (
            Code: "email_invalid",
            Message: "Email invalid.");

        public static (string Code, string Message) PasswordLengthInvalid => (
            Code: "password_length_invalid",
            Message: "Password length must be between 8 and 32 characters.");

        public static (string Code, string Message) LoginInvalid => (
            Code: "login_invalid",
            Message: "Login invalid.");

        public static (string Code, string Message) PasswordMissmatch => (
            Code: "password_missmatch",
            Message: "Password missmatch");

        public static (string Code, string Message) PasswordUpdateMismatch => (
            Code: "password_update_mismatch",
            Message: "Old password isn't correct.");
    }
}
