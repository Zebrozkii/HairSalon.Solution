// using Microsoft.AspNetCore.Mvc;
// using HairSalon.Models;
// using System.Collections.Generic;
//
// namespace HairSalon.Controllers
// {
//   public class ClientsController : Controller
//   {
//
//     [HttpGet("/stylists/{stylistId}/clients/new")]
//     public ActionResult New(int stylistId)
//     {
//        Stylist stylist = stylist.Find(stylistId);
//        return View(stylist);
//     }
//
//     [HttpGet("/stylists/{stylistId}/clients/{clientsId}")]
//     public ActionResult Show(int stylistId, int clientId)
//     {
//       Client client = Client.Find(clientId);
//       Dictionary<string, object> model = new Dictionary<string, object>();
//       Stylist stylist = Stylist.Find(stylistId);
//       model.Add("client", client);
//       model.Add("stylist", stylist);
//       return View(model);
//     }
//     [HttpPost("/items/delete")]
//     public ActionResult DeleteAll()
//     {
//       Client.ClearAll();
//       return View();
//     }
//
//   }
// }