namespace Gateway.Api
{
    public class GatewayError
    {
        public string Message { get; set; }

        public string Code { get; set; }

        public GatewayError((string Message, string Code) error) : this(error.Message, error.Code)
        {

        }

        public GatewayError(string message, string code)
        {
            Message = message;
            Code = code;
        }
    }
}
