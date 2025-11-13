using CadastroEventosMAUI.ViewModels;

namespace CadastroEventosMAUI
{
    public partial class CadastroEventoPage : ContentPage
    {
        int count = 0;

        public CadastroEventoPage(CadastroEventoViewModel viewModel)
        {
            InitializeComponent();
            this.BindingContext = viewModel;
        }

    }
}
