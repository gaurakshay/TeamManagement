using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using AsyncAwaitBestPractices.MVVM;
using Dotmim.Sync;
using Dotmim.Sync.Enumerations;
using Dotmim.Sync.Sqlite;
using Dotmim.Sync.Web.Client;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TeamManagement.Xam.Form.ViewModels
{
    public class MainPageViewModel : BindableObject
    {
        public MainPageViewModel()
        {
            SyncDB = new AsyncCommand(OnSyncRequestAsync);
            DisplayName = new AsyncCommand(OnDisplayRequest);
        }

        public IAsyncCommand SyncDB { get; }

        private async Task OnSyncRequestAsync()
        {
            SQLitePCL.Batteries_V2.Init();

            var webClientOrchestrator = new WebClientOrchestrator("https://8489b03a5d68.ngrok.io/api/sync");

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, $"{nameof(TeamManagement)}.db");
            var clientProvider = new SqliteSyncProvider(databasePath);

            // Creating an agent that will handle all the process
            var agent = new SyncAgent(clientProvider, webClientOrchestrator);

            if (!agent.Parameters.Contains("ID"))
            {
                var employeeID = new Guid("3743D190-288C-4DE2-FDEF-08D85676F789");
                agent.Parameters.Add("ID", employeeID);
            }

            try
            {
                // Launch the sync process
                var s1 = await agent.SynchronizeAsync(SyncType.Reinitialize);
                //var s1 = await agent.SynchronizeAsync();
                // Write results
                Debug.WriteLine(s1);

                Debug.WriteLine("End");
            }
            catch (Exception ex)
            {
                var st = ex.ToString();
                Debug.WriteLine(st);
            }
        }

        public IAsyncCommand DisplayName { get; }

        private async Task OnDisplayRequest()
        {
            // TODO Implement DB access.
            TeamName = "DB Read requested";
        }

        // property for the Employee name
        private string _employeeName;
        public string EmployeeName
        {
            get => _employeeName;
            set
            {
                if (value == _employeeName)
                    return;

                _employeeName = value;
                OnPropertyChanged(nameof(EmployeeName));
            }
        }

        // property for the Team name
        private string _teamName;
        public string TeamName
        {
            get => _teamName;
            set
            {
                if (value == _teamName)
                    return;

                _teamName = value;
                OnPropertyChanged(nameof(TeamName));
            }
        }
    }
}
