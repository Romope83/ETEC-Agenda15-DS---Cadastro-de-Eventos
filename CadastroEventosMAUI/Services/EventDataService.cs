// Services/EventDataService.cs
using CadastroEventosMAUI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using SQLite;

namespace CadastroEventosMAUI.Services
{
    public class EventDataService : IEventDataService
    {
        private readonly EventDatabase _database;

        public EventDataService(EventDatabase database)
        {
            _database = database;
        }

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

            return true;
        }

        public async Task<bool> SalvarEventoAsync(EventoModel evento)
        {
            try
            {
                int resultado;

                if (evento.Id != 0)
                {
                    resultado = await _database.Connection.UpdateAsync(evento);
                    Debug.WriteLine($"Evento '{evento.Nome}' atualizado. Linhas afetadas: {resultado}");
                }
                else
                {
                    resultado = await _database.Connection.InsertAsync(evento);
                    Debug.WriteLine($"Evento '{evento.Nome}' inserido. Linhas afetadas: {resultado}");
                }

                return resultado > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao salvar no banco de dados: {ex.Message}");
                Debug.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<bool> DeleteEventoAsync(int id)
        {
            try
            {
                // usa DeleteAsync<T>(id) para remover por PK
                var resultado = await _database.Connection.DeleteAsync<EventoModel>(id);
                Debug.WriteLine($"DeleteEventoAsync id={id}, linhas afetadas: {resultado}");
                return resultado > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao deletar evento id={id}: {ex.Message}");
                Debug.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<EventoModel?> GetEventoByIdAsync(int id)
        {
            try
            {
                var evento = await _database.Connection.FindAsync<EventoModel>(id);
                Debug.WriteLine(evento != null
                    ? $"GetEventoByIdAsync encontrou Id={id} Nome='{evento.Nome}'"
                    : $"GetEventoByIdAsync NÃO encontrou Id={id}");
                return evento;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao buscar evento por id: {ex.Message}");
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<List<EventoModel>> ListarEventosAsync()
        {
            var list = await _database.Connection.Table<EventoModel>().ToListAsync();
            Debug.WriteLine($"[EventDataService] ListarEventosAsync retorno: {list?.Count ?? 0} registros.");
            if (list != null)
            {
                foreach (var e in list)
                    Debug.WriteLine($"[EventDataService] Evento Id={e.Id} Nome='{e.Nome}'");
            }
            return list;
        }
    }
}

