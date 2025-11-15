using System;
using Microsoft.Maui.Controls;
using CadastroEventosMAUI.ViewModels;

namespace CadastroEventosMAUI.Views
{
    public partial class ResumoEventoPage : ContentPage
    {
        // Usa injeção de dependência para obter o ViewModel
        public ResumoEventoPage(ResumoEventoViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private async void OnVoltarClicked(object sender, EventArgs e)
        {
            // Tenta voltar para a página anterior; se não houver página para voltar, navega para a rota EventListPage
            try
            {
                await Shell.Current.GoToAsync("..");
            }
            catch
            {
                await Shell.Current.GoToAsync($"/{nameof(EventListPage)}");
            }
        }
    }
}