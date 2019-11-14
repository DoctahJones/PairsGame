using System;
using System.Collections.Generic;
using System.Text;

namespace PairsGameMobile.Helper
{
    public interface IDependencyService
    {
        T Get<T>() where T : class;
    }
}
