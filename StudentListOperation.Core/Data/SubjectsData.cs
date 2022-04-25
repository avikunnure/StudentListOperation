using StudentListOperation.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentListOperation.Core.Data
{
    internal class SubjectsData
    {
        internal static List<Subjects> GetSubjects()
        {
            return new List<Subjects>() {
                new Subjects(){ Id=1, Name="English" },
                new Subjects(){ Id=2, Name="Science" },
                new Subjects(){ Id=3, Name="Maths" },
                new Subjects(){ Id=4, Name="Hindi" },
                new Subjects(){ Id=6, Name="Marathi" },
                new Subjects(){ Id=7, Name="C" },
                new Subjects(){ Id=8, Name="C++" },
                new Subjects(){ Id=9, Name="Java" },
                new Subjects(){ Id=10, Name="DotNet" },
            };
        }
    }
}
