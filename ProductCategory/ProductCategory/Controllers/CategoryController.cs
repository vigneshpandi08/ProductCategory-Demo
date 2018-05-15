using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductCategoryApplication.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            CategoryEntities entitiesCategory = new CategoryEntities();
            return View(entitiesCategory.ProductCategories);
        }
        [HttpPost]
        public JsonResult Create(ProductCategory category)
        {
            using (CategoryEntities entitiesCategory = new CategoryEntities())
            {
                entitiesCategory.ProductCategories.Add(category);
                entitiesCategory.SaveChanges();
            }

            return Json(category);
        }
    }
}