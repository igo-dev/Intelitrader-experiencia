using Intelitrader_Mobile.Dtos;
using Intelitrader_Mobile.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace Intelitrader_Mobile.ViewModels
{
    class CreateClientViewModel : ViewModelBase
    {
        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        private string sex;
        public string Sex
        {
            get => sex;
            set
            {
                sex = value;
                OnPropertyChanged();
            }
        }

        private string birthDate;
        public string BirthDate
        {
            get => birthDate;
            set
            {
                if (value == null || value.Length < 9)
                    return;

                birthDate = Convert.ToDateTime(value).ToString("yyyy/MM/dd");
                OnPropertyChanged();
            }
        }

        private readonly IClientService _clientService;
        public ObservableCollection<ClientModel> Clients { get; set; }
        public CreateClientViewModel(IClientService clientService)
        {
            _clientService = clientService;
            BirthDate = DateTime.Now.ToString();
        }

        private Command createSingle;
        

        public Command CreateSingle => createSingle ?? (createSingle = new Command(async () => await OnCreateSingle()));
        private async Task OnCreateSingle()
        {
            try
            {
                ClientModel createClientDto = new ClientModel(Name,BirthDate,Sex);

                await _clientService.CreateSingle(createClientDto);
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
    }
}
