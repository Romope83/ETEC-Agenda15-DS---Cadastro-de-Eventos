
using CadastroEventosMAUI.Models;
using System.Threading.Tasks;

namespace CadastroEventosMAUI.Services
{
    public interface IEventDataService
    {
        Task<bool> SalvarEventoAsync(EventoModel evento);

        bool ValidarEvento(EventoModel evento);
    }
}