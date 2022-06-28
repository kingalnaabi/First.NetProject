using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SHRS.Data;
using SHRS.Models;



namespace SHRS.Controllers;

public class CategoryController : Controller
{ 
    private readonly ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }
   

    public async Task<IActionResult> Index()
{
        IEnumerable<Category> objCategoryList = await _db.Categories.ToListAsync();
        var CultureState = Request.HttpContext.Features.Get<IRequestCultureFeature>();
   
        if (CultureState.RequestCulture.UICulture.Name == "ar")
        {
            IEnumerable<Category>list = objCategoryList.Select(
                c => new Category
                {
                    Id = c.Id,
                    NameAr = c.NameAr,
                    DisplayOrder = c.DisplayOrder,
                    CreatedDateTime = c.CreatedDateTime
                }
                ).ToList();
        return View(list);
        }else
        {
            IEnumerable<Category> List = objCategoryList.Select(
                            c => new Category
                            {
                                Id = c.Id,
                                Name = c.Name,
                                DisplayOrder = c.DisplayOrder,
                                CreatedDateTime = c.CreatedDateTime
                            }
                            ).ToList();
            return View(List);
        }

}
    //GET
    public IActionResult Create()
    {
        return View();
    }
    //post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        { ModelState.AddModelError("name", "The Displayoreder cannot cxactly match the Name"); }
        if (ModelState.IsValid)
        {
            _db.Categories.Add(obj);
            _db.SaveChanges();
            TempData["Success"] = "Category Created Successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }


//GET
public IActionResult Edit(int? id)

{
    if (id == null || id == 0)
    {
        return NotFound();
    }
    var categoryFromDb = _db.Categories.Find(id);
    //var CategoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
   // var CategoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
   if(categoryFromDb == null)
    {
        return NotFound();
    }
    return View(categoryFromDb);
}

IActionResult NotFound()
{
    throw new NotImplementedException();
}

//post
[HttpPost]
[ValidateAntiForgeryToken]

public IActionResult Edit(Category obj)
{
    if (obj.Name == obj.DisplayOrder.ToString())
    
       ModelState.AddModelError("name", "The Displayoreder cannot cxactly match the Name");
    
    if (ModelState.IsValid)
    {
        _db.Categories.Update(obj);
        _db.SaveChanges();
            TempData["Success"] = "Category Updated Successfully";
            return RedirectToAction("Index");
    }
    return View(obj);
}
    //GET
    public IActionResult Delete(int? id)

    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var categoryFromDb = _db.Categories.Find(id);
        //var CategoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
        // var CategoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
        if (categoryFromDb == null)
        {
            return NotFound();
        }
        return View(categoryFromDb);
    }

    IActionResult Notfound()
    {
        throw new NotImplementedException();
    }

    //post
    [HttpPost]
    [ValidateAntiForgeryToken]

    public IActionResult DeletePost(int? id)
    {
        var obj = _db.Categories.Find(id);
        if (obj == null)
        {
            return NotFound();
        }
        
            _db.Categories.Remove(obj);
            _db.SaveChanges();
        TempData["Success"] = "Category Deleted Successfully";
        return RedirectToAction("Index");
        
       
    }
}