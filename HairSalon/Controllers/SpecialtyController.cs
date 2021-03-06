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
      public ActionResult Create(string specialtyName)
      {
        Specialty newSpecialty = new Specialty(specialtyName);
        newSpecialty.Save();
        List<Specialty> allSpecialties = Specialty.GetAll();
        return View("Index", allSpecialties);
      }

      [HttpGet("/specialties/{id}")]
      public ActionResult Show(int id)
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Specialty selectedSpecialty = Specialty.Find(id);
        List<Stylist> specialtyStylist = selectedSpecialty.GetStylists();
        List<Stylist> allStylists = Stylist.GetAll();
        model.Add("selectedSpecialty", selectedSpecialty);
        model.Add("specialtyStylist", specialtyStylist);
        model.Add("allStylists", allStylists);
        return View(model);
      }

      [HttpPost("/specialties/{specialtyId}/stylists/new")]
      public ActionResult AddStylist(int specialtyId, int stylistId)
      {
        Specialty specialty = Specialty.Find(specialtyId);
        Stylist stylist = Stylist.Find(stylistId);
        specialty.AddStylist(stylist);
        return RedirectToAction("Show", new {id = specialtyId});
      }

      [HttpGet("/specialties/{id}/delete")]
       public ActionResult Delete(int id)
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Specialty specialty = Specialty.Find(id);

        Specialty.DeleteSpecialty(id);

        model.Add("specialty", specialty);
        return RedirectToAction("Index");
      }

      [HttpGet("specialties/{id}/edit")]
       public ActionResult Edit(int id)
       {
         Specialty currentSpecialty = Specialty.Find(id);
         return View(currentSpecialty);
       }

       [HttpPost("/specialties/{id}/edit")]
       public ActionResult Update(string name, int id)
       {
         Specialty specialty = Specialty.Find(id);
         specialty.Edit(name);
         List<Specialty> allSpecialties = Specialty.GetAll();
         return View("Index", allSpecialties);
       }

       [HttpGet("/specialties/delete")]
       public ActionResult DeleteAll()
       {
        Specialty.ClearAll();
        return View();
      }
    }
  }
