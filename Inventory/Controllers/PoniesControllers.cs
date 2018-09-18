using Microsoft.AspNetCore.Mvc;
using Inventory.Models;
using System.Collections.Generic;

namespace Inventory.Controllers
{
  public class PoniesController : Controller
  {
    [HttpGet("/ponies")]
    public ActionResult Index()
    {
      List<Pony> ponyList = Pony.GetAll();
      return View(ponyList);
    }

    [HttpGet("/ponies/new")]
    public ActionResult CreateForm()
    {
      return View();
    }


    [HttpPost("/ponies")]
    public ActionResult IndexUpdate()
    {
      Pony userInput = new Pony(Request.Form["newPonyName"], Request.Form["newPonyType"], Request.Form["newPonyCutieMark"], Request.Form["newPonyProductType"]);
      userInput.Save();
      List<Pony> ponyList= Pony.GetAll();

      return View("Index", ponyList);
    }
  }
}
