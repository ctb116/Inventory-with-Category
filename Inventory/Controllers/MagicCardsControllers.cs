
using Microsoft.AspNetCore.Mvc;
using Inventory.Models;
using System.Collections.Generic;

namespace Inventory.Controllers
{
  public class MagicCardsController : Controller
  {
    [HttpGet("/category/{id}/magiccards")]
    public ActionResult Index()
    {
      List<MagicCard> cardList = MagicCard.GetAll();
      return View(cardList);
    }

    // [HttpGet("/category/{id}/magiccards/new")]
    // public ActionResult CreateForm()
    // {
    //   return View();
    // }

    //
    // [HttpPost("/magiccards")]
    // public ActionResult IndexUpdate()
    // {
    //   MagicCard userInput = new MagicCard(Request.Form["newCardName"], Request.Form["newCardColor"], Request.Form["newCardRarity"], Request.Form["newCardType"], Request.Form["newCardSet"]);
    //   userInput.Save();
    //
    //   //Redirects to Index() after posting. This prevents the bug of adding the same items when refreshing after posting. Index doesn't refer to the index.cshtml but rather the function called Index() in this controller. You can add a second parameter after this to specify which controller if needed.
    //   return RedirectToAction("Index");
    // }
  }
}
