using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyAPI.Models;

namespace MyAPI.Controllers
{
    public class StudentController : ApiController
    {

        //GET -Retrieve data

        public IHttpActionResult GetAllStudent()
        {
            IList<StudentDisplay> students = null;
            using (var x = new WebAPI_PeopleEntities())
            {
                students = x.Students
                    .Select(c => new StudentDisplay()
                    {
                        Id = c.id,
                        Name = c.name,
                        Email = c.email,
                        Country = c.country,
                        Phone = c.phone,
                        TempAdd = c.tempadd
                    }).ToList<StudentDisplay>();
            }
            if (students.Count == 0)
                return NotFound();
            return Ok(students);

        }
    }
}
