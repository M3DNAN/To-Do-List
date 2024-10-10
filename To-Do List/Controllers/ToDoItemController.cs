using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using To_Do_List.Data;
using To_Do_List.Models;

namespace To_Do_List.Controllers
{
    public class ToDoItemController : Controller
    {
        ApplicationDbContext Context = new ApplicationDbContext();

        public IActionResult Index()
        {
            var items = Context.ToDoItems.Include(e => e.Customer).Where(e => e.Customer.Name.ToLower() ==
              Request.Cookies["customername"].ToLower()).ToList();
            return View(items);
        }
        
      
        public IActionResult CreateNew()
        {
            return View();
        }
        public IActionResult AddToDb(ToDoItem ToDoItem, IFormFile File)
        {
            var customer = Context.Customers.Where(e => e.Name.ToLower() ==
              Request.Cookies["customername"].ToLower()).AsNoTracking().ToList();
            if (File != null && File.Length > 0)
            {
                var fileName = File.FileName;
                var absolutePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", fileName);
                var filePath = Path.Combine("Files", fileName);
                using (var stream = System.IO.File.Create(absolutePath))
                {
                    File.CopyTo(stream);
                }
                ToDoItem.File = filePath;
            }
            ToDoItem.CustomerId = customer[0].Id;
            Context.ToDoItems.Add(ToDoItem);
            Context.SaveChanges();
            TempData["Add"] = "";

            return RedirectToAction("Index");
        }
    

        [HttpPost]
        public IActionResult EditToDb(ToDoItem updatedToDoItem, IFormFile File)
        {
            var existingToDoItem = Context.ToDoItems.FirstOrDefault(e => e.Id == updatedToDoItem.Id);
            if (existingToDoItem == null)
            {
                return NotFound();
            }

            existingToDoItem.Title = updatedToDoItem.Title;
            existingToDoItem.Description = updatedToDoItem.Description;
            existingToDoItem.Deadline = updatedToDoItem.Deadline;
            
            if (File != null && File.Length > 0)
            {
                var fileName = File.FileName;
                var absolutePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", fileName);
                var filePath = Path.Combine("Files", fileName);
                using (var stream = System.IO.File.Create(absolutePath))
                {
                    File.CopyTo(stream);
                }

                existingToDoItem.File = filePath;
            }

            Context.SaveChanges();

            TempData["Update"] = "";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int Id)
        {
            var ToDoItem = Context.ToDoItems.Find(Id);
            return View(ToDoItem);
        }
        //public IActionResult EditToDb(ToDoItem ToDoItem, IFormFile File)
        //{
        //    Context.ToDoItems.Update(ToDoItem);
        //    Context.SaveChanges();
        //    TempData["update"] = "";
        //    return RedirectToAction("Index");

        //}
        public IActionResult Delete(ToDoItem toDoItem)
        {
            Context.ToDoItems.Remove(toDoItem);
            Context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
