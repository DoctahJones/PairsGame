using PairsGameMobile.Model;
using PairsGameMobile.View;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PairsGameMobile.Helper
{
    public class GameDetailsCreator
    {
        public static IEnumerable<GameInfo> GetGameDetails()
        {
            List<GameInfo> details = new List<GameInfo>
            {
                new GameInfo
                {
                    Name = "Shell Game",
                    GameIcon = "turtleshell.jpg",
                    Description = "Match the Items!"
                }
            };
            ;

            return details;
        }

    }
}
