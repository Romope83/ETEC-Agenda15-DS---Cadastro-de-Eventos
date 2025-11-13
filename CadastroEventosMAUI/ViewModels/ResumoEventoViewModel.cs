// ViewModels/ResumoEventoViewModel.cs

using CadastroEventosMAUI.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json;
using System.Web;
using System.Diagnostics;

namespace CadastroEventosMAUI.ViewModels
{
    // A interface IQueryAttributable permite que esta ViewModel receba dados durante a navegação
    public partial class ResumoEventoViewModel : ObservableObject, IQueryAttributable
    {
        // Propriedade para armazenar o objeto EventoModel recebido
        [ObservableProperty]
        private EventoModel _evento;

        public ResumoEventoViewModel()
        {
            Evento = new EventoModel();
        }

        // Método de interface chamado automaticamente quando a página é navegada
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            // 1. Verifica se o dicionário contém a chave 'data' (que enviamos do CadastroEventoViewModel)
            if (query.TryGetValue("data", out object eventJsonObj))
            {
                string encodedJson = eventJsonObj.ToString();

                try
                {
                    // 2. Decodifica a string URL (que estava codificada para passagem na URL)
                    string decodedJson = HttpUtility.UrlDecode(encodedJson);

                    // 3. Desserializa o JSON para o objeto EventoModel
                    EventoModel eventoRecebido = JsonSerializer.Deserialize<EventoModel>(decodedJson);

                    // 4. Atualiza a propriedade reativa 'Evento' com os dados recebidos
                    Evento = eventoRecebido;

                    Debug.WriteLine($"Dados do evento recebidos e aplicados: {Evento.Nome}");
                }
                catch (JsonException ex)
                {
                    Debug.WriteLine($"Erro ao desserializar JSON: {ex.Message}");
                    // Trate o erro, talvez voltando para a página anterior ou mostrando um alerta.
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Erro inesperado: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine("Nenhum dado 'data' encontrado na Query.");
            }
        }
    }
}