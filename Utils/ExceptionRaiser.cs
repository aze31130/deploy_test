using System;
using System.Globalization;

namespace deploy_test.Utils
{
    public class ExceptionRaiser : Exception
    {
        public ExceptionRaiser() : base() { }

        public ExceptionRaiser(string message) : base(message) { }
        public ExceptionRaiser(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args)) { }
    }
}
