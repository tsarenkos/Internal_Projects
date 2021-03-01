using System;
using DataTransfer_App.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataTransfer_App.WEB.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IPurchaseService _purchaseService;
        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        public IActionResult Index()
        {
            return View(_purchaseService.GetAll());
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException();
            }
            _purchaseService.Add(name);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var purchase = _purchaseService.GetById(id);
            return View(purchase);
        }

        [HttpPost]
        public IActionResult Edit(int id, string name)
        {
            _purchaseService.Edit(id, name);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {            
            _purchaseService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
