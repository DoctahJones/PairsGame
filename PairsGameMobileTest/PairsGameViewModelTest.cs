using Microsoft.VisualStudio.TestTools.UnitTesting;
using PairsGameMobile.Helper;
using PairsGameMobile.Model;
using PairsGameMobile.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PairsGameMobileTest
{
    [TestClass]
    public class PairsGameViewModelTest
    {
        private MockDependencyService dependencyService;

        [TestInitialize]
        public void TestInitialize()
        {
            dependencyService = new MockDependencyService();
            dependencyService.Register<IImageSetFetcher>(new ImageSetFetcher());
        }

        [TestMethod]
        public void TestFillGridWithTiles()
        {
            PairsGameViewModel vm = new PairsGameViewModel(dependencyService);
            var imageNames = new List<string> { "image1.jpg", "image2.jpg", "image3.jpg", "image4.jpg" };

            vm.FillTilesCollectionWithTiles(4, imageNames);


            Assert.IsNotNull(vm.PairsTileItems);
            Assert.AreEqual(8, vm.PairsTileItems.Count);
        }

        [TestMethod]
        public void TestHideTilesAndShuffleHidesTiles()
        {
            PairsGameViewModel vm = new PairsGameViewModel(dependencyService);
            vm.PairsTileItems[5].FrontShown = true;
            vm.RandomNumberGenerator = x => 0;  //change the random generator to not move tiles.


            vm.HideExistingTilesAndShuffle();

            Assert.IsFalse(vm.PairsTileItems[5].FrontShown);
        }

        [TestMethod]
        public void TestHideTilesAndShuffleRandomiseTilesWithoutAddingOrRemoving()
        {
            PairsGameViewModel vm = new PairsGameViewModel(dependencyService);
            int tilesCount = vm.PairsTileItems.Count;

            vm.HideExistingTilesAndShuffle();

            Assert.AreEqual(tilesCount, vm.PairsTileItems.Count);
        }

        [TestMethod]
        public async Task TestSelectionItemChangedDoesNothingWhenCalledWithNull()
        {
            PairsGameViewModel vm = new PairsGameViewModel(dependencyService)
            {
                SelectedTile = null
            };
            await vm.SelectionItemChangedAsync();

            foreach(var currTile in vm.PairsTileItems)
            {
                Assert.IsFalse(currTile.FrontShown);
            }
            Assert.AreEqual(null, vm.SelectedTile);
            Assert.AreEqual(false, vm.IsBusy);
        }

        [TestMethod]
        public async Task TestSelectionItemChangedShowsTileWhenSelected()
        {
            PairsGameViewModel vm = new PairsGameViewModel(dependencyService);
            vm.SelectedTile = vm.PairsTileItems[0];
            await vm.SelectionItemChangedAsync();


            Assert.IsTrue(vm.PairsTileItems[0].FrontShown);
        }

        [TestMethod]
        public async Task TestSelectionItemChangedClearsSelectedTile()
        {
            PairsGameViewModel vm = new PairsGameViewModel(dependencyService);
            vm.SelectedTile = vm.PairsTileItems[0];
            await vm.SelectionItemChangedAsync();


            Assert.IsNull(vm.SelectedTile);
        }

        [TestMethod]
        public async Task TestSelectionItemChangedDoesNothingWhenCalledSecondTimeSameTile()
        {
            PairsGameViewModel vm = new PairsGameViewModel(dependencyService);
            vm.SelectedTile = vm.PairsTileItems[0];
            await vm.SelectionItemChangedAsync();

            vm.SelectedTile = vm.PairsTileItems[0];
            await vm.SelectionItemChangedAsync();


            Assert.IsTrue(vm.PairsTileItems[0].FrontShown);
            for(int i = 1; i < vm.PairsTileItems.Count; i++)
            {
                Assert.IsFalse(vm.PairsTileItems[i].FrontShown);
            }
        }

        [TestMethod]
        public async Task TestSelectionItemChangedDoesntMatchMismatchedPair()
        {
            PairsGameViewModel vm = new PairsGameViewModel(dependencyService);
            vm.SelectedTile = vm.PairsTileItems[0];
            await vm.SelectionItemChangedAsync();

            Tile secondTile;
            if(String.Equals(vm.PairsTileItems[0].TileFront, vm.PairsTileItems[1].TileFront))
            {
                secondTile = vm.PairsTileItems[2];
            }
            else
            {
                secondTile = vm.PairsTileItems[1];
            }
            vm.SelectedTile = secondTile;
            await vm.SelectionItemChangedAsync();

            foreach(var currTile in vm.PairsTileItems)
            {
                Assert.IsFalse(currTile.FrontShown);
            }
        }

        [TestMethod]
        public async Task TestSelectionItemChangedMatchesPairs()
        {
            PairsGameViewModel vm = new PairsGameViewModel(dependencyService);
            vm.SelectedTile = vm.PairsTileItems[0];
            await vm.SelectionItemChangedAsync();

            Tile secondTile = null;
            for(int i = 1; i < vm.PairsTileItems.Count; i++)
            {
                if(String.Equals(vm.PairsTileItems[0].TileFront, vm.PairsTileItems[i].TileFront))
                {
                    secondTile = vm.PairsTileItems[i];
                    break;
                }
            }
            vm.SelectedTile = secondTile;
            await vm.SelectionItemChangedAsync();

            foreach (var currTile in vm.PairsTileItems)
            {
                if(currTile == vm.PairsTileItems[0] || currTile == secondTile)
                {
                    Assert.IsTrue(currTile.FrontShown);
                }
                else
                {
                    Assert.IsFalse(currTile.FrontShown);
                }
                
            }
        }




    }
}
