using CadastroEventosMAUI.Models;
using SQLite;
using System;
using System.Threading.Tasks;

namespace CadastroEventosMAUI.Services
{
    public class EventDatabase
    {
        private const string DatabaseFilename = "eventos.db3";

        private readonly SQLiteAsyncConnection _connection;

        private static bool _initialized = false;

        public EventDatabase(IFileService fileService)
        {
            string databasePath = fileService.GetDatabasePath(DatabaseFilename);

            _connection = new SQLiteAsyncConnection(databasePath);
            Task.Run(() => InitializeAsync()).Wait();

            //InitializeAsync().Wait();
        }

        private async Task InitializeAsync()
        {
            if (!_initialized)
            {
                if (_connection != null)
                {
                    await _connection.CreateTableAsync<EventoModel>();
                    _initialized = true;
                }
            }
        }

        public SQLiteAsyncConnection Connection => _connection;
    }
}