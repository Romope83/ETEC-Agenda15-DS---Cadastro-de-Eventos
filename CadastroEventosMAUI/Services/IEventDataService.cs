using CadastroEventosMAUI.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CadastroEventosMAUI.Services
{
    public interface IEventDataService
    {
        Task<bool> SalvarEventoAsync(EventoModel evento);
        Task<List<EventoModel>> ListarEventosAsync();

        bool ValidarEvento(EventoModel evento);

        Task<EventoModel?> GetEventoByIdAsync(int id);
        Task<bool> DeleteEventoAsync(int id);
    }
}