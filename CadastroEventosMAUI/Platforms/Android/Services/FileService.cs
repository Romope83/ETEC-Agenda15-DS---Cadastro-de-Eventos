
using CadastroEventosMAUI.Services;

namespace CadastroEventosMAUI.Platforms.Android.Services
{
    public class FileService : IFileService
    {
        public string GetDatabasePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}