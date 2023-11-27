//using Microsoft.AspNetCore.Mvc;
using EmployeeDb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
//using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace EmployeeDb.Filters
{
    //[AttributeUsage(AttributeTargets.Method,AllowMultiple =true)]
    //application level Exception filter
    //- used in JwtClaimscontroller
    public class ExceptionFil:Attribute, IExceptionFilter
    {

        public void OnException(Microsoft.AspNetCore.Mvc.Filters.ExceptionContext context)
        {
            Debug.WriteLine(context.Exception);
            context.Result = new RedirectToActionResult("error","values",null);
        }
    }

    //controller level Exception filter
    //-used in JwtController
    [AttributeUsage(AttributeTargets.Class)]
    public class ExceptionFilCtrlLevel : Attribute, IExceptionFilter
    {

        public void OnException(Microsoft.AspNetCore.Mvc.Filters.ExceptionContext context)
        {
            Debug.WriteLine(context.Exception);
            context.Result = new RedirectToActionResult("error", "values", null);
        }
    }

    //method level Exception filter
    //-used in values controller
    public class ExceptionFilMthdLevel : Attribute, IExceptionFilter
    {

        public void OnException(Microsoft.AspNetCore.Mvc.Filters.ExceptionContext context)
        {
            Debug.WriteLine(context.Exception);
            context.Result = new RedirectToActionResult("error", "values", null);
        }
    }
}
