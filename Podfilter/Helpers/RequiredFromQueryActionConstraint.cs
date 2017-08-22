using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace PodfilterWeb.Helpers
{
    public class RequiredFromQueryActionConstraint : IActionConstraint
    {   
        // choose a high order to make this constraint run very late
        public int Order => 999;
        private readonly string parameter;

        public RequiredFromQueryActionConstraint(string parameter)
        {
            this.parameter = parameter;
        }

        public bool Accept(ActionConstraintContext context)
        {
            return context.RouteContext.HttpContext.Request.Query.ContainsKey(parameter);
        }
    } 
}