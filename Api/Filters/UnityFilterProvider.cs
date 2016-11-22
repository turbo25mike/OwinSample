using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.Practices.Unity;

namespace Api.Filters
{
    public class UnityFilterProvider : ActionDescriptorFilterProvider, IFilterProvider
    {
        private readonly IUnityContainer container;

        public UnityFilterProvider(IUnityContainer container)
        {
            this.container = container;
        }

        public new IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
        {
            var filters = base.GetFilters(configuration, actionDescriptor);

            foreach (var filter in filters)
            {
                container.BuildUp(filter.Instance.GetType(), filter.Instance);
            }

            return filters;
        }
    }
}