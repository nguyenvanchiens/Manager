using Manager.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class PermissionController : BaseController
    {
        public PermissionController(MyDbContext dbContext) : base(dbContext)
        {
        }


        public IActionResult Index()
        {
            var permision = _dbContext.Permissions.Select(x => new Permission()
            {
                PermissionId = x.PermissionId,
                PermissionName = x.PermissionName,
                ParentId = x.ParentId,
                CreatedDate = x.CreatedDate,
            }).ToList();
            ViewBag.Permisions = permision;
            var result = permision.Where(a => a.ParentId == null).ToList();
            return View(result);
        }



        public JsonResult GetParentNode()
        {            
            var permission = _dbContext.Permissions.Select(x => new PerrmissionNodeTree()
            {
                PermissionId = x.PermissionId,
                PermissionName = x.PermissionName,
                ParentId = x.ParentId,
                CreatedDate = x.CreatedDate,
            }).ToList();
           

            List<PerrmissionNodeTree> result = new List<PerrmissionNodeTree>();
            result = permission
                            .Where(c => c.ParentId == null)
                            .Select(c => new PerrmissionNodeTree() { PermissionId = c.PermissionId, PermissionName = c.PermissionName, ParentId = c.ParentId, Childrent = GetChildren(permission, c.PermissionId) })
                            .ToList();
            return Json(result);
        }

        public static List<PerrmissionNodeTree> GetChildren(List<PerrmissionNodeTree> permissions, Guid parentId)
        {
            return permissions
                    .Where(c => c.ParentId == parentId)
                    .Select(c => new PerrmissionNodeTree { PermissionId = c.PermissionId, PermissionName = c.PermissionName, ParentId = c.ParentId, Childrent = GetChildren(permissions, c.PermissionId) })
                    .ToList();
        }


        public JsonResult GetSubMenuCheck(Guid pid, Guid idEp)
        {
            List<Permission> subPermision = new List<Permission>();
            Guid pID = Guid.NewGuid();
            Guid.TryParse(pid.ToString(), out pID);

            subPermision = _dbContext.Permissions.Where(a => a.ParentId == pID).OrderBy(a => a.PermissionName).ToList();

            var value = (from e in _dbContext.Employees
                         join
                         r in _dbContext.Roles on e.EmployeeId equals r.EmployeeId
                         join p in _dbContext.Permissions on r.PermissionId equals p.PermissionId
                         where e.EmployeeId == idEp
                         where p.ParentId != null
                         select p.PermissionId).ToList();

            return Json(new { data = subPermision, checkedvalue = value, ideP = idEp });
        }
        [HttpGet]
        public Permission Get(Guid id)
        {
            var result = _dbContext.Permissions.Where(x => x.PermissionId == id).First();
            return result;
        }
        [HttpPost]
        public JsonResult Create(Permission permission)
        {
            permission.PermissionId = Guid.NewGuid();
            permission.CreatedDate = DateTime.Now;
            _dbContext.Permissions.Add(permission);
            var result = _dbContext.SaveChanges();
            return Json(result);
        }
        [HttpPost]
        public JsonResult Edit(Permission permission)
        {
            var permision = _dbContext.Permissions.Where(x=>x.PermissionId== permission.PermissionId).First();
            permision.PermissionName = permission.PermissionName;
            permision.ParentId = permission.ParentId;
            _dbContext.Permissions.Update(permision);
            var result = _dbContext.SaveChanges();
            return Json(result);
        }
        [HttpPost]
        public JsonResult Delete(Guid id)
        {
            var permision = _dbContext.Permissions.Where(x => x.PermissionId == id).First();
            _dbContext.Permissions.Remove(permision);
            var result = _dbContext.SaveChanges();
            return Json(result);
        }
        [HttpGet]
        public JsonResult GetAllPermission()
        {
            var result = _dbContext.Permissions.Select(x => new Permission()
            {
                PermissionId = x.PermissionId,
                PermissionName = x.PermissionName,
                ParentId = x.ParentId,
                CreatedDate = x.CreatedDate,
            }).ToList();
            return Json(result);
        }
    }
}

