using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PairsGameMobile.Helper;
using Xamarin.Forms;

[assembly: Dependency(typeof(ImageSetFetcher))]
namespace PairsGameMobile.Helper
{

    public class ImageSetFetcher : IImageSetFetcher
    {

        private List<string> imageFiles = new List<string>();

        public ImageSetFetcher()
        {
            imageFiles.AddRange(new string[] {
                "angel.png",
                "crab.png",
                "fossil.jpg",
                "mech.jpg",
                "purpleorb.png",
                "redorb.png",
                "turtle.png",
                "wand.png"
            });
        }


        public IEnumerable<string> GetImageFileNames(int count)
        {
            if (count > imageFiles.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(count), $"Not enough files. Asked for {count} had {imageFiles.Count}.");
            }
            return imageFiles.Take(count);
        }
    }
}
