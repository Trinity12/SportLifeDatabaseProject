using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SportLife.Core.Interfaces;

namespace SportLife.Website.Controllers {

    public class ControllerBase : Controller {
        private static readonly string State = "ModelState";
        protected IUnitOfWork UnitOfWork;

        public ControllerBase ( IUnitOfWork unitOfWork ) {
            UnitOfWork = unitOfWork;
        }

        /// <summary>
        ///	Adds errors to current model state from saved model state.
        /// </summary>
        protected void RestoreModelState () {
            var modelState = TempData[State] as ModelStateDictionary;
            if ( modelState != null ) {
                foreach ( var error in modelState.Values.SelectMany(value => value.Errors) )
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);
            }
        }

        /// <summary>
        ///	Saves model state for restoring it in another view.
        /// </summary>
        protected void SaveModelState () {
            TempData[State] = ModelState;
        }

        /// <summary>
        ///	Adds errors to current model state.
        /// </summary>
        /// <param name="errors">Errors to display.</param>
        protected void AddErrors ( IEnumerable<string> errors ) {
            if ( errors != null ) {
                foreach ( var error in errors )
                    ModelState.AddModelError(string.Empty, error);
            }
        }
    }
}