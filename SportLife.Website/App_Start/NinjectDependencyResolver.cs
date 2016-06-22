using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;

namespace SportLife.Website {
    public class NinjectDependencyResolver : IDependencyResolver {
        private IKernel _kernel;

        public NinjectDependencyResolver (IKernel kernelParam) {
            _kernel = kernelParam;
            AddBindings();
        }

        private void AddBindings () {
            throw new NotImplementedException();
        }

        public object GetService ( Type serviceType ) {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices ( Type serviceType ) {
            return _kernel.GetAll(serviceType);
        }
    }
}