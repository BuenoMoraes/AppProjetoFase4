using AppProjetoFase4.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppProjetoFase4
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new ListagemMain();
            MainPage = new NavigationPage(new MainPage());

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
