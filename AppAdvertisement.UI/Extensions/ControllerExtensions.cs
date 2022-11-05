using AdvertisementApp.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace AppAdvertisement.UI.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult ResponseRedirectAction<T>(this Controller controller,IResponse<T> response,string actinonName,string controllerName)
        {
            if (response.ResponseType==ResponseType.NotFound)
            {
                return controller.NotFound();
            }
            if (response.ResponseType==ResponseType.ValidationError)
            {
                foreach (var item in response.ValidationErrors)
                {
                    controller.ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return controller.View(response.Data);
            }
            return controller.RedirectToAction(actinonName, controllerName);
        } 
        public static IActionResult ResponseView<T>(this Controller controller, IResponse<T> response)
        {
            if (response.ResponseType==ResponseType.NotFound)
            {
                return controller.NotFound();
            }
            return controller.View(response.Data);
        }
        public static IActionResult ResponseRedirectToAction(this Controller controller,IResponse response,string actionName,string controllerName)
        {
            if (response.ResponseType==ResponseType.NotFound)
            {
                return controller.NotFound();
            }
            return controller.RedirectToAction(actionName, controllerName);
        }



    }
}
