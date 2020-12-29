using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Result_Submission_for_Dual_Examinar.Models;

namespace Result_Submission_for_Dual_Examinar.Controllers
{
    public class UsersController : Controller
    {
        private DualExaminerDBContext db = new DualExaminerDBContext();

        public ActionResult Index()
        {
            if (Session["UserType"] != null && Session["UserType"].ToString().Trim() != "" && Session["UserType"].ToString().Trim().ToLower() != "student")
            {
            
                return View(db.Users.Where(x => x.Type == "teacher").ToList());
            }
            else
            {
                TempData["Message"] = "Unauthorized access attempt!";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Details(long? id)
        {
            if (Session["UserType"] != null && Session["UserType"].ToString().Trim() != "" && Session["UserType"].ToString().Trim().ToLower() != "student")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            else
            {
                TempData["Message"] = "Unauthorized access attempt!";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Create()
        {
            if (Session["UserType"] != null && Session["UserType"].ToString().Trim() != "" && Session["UserType"].ToString().Trim().ToLower() != "student")
            {
                return View();
            }
            else
            {
                TempData["Message"] = "Unauthorized access attempt!";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (Session["UserType"] != null && Session["UserType"].ToString().Trim() != "" && Session["UserType"].ToString().Trim().ToLower() != "student")
            {
                if (ModelState.IsValid)
                {
                    using (DualExaminerDBContext context = new DualExaminerDBContext())
                    {

                        User ifUserNameExist = context.Users.Where(x => x.UserName.Trim().ToLower() == user.UserName.Trim().ToLower()).FirstOrDefault();

                        if (ifUserNameExist != null)
                        {
                            TempData["Message"] = "User Name already taken. Please, try with another Name!";
                            return RedirectToAction("Register");
                        }

                        string hashedData = HomeController.ComputeSHA512Hash(user.Password);
                        user.Password = hashedData;
                        user.Type = "teacher";
                        context.Users.Add(user);
                        context.SaveChanges();

                        TempData["Message"] = "Registered Successfully.";
                        return RedirectToAction("Index");
                    }
                }
                TempData["Message"] = "Please, provide valid data!";
                return View(user);
            }
            else
            {
                TempData["Message"] = "Unauthorized access attempt!";
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult Edit(long? id)
        {
            if (Session["UserType"] != null && Session["UserType"].ToString().Trim() != "" && Session["UserType"].ToString().Trim().ToLower() != "student")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            else
            {
                TempData["Message"] = "Unauthorized access attempt!";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (Session["UserType"] != null && Session["UserType"].ToString().Trim() != "" && Session["UserType"].ToString().Trim().ToLower() != "student")
            {
                if (ModelState.IsValid)
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Message"] = "Updated Successfully.";
                    return RedirectToAction("Index");
                }
                TempData["Message"] = "Please, provide valid data!";
                return View(user);
            }
            return View(user);
        }

        public ActionResult Delete(long? id)
        {
            if (Session["UserType"] != null && Session["UserType"].ToString().Trim() != "" && Session["UserType"].ToString().Trim().ToLower() != "student")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            else
            {
                TempData["Message"] = "Unauthorized access attempt!";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            TempData["Message"] = "Updated Successfully.";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
