namespace EfExample
{
    using System;

    internal class Program
    {
        static void Main(string[] args)
        {
            int v = MiFuncion(x => x + 10, () =>
            {
                Console.WriteLine("HOLA MUNDO!");
            });
        }

        private static int MiFuncion(Func<int,int> v, Action action)
        {
            action(); // action.Invoke();
            return v(33);
        }
    }
}
