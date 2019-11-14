using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PairsGameMobile.Model
{
    public class Tile
    {
        public string TileBack { get; set; }

        public string TileFront { get; set; }

        public bool FrontShown { get; set; }

        public TileType TileType { get; set; }

        public string CurrentImage
        {
            get
            {
                return FrontShown ? TileFront : TileBack;
            }
        }

        public Tile()
        {
        }
    }

    public enum TileType
    {
        VALKYR, PURPLEORB, REDORB, WAND, TURTLE, MECH, CRAB, FOSSIL
    }

}
