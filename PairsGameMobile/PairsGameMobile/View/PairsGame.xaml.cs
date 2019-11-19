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
            CollectionView view = sender as CollectionView;
            var margin = view.Margin.Top + view.Margin.Bottom;
            double extraHeight = (view.Height - margin) - view.Width;
            if (extraHeight > 0)
            {
                GridLayout.VerticalItemSpacing = extraHeight / 4.5; //a bit more than 4(for 3 row gaps + bottom) to give some extra breathing room.
            }
        }

    }
}