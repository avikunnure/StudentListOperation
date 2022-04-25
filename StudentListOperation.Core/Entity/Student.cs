using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentListOperation.Core.Entity
{
    [Table("Student")]
    public class Student:BaseEntity
    {

       
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]       
        public string ClassName { get; set; }
        [NotMapped]
        public string SubjectName { get; set; }

        [NotMapped]
        public decimal Marks { get; set; }

        public virtual List<StudentSubjects> Subjects { get; set; }

        [NotMapped]
        public List<KeyValuePair<int,string>> SubjectsNVList { get; set; }= new List<KeyValuePair<int, string>>();
        public Student()
        {
            Subjects = new List<StudentSubjects>();
        }
        
    }
}
