using PairsGameMobile.Helper;
using PairsGameMobile.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PairsGameMobile.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public IGameFactory GameFactory { get; set; }

        public MainPage()
        {
            InitializeComponent();
            GameFactory = DependencyService.Get<IGameFactory>();
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (!(e.SelectedItem is GameInfo info))
            {
                return;
            }
            await Navigation.PushAsync(GameFactory.CreateGamePage(info));

            ((ListView)sender).SelectedItem = null;
        }
    }
}