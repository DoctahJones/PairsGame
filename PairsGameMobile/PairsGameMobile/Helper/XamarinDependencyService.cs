using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PairsGameMobile.Helper
{
    public class XamarinDependencyService : IDependencyService
    {
        public T Get<T>() where T : class
        {
            return DependencyService.Get<T>();
        }
    }
}
