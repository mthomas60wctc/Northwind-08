using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ProductController : Controller
{
    // this controller depends on the NorthwindRepository
    private DataContext _dataContext;
    public ProductController(DataContext db) => _dataContext = db;
    public IActionResult Discount() => View(_dataContext.Discounts.Include("Product").Where(d => d.StartTime <= DateTime.Now && d.EndTime > DateTime.Now).OrderBy(d => d.EndTime));
    public IActionResult Category() => View(_dataContext.Categories.OrderBy(c => c.CategoryName));
    public IActionResult AddCategory() => View(new Category());
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddCategory(Category model)
    {
        if (ModelState.IsValid)
        {
            if (_dataContext.Categories.Any(b => b.CategoryName == model.CategoryName))
            {
                ModelState.AddModelError("", "Name must be unique");
            }
            else
            {
                _dataContext.AddCategory(model);
                return RedirectToAction("Index");
            }
        }
        return View();
    }
    public IActionResult DeleteCategory(int id)
    {
        _dataContext.DeleteCategory(_dataContext.Categories.FirstOrDefault(b => b.CategoryId == id));
        return RedirectToAction("Index");
    }
    public IActionResult CategoryDetail(int id) => View(new ProductViewModel
    {
        category = _dataContext.Categories.FirstOrDefault(b => b.CategoryId == id),
        Products = _dataContext.Products.Where(p => p.CategoryId == id)
    });

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddProduct(int id)
    {
        ViewBag.CategoryId = id;
        return View(new Product());
    }

    public IActionResult AddProduct(int id, Product post)
    {
        post.CategoryId = id;
        if (ModelState.IsValid)
        {
            _dataContext.AddProduct(post);
            return RedirectToAction("CategoryDetail", new { id = id });
        }
        @ViewBag.CategoryId = id;
        return View();
    }
    public IActionResult DeleteProduct(int id)
    {
        Product post = _dataContext.Products.FirstOrDefault(p => p.ProductId == id);
        int CategoryId = post.CategoryId;
        _dataContext.DeleteProduct(post);
        return RedirectToAction("CategoryDetail", new { id = CategoryId });
    }
}