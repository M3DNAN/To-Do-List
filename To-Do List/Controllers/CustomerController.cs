using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using To_Do_List.Data;
using To_Do_List.Models;

namespace To_Do_List.Controllers
{
    public class CustomerController : Controller
    {
        ApplicationDbContext Context = new ApplicationDbContext();

        public IActionResult Create()
        {
            return View();
        }
        public IActionResult AddName(string name)
        {
            var customer = Context.Customers.FirstOrDefault(d => d.Name == name);
            if (customer == null)
            {
                customer = new Customer { Name = name };
                Context.Customers.Add(customer);
                Context.SaveChanges();
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Append("customername", name, cookieOptions);
            }

            return RedirectToAction("Index", "ToDoItem");
        }
    }
}
