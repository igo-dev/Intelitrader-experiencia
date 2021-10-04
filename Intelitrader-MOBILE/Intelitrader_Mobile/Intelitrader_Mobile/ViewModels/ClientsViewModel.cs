using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Extensions;
using Intelitrader_Mobile.Services;
using System.Diagnostics;
using System.Collections.ObjectModel;
using Intelitrader_Mobile.Views;
using Intelitrader_Mobile.Dtos;

namespace Intelitrader_Mobile.ViewModels
{
    class ClientsViewModel : ViewModelBase
    {
        public ObservableCollection<ClientModel> Clients { get; set; }
        private readonly IClientService _clientService;
        public ClientsViewModel(IClientService clientService)
        {
            Clients = new ObservableCollection<ClientModel>();
            _clientService = clientService;
        }


        private ClientModel selectedItem;
        public ClientModel SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged();
            }
        }


        private string searchInput;
        public string SearchInput
        {
            get => searchInput;
            set
            {
                searchInput = value;
                OnPropertyChanged();
            }
        }


        private Command getAll;
        public Command GetAll => getAll ?? (getAll = new Command(async () => await OnGetAll()));
        private async Task OnGetAll()
        {
            
            List<ClientModel> tmpClients;

            try
            {
                tmpClients = await _clientService.GetAll();
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
                IsBusy = false;
                await Application.Current.MainPage.DisplayToastAsync(ex.Message, 3000);
                return;
            }

            Clients.Clear();
            foreach (ClientModel item in tmpClients)
                Clients.Add(item);

            IsBusy = false;
        }


        private Command openDetail;
        public Command OpenDetail => openDetail ?? (openDetail = new Command(async () => await OnOpenClientsDetail()));
        private async Task OnOpenClientsDetail()
        {
            var viewModel = new ClientDetailViewModel(_clientService, SelectedItem);
            var page = new ClientDetailPage() { Title = "Detalhes"};
            page.BindingContext = viewModel;
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }


        private Command openCreateClient;
        public Command OpenCreateClient => openCreateClient ?? (openCreateClient = new Command(async () => await OnOpenCreateClient()));
        private async Task OnOpenCreateClient()
        {
            var viewModel = new CreateClientViewModel(_clientService);
            var page = new CreateClientPage() { Title = "Novo Cliente" };
            page.BindingContext = viewModel;
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }

        
        private Command search;
        public Command Search => search ?? (search = new Command(async () => await OnSearch()));
        private async Task OnSearch()
        {
            IsBusy = true;
            List<ClientModel> filteredClients;

            if (string.IsNullOrWhiteSpace(searchInput))
            {
                await OnGetAll();
                return;
            }

            try
            {
                filteredClients = await _clientService.Search(SearchInput);
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
                IsBusy = false;
                await Application.Current.MainPage.DisplayToastAsync(ex.Message, 3000);
                return;
            }

            Clients.Clear();

            foreach (ClientModel item in filteredClients)
                Clients.Add(item);

            IsBusy = false;
        }

    }
}
