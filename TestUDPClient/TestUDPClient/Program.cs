using System;
using System.Net.Sockets;
using TestUDPEntities;
using TestUDPUtils;

namespace TestUDPClient
{
    class Program
    {
        private const int Port = 11001;
        private const int RemotePort = 11000;
        
        static void Main(string[] args)
        {
            var input = "";
            UdpClient udpClient = new UdpClient();
            udpClient.Connect("127.0.0.1", RemotePort);
            while (!string.Equals(input, "EXIT", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Write any sentence to add it to the diary. Write \"Exit\" to exit:");
                
                input = Console.ReadLine();
                if (!string.Equals(input, "EXIT", StringComparison.InvariantCultureIgnoreCase))
                {
                    var diaryEntry = new DiaryEntry
                    {
                        Date = DateTime.UtcNow,
                        Text = input
                    };

                    var byteContent = SerializationUtils.ToByteArray(diaryEntry);
                    udpClient.Send(byteContent, byteContent.Length);
                    Console.Clear();
                }
            }
        }
    }
}