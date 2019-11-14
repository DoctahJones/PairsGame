using System;
using System.Collections.Generic;
using System.Text;

namespace PairsGameMobile.Helper
{
    public interface IImageSetFetcher
    {

        IEnumerable<string> GetImageFileNames(int count);
    }
}
