using Result_Submission_for_Dual_Examinar.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Result_Submission_for_Dual_Examinar.Controllers
{
    public class MarksController : Controller
    {
        DualExaminerDBContext context = new DualExaminerDBContext();

        public ActionResult Index(long id) // CourseId
        {
            if (Session["UserName"] != null && Session["UserName"].ToString().Trim() != "")
            {
                if (Session["UserType"].ToString().Trim().ToLower() == "admin")
                {
                    List<Mark> MarkList = new List<Mark>();

                    Course course = context.Courses.Where(x => x.CourseId == id).FirstOrDefault();
                    List<StudentCourse> sCourseList = context.StudentCourses.Where(x => x.CourseId == id).ToList();
                    if (sCourseList != null && sCourseList.Count() > 0)
                    {
                        foreach (var sCourse in sCourseList)
                        {
                            Mark mark = new Mark();

                            mark = context.Marks.Where(x => x.CourseId == id && x.StudentId == sCourse.StudentId).FirstOrDefault();
                            if (mark == null)
                            {
                                mark = new Mark();
                                mark.MarkId = 0;
                                mark.Course = course;
                                mark.CourseId = course.CourseId;
                                mark.Student = context.Users.Where(x => x.Id == sCourse.StudentId).FirstOrDefault();
                                mark.StudentId = mark.Student.Id;
                            }

                            MarkList.Add(mark);
                        }
                        return View(MarkList);
                    }
                    else
                    {
                        TempData["Message"] = "No student enrolled yet in this course!";
                        return RedirectToAction("Index", "Course");
                    }
                }
                else if (Session["UserType"].ToString().Trim().ToLower() == "teacher")
                {
                    List<Mark> MarkList = new List<Mark>();
                    Course course = context.Courses.Where(x => x.CourseId == id).FirstOrDefault();
                    List<StudentCourse> sCourseList = context.StudentCourses.Where(x => x.CourseId == id).ToList();
                    if (sCourseList != null && sCourseList.Count() > 0)
                    {
                        foreach (var sCourse in sCourseList)
                        {
                            Mark mark = new Mark();
                            mark = context.Marks.Where(x => x.CourseId == id && x.StudentId == sCourse.StudentId).FirstOrDefault();
                            if (mark == null)
                            {
                                mark = new Mark();
                                mark.MarkId = 0;
                                mark.Course = course;
                                mark.CourseId = course.CourseId;
                                mark.Student = context.Users.Where(x => x.Id == sCourse.StudentId).FirstOrDefault();
                                mark.StudentId = mark.Student.Id;
                            }

                            MarkList.Add(mark);
                        }
                        return View(MarkList);
                    }
                    else
                    {
                        TempData["Message"] = "No student enrolled yet in this course!";
                        return RedirectToAction("Index", "Course");
                    }

                }
            }
            return View();
        }

        public ActionResult SaveMark(Mark mark)
        {
            try
            {
                if (Session["UserName"] != null && Session["UserName"].ToString().Trim() != "" && Session["UserName"].ToString().Trim() != "student")
                {
                    //Mark mark = markList.FirstOrDefault();

                    TryUpdateModel(mark);

                    if (ModelState.IsValid)
                    {
                        if (mark.MarkId > 0)
                        {
                            Mark _mark = context.Marks.Where(x => x.MarkId == mark.MarkId).FirstOrDefault();
                            if (mark.MidCTMark > 0)
                            {
                                _mark.MidCTMark = mark.MidCTMark;
                            }
                            if (mark.MidExamMark > 0)
                            {
                                _mark.MidExamMark = mark.MidExamMark;
                            }
                            if (mark.FinalCTMark > 0)
                            {
                                _mark.FinalCTMark = mark.FinalCTMark;
                            }
                            if (mark.FinalExamMark > 0)
                            {
                                _mark.FinalExamMark = mark.FinalExamMark;
                            }
                            context.Entry(_mark).State = EntityState.Modified;
                            context.SaveChanges();
                        }
                        else
                        {
                            Mark _mark = new Mark();
                            _mark = mark;
                            if (mark.CourseId > 0 && mark.StudentId > 0)
                            {
                                context.Marks.Add(_mark);
                                context.SaveChanges();
                            }
                            else 
                            {
                                TempData["Message"] = "Something went wrong !";
                                return RedirectToAction("Index", "Course");
                            }

                        }
                        TempData["Message"] = "Saved Successfully!";
                        return RedirectToAction("Index", new { id = mark.CourseId });
                    }
                    else 
                    {
                        TempData["Message"] = "Mark is not valid!";
                        return RedirectToAction("Index", new { id = mark.CourseId });
                    }
                }
                else 
                {
                    TempData["Message"] = "Unautherized access attempt!";
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                //throw;
            }
            return RedirectToAction("Index","Course");
        }

        public ActionResult GetMarks(long id) // CourseId
        {
            if (Session["UserName"] != null && Session["UserName"].ToString().Trim() != "")
            {
                List<Mark> MarkList = new List<Mark>();

                if (Session["UserType"].ToString().Trim().ToLower() == "admin")
                {
                    Course course = context.Courses.Where(x => x.CourseId == id).FirstOrDefault();
                    List<StudentCourse> sCourseList = context.StudentCourses.Where(x => x.CourseId == id).ToList();
                    if (sCourseList != null && sCourseList.Count() > 0)
                    {
                        foreach (var sCourse in sCourseList)
                        {
                            Mark mark = new Mark();

                            mark = context.Marks.Where(x => x.CourseId == id && x.StudentId == sCourse.StudentId).FirstOrDefault();
                            if (mark == null)
                            {
                                mark = new Mark();
                                mark.Course = course;
                                mark.CourseId = course.CourseId;
                                mark.Student = context.Users.Where(x => x.Id == sCourse.StudentId).FirstOrDefault();
                                mark.StudentId = mark.Student.Id;
                            }

                            MarkList.Add(mark);
                        }
                    }
                    else
                    {
                        TempData["Message"] = "No student enrolled yet in this course!";
                        return RedirectToAction("Index", "Course");
                    }
                }
                else if (Session["UserType"].ToString().Trim().ToLower() == "teacher")
                {
                    Course course = context.Courses.Where(x => x.CourseId == id).FirstOrDefault();
                    List<StudentCourse> sCourseList = context.StudentCourses.Where(x => x.CourseId == id).ToList();
                    if (sCourseList != null && sCourseList.Count() > 0)
                    {
                        foreach (var sCourse in sCourseList)
                        {
                            Mark mark = new Mark();
                            mark = context.Marks.Where(x => x.CourseId == id && x.StudentId == sCourse.StudentId).FirstOrDefault();
                            if (mark == null)
                            {
                                mark = new Mark();
                                mark.Course = course;
                                mark.CourseId = course.CourseId;
                                mark.Student = context.Users.Where(x => x.Id == sCourse.StudentId).FirstOrDefault();
                                mark.StudentId = mark.Student.Id;
                            }

                            MarkList.Add(mark);
                        }
                    }
                    else
                    {
                        TempData["Message"] = "No student enrolled yet in this course!";
                        return RedirectToAction("Index", "Course");
                    }

                }
                else if (Session["UserType"].ToString().Trim().ToLower() == "student")
                {
                    long studentId = Convert.ToInt64(Session["UserId"].ToString());
                    Course course = context.Courses.Where(x => x.CourseId == id).FirstOrDefault();
                    List<StudentCourse> sCourseList = context.StudentCourses.Where(x => x.CourseId == id && x.StudentId == studentId).ToList();
                    if (sCourseList != null && sCourseList.Count() > 0)
                    {
                        foreach (var sCourse in sCourseList)
                        {
                            Mark mark = new Mark();
                            mark = context.Marks.Where(x => x.CourseId == id && x.StudentId == sCourse.StudentId).FirstOrDefault();
                            if (mark == null)
                            {
                                mark = new Mark();
                                mark.Course = course;
                                mark.CourseId = course.CourseId;
                                mark.Student = context.Users.Where(x => x.Id == sCourse.StudentId).FirstOrDefault();
                                mark.StudentId = mark.Student.Id;
                            }

                            MarkList.Add(mark);
                        }
                    }
                    else
                    {
                        TempData["Message"] = "No student enrolled yet in this course!";
                        return RedirectToAction("Index", "Course");
                    }

                }

                foreach (var mark in MarkList)
                {
                    if (mark.MidCTMark > 0)
                    {
                        if (mark.MidExamMark > 0)
                        {
                            if (mark.FinalCTMark > 0)
                            {
                                if (mark.FinalExamMark > 0)
                                {
                                    decimal MidTotal = mark.MidCTMark + mark.MidExamMark;
                                    decimal FinalTotal = mark.FinalCTMark + mark.FinalExamMark;

                                    if (Math.Abs(FinalTotal - MidTotal) > 20)
                                    {
                                        mark.Result = -1;
                                    }
                                    else
                                    {
                                        mark.Result = (MidTotal + FinalTotal) / 2;
                                    }

                                }
                                else
                                {
                                    mark.Result = -2;
                                }
                            }
                            else
                            {
                                mark.Result = -2;
                            }
                        }
                        else
                        {
                            mark.Result = -2;
                        }
                    }
                    else
                    {
                        mark.Result = -2;
                    }
                }

                return View(MarkList);
            }

            return View();
        }

    }
}