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
    public class StudentsController : Controller
    {
        private DualExaminerDBContext db = new DualExaminerDBContext();

        public ActionResult Index()
        {
            if (Session["UserType"] != null && Session["UserType"].ToString().Trim() != "" && Session["UserType"].ToString().Trim().ToLower() != "student")
            {
                long TeacherId = Convert.ToInt64(Session["UserId"].ToString());

                List<User> StudentList = new List<User>();

                StudentList = db.Users.Where(x => x.Type == "student").ToList();

                StudentList = GetCourseList(StudentList);

                return View(StudentList);
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString().Trim() != "" && Session["UserType"].ToString().Trim().ToLower() == "student")
            {
                List<User> StudentList = new List<User>();
                StudentList = db.Users.Where(x => x.Type == "student").ToList();

                StudentList = GetCourseList(StudentList);

                return View(StudentList);
            }
            else
            {
                TempData["Message"] = "Unauthorized access attempt!";
                return RedirectToAction("Index", "Home");
            }
        }

        private List<User> GetCourseList(List<User> studentList)
        {
            foreach (var student in studentList)
            {
                student.CourseList = new List<Course>();
                List<StudentCourse> sCourses = db.StudentCourses.Where(x => x.StudentId == student.Id).ToList();
                foreach (var course in sCourses)
                {
                    course.Course = db.Courses.Where(x => x.CourseId == course.CourseId).FirstOrDefault();
                    student.CourseList.Add(course.Course);
                }
            }
            return studentList;
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
                List<Course> CourseList = db.Courses.ToList();
                ViewBag.Courses = new SelectList(CourseList, "CourseId", "Name");
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
                List<Course> CourseList = db.Courses.ToList();
                ViewBag.Courses = new SelectList(CourseList, "CourseId", "Name");
                try
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
                            user.Type = "student";
                            context.Users.Add(user);

                            if (user.CourseIdList?.Count() > 0)
                            {
                                if (user.CourseIdList?.Count() > 3)
                                {
                                    TempData["Message"] = "A student can enroll max three courses at a time!";
                                    return View("Create", user);
                                }
                                else 
                                {
                                    foreach (var item in user.CourseIdList)
                                    {
                                        List<StudentCourse> ifFull = context.StudentCourses.Where(x => x.CourseId == item).ToList();

                                        if (ifFull != null && ifFull.Count()>=5)
                                        {
                                            TempData["Message"] = "Selected course already reached to max student limit!";
                                            return View("Create", user);
                                        }

                                        StudentCourse sCourse = new StudentCourse();
                                        sCourse.Student = user;
                                        sCourse.CourseId = item;
                                        context.StudentCourses.Add(sCourse);
                                    }

                                    context.SaveChanges();
                                }

                            }
                            else
                            {
                                TempData["Message"] = "At least one course must be selected to enroll as a student!";
                                return View("Create", user);
                            }

                            TempData["Message"] = "Registered Successfully.";
                            return RedirectToAction("Index");
                        }
                    }
                    TempData["Message"] = "Please, provide valid data!";
                    return View(user);
                }
                catch (Exception ex)
                {
                    TempData["Message"] = "Something went wrong!";
                    return RedirectToAction("Index", "Home");
                }

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
