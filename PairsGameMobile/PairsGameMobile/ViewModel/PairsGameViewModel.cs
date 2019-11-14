using PairsGameMobile.Helper;
using PairsGameMobile.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace PairsGameMobile.ViewModel
{
    public class PairsGameViewModel : BaseViewModel
    {
        private readonly IDependencyService dependencyService;

        /// <summary>
        /// The length of time to display the tiles to the user before 
        /// </summary>
        private int delayToShowNonPairMilliseconds;

        private Tile[] currentlySelectedItems;

        public IImageSetFetcher TileImageSetFetcher { get; set; }

        public ObservableCollection<Tile> PairsTileItems { get; }

        public Command NewGameCommand { get; }

        public Command SelectedTileChangedCommand { get; }

        private Tile selectedTile;
        public Tile SelectedTile
        {
            get => selectedTile;
            set
            {
                if (selectedTile == value)
                {
                    return;
                }
                selectedTile = value;
                OnPropertyChanged();
            }
        }

        public PairsGameViewModel(): this(new XamarinDependencyService())
        {

        }

        public PairsGameViewModel(IDependencyService depService)
        {
            this.dependencyService = depService;

            Title = "Pairs";
            PairsTileItems = new ObservableCollection<Tile>();

            delayToShowNonPairMilliseconds = 1000;
            currentlySelectedItems = new Tile[2];

            TileImageSetFetcher = dependencyService.Get<IImageSetFetcher>();
            FillTilesCollectionWithTiles(8, TileImageSetFetcher.GetImageFileNames(8).ToList());

            NewGameCommand = new Command(() => NewGame());
            SelectedTileChangedCommand = new Command(() => SelectionItemChanged());
        }


        public void FillTilesCollectionWithTiles(int pairs, List<string> imageNames)
        {
            if (imageNames.Count < pairs)
            {
                throw new ArgumentException("There must be at least as many images as pairs.", nameof(imageNames));
            }
            this.PairsTileItems.Clear();
            for (int i = 0; i < pairs * 2; i++)
            {
                PairsTileItems.Add(new Tile
                {
                    FrontShown = true,
                    TileBack = "turtleshell.jpg",
                    TileFront = imageNames[i / 2]
                });
            }
        }

        public void SelectionItemChanged()
        {
            if (IsBusy)
            {
                return;
            }
            IsBusy = true;

            //dostuff


            IsBusy = false;
        }

        public void NewGame()
        {

        }

    }
}
