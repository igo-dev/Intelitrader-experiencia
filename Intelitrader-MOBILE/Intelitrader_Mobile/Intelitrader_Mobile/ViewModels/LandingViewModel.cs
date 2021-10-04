using Intelitrader_Mobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Intelitrader_Mobile.ViewModels
{
    class LandingViewModel : ViewModelBase
    {

        private Command close;
        public Command Close => close ?? (close = new Command(async () => await OnClose()));
        private async Task OnClose()
        {
            await App.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
