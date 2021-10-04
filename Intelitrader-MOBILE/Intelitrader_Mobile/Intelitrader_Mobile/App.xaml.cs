using Intelitrader_Mobile.Services;
using Intelitrader_Mobile.ViewModels;
using Intelitrader_Mobile.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xamarin.Forms;

namespace Intelitrader_Mobile
{
    public partial class App : Application
    {
        protected static IServiceProvider ServiceProvider { get; set; }
        private void SetupServices()
        {
            var services = new ServiceCollection();

            // Registrando DI
            services.AddTransient<ClientsViewModel>();
            services.AddSingleton<IClientService, ClientService>();

            ServiceProvider = services.BuildServiceProvider();
        }
        public static ViewModelBase GetViewModel<TViewModel>()
        where TViewModel : ViewModelBase
        {
            return ServiceProvider.GetService<TViewModel>();
        }

        public App()
        {
            InitializeComponent();
            SetupServices();
            MainPage = new NavigationPage( new ClientsPage() { Title = "Inteli-CRUD" } );
            var page = new LandingPage() { BindingContext = new LandingViewModel()};
            MainPage.Navigation.PushModalAsync(page);
        }
        
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
