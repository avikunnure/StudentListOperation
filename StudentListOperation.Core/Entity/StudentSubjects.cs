using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentListOperation.Core.Entity
{
    [Table("StudentSubjects")]
    public class StudentSubjects:BaseEntity
    {
        [Required]
        [ForeignKey("Subjects")]
        public int SubjectId { get; set; }

        public  virtual Subjects Subjects { get; set; }

        [NotMapped]
        public string SubjectName { get; set; }

        [NotMapped]
        public bool IsDeleted { get; set; }


        public decimal Marks { get; set; }


        [ForeignKey("Students")]
        public int StudentId { get; set; }

        public virtual Student Students { get; set; }


    }
}
