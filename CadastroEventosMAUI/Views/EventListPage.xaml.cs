namespace CadastroEventosMAUI.Views
{
    public partial class EventListPage : ContentPage
    {
        public EventListPage(ViewModels.EventListViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is ViewModels.EventListViewModel vm)
            {
                // chama o comando gerado pelo CommunityToolkit
                if (vm.LoadEventsCommand != null)
                {
                    await vm.LoadEventsCommand.ExecuteAsync(null);
                }
            }
        }
    }
}