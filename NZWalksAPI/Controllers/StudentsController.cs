﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var students = new List<string>
            {
                "John Doe",
                "Jane Smith",
                "Alice Johnson"
            };
            return Ok(students);
        }
    }
}
