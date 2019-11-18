using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace PairsGameMobile.Model
{
    [DebuggerDisplay("{TileFront} shown={FrontShown}")]
    public class Tile : INotifyPropertyChanged
    {
        public string TileBack { get; set; }

        public string TileFront { get; set; }


        private bool frontShown;
        public bool FrontShown
        {
            get => frontShown;
            set
            {
                if (value == frontShown)
                {
                    return;
                }
                frontShown = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CurrentImage));
            }
        }

        public TileType TileType { get; set; }

        public string CurrentImage
        {
            get
            {
                return frontShown ? TileFront : TileBack;
            }
        }

        public Tile()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public enum TileType
    {
        VALKYR, PURPLEORB, REDORB, WAND, TURTLE, MECH, CRAB, FOSSIL
    }

}
