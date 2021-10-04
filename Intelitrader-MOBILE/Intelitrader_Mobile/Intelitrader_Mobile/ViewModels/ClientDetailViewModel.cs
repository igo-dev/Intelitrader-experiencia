using Intelitrader_Mobile.Dtos;
using Intelitrader_Mobile.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace Intelitrader_Mobile.ViewModels
{
    class ClientDetailViewModel : ViewModelBase
    {
        private readonly IClientService _clientService;
        public ClientDetailViewModel(IClientService clientService, ClientModel selectedClient)
        {
            Client = selectedClient;
            _clientService = clientService;
            isEditing = false;
        }

        private ClientModel client;
        public ClientModel Client
        {
            get => client;
            set
            {
                client = value;
                OnPropertyChanged();
            }
        }

        private bool isEditing;
        public bool IsEditing
        {
            get => isEditing;
            set
            {
                isEditing = value;
                OnPropertyChanged();
            }
        }

        private Command deleteSingle;
        public Command DeleteSingle => deleteSingle ?? (deleteSingle = new Command(async () => await OnDeleteSingle()));
        private async Task OnDeleteSingle()
        {
            IsBusy = true;
            try
            {
                await _clientService.DeleteSingle(Client.Id);
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
                IsBusy = false;
                await Application.Current.MainPage.DisplayToastAsync(ex.Message, 3000);
                return;
            }

            await Application.Current.MainPage.Navigation.PopAsync();

            IsBusy = false;
        }

        private Command setEditMode;
        public Command SetEditMode => setEditMode ?? (setEditMode = new Command(async () => await OnSetEditMode()));
        private async Task OnSetEditMode()
        {
            await Task.Run(() => IsEditing = true);
        }

        private Command update;
        public Command Update => update ?? (update = new Command(async () => await OnUpdate()));
        private async Task OnUpdate()
        {
            IsBusy = true;
            try
            {
                await _clientService.Update(Client);
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
                IsBusy = false;
                IsEditing = false;
                await Application.Current.MainPage.DisplayToastAsync(ex.Message, 3000);
                return;
            }

            IsBusy = false;
            IsEditing = false;
        }

    }
}
