using System;
using System.Collections.Generic;
using System.Text;
using PairsGameMobile.Helper;
using PairsGameMobile.Model;
using PairsGameMobile.View;
using Xamarin.Forms;

[assembly: Dependency(typeof(GameFactory))]
namespace PairsGameMobile.Helper
{
    
    public class GameFactory : IGameFactory
    {


        public ContentPage CreateGamePage(GameInfo info)
        {
            switch(info.Name)
            {
                case "Shell Game":
                    return new PairsGame();
            }
            return null;
        }
    }
}
