using System;
using Microsoft.AspNetCore.Mvc;
using redditAPI.DataModels;


namespace redditAPI.Controllers
    {
        [ApiController]
        [Route("[controller]")]
        public class UserController : ControllerBase
        {
            private readonly FirestoreService firestoreService;

            public UserController(FirestoreService firestoreService)
            {
                this.firestoreService = firestoreService;
            }

            [HttpPost]
            public async Task<IActionResult> CreateUser([FromBody] User user)
            {
                await firestoreService.AddDocument("users", user);
                return Ok("User created successfully.");
            }

        }
    }


