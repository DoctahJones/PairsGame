using PairsGameMobile.Helper;
using PairsGameMobile.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PairsGameMobile.ViewModel
{
    public class GameBrowserViewModel : BaseViewModel
    {
        public ObservableCollection<GameInfo> GameDetails { get; }


        public GameBrowserViewModel()
        {
            Title = "Daves Games";

            this.GameDetails = new ObservableCollection<GameInfo>(GameDetailsCreator.GetGameDetails());


        }



    }
}
