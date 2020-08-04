using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BillTracker.BusinessLayer.Interface;
using BillTracker.Entities;

namespace BillTracker.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        /// <summary>
        /// create new user by submitting user details and send success message on response
        /// Post api/User/NewUser
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("NewUser")]
        [HttpPost]
        public async Task<ActionResult<String>> NewUser(User user)
        {
            try
            {
                var result ="";
                if (string.IsNullOrWhiteSpace(user.UserName))
                {
                    return BadRequest("Please enter UserName");
                }
                else if (string.IsNullOrWhiteSpace(user.Password))
                {
                    return BadRequest("Please enter Password");
                }
                else if (string.IsNullOrWhiteSpace(user.ConfirmPassword))
                {
                    return BadRequest("Please enter ConfirmPassword");
                }
                else if (string.IsNullOrWhiteSpace(user.Email))
                {
                    return BadRequest("Please enter Email");
                }
                else
                {
                     result =await _userService.RegisterNewUser(user);
                }
            
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
            
           

        }

        /// <summary>
        /// validate user present in db and send user details as response
        /// POST api/User/ValidateUser
        /// </summary>
        /// <param name="userCredentials"></param>
        /// <returns>User details</returns>
        [Route("ValidateUser")]
        [HttpPost]
        public async Task<ActionResult<User>> ValidateUser(UserLogin userCredentials)
        {
            try
            {
                var result =await _userService.VerifyUser(userCredentials);
                return result;
            }
            catch(Exception exception)
            {
                return BadRequest(exception.ToString());
            }
           
        }


    }
}
