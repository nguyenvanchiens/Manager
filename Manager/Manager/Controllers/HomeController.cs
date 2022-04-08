using Manager.Properties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Manager.Controllers
{
    
    public class HomeController : BaseController
    {      
        public HomeController(MyDbContext dbContext) : base(dbContext)
        {
        }

        
        public IActionResult Index([FromQuery] string filter)
        {
            ViewBag.isAdmin = GetClaimAdmin();
            ViewBag.isCreate = GetClaimAdd();
            ViewBag.Deparments = _dbContext.Department.ToList();
            if (filter != null)
            {
                ViewBag.Employeess = from e in _dbContext.Employees
                                     join d in _dbContext.Department
                                     on e.DepartmentId equals d.DepartmentId
                                     where (e.EmployeeName.Contains(filter) || e.EmployeeCode.Contains(filter) || e.Address.Contains(filter))
                                     orderby e.CreatedDate ascending
                                     select new { e.EmployeeId, e.EmployeeCode, e.EmployeeName, e.Address, e.DateOfBirth, d.DepartmentName };
            }
            else
            {
                ViewBag.Employeess = (from e in _dbContext.Employees
                                     join d in _dbContext.Department
                                     on e.DepartmentId equals d.DepartmentId
                                     orderby e.CreatedDate ascending
                                     select new { e.EmployeeId, e.EmployeeCode, e.EmployeeName, e.Address, e.DateOfBirth, d.DepartmentName }).ToList();
            }
            return View();
        }
       
       
        [HttpGet]
        public Employee GetEmployeeCode(Guid employeeId, string employeeCode)
        {
            var result = _dbContext.Employees.Where(x => x.EmployeeCode == employeeCode && x.EmployeeId != employeeId).FirstOrDefault();
            return result;
        }
        
        [HttpPost]
        public JsonResult AddEmployee(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid();
            employee.CreatedDate = DateTime.Now;
            employee.PassWord = Helper.CalculateMD5Hash(employee.PassWord);
            _dbContext.Employees.Add(employee);
            var result = _dbContext.SaveChanges();
            return Json(result);
        }
        [HttpPost]
        public JsonResult UpdateEmployee(Employee employee)
        {
            var entity = _dbContext.Employees.Where(x => x.EmployeeId == employee.EmployeeId).First();
            entity.EmployeeName = employee.EmployeeName;
            entity.EmployeeCode = employee.EmployeeCode;
            entity.DateOfBirth = employee.DateOfBirth;
            entity.DepartmentId = employee.DepartmentId;
            entity.Phone = employee.Phone;
            entity.Address = employee.Address;
            entity.UserName = employee.UserName;
            _dbContext.Employees.Update(entity);
            var result = _dbContext.SaveChanges();
            return Json(result);
        }
        [HttpGet]
        public Employee Get(Guid employeeId)
        {
            var entity = _dbContext.Employees.Where(x => x.EmployeeId == employeeId).First();
            return entity;
        }
        
        [HttpPost]
        public IActionResult Delete(Guid EmployeeId)
        {
            var employee = _dbContext.Employees.Where(c => c.EmployeeId == EmployeeId).First();
            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public string NewCodeEmployee()
        {
            string result = "";
            var employeeLast = (from e in _dbContext.Employees orderby e.EmployeeCode descending select e.EmployeeCode).First();
            var chars = "";
            string strRemoveLast = "";
            if (employeeLast == null)
            {
                result = "NV01";
            }
            foreach (var item in employeeLast)
            {
                chars += "/" + item;
            }
            var str = chars.Split('/').Last();
            int lastCode = int.Parse(str) + 1;
            if (lastCode == 0)
            {
                strRemoveLast = employeeLast.Substring(0, employeeLast.Length - 2);
            }
            strRemoveLast = employeeLast.Substring(0, employeeLast.Length - 1);
            result = strRemoveLast + lastCode;
            return result;
        }
        [HttpGet()]
        public ActionResult Role(Guid id)
        {
            ViewBag.Permissions = _dbContext.Permissions.Where(x=>x.ParentId==null).ToList();
            ViewBag.EmployeeId = id;
            var employee = Get(id);
            ViewBag.checkedvalue = (from e in _dbContext.Employees join 
                               r in _dbContext.Roles on e.EmployeeId equals r.EmployeeId
                               join p in _dbContext.Permissions on r.PermissionId equals p.PermissionId
                               where e.EmployeeId == id
                               select p.PermissionId).ToList();
            return View(employee);           
        }
        [HttpPost]
        public JsonResult Role(Guid id, string[] listId)
        {
            var employee = _dbContext.Employees.Where(x => x.EmployeeId == id).First();
            List<Role> list = _dbContext.Roles.Where(x => x.EmployeeId == id).ToList();
            _dbContext.Roles.RemoveRange(list);
            _dbContext.SaveChanges();
            if (listId.Length > 0)
            {
                var role = new Role();
                for (int i = 0; i < listId.Length; i++)
                {
                    role.RoleId = Guid.NewGuid();
                    role.EmployeeId = id;
                    role.PermissionId = Guid.Parse(listId[i]);
                    role.Licensed = true;
                    _dbContext.Roles.Add(role);
                    _dbContext.SaveChanges();
                }
            }
            return Json(true);
        }
        public bool GetClaimAdmin()
        {
            
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            var values = roles.Select(x => x.Value).ToList();
            bool flg = false;
            foreach (var item in values)
            {
                if (item == Resources.Admin)
                {
                    flg = true;
                }
            }
            return flg;
        } 
        public bool GetClaimAdd()
        {
            
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            var values = roles.Select(x => x.Value).ToList();
            bool flg = false;
            foreach (var item in values)
            {
                if (item == Resources.Admin || item == Resources.Create || item == Resources.User)
                {
                    flg = true;
                }
            }
            return flg;
        }
    }
}
