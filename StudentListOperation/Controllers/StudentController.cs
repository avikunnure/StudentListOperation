using Microsoft.AspNetCore.Mvc;
using StudentListOperation.Core.Entity;

namespace StudentListOperation.Controllers
{
    public class StudentController : Controller
    {
        public readonly StudentDBContext context;
        public StudentController(StudentDBContext _context)
        {
            context = _context;
        }

        public IActionResult Index(string SearchText = "", string FilterBy = "Name")
        {
            var list = new List<Student>();;
            if (string.IsNullOrEmpty(SearchText) != true)
            {
                if (FilterBy.ToLowerInvariant() == "class")
                {
                    list = (from s in context.students
                           join ssb in context.StudentSubjects on s.Id equals ssb.StudentId
                           join sb in context.Subjects on ssb.SubjectId equals sb.Id
                           where s.ClassName == SearchText
                            select new Student()
                           {
                               Id = s.Id,
                               Marks = ssb.Marks,
                               LastName = s.LastName,
                               FirstName = s.FirstName,
                               ClassName = s.ClassName,
                               MiddleName = s.MiddleName,
                               SubjectName = sb.Name,

                           }).ToList();
                }
                if (FilterBy.ToLowerInvariant() == "name")
                {
                    list = (from s in context.students
                            join ssb in context.StudentSubjects on s.Id equals ssb.StudentId
                            join sb in context.Subjects on ssb.SubjectId equals sb.Id
                            where (s.FirstName + s.LastName) == (SearchText) ||
                            (s.FirstName) == (SearchText) ||
                            (s.LastName) == (SearchText)
                            select new Student()
                            {
                                Id = s.Id,
                                Marks = ssb.Marks,
                                LastName = s.LastName,
                                FirstName = s.FirstName,
                                ClassName = s.ClassName,
                                MiddleName = s.MiddleName,
                                SubjectName = sb.Name,

                            }).ToList();
                }
                if (FilterBy.ToLowerInvariant() == "subject")
                {
                    list = (from s in context.students
                            join ssb in context.StudentSubjects on s.Id equals ssb.StudentId
                            join sb in context.Subjects on ssb.SubjectId equals sb.Id
                            where s.SubjectName== SearchText
                            select new Student()
                            {
                                Id = s.Id,
                                Marks = ssb.Marks,
                                LastName = s.LastName,
                                FirstName = s.FirstName,
                                ClassName = s.ClassName,
                                MiddleName = s.MiddleName,
                                SubjectName = sb.Name,

                            }).ToList();
                   
                }
            }
            else
            {

                list = (from s in context.students
                        join ssb in context.StudentSubjects on s.Id equals ssb.StudentId
                        join sb in context.Subjects on ssb.SubjectId equals sb.Id
                        select new Student()
                        {
                            Id = s.Id,
                            Marks = ssb.Marks,
                            LastName = s.LastName,
                            FirstName = s.FirstName,
                            ClassName = s.ClassName,
                            MiddleName = s.MiddleName,
                            SubjectName = sb.Name,

                        }).ToList();
            }
            ViewBag.SearchText=SearchText;
            return View(list.ToList());
        }
        public IActionResult Create()
        {
            var std = new Student();
            std.SubjectsNVList = context.Subjects.Select(x => new KeyValuePair<int, string>(x.Id, x.Name)).ToList();
            return View(std);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {

            student.SubjectsNVList = context.Subjects.Select(x => new KeyValuePair<int, string>(x.Id, x.Name)).ToList();
            for (int i = 0; i < student.Subjects.Count; i++)
            {
                if (student.Subjects[i].IsDeleted)
                {
                    ModelState.Remove($"Subjects[{i}]");
                    ModelState.Remove($"Subjects[{i}].StudentId");
                    ModelState.Remove($"Subjects[{i}].Marks");
                    ModelState.Remove($"Subjects[{i}].SubjectId");
                    ModelState.Remove($"Subjects[{i}].SubjectName");
                    ModelState.Remove($"Subjects[{i}].Subjects");
                    ModelState.Remove($"Subjects[{i}].Students");
                }
                ModelState.Remove($"Subjects[{i}].SubjectName");
                ModelState.Remove($"Subjects[{i}].Subjects");
                ModelState.Remove($"Subjects[{i}].Students");

            }
            ModelState.Remove("SubjectName");
            if (ModelState.IsValid)
            {
                context.Add(student);

                await context.SaveChangesAsync();
                foreach (var item in student.Subjects.Where(x => x.IsDeleted == false))
                {
                    item.StudentId = student.Id;
                    if (item.Id == 0)
                    {
                        context.Add(item);
                    }
                    else
                    {
                        context.Update(item);
                    }
                }
                await context.SaveChangesAsync();
                ViewBag.MessageError = "Save Successfully";
                return RedirectToAction("Index");
            }
            return View(student);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEduction(Student student)
        {
            student.SubjectsNVList = context.Subjects.Select(x => new KeyValuePair<int, string>(x.Id, x.Name)).ToList();
            for (int i = 0; i < student.Subjects.Count; i++)
            {
                if (student.Subjects[i].IsDeleted)
                {
                    ModelState.Remove($"Subjects[{i}]");
                    ModelState.Remove($"Subjects[{i}].StudentId");
                    ModelState.Remove($"Subjects[{i}].Marks");
                    ModelState.Remove($"Subjects[{i}].SubjectId");
                    ModelState.Remove($"Subjects[{i}].SubjectName");
                    ModelState.Remove($"Subjects[{i}].Subjects");
                    ModelState.Remove($"Subjects[{i}].Students");
                }
                ModelState.Remove($"Subjects[{i}].SubjectName");
                ModelState.Remove($"Subjects[{i}].Subjects");
                ModelState.Remove($"Subjects[{i}].Students");
            }
            ModelState.Remove("SubjectName");
            if (ModelState.IsValid)
            {
                student.Subjects.Add(new StudentSubjects());
                return View("Create", student);
            }
            return View("Create", student);
        }


        public async Task<IActionResult> Edit(int? id)
        {

            var student = await context.students.FindAsync(id);
            student.Subjects = new List<StudentSubjects>();
            student.SubjectsNVList = context.Subjects.Select(x => new KeyValuePair<int, string>(x.Id, x.Name)).ToList();
            student.Subjects.AddRange(context.StudentSubjects.Where(x => x.StudentId == id).AsNoTracking().ToList());
            return View("Create", student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            ModelState.Remove("SubjectName");
            for (int i = 0; i < student.Subjects.Count; i++)
            {
                if (student.Subjects[i].IsDeleted)
                {
                    ModelState.Remove($"Subjects[{i}]");
                    ModelState.Remove($"Subjects[{i}].StudentId");
                    ModelState.Remove($"Subjects[{i}].Marks");
                    ModelState.Remove($"Subjects[{i}].SubjectId");
                    ModelState.Remove($"Subjects[{i}].SubjectName");
                    ModelState.Remove($"Subjects[{i}].Subjects");
                    ModelState.Remove($"Subjects[{i}].Students");
                }
                ModelState.Remove($"Subjects[{i}].SubjectName");
                ModelState.Remove($"Subjects[{i}].Subjects");
                ModelState.Remove($"Subjects[{i}].Students");
            }
            if (ModelState.IsValid)
            {
                student.SubjectsNVList = context.Subjects.Select(x => new KeyValuePair<int, string>(x.Id, x.Name)).ToList();
                foreach (var item in student.Subjects)
                {
                    item.StudentId = id;
                    if (item.Id == 0 && item.IsDeleted == false)
                    {
                        context.Add(item);
                    }
                    else if (item.IsDeleted == false)
                    {
                        context.Update(item);
                    }
                    else if (item.IsDeleted == true && item.Id > 0)
                    {
                        context.Remove(item);
                    }
                }
                context.Update(student);
                await context.SaveChangesAsync();
                ViewBag.MessageError = "Save Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View("Create", student);
        }

        public async Task<IActionResult> Delete(int? id)
        {

            var employee = await context.students
                .FirstOrDefaultAsync(m => m.Id == id);
            context.students.Remove(employee);
            await context.SaveChangesAsync();
            if (employee == null)
            {
                ViewBag.MessageError = "Not Found";
            }

            return RedirectToAction("Index");
        }
    }
}
