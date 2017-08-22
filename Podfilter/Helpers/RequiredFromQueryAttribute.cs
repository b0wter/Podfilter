using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace PodfilterWeb.Helpers
{
    public class RequiredFromQueryAttribute : FromQueryAttribute, IParameterModelConvention
    {   
        public void Apply(ParameterModel parameter)
        {
            if(parameter.Action.Selectors != null && parameter.Action.Selectors.Any())
            {
                parameter.Action.Selectors.Last().ActionConstraints.Add(new RequiredFromQueryActionConstraint(parameter.BindingInfo?.BinderModelName ?? parameter.ParameterName));
            }
        }
    }  
}