using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PostalovTeam.Models;
using File = PostalovTeam.Models.File;

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
        public ActionResult DeleteCategory(int? categoryId)
        {
            Category category = pte.Categories.Find(categoryId);
            return View(category);
        }

        // POST: Test/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult DeleteCategoryConfirmed(int id)
        {
            Category category = pte.Categories.Find(id);
            pte.Categories.Remove(category);
            pte.SaveChanges();
            return RedirectToAction("Index", "Store");
        }

        public ActionResult Products(int categoryId)
        {
            var products = pte.Products.Where(p => p.CategoryId == categoryId).Include("File").ToList();
            return View(products);
        }

        public ActionResult Product(int productId)
        {
            var product = pte.Products.Include("File").SingleOrDefault(x => x.ProductId == productId);
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
                    CategoryId = newProduct.CategoryId,

                };
                pte.Products.Add(product);

                byte[] fileData = null;
                using (var binaryReader = new BinaryReader(Request.Files["photo"].InputStream))
                {
                    fileData = binaryReader.ReadBytes(Request.Files["photo"].ContentLength);
                }
                var photo = new PostalovTeam.Models.File
                {
                    Content = fileData,
                };
                File file = pte.Files.Add(photo);
                product.File = file;
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
        public ActionResult DeleteProduct(int? productId)
        {
            Product product = pte.Products.Find(productId);
            return View(product);
        }

        // POST: Test/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult DeleteProductConfirmed(int id)
        {
            Product product = pte.Products.Find(id);
            pte.Products.Remove(product);
            pte.SaveChanges();
            return RedirectToAction("Index", "Store");
        }

    }
}