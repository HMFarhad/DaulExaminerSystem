using Result_Submission_for_Dual_Examinar.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Result_Submission_for_Dual_Examinar.Controllers
{
    public class CourseController : Controller
    {
        private DualExaminerDBContext context = new DualExaminerDBContext();

        public ActionResult Index()
        {
            if (Session["UserName"] != null && Session["UserName"].ToString().Trim() != "")
            {
                List<Course> CourseList = new List<Course>();
                if (Session["UserType"].ToString().Trim().ToLower() == "admin")
                {
                    CourseList = context.Courses.ToList();

                    CourseList = GetCourseTeacher(CourseList);

                    return View(CourseList);
                }
                else if (Session["UserType"].ToString().Trim().ToLower() == "teacher")
                {
                    long Id = Convert.ToInt64(Session["UserId"].ToString());
                    CourseList = context.Courses.Where(x => x.MidTeacherId == Id || x.FinalTeacherId == Id).ToList();

                    CourseList = GetCourseTeacher(CourseList);

                    return View(CourseList);
                }
                else
                {
                    long Id = Convert.ToInt64(Session["UserId"].ToString());

                    List<StudentCourse> sCourseList = new List<StudentCourse>();
                    sCourseList = context.StudentCourses.Where(x => x.StudentId == Id).ToList();

                    foreach (var sCourse in sCourseList)
                    {
                        Course course = context.Courses.Where(x => x.CourseId == sCourse.CourseId).FirstOrDefault();
                        CourseList.Add(course);
                    }

                    CourseList = GetCourseTeacher(CourseList);
                    return View(CourseList);
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        private List<Course> GetCourseTeacher(List<Course> courseList)
        {
            foreach (var item in courseList)
            {
                item.MidTeacher = context.Users.Where(x => x.Id == item.MidTeacherId).FirstOrDefault();
                item.FinalTeacher = context.Users.Where(x => x.Id == item.FinalTeacherId).FirstOrDefault();
            }
            return courseList;
        }

        public ActionResult Details(long id)
        {
            if (Session["UserName"] != null && Session["UserName"].ToString() != "")
            {
                Course course = context.Courses.Where(x => x.CourseId == id).FirstOrDefault();

                return View(course);
            }
            else
            {
                TempData["Message"] = "Unauthorized access attempt!";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Create()
        {
            if (Session["UserName"] != null && Session["UserName"].ToString() != "" && Session["UserName"].ToString() == "Admin")
            {
                List<User> UserList = context.Users.Where(x => x.Type.Trim().ToLower() == "teacher").ToList();

                ViewBag.Users = new SelectList(UserList, "Id", "FullName");

                return View();
            }
            else
            {
                TempData["Message"] = "Unauthorized access attempt!";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult Create(Course course)
        {
            try
            {
                List<User> UserList = context.Users.Where(x => x.Type.Trim().ToLower() == "teacher").ToList();

                ViewBag.Users = new SelectList(UserList, "Id", "FullName");

                if (ModelState.IsValid)
                {
                    List<Course> CheckMidTeacherLimit = context.Courses.Where(x => x.MidTeacherId == course.MidTeacherId || x.FinalTeacherId == course.MidTeacherId).ToList();

                    if (CheckMidTeacherLimit != null && CheckMidTeacherLimit.Count() >= 3)
                    {
                        TempData["Message"] = "Assigned teacher for Mid Term is already reached to max course limit three (3)!" ;
                        return View(course);
                    }
                    List<Course> CheckFinalTeacherLimit = context.Courses.Where(x => x.MidTeacherId == course.FinalTeacherId || x.FinalTeacherId == course.FinalTeacherId).ToList();

                    if (CheckFinalTeacherLimit != null && CheckFinalTeacherLimit.Count() >= 3)
                    {
                        TempData["Message"] = "Assigned teacher for Final Term is already reached to max course limit three (3)!";
                        return View(course);
                    }

                    context.Courses.Add(course);
                    context.SaveChanges();

                    TempData["Message"] = "Course Successfully Added.";
                    return RedirectToAction("Index");
                }
                TempData["Message"] = "Please, provide valid data!";
                return View(course);
            }
            catch(Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View(course);
            }
        }

        public ActionResult Edit(long id)
        {
            if (Session["UserName"] != null && Session["UserName"].ToString() != "" && Session["UserName"].ToString() == "Admin")
            {
                List<User> UserList = context.Users.Where(x => x.Type.Trim().ToLower() == "teacher").ToList();

                ViewBag.Users = new SelectList(UserList, "Id", "FullName");

                Course course = context.Courses.Where(x => x.CourseId == id).FirstOrDefault();

                return View(course);
            }
            else
            {
                TempData["Message"] = "Unauthorized access attempt!";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult Edit(Course course)
        {
            try
            {
                List<User> UserList = context.Users.Where(x => x.Type.Trim().ToLower() == "teacher").ToList();

                ViewBag.Users = new SelectList(UserList, "Id", "FullName");

                if (ModelState.IsValid)
                {
                    List<Course> CheckMidTeacherLimit = context.Courses.Where(x => x.MidTeacherId == course.MidTeacherId || x.FinalTeacherId == course.MidTeacherId).ToList();

                    if (CheckMidTeacherLimit != null && CheckMidTeacherLimit.Count() >= 3)
                    {
                        TempData["Message"] = "Assigned teacher for Mid Term is already reached to max course limit three (3)!";
                        return View(course);
                    }
                    List<Course> CheckFinalTeacherLimit = context.Courses.Where(x => x.MidTeacherId == course.FinalTeacherId || x.FinalTeacherId == course.FinalTeacherId).ToList();

                    if (CheckFinalTeacherLimit != null && CheckFinalTeacherLimit.Count() >= 3)
                    {
                        TempData["Message"] = "Assigned teacher for Final Term is already reached to max course limit three (3)!";
                        return View(course);
                    }

                    context.Entry(course).State = EntityState.Modified;
                    context.SaveChanges();

                    TempData["Message"] = "Course Successfully Updated.";
                    return RedirectToAction("Index");
                }
                TempData["Message"] = "Please, provide valid data!";
                return View(course);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return View(course);
            }
        }

        public ActionResult Delete(int id)
        {
            if (Session["UserName"] != null && Session["UserName"].ToString() != "" && Session["UserName"].ToString() == "Admin")
            {
                List<User> UserList = context.Users.Where(x => x.UserName.Trim().ToLower() != "admin").ToList();

                ViewBag.Users = new SelectList(UserList, "Id", "FullName");

                Course course = context.Courses.Where(x => x.CourseId == id).FirstOrDefault();

                return View(course);
            }
            else
            {
                TempData["Message"] = "Unauthorized access attempt!";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            try
            {
                if (Session["UserName"] != null && Session["UserName"].ToString() != "" && Session["UserName"].ToString() == "Admin")
                {
                    Course course = context.Courses.Find(id);
                    context.Courses.Remove(course);
                    context.SaveChanges();
                    TempData["Message"] = "Successfully Deleted!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = "Unauthorized access attempt!";
                    return RedirectToAction("Index", "Home");
                }

            }
            catch(Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
