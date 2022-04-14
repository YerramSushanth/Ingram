using IngramReactAPI.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using IngramReactAPI.Database.Entities;

namespace IngramReactAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        DatabaseContext db;
        public DepartmentController()
        {
            db = new DatabaseContext();
        }
        [HttpGet]
        public JsonResult GetDepartments()
        {
            //List(Department) dep = new List(Department)
            //{
            //    DepartmentId = 1,
            //    DepartmentName = "susha"
            //};

            return new JsonResult(db.Departments.ToList());
        }

        [HttpPost]
        public IActionResult InsertDepartment(Department department)
        {
            try
            {
                db.Departments.Add(department);
                db.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, department);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateDepartment(Department department)
        {
            try
            {
                db.Departments.Update(department);
                db.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, "Updated Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDepartment(int id)
        {
            try
            {
                db.Departments.Remove(db.Departments.Find(id));
                db.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, "Deleted Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
