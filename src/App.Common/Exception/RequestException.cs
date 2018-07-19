namespace App.Common.Exception
{
    public class RequestException : System.Exception
    {
        public RequestException()
        { }

        public RequestException(string message)
            : base(message)
        { }

        public RequestException(string message, System.Exception innerException)
            : base(message, innerException)
        { }
    }
}