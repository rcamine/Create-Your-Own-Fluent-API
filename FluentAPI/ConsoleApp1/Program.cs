namespace ConsoleApp1
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            //This is just a sample of creating your own FluentAPI, running it pointing to a fake db won't work.
            var connection = FluentSqlConnection
                .CreateConnection(config => config.TimeoutInSeconds = 60)
                .ForServer("localhost")
                .AndDatabase("mydb")
                .AsUser("rcamine")
                .WithPassword("mypass")
                .Connect();
        }
    }
}
