# Cadastro de Eventos .NET MAUI

Este projeto implementa um sistema de cadastro e gerenciamento de eventos simples, utilizando o **.NET MAUI** para desenvolvimento multiplataforma (Windows e Android) e seguindo as melhores práticas de arquitetura de software, com ênfase nos princípios **SOLID** e no padrão **MVVM (Model-View-ViewModel)**.

## 🧱 Arquitetura e Estrutura do Projeto

A solução foi estruturada para garantir a **Separação de Preocupações (SRP)**, dividindo a aplicação em camadas bem definidas.

| **Pasta** | **Conteúdo Principal** | **Função Arquitetural** |
| --- | --- | --- |
| **`Views`** | `EventListPage.xaml`, `CadastroEventoPage.xaml`, `ResumoEventoPage.xaml` | Camada de Apresentação (UI). Responsável apenas por exibir dados e capturar interação do usuário. |
| **`ViewModels`** | `EventListViewModel.cs`, `CadastroEventoViewModel.cs`, etc. | Camada de Lógica da View. Gerencia o estado da UI, comandos e orquestra a comunicação entre a View e os Services. |
| **`Models`** | `EventoModel.cs` | Camada de Domínio. Contém dados e a lógica de negócio **pura** (cálculos de `CustoTotal`, `DuracaoEmDias`). |
| **`Services`** | `IEventDataService.cs`, `EventDataService.cs`, `EventDatabase.cs` | Camada de Negócio e Persistência. Contém as interfaces e as implementações da lógica de acesso ao banco (CRUD). |
| **`Platforms`** | `FileService.cs` (por plataforma) | Código Específico de Plataforma. Implementa dependências que variam por sistema operacional (ex: caminho do arquivo DB). |

## 🎯 Princípios SOLID Aplicados

Os princípios SOLID garantem que o código seja limpo, flexível e fácil de manter:

- **S (Single Responsibility Principle - Princípio da Responsabilidade Única):**
    - **`EventoModel`** é responsável apenas por **dados e cálculos inerentes ao evento**.
    - **`EventDataService`** é responsável apenas pela **persistência (SQLite)** e **validação** (regras de negócio).
    - **`ViewModels`** são responsáveis apenas pela **lógica da tela** (comandos, estado da UI).
- **O (Open/Closed Principle - Princípio Aberto/Fechado):**
    - A lógica de CRUD é implementada na classe `EventDataService`, que pode ser estendida ou substituída (ex: mudar de SQLite para Firebase), mas a interface (`IEventDataService`) permanece inalterada.
- **L (Liskov Substitution Principle - Princípio da Substituição de Liskov):**
    - As implementações de serviços (como `EventDataService`) são completamente substituíveis pela sua interface (`IEventDataService`) sem que as ViewModels (consumidores) notem a diferença.
- **I (Interface Segregation Principle - Princípio da Segregação de Interfaces):**
    - Interfaces como `IEventDataService` são definidas com métodos específicos para dados de eventos (CRUD), evitando a criação de interfaces "gordas" e desnecessárias.
- **D (Dependency Inversion Principle - Princípio da Inversão de Dependência):**
    - As camadas de alto nível (`ViewModels`) não dependem de implementações concretas (`EventDataService` ou `EventDatabase`), mas sim de **abstrações** (`IEventDataService`). Isto é gerenciado via **Injeção de Dependência (DI)** no `MauiProgram.cs`.

---

## ⚙️ Persistência de Dados (SQLite)

O projeto utiliza o **SQLite** para armazenar dados localmente em ambas as plataformas (Windows e Android).

- **ORM Utilizado:** `sqlite-net-pcl` para mapear a classe `EventoModel` diretamente para uma tabela do banco.
- **Abstração de Plataforma:** O serviço **`IFileService`** é usado para abstrair o caminho do arquivo do banco (`eventos.db3`). Cada plataforma (Windows, Android) fornece uma implementação específica que retorna o caminho de armazenamento local correto, conforme exigido pelo sistema operacional.
- **CRUD Implementado:**
    - **Create/Update:** O método `SalvarEventoAsync` em `EventDataService` insere um novo evento (`Id == 0`) ou atualiza um existente (`Id != 0`).
    - **Read:** O método `GetAllEventsAsync` busca todos os eventos para a tela de listagem, e `GetEventByIdAsync` busca um único evento para edição.

---

## 📲 Funcionalidades CRUD e Navegação

O aplicativo suporta as seguintes operações em ambas as plataformas (Windows e Android):

| **Operação** | **Tela de Origem** | **Ação** | **Fluxo de Dados** |
| --- | --- | --- | --- |
| **Listagem (Read)** | `EventListPage` | Carrega todos os eventos do SQLite na inicialização (`Appearing` trigger). | `EventListViewModel` $\rightarrow$ `IEventDataService` $\rightarrow$ SQLite |
| **Cadastro (Create)** | `EventListPage` $\rightarrow$ `CadastroEventoPage` | Preenche o formulário e salva. O `Id` é `0`. | `CadastroEventoViewModel` $\rightarrow$ `SalvarEventoAsync` (Insert) |
| **Edição (Update)** | `EventListPage` (clique no item) $\rightarrow$ `CadastroEventoPage` | Navega com o `Id` do evento na URL (`IQueryAttributable` no ViewModel) e preenche os campos. | `CadastroEventoViewModel` $\rightarrow$ `GetEventByIdAsync` $\rightarrow$ `SalvarEventoAsync` (Update) |
| **Visualização (Read)** | `CadastroEventoPage` $\rightarrow$ `ResumoEventoPage` | Exibe o resumo do evento, incluindo as propriedades calculadas (`CustoTotal`, `DuracaoEmDias`). | Navegação do Shell com Serialização JSON do objeto. |



Link para o youtube do sistema funcionando:
https://www.youtube.com/watch?v=E-Oy4Wkvy4I