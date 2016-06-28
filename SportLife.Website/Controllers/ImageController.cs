using System.Drawing;
using System.Web.Mvc;
using SportLife.Core.Interfaces;
using SportLife.Website.Helpers.Converters;

namespace SportLife.Website.Controllers
{
    public class ImageController : Controller
    {
        private IUnitOfWork _unitOfWork;

        private IUnitOfWork UnitOfWork
           => _unitOfWork ?? (_unitOfWork = DependencyResolver.Current.GetService<IUnitOfWork>());

        // GET: Image
        public ActionResult Index(int id)
        {
            var fileToRetrieve = UnitOfWork.ImageRepository.Get(id);
            if (fileToRetrieve == null)
            {
                var image = new System.Drawing.Bitmap(Server.MapPath("~/Media/Images/default.png"));
                return File(ImagePathConverter.ImageToByteArray(image), image.GetType().ToString());
            }
            return File(fileToRetrieve.Content, fileToRetrieve.ContentType);
        }

        #region Properties

        #endregion
    }
}