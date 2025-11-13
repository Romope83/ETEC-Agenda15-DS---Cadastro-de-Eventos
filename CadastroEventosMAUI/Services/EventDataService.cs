
using CadastroEventosMAUI.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CadastroEventosMAUI.Services
{
    public class EventDataService : IEventDataService
    {
        /// <summary>
        /// Valida as regras de negócio do Evento.
        /// </summary>
        /// <param name="evento">O modelo do evento a ser validado.</param>
        /// <returns>Verdadeiro se o evento for válido; Falso caso contrário.</returns>
        public bool ValidarEvento(EventoModel evento)
        {

            if (string.IsNullOrWhiteSpace(evento.Nome))
            {
                Debug.WriteLine("Erro de Validação: Nome do evento é obrigatório.");
                return false;
            }

            if (evento.DataTermino < evento.DataInicio)
            {
                Debug.WriteLine("Erro de Validação: Data de término não pode ser anterior à data de início.");
                return false;
            }

            if (evento.DuracaoEmDias < 0)
            {
                Debug.WriteLine("Erro de Validação: Duração negativa (datas inválidas).");
                return false;
            }


            return true;
        }

        /// <summary>
        /// Simula a persistência assíncrona dos dados do evento.
        /// </summary>
        /// <param name="evento">O modelo do evento a ser salvo.</param>
        /// <returns>Verdadeiro se a operação for concluída com sucesso.</returns>
        public Task<bool> SalvarEventoAsync(EventoModel evento)
        {

            Debug.WriteLine("Simulando salvamento de dados...");

            Debug.WriteLine($"Evento Salvo: {evento.Nome} | Custo Total: {evento.CustoTotal:C} | Duração: {evento.DuracaoEmDias} dias");

            return Task.FromResult(true);
        }
    }
}