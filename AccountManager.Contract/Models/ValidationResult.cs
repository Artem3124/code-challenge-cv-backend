namespace AccountManager.Contract.Models
{
    public class ValidationError
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public UserAttribute Attribute { get; set; }
        
        public ValidationError(string code, string message, UserAttribute attribute)
        {
            Code = code;
            Message = message;
            Attribute = attribute;
        }
    }

    public class ValidationResult
    {
        public List<ValidationError> Errors { get; set; } = new();
    }
}
