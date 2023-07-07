namespace Shared.Core.Exceptions
{
    public class InvalidStateException : Exception
    {
        public InvalidStateException(string state, string operation) : base($"State {state} is invalid for {operation}")
        {

        }
    }
}
