using AppProjetoFase4.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppProjetoFase4.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListagemViewLivro : ContentPage
    {
        public ListagemViewModel ViewModel { get; set; }
        public ListagemViewLivro()
        {
            InitializeComponent();
            this.ViewModel = new ListagemViewModel();
            this.BindingContext = this.ViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await this.ViewModel.GetLivros();

        }

        private void ButtonUsuarios_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListagemViewUsuario());
        }
    }
}