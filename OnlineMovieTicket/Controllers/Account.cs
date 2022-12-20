using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineMovieTicket.Data;
using OnlineMovieTicket.Data.Static;
using OnlineMovieTicket.Data.ViewModels;
using OnlineMovieTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMovieTicket.Controllers
{
    public class Account : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDBContext _context;

        public Account(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,ApplicationDBContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        //List of All Users SignUp in This applicationUser and Identity
        public async Task<IActionResult> AllUsers()
        {
            var allUser = await _context.Users.ToListAsync();
            return View(allUser);
        }

        //Get: Account/Login
        public IActionResult Login()
        {
            var Response = new LoginVM();
            return View(Response);
        }
        [HttpPost]
        public  async Task<IActionResult> Login(LoginVM loginVM)
        {//when  not valid then go login page
            if (!ModelState.IsValid) return View(loginVM);
            //checking userEmail and password
            var user =await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if(user != null)
            {
                var passwordCheck =await _userManager.CheckPasswordAsync(user, loginVM.Password);
                //if passwordCheck is true then signIn using the signInManager
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    //if resultsucceed return user to home page 
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index","Movies");
                    }
                    //when  password not matched then
                    TempData["Error"] = "Incorrect Credential, Try again with Correct Email and Password*";
                    return View(loginVM);
                }
            }
            //when name and password not matched then
            TempData["Error"] = "Incorrect Credential, Try again with Correct Email and Password*";
            return View(loginVM);
        }
        //Get: Account/Register
        public IActionResult Register() {
            var response = new RegisterVM();
            return View(response); 
        }
        //Post:
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            //when  not valid then go Register page
            if (!ModelState.IsValid) return View(registerVM);

            //finding if data exist in database
            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            //Checking if that user already  exist in database 
            if (user != null)
            {
                //when Email already in db then
                TempData["Error"] ="User with this Email Already exist!";
                return View(registerVM);
            }
            //Puuting some data to ApplicationUser object from registerVM
            var newUser = new ApplicationUser()
            {
                FullName = registerVM.FullName,
                UserName=registerVM.EmailAddress,
                Email=registerVM.EmailAddress,               
            };
            //Saving Password in db  through ApplicationUser.cs _userManager 
            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);
            if (newUserResponse.Succeeded)
            {//Saving the role 'User' through Data/Static/UserRoles.cs 
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }
             return View("Registered");           
            }
        //Get: Account/RegisterCompleted
        public IActionResult RegisterCompleted() => View("Registered");

        //Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movies");
        }
        //AccessDenied
        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }

    }
 }
