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

      //Redirects to Index() after posting. This prevents the bug of adding the same items when refreshing after posting. Index doesn't refer to the index.cshtml but rather the function called Index() in this controller. You can add a second parameter after this to specify which controller if needed.
      return RedirectToAction("Index");
    }
  }
}
