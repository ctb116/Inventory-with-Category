
using Microsoft.AspNetCore.Mvc;
using Inventory.Models;
using System.Collections.Generic;

namespace Inventory.Controllers
{
  public class CategoryController : Controller
  {
    [HttpGet("/category")]
    public ActionResult Index()
    {
      List<Category> categoryList = Category.GetAll();
      return View(categoryList);
    }

    [HttpGet("/category/new")]
    public ActionResult CreateForm()
    {
      return View();
    }


    [HttpPost("/category")]
    public ActionResult IndexUpdate()
    {
      Category userInput = new Category(Request.Form["newCategory"]);
      userInput.Save();

      //Redirects to Index() after posting. This prevents the bug of adding the same items when refreshing after posting. Index doesn't refer to the index.cshtml but rather the function called Index() in this controller. You can add a second parameter after this to specify which controller if needed.
      return RedirectToAction("Index");
    }

    [HttpGet("/category/{id}")]
    public ActionResult Details(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Category selectedCategory = Category.Find(id);
      List<MagicCard> categoryCards = selectedCategory.GetMagicCards();
      model.Add("category", selectedCategory);
      model.Add("magiccards", categoryCards);
      return View(model);
    }

    // [HttpPost("/magiccards")]
    // public ActionResult CreateItem(int categoryId, string newCardName, string newCardColor, string newCardRarity, string newCardType, string newCardSet)
    // {
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   Category foundCategory = Category.Find(categoryId);
    //   MagicCard newCard = new MagicCard(newCardName, newCardColor, newCardRarity, newCardType, newCardSet, categoryId);
    //   foundCategory.AddCard(newCard);
    //   List<MagicCard> categoryItems = foundCategory.GetMagicCards();
    //   model.Add("magiccards", categoryItems);
    //   model.Add("category", foundCategory);
    //   return View("Details", model);
    // }
  }
}
