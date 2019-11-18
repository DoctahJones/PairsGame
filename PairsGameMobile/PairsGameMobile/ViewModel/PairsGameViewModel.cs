using PairsGameMobile.Helper;
using PairsGameMobile.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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
            HideExistingTilesAndShuffle();

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

        public async void SelectionItemChanged()
        {
            if (IsBusy || SelectedTile?.FrontShown == true)
            {
                SelectedTile = null;
                return;
            }
            IsBusy = true;
            if (SelectedTile != null)
            {
                var selected = SelectedTile;
                SelectedTile = null;
                selected.FrontShown = !selected.FrontShown;

                if (currentlySelectedItems[0] == null) //first of pair
                {
                    currentlySelectedItems[0] = selected;
                }
                else //second of pair
                {
                    currentlySelectedItems[1] = selected;
                    await HandlePairOfCards();
                }
                
            }
            IsBusy = false;
        }

        private async Task HandlePairOfCards()
        {
            bool correctPair = CheckIfTilesAreOfSameType(currentlySelectedItems);
            if (correctPair)
            {
                if (CheckIfAllTilesDisplayed())
                {
                    GameWon();
                }
                else
                {
                    currentlySelectedItems[0] = currentlySelectedItems[1] = null;
                }
            }
            else
            {
                await Task.Delay(delayToShowNonPairMilliseconds); //delay so user can see the pair are not matched.
                foreach (var currTile in currentlySelectedItems)
                {
                    currTile.FrontShown = false;
                }
                currentlySelectedItems[0] = currentlySelectedItems[1] = null;
            }
        }

        private bool CheckIfTilesAreOfSameType(Tile[] tiles)
        {
            if(tiles.Length < 2)
            {
                return true;
            }
            Tile first = tiles[0];
            for(int i = 1; i < tiles.Length; i++)
            {
                if (!String.Equals(tiles[i].TileFront, first.TileFront))
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckIfAllTilesDisplayed()
        {
            foreach(var currTile in PairsTileItems)
            {
                if(!currTile.FrontShown)
                {
                    return false;
                }
            }
            return true;
        }

        public void NewGame()
        {
            
        }

        public void GameWon()
        {

        }

    }
}
