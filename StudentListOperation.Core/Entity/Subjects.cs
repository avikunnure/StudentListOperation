using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentListOperation.Core.Entity
{
    [Table("Subjects")]
    public class Subjects:BaseEntity
    {
        public string Name { get; set; }


    }

}
