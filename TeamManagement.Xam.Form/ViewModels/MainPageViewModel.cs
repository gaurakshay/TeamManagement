using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using AsyncAwaitBestPractices.MVVM;
using Dotmim.Sync;
using Dotmim.Sync.Enumerations;
using Dotmim.Sync.Sqlite;
using Dotmim.Sync.Web.Client;
using SQLite;
using TeamManagement.Models;
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
            AddEmployee = new AsyncCommand(OnAddEmployee);
        }

        public IAsyncCommand SyncDB { get; }

        private async Task OnSyncRequestAsync()
        {
            var webClientOrchestrator = new WebClientOrchestrator("https://80b5c1cb41b4.ngrok.io/api/sync");

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, $"{nameof(TeamManagement)}.db");
            var clientProvider = new SqliteSyncProvider(databasePath);

            // Creating an agent that will handle all the process
            var agent = new SyncAgent(clientProvider, webClientOrchestrator);

            //if (!agent.Parameters.Contains("ID"))
            //{
            //    var employeeID = new Guid("3743D190-288C-4DE2-FDEF-08D85676F789");
            //    agent.Parameters.Add("ID", employeeID);
            //}

            try
            {
                // Launch the sync process
                var s1 = await agent.SynchronizeAsync(SyncType.Normal);
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
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "test.db");
            var db = new SQLiteAsyncConnection(databasePath);
            await db.CreateTableAsync<Employee>();
            var query = await db.Table<Employee>().ToListAsync();

            foreach (var emp in query)
                EmployeeName += $"{emp.FirstName} \n";
        }

        public IAsyncCommand AddEmployee { get; }

        private async Task OnAddEmployee()
        {
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "test.db");
            var db = new SQLiteAsyncConnection(databasePath);
            await db.CreateTableAsync<Employee>();
            var new_employee = new Employee
            {
                FirstName = "Wasssupppp",
                LastName = "nothhinngg",
                ID = new Guid()
            };
            await db.InsertAsync(new_employee);
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
