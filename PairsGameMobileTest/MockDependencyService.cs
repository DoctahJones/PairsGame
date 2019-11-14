using PairsGameMobile.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace PairsGameMobileTest
{
    public class MockDependencyService : IDependencyService
    {

        private readonly Dictionary<Type, object> registeredServices = new Dictionary<Type, object>();

        public T Get<T>() where T : class
        {
            return (T)registeredServices[typeof(T)];
        }

        public void Register<T>(object impl)
        {
            this.registeredServices[typeof(T)] = impl;
        }
    }
}
