using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class DepartmentController : BaseController
    {
        public DepartmentController(MyDbContext dbContext) : base(dbContext)
        {
        }

        // GET: DepartmentController
        public ActionResult Index([FromQuery]string filter)
        {
            if (filter != null)
            {
                ViewBag.Departments = _dbContext.Department.Where(x => x.DepartmentCode.Contains(filter) || x.DepartmentName.Contains(filter))
                    .ToList();
            }
            else
            {
                ViewBag.Departments = _dbContext.Department.ToList();
            }
            return View();
        }

        [HttpPost]
        public JsonResult AddDepartment(Department department)
        {
            department.DepartmentId = Guid.NewGuid();
            department.CreatedDate = DateTime.Now;
            _dbContext.Department.Add(department);
            var result  = _dbContext.SaveChanges();
            return Json(result);
        }
        [HttpPost]
        public JsonResult UpdateDepartment(Department department)
        {
            var entity = _dbContext.Department.Where(x => x.DepartmentId == department.DepartmentId).First();
            entity.DepartmentCode = department.DepartmentCode;
            entity.DepartmentName = department.DepartmentName;
            _dbContext.Department.Update(entity);
            var result = _dbContext.SaveChanges();
            return Json(result);
        }
        [HttpGet]
        public IActionResult Get(Guid departmentId)
        {
            var entity = _dbContext.Department.Where(x => x.DepartmentId == departmentId).First();
            return Ok(entity);

        }
        [HttpPost]
        public IActionResult Delete(Guid DepartmentId)
        {
            var employee = _dbContext.Department.Where(c => c.DepartmentId == DepartmentId).First();
            _dbContext.Department.Remove(employee);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Department");
        }
    }
}
