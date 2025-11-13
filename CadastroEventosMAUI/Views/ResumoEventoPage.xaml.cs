
using CadastroEventosMAUI.ViewModels;

namespace CadastroEventosMAUI.Views
{
    public partial class ResumoEventoPage : ContentPage
    {
        // Usa injeção de dependência para obter o ViewModel
        public ResumoEventoPage(ResumoEventoViewModel viewModel)
        {
            InitializeComponent();
            this.BindingContext = viewModel;
        }
    }
}