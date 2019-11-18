using PairsGameMobile.Helper;
using PairsGameMobile.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        /// <summary>
        /// Generates a random number greater than or equal to 0 but LESS THAN (not including) the passed in int.
        /// </summary>
        public Func<int, int> RandomNumberGenerator { get; set; }

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

        public PairsGameViewModel() : this(new XamarinDependencyService())
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


            RandomNumberGenerator = new Random().Next;
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
                    FrontShown = false,
                    TileBack = "turtleshell.jpg",
                    TileFront = imageNames[i / 2]
                });
            }
        }

        public void HideExistingTilesAndShuffle()
        {
            int itemsInList = PairsTileItems.Count;
            for (int i = 0; i < itemsInList; i++)
            {
                PairsTileItems.Move(i + RandomNumberGenerator(itemsInList - i), i);
                PairsTileItems[i].FrontShown = false;
            }
        }

        public void SelectionItemChanged()
        {
            if (IsBusy)
            {
                return;
            }
            IsBusy = true;
            if(SelectedTile != null)
            {
                SelectedTile.FrontShown = !SelectedTile.FrontShown;
                SelectedTile = null;
            }
            


            IsBusy = false;
        }

        public void NewGame()
        {

        }

    }
}
