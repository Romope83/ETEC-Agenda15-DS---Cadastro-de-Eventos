// ViewModels/EventListViewModel.cs

using CadastroEventosMAUI.Models;
using CadastroEventosMAUI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CadastroEventosMAUI.ViewModels
{
    public partial class EventListViewModel : ObservableObject
    {
        private readonly IEventDataService _dataService;

        // Lista de eventos que será exibida na UI
        [ObservableProperty]
        private ObservableCollection<EventoModel> _eventos = new();

        public EventListViewModel(IEventDataService dataService)
        {
            _dataService = dataService;
        }

        // Comando chamado quando a página é carregada
        [RelayCommand]
        private async Task LoadEventsAsync()
        {
            var events = await _dataService.ListarEventosAsync();
            // Limpa a lista existente e a preenche com os novos dados
            Eventos.Clear();
            if (events != null)
            {
                foreach (var e in events)
                {
                    Eventos.Add(e);
                }
            }

            Debug.WriteLine($"[EventListVM] Eventos carregados: {Eventos.Count}");
        }

        // Comando para o botão "Adicionar"
        [RelayCommand]
        private async Task AddEventAsync()
        {
            // Navega para a página de cadastro (rota registrada no Shell)
            await Shell.Current.GoToAsync("/CadastroEventoPage");
        }

        // Comando para editar um item selecionado
        [RelayCommand]
        private async Task EditEventAsync(EventoModel evento)
        {
            if (evento == null)
                return;

            // Navega para a página de cadastro, passando o ID do evento como parâmetro de consulta.
            // O CadastroEventoViewModel saberá buscar esse evento e preencher a tela para edição.
            await Shell.Current.GoToAsync($"/CadastroEventoPage?id={evento.Id}");
        }

        // Comando para deletar um evento e recarregar a lista
        [RelayCommand]
        private async Task DeleteEventAsync(EventoModel evento)
        {
            if (evento == null)
                return;

            bool confirm = await Shell.Current.DisplayAlert("Confirmar", $"Deseja excluir '{evento.Nome}'?", "Sim", "Não");
            if (!confirm)
                return;

            bool sucesso = await _dataService.DeleteEventoAsync(evento.Id);
            if (sucesso)
            {
                await LoadEventsAsync();
            }
            else
            {
                Debug.WriteLine($"[EventListVM] Falha ao deletar evento Id={evento.Id}");
                await Shell.Current.DisplayAlert("Erro", "Falha ao excluir o evento. Tente novamente.", "OK");
            }
        }
    }
}