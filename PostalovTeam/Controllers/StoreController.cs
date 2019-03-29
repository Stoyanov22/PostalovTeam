using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PostalovTeam.Models;

namespace PostalovTeam.Controllers
{
    [AllowAnonymous]
    public class StoreController : Controller
    {
        PostalovTeamEntities pte = new PostalovTeamEntities();

        // GET: Store
        public ActionResult Index()
        {
            var categories = pte.Categories.ToList();
            return View(categories);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CreateCategory()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CreateCategory(Category newCategory)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = newCategory.Name
                };
                pte.Categories.Add(category);
                pte.SaveChanges();
            }

            return RedirectToAction("Index", "Store");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditCategory(int categoryId)
        {
            var category = pte.Categories.SingleOrDefault(x => x.CategoryId == categoryId);
            return View(category);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditCategory(Category newCategory)
        {
            if (ModelState.IsValid)
            {
                var category = pte.Categories.SingleOrDefault(x => x.CategoryId == newCategory.CategoryId);
                category.Name = newCategory.Name;
                pte.SaveChanges();
            }
            return RedirectToAction("Index", "Store");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult DeleteCategory(int categoryId)
        {
            var category = pte.Categories.SingleOrDefault(x => x.CategoryId == categoryId);
            pte.Categories.Remove(category);
            return RedirectToAction("Index", "Store");
        }

        public ActionResult Products(int categoryId)
        {
            var products = pte.Products.Where(p => p.CategoryId == categoryId).ToList();
            return View(products);
        }

        public ActionResult Product(int productId)
        {
            var product = pte.Products.SingleOrDefault(x => x.ProductId == productId);
            return View(product);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CreateProduct()
        {
            var categories = pte.Categories.ToList();
            return View(categories);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CreateProduct(Product newProduct)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Name = newProduct.Name,
                    Price = newProduct.Price,
                    CategoryId = newProduct.CategoryId
                };
                pte.Products.Add(product);
                pte.SaveChanges();
            }

            return RedirectToAction("Index", "Store");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditProduct(int productId)
        {
            var product = pte.Products.SingleOrDefault(x => x.ProductId == productId);
            return View(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditProduct(Product newProduct)
        {
            if (ModelState.IsValid)
            {
                var product = pte.Products.SingleOrDefault(x => x.ProductId == newProduct.ProductId);
                product.Name = newProduct.Name;
                product.Price = newProduct.Price;
                product.CategoryId = newProduct.CategoryId;
                pte.SaveChanges();
            }
            return RedirectToAction("Index", "Store");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult DeleteProduct(int productId)
        {
            var product = pte.Products.SingleOrDefault(x => x.ProductId == productId);
            pte.Products.Remove(product);
            return RedirectToAction("Index", "Store");
        }

    }
}