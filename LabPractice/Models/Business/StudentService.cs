using System;
using System.Collections.Generic;
using LabPractice.Models.Data;
using LabPractice.Models.View;
using Newtonsoft.Json;

namespace LabPractice.Models.Business
{
    public class StudentService
    {
        private readonly DataService dataService;
        public StudentService()
        {
            this.dataService = new DataService("Student");

        }

        public List<Student> GetStudents() {
            string dataString = this.dataService.ReadData();
            List<Student> studentList = null;

            if (dataString != "")
            {
                studentList = JsonConvert.DeserializeObject<List<Student>>(dataString);
            }
            else
            {
                studentList = new List<Student>();
            }
            return studentList;
        }

        public void AddStudent(Student student) {
            List<Student> studentList = this.GetStudents();
            studentList.Add(student);

            this.dataService.WriteData(JsonConvert.SerializeObject(studentList));
        }

        public Student GetStudentById(Guid id) {
            List<Student> studentsList = this.GetStudents();

            if(studentsList.Count > 0) {
                return studentsList.Find(student => student.Uuid == id);    
            }

            return null;
        }

        public void DeleteStudentById(Guid id) {
            List<Student> studentList = this.GetStudents();

            Student studentToBeDeleted = studentList.Find(x => x.Uuid == id);

            if(studentToBeDeleted != null) {
                studentList.Remove(studentToBeDeleted);
                this.dataService.WriteData(JsonConvert.SerializeObject(studentList));
            }

        }

        public void UpdateStudentById(Guid id, EditStudentView formData)
        {
            List<Student> studentList = this.GetStudents();
            if(studentList.Count > 0) {
                // Get student list in database files
                Student studentToBeEdit = studentList.Find(x => x.Uuid == id);
                // File student record position in the list
                int index = studentList.FindIndex(x => x.Uuid == id);

                if(studentToBeEdit != null && index > -1) {
                    studentToBeEdit.FirstName   = formData.FirstName;
                    studentToBeEdit.LastName    = formData.LastName;
                    studentToBeEdit.Gender      = formData.Gender;
                    studentToBeEdit.Email       = formData.Email;
                    studentToBeEdit.Birthday    = formData.Birthday;
                    studentToBeEdit.Note        = formData.Note;

                    studentList[index] = studentToBeEdit;

                    this.dataService.WriteData(JsonConvert.SerializeObject(studentList));
                }
            }
        }
    }
}
