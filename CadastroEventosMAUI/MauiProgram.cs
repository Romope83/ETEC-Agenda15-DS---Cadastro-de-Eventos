using Microsoft.Extensions.Logging;

//using CadastroEventosMAUI.Models;
using CadastroEventosMAUI.Services;
using CadastroEventosMAUI.ViewModels;
using CadastroEventosMAUI.Views;

namespace CadastroEventosMAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // ----------------------------------------------------
            // 💡 REGISTRO DE DEPENDÊNCIAS (Injeção de Dependência)
            // ----------------------------------------------------

            // 1. Serviços (Transient ou Singleton, dependendo da necessidade. Transient é comum para services)
            // Usaremos IEventDataService para o DIP.
            builder.Services.AddTransient<IEventDataService, EventDataService>();

            // 2. ViewModels (Transient: uma nova instância para cada View)
            builder.Services.AddTransient<CadastroEventoViewModel>();
            builder.Services.AddTransient<ResumoEventoViewModel>();

            // 3. Views/Páginas (Transient: Uma nova instância para cada uso)
            builder.Services.AddTransient<CadastroEventoPage>();
            builder.Services.AddTransient<ResumoEventoPage>();

            // ----------------------------------------------------
            // FIM DO REGISTRO
            // ----------------------------------------------------

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
