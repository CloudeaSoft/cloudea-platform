namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
        string Serialize(IFoo obj)
        {
            return obj.Serialize();
        }
    }

    interface IFoo
    {
        string Serialize();
    }


    class Student : IFoo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Gender { get; set; }
        public string Class { get; set; }
        public string Serialize()
        {
            return "";
        }
    }
}