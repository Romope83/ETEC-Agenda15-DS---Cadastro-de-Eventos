using CadastroEventosMAUI.Services;
using CadastroEventosMAUI.ViewModels;
using CadastroEventosMAUI.Views;
using CommunityToolkit.Maui;

namespace CadastroEventosMAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // serviços
            // Remover inscrições duplicadas; registre EventDatabase e IEventDataService apenas uma vez
            builder.Services.AddSingleton<EventDatabase>();
            builder.Services.AddSingleton<IEventDataService, EventDataService>();

            // ViewModels e Views (ajuste conforme sua intenção)
            builder.Services.AddTransient<CadastroEventoViewModel>();
            builder.Services.AddTransient<ResumoEventoViewModel>();

            builder.Services.AddTransient<CadastroEventoPage>();
            builder.Services.AddTransient<ResumoEventoPage>();

            // Use singleton apenas se você quiser um VM única para toda a aplicação
            builder.Services.AddSingleton<EventListViewModel>();
            builder.Services.AddSingleton<EventListPage>();

            // File services por plataforma (já existentes no seu código)
#if ANDROID
            builder.Services.AddSingleton<IFileService, Platforms.Android.Services.FileService>();
#elif WINDOWS
            builder.Services.AddSingleton<IFileService, Platforms.Windows.Services.FileService>();
#endif

            return builder.Build();
        }
    }
}