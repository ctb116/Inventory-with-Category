
using Microsoft.AspNetCore.Mvc;
using Inventory.Models;
using System.Collections.Generic;

namespace Inventory.Controllers
{
  public class MagicCardsController : Controller
  {
    [HttpGet("/magiccards")]
    public ActionResult Index()
    {
      List<MagicCard> cardList = MagicCard.GetAll();
      return View(cardList);
    }

    [HttpGet("/magiccards/new")]
    public ActionResult CreateForm()
    {
      return View();
    }


    [HttpPost("/magiccards")]
    public ActionResult IndexUpdate()
    {
      MagicCard userInput = new MagicCard(Request.Form["newCardName"], Request.Form["newCardColor"], Request.Form["newCardRarity"], Request.Form["newCardType"], Request.Form["newCardSet"]);
      userInput.Save();
      List<MagicCard> cardList= MagicCard.GetAll();

      return View("Index", cardList);
    }
  }
}
