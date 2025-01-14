﻿namespace NHSUKViewComponents.Web.ViewComponents
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using NHSUKViewComponents.Web.ViewModels;

    public class ErrorSummaryViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string[]? orderOfPropertyNames)
        {
            var errors = ViewData.ModelState
                .SelectMany(kvp => kvp.Value.Errors.Select(e => new ErrorSummaryListItem(kvp.Key, e.ErrorMessage)))
                .ToList();

            var groupingMetadata = ViewData["GroupedFormControlMetadata"] as Dictionary<string, bool>;

            foreach (var error in errors)
            {
                string key = error.Key;
                bool isGrouped = groupingMetadata != null && groupingMetadata.ContainsKey(key) && groupingMetadata[key];

                if (isGrouped)
                {
                    error.Key += "-0";
                }
            }

            var orderedErrors = GetOrderedErrors(errors, orderOfPropertyNames ?? new string[0]);

            var errorSummaryViewModel = new ErrorSummaryViewModel(orderedErrors);

            return View(errorSummaryViewModel);
        }

        private static List<ErrorSummaryListItem> GetOrderedErrors(
            List<ErrorSummaryListItem> errors,
            string[] orderOfPropertyNames)
        {
            return errors.OrderBy(e => orderOfPropertyNames.ToList().IndexOf(e.Key)).ToList();
        }
    }
}
