// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
try {
    Console.WriteLine("1");
    throw new ArgumentNullException("ex");
    Console.WriteLine("2");

}
catch (Exception ex) {
    Console.WriteLine(ex);
}
finally {
    Console.WriteLine("3");
}