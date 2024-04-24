using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
public class CarCodeDB : DbContext
{
    public DbSet<CarCode> Info { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB; " +
                                      "Initial Catalog=CarCode;" +
                                      "Integrated Security=True;" +
                                      "Connect Timeout=2;";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
    public string Seeker(string msg)
    {
        var db = new CarCodeDB();
        var item = db.Info.FirstOrDefault(c => c.Code == msg || c.Region == msg);
        string response = item != null ? $"{item.Code} - {item.Region}" : "no item found";
        return response;
    }

}

internal class Program
{
    private static void Main(string[] args)
    {
        IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
        TcpListener listener = new TcpListener(ipPoint);
        try
        {
            listener.Start();
            Console.WriteLine("Server connected . . .\n");

            TcpClient client = listener.AcceptTcpClient();
            var db = new CarCodeDB();

            while (client.Connected)
            {
                NetworkStream ns = client.GetStream();
                StreamReader sr = new StreamReader(ns);
                string given = sr.ReadLine();
                Console.WriteLine($"{client.Client.RemoteEndPoint} - {given} at {DateTime.Now.ToShortTimeString()}");

                var res = db.Seeker(given);

                StreamWriter sw = new StreamWriter(ns);
                sw.WriteLine(res);
                sw.Flush();
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally { listener.Stop(); }


    }
   
}