using Microsoft.Maui.Controls;

namespace CadastroEventosMAUI.Views
{
    public partial class CadastroEventoPage : ContentPage
    {
        public CadastroEventoPage(ViewModels.CadastroEventoViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}