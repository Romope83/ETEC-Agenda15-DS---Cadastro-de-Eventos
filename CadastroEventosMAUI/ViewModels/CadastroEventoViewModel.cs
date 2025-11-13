// ViewModels/CadastroEventoViewModel.cs

using CadastroEventosMAUI.Models;
using CadastroEventosMAUI.Services;
using CadastroEventosMAUI.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Text.Json;
using System.Web;

namespace CadastroEventosMAUI.ViewModels
{
    public partial class CadastroEventoViewModel : ObservableObject
    {
        private readonly IEventDataService _eventDataService;

        private EventoModel _evento;

        public CadastroEventoViewModel(IEventDataService eventDataService)
        {
            _eventDataService = eventDataService;
            _evento = new EventoModel();
        }

        public EventoModel Evento
        {
            get => _evento;
            set => SetProperty(ref _evento, value);
        }


        [RelayCommand]
        private async Task SalvarEventoAsync()
        {
            if (!_eventDataService.ValidarEvento(Evento))
            {
                await Shell.Current.DisplayAlert("Erro de Validação", "Verifique se o nome está preenchido e as datas estão corretas.", "OK");
                return;
            }

            bool sucesso = await _eventDataService.SalvarEventoAsync(Evento);

            if (sucesso)
            {
                string eventoJson = JsonSerializer.Serialize(Evento);
                string encodedJson = System.Web.HttpUtility.UrlEncode(eventoJson);

                string rotaResumo = nameof(ResumoEventoPage);

                await Shell.Current.GoToAsync($"{rotaResumo}?data={encodedJson}");

                Debug.WriteLine("Navegação para a página de resumo com dados completos solicitada.");
            }
            else
            {
                await Shell.Current.DisplayAlert("Erro", "Falha ao salvar o evento. Tente novamente.", "OK");
            }
        }
    }
}
