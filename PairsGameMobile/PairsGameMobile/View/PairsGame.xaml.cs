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
    public partial class PairsGame : ContentPage
    {

        public PairsGame()
        {
            InitializeComponent();
        }

        private void CollectionView_SizeChanged(object sender, EventArgs e)
        {
            //todo possible make margins between rows resize dynamically so it fulls more of the screen.

            //Layout layout = sender as Layout;

            //cols = (int)Math.Round(layout.Width / MaxCellSize);
            //rows = (int)Math.Round(layout.Height / MaxCellSize);

            //if (cols * rows > MaxCellCount)
            //{
            //    cellSize = (int)Math.Sqrt((layout.Width * layout.Height) / MaxCellCount);
            //    cols = (int)(layout.Width / cellSize);
            //    rows = (int)(layout.Height / cellSize);
            //}
            //else
            //{
            //    cellSize = (int)Math.Min(layout.Width / cols, layout.Height / rows);
            //}

            //xMargin = (int)((layout.Width - cols * cellSize) / 2);
            //yMargin = (int)((layout.Height - rows * cellSize) / 2);

            //if (cols > 0 && rows > 0)
            //{
            //    lifeGrid.SetSize(cols, rows);
            //    UpdateLayout();
            //    UpdateLives();
            //}
        }
    }
}