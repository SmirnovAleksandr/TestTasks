using System;

namespace SeleniumExtension.Support.Utils
{
    public interface IWait
    {
        string Message { get; set; }

        TimeSpan Timeout { get; set; }
        TimeSpan PollingInterval { get; set; }

        void IgnoreExceptionTypes(params Type[] exceptionTypes);
        TResult Until<TResult>(Func<TResult> condition, bool throwTimeoutException = true);
        TResult Until<TResult>(Func<TResult> condition, TimeSpan timeout, bool throwTimeoutException = true);
    }
}
