using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    public class SalesHeadController : Controller
    {
        private readonly SalesHeadRepository _repository;

        public SalesHeadController(IConfiguration configuration)
        {
            _repository = new SalesHeadRepository(configuration);
        }

        public IActionResult Index()
        {
            var SalesHeads = _repository.GetSalesHeadList();
            return View(SalesHeads);
        }
        public IActionResult Create()
        {
            var model = new SalesHead
            {
                ListItem = _repository.GetItemList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(SalesHead SalesHead)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveSalesHead(SalesHead); 
                return RedirectToAction("Index");
            }
            return View(SalesHead);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var SalesHead = _repository.GetSalesHeadById(id);
            if (SalesHead == null)
                return NotFound();

            SalesHead.ListItem = _repository.GetItemList();

            return View(SalesHead);
        }

        [HttpPost]
        public IActionResult Edit(SalesHead SalesHead)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveSalesHead(SalesHead); 
                return RedirectToAction("Index");
            }
            return View(SalesHead);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var SalesHead = _repository.GetSalesHeadById(id);
            if (SalesHead == null)
                return NotFound();

            return View(SalesHead); 
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.DeleteSalesHead(id);
            return RedirectToAction("Index");
        }


    }

}

