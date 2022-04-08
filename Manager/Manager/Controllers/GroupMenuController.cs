using Manager.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class GroupMenuController : BaseController
    {
        private List<Menu> menuList = new List<Menu>();
        public GroupMenuController(MyDbContext dbContext) : base(dbContext)
        {
        }

        public IActionResult Index()
        {
            var result = _dbContext.Menus.Where(x=>x.ParentId==Guid.Empty).ToList();
            ViewBag.Menus = result;
            return View();
        }
        [HttpPost]
        public JsonResult Create(Menu menu)
        {
            menu.MenuId = Guid.NewGuid();
            menu.Title = menu.Title;
            menu.Link = menu.Link;
            menu.ParentId = menu.ParentId;
            _dbContext.Menus.Add(menu);
            _dbContext.SaveChanges();
            return Json(menu);
        }
        [HttpPost]
        public JsonResult Edit(Menu menu)
        {
            var entity = _dbContext.Menus.Where(x=>x.MenuId==menu.MenuId).First();
            entity.Title = menu.Title;
            entity.Link = menu.Link;
            entity.ParentId = menu.ParentId;
            _dbContext.Menus.Update(entity);
            _dbContext.SaveChanges();
            return Json(entity);
        }
        [HttpPost]
        public JsonResult Delete(Guid id)
        {
            var menu = _dbContext.Menus.ToList();
            var entity = menu.Where(x => x.MenuId == id).First();
            var childrentMenu = getChidrentMenu(menu,id);
            _dbContext.Menus.Remove(entity);
            _dbContext.Menus.RemoveRange(childrentMenu);
           
            return Json(entity);
        }
        public List<Menu> getChidrentMenu(List<Menu> menus,Guid id)
        {          
            if (menus.Count > 0)
            {
                var entity = menus.Where(x => x.MenuId == id || x.ParentId == id).ToList();
                var count = entity.Count();
                //if (entity==null)
                //{
                //    return menuList;
                //}
                //else
                //{
                //    menuList.Add(entity);
                //    menus.Remove(entity);
                //}
                //getChidrentMenu(menus, entity.MenuId);
            }
            return menuList;
        }
        public JsonResult GetParentNode()
        {
            var menus = _dbContext.Menus.Select(x => new TreeNodeMenu()
            {
                MenuId = x.MenuId,
                Title = x.Title,
                Link = x.Link,
                ParentId = x.ParentId
            }).ToList();
            List<TreeNodeMenu> result = new List<TreeNodeMenu>();
            result = menus.Where(c=>c.ParentId == Guid.Empty)
                          .Select(c => new TreeNodeMenu() { MenuId = c.MenuId, Title = c.Title, ParentId = c.ParentId, Link = c.Link, data = GetChildren(menus, c.MenuId) })
                          .ToList();
            return Json(result.ToArray());
        }

        public static List<TreeNodeMenu> GetChildren(List<TreeNodeMenu> menus, Guid parentId)
        {
            return menus
                    .Where(c => c.ParentId == parentId)
                    .Select(c => new TreeNodeMenu { MenuId = c.MenuId, Title = c.Title, Link = c.Link, ParentId = c.ParentId, data = GetChildren(menus, c.MenuId) })
                    .ToList();
        }
        [HttpGet]
        public JsonResult GetAllMenu()
        {
            var result = _dbContext.Menus.Select(x => new Menu()
            {
                MenuId = x.MenuId,
                Title = x.Title,
                ParentId = x.ParentId,
                Link = x.Link,
            }).ToList();
            return Json(result);
        }
    }
}
