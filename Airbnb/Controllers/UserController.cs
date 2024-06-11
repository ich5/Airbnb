﻿using Airbnb.Data;using Airbnb.Models;using Airbnb.Models.Airbnb.Models;using Microsoft.AspNetCore.Authorization;using Microsoft.AspNetCore.Mvc;namespace Airbnb.Controllers{    [Authorize(Policy = "BasicAuthentication")]    [ApiController]    [Route("[controller]")]    public class UserController : ControllerBase    {        private readonly DataContext _database;        public UserController()        {            string connectionString = "server=127.0.0.1; port=3306; database=airbnb; user=root; password= ";            _database = new DataContext(connectionString);        }        [HttpPut("{id}")]        public ActionResult Put(int id, [FromBody] User updatedUser)        {            var existingUser = _database.GetUserById(id);            if (existingUser == null)            {                return NotFound(); // User with the specified ID not found            }            existingUser.UserName = updatedUser.UserName;            existingUser.FirstName = updatedUser.FirstName;            existingUser.LastName = updatedUser.LastName;            existingUser.Email = updatedUser.Email;            existingUser.Password = updatedUser.Password;            existingUser.Type = updatedUser.Type;            _database.UpdateUser(existingUser);            return Ok("User updated successfully");        }        [HttpGet]        public ActionResult<IEnumerable<User>> Get()        {            var users = _database.GetUsers();            return Ok(users);        }        [AllowAnonymous] // Allow anonymous access for registration        [HttpPost]        public ActionResult Post([FromBody] User user)        {            if (user == null)            {                return BadRequest(new { message = "User data is null" });            }            try            {                _database.AddUser(user);                return StatusCode(201, new { message = "User added successfully" });            }            catch (Exception ex)            {                Console.WriteLine(ex.Message);                return StatusCode(500, new { message = "An error occurred while creating the user", error = ex.Message });            }        }        [HttpGet("{id}")]        public ActionResult<User> GetUserById(int id)        {            var user = _database.GetUserById(id);            if (user != null)            {                return Ok(user);            }            else            {                return NotFound(); // User with the specified ID not found            }        }        [HttpDelete("{id}")]        public ActionResult Delete(int id)        {            var existingUser = _database.GetUserById(id);            if (existingUser == null)            {                return NotFound(); // User with the specified ID not found            }            _database.DeleteUser(id);            return Ok("User deleted successfully");        }            }}