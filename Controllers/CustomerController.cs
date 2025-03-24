using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class CustomerController : Controller
{
    // this controller depends on the NorthwindRepository
    private DataContext _dataContext;
    public CustomerController(DataContext db) => _dataContext = db;

    public IActionResult Customer() => View(_dataContext.Customers);
    public IActionResult Register() => View(new Customer());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(Customer model)
    {

        if (ModelState.IsValid)
        {
            if (!_dataContext.Customers.Any(b => b.CompanyName == model.CompanyName))
            {
                _dataContext.AddCustomer(model);
                return RedirectToAction("Customer");
            }
            ModelState.AddModelError("", "Company Name must be unique");
        }
        return View();
    }
}