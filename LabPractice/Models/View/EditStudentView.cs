using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LabPractice.Models.Data;

namespace LabPractice.Models.View
{
    public class EditStudentView
    {
        public Guid Uuid { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("LastName")]
        public string LastName { get; set; }

        [Required]
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Birthday { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$", ErrorMessage = " Email must match with email address format")]
        public string Email { get; set; }

        [StringLength(250)]
        public string Note { get; set; }

        public int GetAge()
        {
            var existedTime = DateTime.Now - this.Birthday;
            return (int)(existedTime.TotalDays / 365.2425);
        }
    }
}
