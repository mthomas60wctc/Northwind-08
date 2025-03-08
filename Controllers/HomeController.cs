using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class HomeController(DataContext db) : Controller
{
    // this controller depends on the DataContext
    private readonly DataContext _dataContext = db;

    public IActionResult Index() => View(_dataContext.Categories.OrderBy(b => b.CategoryName));
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
    public IActionResult AddProduct(int id)
    {
        ViewBag.CategoryId = id;
        return View(new Product());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
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