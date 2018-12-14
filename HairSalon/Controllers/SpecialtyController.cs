using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class SpecialtyController : Controller
    {
      [HttpGet("/specialty")]
      public ActionResult Index()
      {
          List<Specialty> allSpecialties  = Specialty.GetAll();
          return View(allSpecialties);
      }

      [HttpGet("/specialty/new")]
      public ActionResult New()
      {
        return View();
      }

      [HttpPost("/specialty/create")]
      public ActionResult Create(string name)
      {
        Specialty newSpecialty = new Specialty(name);
        newSpecialty.Save();
        return RedirectToAction("Index", newSpecialty)
      }

    }
  }
