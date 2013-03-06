
using ArmaServerInfo;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Trace.Listeners.Add(new ConsoleTraceListener());

            GameServerCollection serverCollection = GetServerList("ServerList.txt");            

            Console.WriteLine("Querying servers...");
            serverCollection.Update();
            foreach (var pair in serverCollection)
            {
                Console.WriteLine(pair.Value.ToString());
            }
            Console.WriteLine();
            Console.WriteLine("Press ENTER to exit...");
            Console.ReadLine();
        }

        static GameServerCollection GetServerList(string fileName)
        {
            if (String.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException("fileName");
            if (!File.Exists(fileName))
                return null;

            string[] fileLines = File.ReadAllLines(fileName);
            if (fileLines == null || fileLines.Length == 0)
                return null;
            GameServerCollection server = new GameServerCollection();
            foreach (var line in fileLines)
            {
                string[] lineParts = line.Split(';');
                server.Add(lineParts[0], new GameServer(lineParts[1], Int32.Parse(lineParts[2]), lineParts[0]));
            }
            return server;
        }
    }
}
