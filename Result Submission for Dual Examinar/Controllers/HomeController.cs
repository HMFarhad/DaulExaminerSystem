using Result_Submission_for_Dual_Examinar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Result_Submission_for_Dual_Examinar.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (DualExaminerDBContext context = new DualExaminerDBContext())
                    {

                        string hashedData = ComputeSHA512Hash(model.Password);

                        User ifUserNameExist = context.Users.Where(x => x.UserName.Trim().ToLower() == model.UserName.Trim().ToLower() && x.Password == hashedData.Trim()).FirstOrDefault();

                        if (ifUserNameExist == null)
                        {

                            TempData["Message"] = "Invalid user name or password!";
                            return RedirectToAction("Index");
                        }
                        Session["UserName"] = ifUserNameExist.UserName;
                        Session["UserId"] = ifUserNameExist.Id;
                        Session["UserType"] = ifUserNameExist.Type;
                        return RedirectToAction("Index", "Course");
                    }
                }
                TempData["Message"] = "Please, provide valid data!";
                return View();
            }
            catch (Exception ex)
            {
                TempData["Message"] = "User may not exists or invalid user name or password!";
                return View();
            }
           
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User User)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (DualExaminerDBContext context = new DualExaminerDBContext())
                    {

                        User ifUserNameExist = context.Users.Where(x => x.UserName.Trim().ToLower() == User.UserName.Trim().ToLower()).FirstOrDefault();

                        if (ifUserNameExist != null)
                        {
                            TempData["Message"] = "User Name already taken. Please, try with another Name!";
                            return RedirectToAction("Register");
                        }

                        string hashedData = ComputeSHA512Hash(User.Password);
                        User.Password = hashedData;
                        User.Type = "teacher";
                        context.Users.Add(User);
                        context.SaveChanges();

                        TempData["Message"] = "Registered Successfully.";
                        return RedirectToAction("Index");
                    }
                }

                TempData["Message"] = "Please, provide valid infromation.";
                return View(User);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View(User);
            }
          
        }

        public ActionResult Logout()
        {
            Session["UserName"] = null;
            return RedirectToAction("Index", "Home");
        }

        public static string ComputeSHA512Hash(string rawData)
        {
            // Create a SHA512   
            using (SHA512 SHA512Hash = SHA512.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = SHA512Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}