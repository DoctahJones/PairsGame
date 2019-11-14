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
            var imageNames = new List<string> { "image1.jpg", "image2.jpg" , "image3.jpg", "image4.jpg" };
            
            vm.FillTilesCollectionWithTiles(4, imageNames);


            Assert.IsNotNull(vm.PairsTileItems);
            Assert.AreEqual(8, vm.PairsTileItems.Count);
        }
    }
}
