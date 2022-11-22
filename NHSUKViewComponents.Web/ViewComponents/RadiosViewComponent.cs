namespace NHSUKViewComponents.Web.ViewComponents
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using NHSUKViewComponents.Web.Enums;
    using NHSUKViewComponents.Web.ViewModels;

    public class RadiosViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(
            string aspFor,
            string label,
            IEnumerable<RadiosListItemViewModel> radios,
            bool populateWithCurrentValues,
            string? hintText,
            bool required
        )
        {
            var radiosList = radios.Select(
                r => new RadiosItemViewModel(
                    r.Enumeration.Name,
                    r.Label,
                    IsSelectedRadio(aspFor, r.Enumeration, populateWithCurrentValues),
                    r.HintText
                )
            );

            var viewModel = new RadiosViewModel(
                aspFor,
                label,
                string.IsNullOrEmpty(hintText) ? null : hintText,
                radiosList,
                required
            );

            return View(viewModel);
        }

        private bool IsSelectedRadio(string aspFor, Enumeration radioItem, bool populateWithCurrentValue)
        {
            var model = ViewData.Model;

            var property = model.GetType().GetProperty(aspFor);
            var value = (Enumeration)property?.GetValue(model)!;

            return populateWithCurrentValue && value.Equals(radioItem);
        }
    }
}
