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
using System.Collections.Generic;

namespace CadastroEventosMAUI.ViewModels
{
    public partial class CadastroEventoViewModel : ObservableObject, IQueryAttributable
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

        // Recebe query parameters quando a página é navegada: ?id=123
        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("id", out var idObj) && int.TryParse(idObj?.ToString(), out var id))
            {
                Debug.WriteLine($"[CadastroEventoVM] Recebido id={id}, carregando do banco...");
                var ev = await _eventDataService.GetEventoByIdAsync(id);
                if (ev != null)
                {
                    Evento = ev;
                    Debug.WriteLine($"[CadastroEventoVM] Evento carregado: Id={Evento.Id} Nome='{Evento.Nome}'");
                }
                else
                {
                    Debug.WriteLine($"[CadastroEventoVM] Nenhum evento encontrado para id={id}");
                }
            }
            else
            {
                Debug.WriteLine("[CadastroEventoVM] Nenhum id na query; mantendo novo Evento em branco.");
            }
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
                // Após salvar, volte para a lista (ou vá para o resumo se preferir)
                await Shell.Current.GoToAsync($"//{nameof(Views.EventListPage)}");
            }
            else
            {
                Debug.WriteLine("[CadastroEventoVM] Falha ao salvar evento. Verifique Output para detalhes.");
                await Shell.Current.DisplayAlert("Erro", "Falha ao salvar o evento. Tente novamente.", "OK");
            }
        }
    }
}
