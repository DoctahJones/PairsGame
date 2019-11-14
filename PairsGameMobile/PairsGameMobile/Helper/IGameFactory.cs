using PairsGameMobile.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PairsGameMobile.Helper
{
    public interface IGameFactory
    {
        ContentPage CreateGamePage(GameInfo info);
    }

}
