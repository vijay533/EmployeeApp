using EmployeeDb.Context;
using EmployeeDb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Text;

namespace EmployeeDb.Filters
{
    public class ActionFil :ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //var emp = context.ActionArguments["emp"] as Employee;
            //emp.age = 30;
            //context.HttpContext.Items["empdata"] = emp;

            Debug.WriteLine("Method executing...");
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {


                ObjectResult objectResult= (ObjectResult)context.Result;
                var t = objectResult.Value;
                if (t is string)
                {
                    var s = objectResult.Value?.ToString().ToLower();
                    objectResult.Value = s;
                    Debug.WriteLine(s);
                }
            else
            {
                Type type = t.GetType();
                if(type is Employee)
                {
                    foreach (var t2 in type.GetProperties())
                    {
                        if (t2.GetValue(t) is string)
                        {
                            string mstr = t2.GetValue(t).ToString().ToLowerInvariant();
                            t2.SetValue(t, mstr);
                        }
                        Debug.WriteLine(t2.Name + " " + t2.GetValue(t));
                    }
                }
               
            }
               
        }
    }
}
