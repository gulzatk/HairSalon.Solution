using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class SpecialtyController : Controller
    {
      [HttpGet("/specialties")]
      public ActionResult Index()
      {
          List<Specialty> allSpecialties  = Specialty.GetAll();
          return View(allSpecialties);
      }

      [HttpGet("/specialties/new")]
      public ActionResult New()
      {
        return View();
      }

      [HttpPost("/specialties")]
      public ActionResult Create(string name)
      {
        Specialty newSpecialty = new Specialty(name);
        newSpecialty.Save();
        List<Specialty> all allSpecialties = Specialty.GetAll();
        return View("Index", allSpecialties);
      }

      [HttpGet("/specialties/{id}")]
      public ActionResult Show(int id)
      {
        Dictionary<string, object> model = Dictionary<string, object>();
        Specialty selectedSpecialty = Specialty.Find(id);
        List<Stylist> specialtyStylist = selectedSpecialty.GetStylist();
        List<Stylist> allStylists = Stylist.GetAll();
        model.Add("selectedSpecialty", selectedSpecialty);
        model.Add("specialtyStylist", specialtyStylist);
        model.Add("allStylists", allStylists);
        return View(model);
      }
    }
  }
