using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace AllAuth.Lib
{
    /// <summary>
    /// A wrapper class for Trace messages.
    /// </summary>
    public static class Logger
    {
        private const string TimeFormat = "MMM dd HH:mm:ss";

        public static TraceLevel LogOutLevel = TraceLevel.Info;
        public static bool TraceOut { private get; set; }
        public static bool ConsoleOut { private get; set; }
        public static string FileOut { private get; set; }

        public static EventHandler<LoggerEventArgs> Callback;

        [Conditional("TRACE")]
        public static void Error(string format, params object[] args)
        {
            if (LogOutLevel < TraceLevel.Error)
                return;

            Error(string.Format(format, args));
        }

        [Conditional("TRACE")]
        public static void Error(string message)
        {
            if (LogOutLevel < TraceLevel.Error)
                return;

            TraceLine(TraceLevel.Error, message);
        }

        [Conditional("TRACE")]
        public static void Warning(string format, params object[] args)
        {
            if (LogOutLevel < TraceLevel.Warning)
                return;

            Warning(string.Format(format, args));
        }

        [Conditional("TRACE")]
        public static void Warning(string message)
        {
            if (LogOutLevel < TraceLevel.Warning)
                return;

            TraceLine(TraceLevel.Warning, message);
        }

        [Conditional("TRACE")]
        public static void Info(string format, params object[] args)
        {
            if (LogOutLevel < TraceLevel.Info)
                return;

            Info(string.Format(format, args));
        }

        [Conditional("TRACE")]
        public static void Info(string message)
        {
            if (LogOutLevel < TraceLevel.Info)
                return;

            TraceLine(TraceLevel.Info, message);
        }
        
        [Conditional("TRACE")]
        public static void Verbose(string format, params object[] args)
        {
            if (LogOutLevel < TraceLevel.Verbose)
                return;

            Verbose(string.Format(format, args));
        }

        [Conditional("TRACE")]
        public static void Verbose(string message)
        {
            if (LogOutLevel < TraceLevel.Verbose)
                return;

            TraceLine(TraceLevel.Verbose, message);
        }

        private static void TraceLine(TraceLevel level, string message)
        {
            message = message.Trim();
            if (string.IsNullOrEmpty(message))
                return;

            string traceLevel;
            switch (level)
            {
                case TraceLevel.Error:
                    traceLevel = "ERROR";
                    break;

                case TraceLevel.Warning:
                    traceLevel = "WARNING";
                    break;

                case TraceLevel.Info:
                    traceLevel = "INFO";
                    break;

                case TraceLevel.Verbose:
                    traceLevel = "VERBOSE";
                    break;

                default:
                    // Shouldn't happen, but we'll catch it anyway
                    traceLevel = "DEFAULT";
                    break;
            }

            var stackTrace = new StackTrace();
            var methodBase = stackTrace.GetFrame(2).GetMethod();
			var methodBaseClass = methodBase.ReflectedType;
			var methodBaseClassName = methodBaseClass != null ? methodBaseClass.Name : null;
			var methodBaseNamespace = methodBaseClass != null ? methodBaseClass.Namespace : null;

            if (TraceOut)
            {
                // Errrrr, probably need to rewrite this at some point....
                // (intelisense autoconversion + xamarin's lack of c#6 lead to this point)
                var traceMessage =
                    string.Format("{0}{1}{2}",
                        string.Format("{0} :: ThreadId {1} :: ", DateTime.Now, Thread.CurrentThread.ManagedThreadId),
                        string.Format("{0} :: ",
							methodBaseNamespace + "." + methodBaseClassName + "." + methodBase.Name),
                        string.Format("{0} :: {1}", traceLevel, message));

                Trace.WriteLine(traceMessage);
            }

            if (ConsoleOut)
            {
                var consoleMessage = string.Format("{0} :: {1} :: {2}", DateTime.Now.ToString(TimeFormat), traceLevel,
                    message);
                Console.WriteLine(consoleMessage);
            }

            if (!string.IsNullOrEmpty(FileOut))
            {
                var fileMessage = String.Format("{0} :: {1} :: {2}", DateTime.Now.ToString(TimeFormat), traceLevel,
                    message);
                try
                {
                    File.AppendAllLines(FileOut, new[] { fileMessage });
                }
                catch (Exception)
                {
                    // Intentional ignore error
                }
            }

			if (Callback != null)
            	Callback.Invoke(null, new LoggerEventArgs(level, message));
        }

        public class LoggerEventArgs
        {
			public TraceLevel LogLevel { get; private set; }
			public string Message { get; private set; }

            public LoggerEventArgs(TraceLevel logLevel, string message)
            {
                LogLevel = logLevel;
                Message = message;
            }
        }
    }
}
