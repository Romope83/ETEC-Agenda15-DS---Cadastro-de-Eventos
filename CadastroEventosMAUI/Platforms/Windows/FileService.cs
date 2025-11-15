
using CadastroEventosMAUI.Services;
using System;
using System.IO;

namespace CadastroEventosMAUI.Platforms.Windows.Services
{
    public class FileService : IFileService
    {
        public string GetDatabasePath(string filename)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), filename);
            return path;
        }
    }
}