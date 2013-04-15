using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace QueueComparison.Web.Shared
{
    public class DiagnosticsHelper
    {
        public static string DefaultAdminError = "Oops, looks like we have a problem. We're on it! Thank you for your patience.";

        public static void ThrowArgumentNull(string arg)
        {
            DiagnosticsHelper.Throw(new ArgumentNullException(arg));
        }

        public static void TraceException(string message, Exception exception)
        {
            // does not throw, just traces
            string exceptionMessage = exception != null ? exception.Message : "";

            Trace.TraceError(string.Format("{0}. INNER EXCEPTION: {1}", message, exceptionMessage));
        }

        public static void TraceException(string message)
        {
            TraceException(message, null);
        }

        public static void TraceException(Exception exception)
        {
            string exceptionMessage = exception != null ? exception.Message : "";
            TraceException(exceptionMessage, exception);
        }

        public static void ThrowAdmin(string message)
        {
            DiagnosticsHelper.ThrowAdmin(DefaultAdminError, message, null);
        }

        public static void ThrowAdmin(Exception exception)
        {
            string exceptionMessage = exception != null ? exception.Message : "";
            DiagnosticsHelper.ThrowAdmin(DefaultAdminError, exceptionMessage, exception);
        }

        public static void ThrowAdmin(string message, Exception exception)
        {

            DiagnosticsHelper.ThrowAdmin(DefaultAdminError, message, exception);
        }

        public static void ThrowAdmin(string showMessage, string message, Exception exception)
        {
            TraceException(message, exception);
            if (string.IsNullOrEmpty(showMessage))
                showMessage = DefaultAdminError;
            throw new Exception(showMessage, new Exception(message, exception));
        }

        public static void Throw(string message)
        {
            DiagnosticsHelper.Throw(message, null);
        }

        public static void Throw(Exception exception)
        {

            string exceptionMessage = exception != null ? exception.Message : "";
            DiagnosticsHelper.Throw(exceptionMessage, exception);
        }

        public static void Throw(string message, Exception exception)
        {
            TraceException(message, exception);
            throw new Exception(message, exception);
        }

        public static void TraceInformation(string message)
        {
            Trace.TraceInformation(message);
        }
        public static void TraceWarning(string message)
        {
            Trace.TraceWarning(message);
        }
        public static void TraceDebug(string message)
        {
            Trace.TraceInformation(message);
        }
    }
}