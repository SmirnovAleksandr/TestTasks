using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtension.Support.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MobileTestTask.Utils
{
    public class Wait : IWait
    {
        private readonly IClock _clock;
        private readonly List<Type> _ignoredExceptions = new List<Type>();

        public string Message { get; set; }

        public TimeSpan Timeout { get; set; }
        public TimeSpan PollingInterval { get; set; }

        private static TimeSpan DefaultSleepInterval
        {
            get { return TimeSpan.FromMilliseconds(5000); }
        }

        public Wait()
        {
            _clock = new SystemClock();
            Timeout = DefaultSleepInterval;
            PollingInterval = DefaultSleepInterval;
        }

        #region until methods

        public TResult Until<TResult>(Func<TResult> condition, bool throwTimeoutException = true)
        {
            return Until(condition, Timeout, throwTimeoutException);
        }

        public TResult Until<TResult>(Func<TResult> condition, TimeSpan timeout, bool throwTimeoutException = true)
        {
            if (condition == null)
                throw new ArgumentNullException("condition", "condition cannot be null");

            var resultType = typeof(TResult);

            if ((resultType.IsValueType && resultType != typeof(bool)) || !typeof(object).IsAssignableFrom(resultType))
            {
                throw new ArgumentException("Can only wait on an object or boolean response, tried to use type: " +
                    resultType, "condition");
            }

            Exception lastException = null;
            var endTime = _clock.LaterBy(timeout);

            while (true)
            {
                System.Diagnostics.Debug.WriteLine("waiting ");
                try
                {
                    var result = condition();
                    if (resultType == typeof(bool))
                    {
                        var boolResult = result as bool?;
                        if (boolResult.HasValue && boolResult.Value)
                        {
                            return result;
                        }
                    }
                    else
                    {
                        if (result != null)
                        {
                            return result;
                        }
                    }
                }
                catch (Exception exception)
                {
                    if (!IsIgnoredException(exception))
                    {
                        throw;
                    }

                    lastException = exception;
                }

                if (!_clock.IsNowBefore(endTime))
                {
                    if (!throwTimeoutException)
                        return default(TResult);

                    var timeoutMessage = string.Format(CultureInfo.InvariantCulture,
                        "Timed out after {0} seconds", timeout.TotalSeconds);

                    if (!string.IsNullOrEmpty(Message))
                    {
                        timeoutMessage += ": " + Message;
                    }

                    throw new TimeoutException(timeoutMessage, lastException);
                }

                Thread.Sleep(PollingInterval);
            }
        }

        #endregion

        #region ignore exception methods

        public void IgnoreExceptionTypes(params Type[] exceptionTypes)
        {
            if (exceptionTypes == null)
                throw new ArgumentNullException("exceptionTypes", "exceptionTypes cannot be null");

            if (exceptionTypes.Any(exceptionType => !typeof(Exception).IsAssignableFrom(exceptionType)))
                throw new ArgumentException("All types to be ignored must derive from System.Exception", "exceptionTypes");

            _ignoredExceptions.AddRange(exceptionTypes);
        }

        private bool IsIgnoredException(Exception exception)
        {
            return _ignoredExceptions.Any(type => type.IsInstanceOfType(exception));
        }

        #endregion

        /// <summary>
        /// Waitin for displaing element 
        /// </summary>
        /// <param name="el">Element expected to be dispayed</param>
        /// <param name="doThrowTimeOutException">Throw timeout exception if element doesnt displayed</param>
        /// <returns></returns>
        public bool WaitElement(IWebElement el, bool doThrowTimeOutException = true)
        {
            //Timeout           // общее время ожидание
            //PollingInterval   // перерыв между попытками
            var _clock = new SystemClock();
            var endTime = _clock.LaterBy(Timeout);

            while (_clock.IsNowBefore(endTime))
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine("Waiting for " + el.Text);
                    if (el.Displayed) return true;
                }
                catch (NoSuchElementException nsee)
                {
                    System.Diagnostics.Debug.WriteLine("Element still Not found");
                }
                catch (WebDriverException wde)
                {
                    System.Diagnostics.Debug.WriteLine("WDE: " + wde.Message);
                }
                catch (System.Reflection.TargetInvocationException tie)
                {
                    System.Diagnostics.Debug.WriteLine("----------------------------------");
                    System.Diagnostics.Debug.WriteLine(tie.Message);
                    System.Diagnostics.Debug.WriteLine("----------------------------------");
                }

                Thread.Sleep(PollingInterval);
            }

            //           MakeScreenshot("Element not found");

            if (doThrowTimeOutException)
                throw new TimeoutException("Element " + el + "not found during " + "timeout");
            else
            {
                return false;
            }
        }


        

    }
}
