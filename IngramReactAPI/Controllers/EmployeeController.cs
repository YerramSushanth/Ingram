using IngramReactAPI.Database;
using IngramReactAPI.Database.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;


namespace IngramReactAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        DatabaseContext db;
        private readonly IWebHostEnvironment _env;
        public EmployeeController(IWebHostEnvironment env)
        {
            db = new DatabaseContext();
            _env = env;
        }
        [HttpGet]
        public JsonResult GetEmployees()
        {
            return new JsonResult(db.Employees.ToList());
        }

        [Route("InsertEmployee")]
        [HttpPost]
        public IActionResult InsertEmployee(Employee employee)
        {
            try
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, employee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateEmployee(Employee employee)
        {
            try
            {
                db.Employees.Update(employee);
                db.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, "Updated Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                db.Employees.Remove(db.Employees.Find(id));
                db.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, "Deleted Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + fileName;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(fileName);
            }
            catch (Exception ex)
            {
                return new JsonResult("anonymous.png " +ex.Message);
            }
        }
    }
}
