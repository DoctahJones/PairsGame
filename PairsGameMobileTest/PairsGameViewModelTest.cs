using Microsoft.VisualStudio.TestTools.UnitTesting;
using PairsGameMobile.Helper;
using PairsGameMobile.ViewModel;
using System.Collections.Generic;

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


    }
}
