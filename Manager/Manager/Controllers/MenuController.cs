using Manager.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class MenuController : BaseController
    {
        private List<Menu> menuList = new List<Menu>();
        public MenuController(MyDbContext dbContext) : base(dbContext)
        {
        }

        public IActionResult Index()
        {
            var result = _dbContext.Menus.Where(x=>x.ParentId==Guid.Empty)
                .Select(x=> new Menu
                {
                    MenuId = x.MenuId,
                    Link = x.Link,
                    Title = x.Title,
                    ParentId = x.ParentId,
                }).ToList();
            var language = _dbContext.Languages.ToList();
            ViewBag.Menus = result;
            ViewBag.Language = language;
            return View();
        }
        public JsonResult Get(Guid id)
        {
            var menu = _dbContext.Menus.Find(id);
            return Json(menu);
        }
        [HttpPost]
        public JsonResult Create(Menu menu)
        {
            menu.MenuId = Guid.NewGuid();
            menu.Title = menu.Title;
            menu.Link = menu.Link;
            menu.ParentId = menu.ParentId;
            menu.LanguageId = menu.LanguageId;
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
          
            _dbContext.Menus.Remove(entity);
            //_dbContext.Menus.RemoveRange(childrentMenu);
           _dbContext.SaveChanges();
            return Json(entity);
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
