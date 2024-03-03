using System.Globalization;

namespace Portfolio.API.Helpers
{
    public class AppExceptions : Exception
    {
        public AppExceptions() : base() {}

        public AppExceptions(string message) : base(message) { } 

        public AppExceptions(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        { }
    }
}
