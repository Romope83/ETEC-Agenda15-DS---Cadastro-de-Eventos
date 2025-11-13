using CadastroEventosMAUI.Views;

namespace CadastroEventosMAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ResumoEventoPage), typeof(ResumoEventoPage));
        }
    }
}
