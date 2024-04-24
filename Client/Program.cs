using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Configuration;
using System.Net;
using System.Net.Sockets;

internal class Program
{
    private static void Main(string[] args)
    {
        //IPAddress address = Dns.GetHostEntry("localhost").AddressList[1];
        //string address = "127.0.0.1";

        string address = ConfigurationManager.AppSettings["serverAddress"]!;
        short port = short.Parse(ConfigurationManager.AppSettings["serverPort"]!);

        IPEndPoint serverPoint = new IPEndPoint(IPAddress.Parse(address), port);
        TcpClient client = new TcpClient();
        try
        {
            client.Connect(serverPoint);
            string msg = "";
            while (msg != "exit")
            {
                Console.Write("Enter your request: ");
                msg = Console.ReadLine();
                if (string.IsNullOrEmpty(msg))
                {
                    Console.WriteLine("> enter a data...\n");
                    continue;
                }

                NetworkStream ns = client.GetStream();
                StreamWriter sw = new StreamWriter(ns);
                sw.WriteLine(msg);
                sw.Flush();
                 
                StreamReader sr = new StreamReader(ns);
                string response = sr.ReadLine();
                if (!string.IsNullOrEmpty(response))
                    Console.WriteLine($">>> {response}");
                else
                    Console.WriteLine(">>> no item found");

                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally { client.Close(); }

    }

}