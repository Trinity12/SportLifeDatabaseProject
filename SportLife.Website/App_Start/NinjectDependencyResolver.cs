using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using SportLife.Core.Generic;
using SportLife.Core.Interfaces;

namespace SportLife.Website {
    public class NinjectDependencyResolver : IDependencyResolver {
        private IKernel _kernel;

        public NinjectDependencyResolver (IKernel kernelParam) {
            _kernel = kernelParam;
            AddBindings();
        }

        private void AddBindings () {
            _kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
        }

        public object GetService ( Type serviceType ) {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices ( Type serviceType ) {
            return _kernel.GetAll(serviceType);
        }
    }
}