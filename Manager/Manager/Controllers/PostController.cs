using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    public class PostController : BaseController
    {
        public PostController(MyDbContext dbContext) : base(dbContext)
        {
        }

        // GET: PostController
        public ActionResult Index(string filter)
        {
            ViewBag.Posts = _dbContext.Posts.ToList().OrderByDescending(x=>x.CreatedDate);
            return View();
        }

        // GET: PostController/Details/5
        public Post Details(Guid id)
        {
            var post = _dbContext.Posts.Where(x=>x.PostId == id).FirstOrDefault();
            return post;
        }

        // GET: PostController/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: PostController/Create
        [HttpPost]
        public JsonResult Create(Post post)
        {
            try
            {
                post.PostId = Guid.NewGuid();
                post.CreatedDate = new DateTime();
                _dbContext.Posts.Add(post);
               var result = _dbContext.SaveChanges();
                return Json(result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        // GET: PostController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var result = _dbContext.Posts.Where(x=>x.PostId==id).First();
            return View(result);
        }

        // POST: PostController/Edit/5
        [HttpPut]
        public JsonResult Edit(Guid id, Post post)
        {
            var entity = _dbContext.Posts.Where(x => x.PostId == id).First();
            entity.Language = post.Language;
            entity.Title = post.Title;
            entity.Content = post.Content;
            entity.Description = post.Description;
            entity.Tag = post.Tag;
            entity.isComment = post.isComment;
            entity.StartDate = post.StartDate;
            entity.EndDate = post.EndDate;
            entity.LimitedDate = post.LimitedDate;
            entity.SeoTitle = post.SeoTitle;
            entity.MetaTitle = post.MetaTitle;
            entity.MetaKeyword = post.MetaKeyword;
            entity.MetaDescription = post.MetaDescription;
            entity.LinkImage = post.LinkImage;
            entity.isShowHome = post.isShowHome;
            _dbContext.Posts.Update(entity);
            var result = _dbContext.SaveChanges();
            return Json(result);
        }

        // POST: PostController/Delete/5
        [HttpPost]
        public JsonResult Delete(Guid id)
        {        
              var post = _dbContext.Posts.Where(x=>x.PostId==id).First();
              _dbContext.Posts.Remove(post);
              var result = _dbContext.SaveChanges();
              return Json(result);          
        }
    }
}
