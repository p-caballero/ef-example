namespace EfExample.Infrastructure.Extensions
{
    using System;

    public static class DbContextOptionsBuilderExtension
    {
        public static void PrintConnectionString(this string text)
        {
            //Console.WriteLine(string.Format("Connection string: {0}", text));
            //Console.WriteLine(string.Format(CultureInfo.CurrentCulture, "Connection string: {0}", text));
            Console.WriteLine($"Connection string: {text}");
        }
    }
}
