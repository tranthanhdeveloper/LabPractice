using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using AutoMapper;

using LabPractice.Models.Business;
using LabPractice.Models.Data;
using LabPractice.Models.View;

namespace LabPractice.Controllers
{
    public class StudentController : Controller
    {
        public ActionResult Index()
        {
            try {
                StudentService studentService = new StudentService();

                List<Student> studentList = studentService.GetStudents();

                List<StudentListItemView> studentItemViewList = Mapper.Map<List<StudentListItemView>>(studentList);
                return View (studentItemViewList);
            }
            catch (Exception e) {
                throw (e);
            }
        }

        public ActionResult Details(Guid id)
        {
            return View ();
        }

        public ActionResult Create()
        {
            return View (new CreateStudentView());
        } 

        [HttpPost]
        public ActionResult Create(CreateStudentView FormData)
        {
            try {
                //if(String.IsNullOrEmpty(FormData.FirstName))
                //{
                //    ViewData["FirstNameError"] = " First name must be not null";
                //    return View();
                //}

                if (!ModelState.IsValid)
                {
                    return View();
                }
                StudentService studentService = new StudentService();

                List<Student> studentList = studentService.GetStudents();

                Student student = Mapper.Map<Student>(FormData);

                student.Uuid = Guid.NewGuid();
                student.LastLogin = null;
                student.CreatedAt = DateTime.Now;
                student.UpdatedAt = DateTime.Now;

                studentService.AddStudent(student);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return View();
            }
        }

        public ActionResult Edit(Guid id)
        {
            try {
                StudentService studentService = new StudentService();
                Student student = studentService.GetStudentById(id);

                EditStudentView selectedStudent = Mapper.Map<EditStudentView>(student);

                return View(selectedStudent);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(Guid id, EditStudentView FormData)
        {
            try
            {
                if (FormData.GetAge() < 18)
                {
                    ModelState.AddModelError("AgeInvalid", "Student age must be large than 18");
                }
                if (!ModelState.IsValid)
                {
                    return View();
                }
                StudentService studentService = new StudentService();
                studentService.UpdateStudentById(id, FormData);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return View();
            }
        }

        public ActionResult Delete(Guid id)
        {
            try {
                StudentService studentService = new StudentService();
                ViewBag.Student = studentService.GetStudentById(id);
            }
            catch (Exception e) {
                Console.WriteLine("Exception: " + e.Message);
                ViewBag.Student = null;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try {
                StudentService studentService = new StudentService();
                studentService.DeleteStudentById(id);
                return RedirectToAction ("Index");
            } catch {
                return View ();
            }
        }
    }
}