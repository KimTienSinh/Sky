﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkyBook.Models;
using SkyBook.Reponsitories;

namespace SkyBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private IAccountReponsitory _acccountRepo;

        public AccountsController(IAccountReponsitory repo) 
        {
            _acccountRepo = repo;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpModel signUpmodel)
        {
            var result = await _acccountRepo.SignUpAsync(signUpmodel);
            if(result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return Unauthorized();
        } 

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInModel signInModel)
        {
            var result = await _acccountRepo.SignInAsync(signInModel);
            if(string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }
            return Ok(result);
        }
    }
}