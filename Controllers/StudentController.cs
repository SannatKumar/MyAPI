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

        //Post The data
        public IHttpActionResult PostNewStudent(StudentDisplay student)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Data. Please Recheck!");

            using (var x = new WebAPI_PeopleEntities())
            {
                x.Students.Add(new Student()
                {
                    name = student.Name,
                    email = student.Email,
                    country = student.Country,
                    phone = student.Phone,
                    tempadd = student.TempAdd
                });

                x.SaveChanges();
            }
            return Ok();
        }

        //PUT or Update the data

        public IHttpActionResult PutCustomer(StudentDisplay student)
        {
            if (!ModelState.IsValid)
                return BadRequest("This is invalid Model. Please recheck");

            using( var x = new WebAPI_PeopleEntities())
            {
                var checkExistingStudent = x.Students.Where(s => s.id == student.Id).FirstOrDefault<Student>();

                if (checkExistingStudent != null)
                {
                    checkExistingStudent.name = student.Name;
                    checkExistingStudent.country = student.Country;
                    checkExistingStudent.phone = student.Phone;
                    checkExistingStudent.tempadd = student.TempAdd;

                    x.SaveChanges();
                }
                else
                    return NotFound();
            }

            return Ok();
        }


        //Delete a record
    }
}
