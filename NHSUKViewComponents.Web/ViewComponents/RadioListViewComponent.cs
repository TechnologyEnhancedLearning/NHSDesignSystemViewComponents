﻿namespace NHSUKViewComponents.Web.ViewComponents
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using NHSUKViewComponents.Web.ViewModels;

    public class RadioListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(
            string aspFor,
            string label,
            IEnumerable<RadiosItemViewModel> radios,
            bool populateWithCurrentValues,
            string hintText,
            bool required,
            string? requiredClientSideErrorMessage = default,
            string cssClass = default
        )
        {
            var model = ViewData.Model;
            var property = model.GetType().GetProperty(aspFor);
            var errorMessages = ViewData.ModelState[property?.Name]?.Errors.Select(e => e.ErrorMessage) ??
                                new string[] { };

            var radiosList = radios.Select(
                r => new RadiosItemViewModel(
                    r.Value,
                    r.Label,
                    IsSelectedRadio(aspFor, r, populateWithCurrentValues),
                    r.HintText
                )
            );

            var viewModel = new RadiosViewModel(
                aspFor,
                label,
                string.IsNullOrEmpty(hintText) ? null : hintText,
                radiosList,
                errorMessages,
                required,
                string.IsNullOrEmpty(requiredClientSideErrorMessage) ? null : requiredClientSideErrorMessage,
                string.IsNullOrEmpty(cssClass) ? null : cssClass
            );

            return View(viewModel);
        }

        private bool IsSelectedRadio(string aspFor, RadiosItemViewModel radioItem, bool populateWithCurrentValue)
        {
            var model = ViewData.Model;
            var property = model.GetType().GetProperty(aspFor);
            var value = property?.GetValue(model);

            return populateWithCurrentValue && value != null && value.Equals(radioItem.Value);
        }
    }
}
