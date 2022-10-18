﻿namespace NHSUKViewComponents.Web.ViewComponents
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using NHSUKViewComponents.Web.Model.SearchSortFilterPaginate;
    using NHSUKViewComponents.Web.ViewModels;

    public class BackLinkWithReturnPageQueryViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(
            string aspController,
            string aspAction,
            ReturnPageQuery returnPageQuery,
            Dictionary<string, string> routeData,
            string linkText
        )
        {
            var aspAllRouteData = routeData != null
                ? routeData.Concat(returnPageQuery.ToRouteDataDictionary())
                    .ToDictionary(x => x.Key, x => x.Value)
                : returnPageQuery.ToRouteDataDictionary();
            return View(new LinkViewModelWithFragment(
                aspController,
                aspAction,
                linkText,
                aspAllRouteData,
                returnPageQuery.ItemIdToReturnTo
            ));
        }
    }
}
