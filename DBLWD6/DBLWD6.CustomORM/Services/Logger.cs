using System;

namespace DBLWD6.CustomORM.Services
{
    public static class Logger
    {
        private static ConsoleColor _defaultColor = Console.ForegroundColor;

        public static void LogQuery(string query, string operation)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n[SQL Query - {operation} - {DateTime.Now:yyyy-MM-dd HH:mm:ss}]");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(query);
            Console.ForegroundColor = _defaultColor;
            Console.WriteLine();
        }

        public static void LogError(string message, string query)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n[SQL Error - {DateTime.Now:yyyy-MM-dd HH:mm:ss}]");
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Query:");
            Console.WriteLine(query);
            Console.ForegroundColor = _defaultColor;
            Console.WriteLine();
        }

        public static void LogSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n[Success - {DateTime.Now:yyyy-MM-dd HH:mm:ss}]");
            Console.WriteLine(message);
            Console.ForegroundColor = _defaultColor;
            Console.WriteLine();
        }
    }
}
