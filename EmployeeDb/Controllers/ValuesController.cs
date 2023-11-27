using EmployeeDb.Context;
using EmployeeDb.Filters;
using EmployeeDb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        CompanyContext context ;

        public ValuesController(CompanyContext context) 
        { 
            this.context = context;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        // Exception filter Action method level
        [ExceptionFilMthdLevel]
        public IEnumerable<Employee> Get()
        {
            //throw exception method level
            //throw new Exception("this is error");
            return context.employees.ToList();
        }

        // GET api/<ValuesController>/5
        [HttpGet("Id")]
        // Action filter to return message in lower case
        [ActionFil]
        // Exception filter Action method level
        [ExceptionFilMthdLevel]
        public IActionResult Get([FromQuery]int id)
        {


                var i = (from x in context.employees where x.Id == id select x).FirstOrDefault();
                if(i==null)
                {
                    throw new Exception("User Not found");
                   // return NotFound("User Not Found");
                }
                return Ok(i);
            
        }




        // POST api/<ValuesController>
        [HttpPost]
        //Action filter to return message in lower case
        [ActionFil]
        public string  Post( Employee emp)
        {
            try
            {
                //var e=HttpContext.Items["empdata"];
                context.employees.Add(emp);
                context.SaveChanges();
                return "POSTED";
            }
            catch(Exception e)
            {
                return "Failed to Post";
            }
            
        }

        // PUT api/<ValuesController>/5
        [HttpPut]
        public string Put([FromQuery]int id, [FromBody] Employee emp)
        {
            try
            {
                var i = context.employees.Where(x => x.Id == id).FirstOrDefault();
                if (i == null)
                    return "User Not Found";
                i.Name = emp.Name;
                i.age = emp.age;
                i.Nationality = emp.Nationality;
                i.city = emp.city;
                context.SaveChanges();
                return "updated succesfully";
            }
            catch (Exception ex)
            {
                return "Failed";
            }
           
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete]
        //Action filter to return message in lower case
        [ActionFil]
        public string Delete(int id)
        {
            try
            {
                var i = context.employees.Where(x => x.Id == id).FirstOrDefault();
                context.employees.Remove(i);
                context.SaveChanges();
                return "Deleted";
            }
            catch(Exception e)
            {
                return "Failed to Delete";
            }
        }
        [HttpGet("error")]
        public string Error()
        {
            return "you Got an ERROR";
        }
    }
}
