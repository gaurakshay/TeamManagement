using Dotmim.Sync;
using Dotmim.Sync.Sqlite;
using Dotmim.Sync.Web.Client;
using System;
using System.Threading.Tasks;

namespace DotmimSampleClient
{
    class Program
    {
        private static readonly string clientConnectionString = "client.db";
        private static readonly string syncAPIUri = "https://localhost:1013/api/sync";
        static async Task Main(string[] args)
        {
            Console.WriteLine("Press enter to start");
            Console.ReadLine();

            await SynchronizeAsync();
        }

        private static async Task SynchronizeAsync()
        {
            var serverProvider = new WebClientOrchestrator(syncAPIUri);
            var clientProvider = new SqliteSyncProvider(clientConnectionString);
            var agent = new SyncAgent(clientProvider, serverProvider);

            //var progress = new SynchronousProgress<ProgressArgs>(args => Console.WriteLine($"{args.Context}"));

            // Setup a filter on the team name.



            do
            {
                Console.Clear();
                //var result = await agent.SynchronizeAsync(progress);
                var result = await agent.SynchronizeAsync();

                Console.WriteLine(result);
            } while (Console.ReadKey().Key != ConsoleKey.Escape);

            Console.WriteLine("End");
        }
    }
}
