using System;

namespace Marsey;

public abstract class Utility
{
    public enum LogType
    {
        INFO,
        WARN,
        FATL,
        DEBG
    }
    public static void Log(LogType logType, string message)
    {
        Console.WriteLine($"[MARSEY] [{logType.ToString()}] {message}");
    }
}
