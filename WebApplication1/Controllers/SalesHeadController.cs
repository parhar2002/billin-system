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
            var salesHeads = _repository.GetSalesHeadList();
            return View(salesHeads);
        }

        public IActionResult Create()
        {
            var model = new SalesHead
            {
                Prefix = GeneratePrefix(),
                Seq = GetNextSequenceNumber(),
                ListItem = _repository.GetItemList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(SalesHead salesHead)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveSalesHead(salesHead);
                return RedirectToAction("Index");
            }

            // Re-populate ListItem on failure
            salesHead.ListItem = _repository.GetItemList();
            return View(salesHead);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var salesHead = _repository.GetSalesHeadById(id);
            if (salesHead == null)
                return NotFound();

            salesHead.ListItem = _repository.GetItemList();
            return View(salesHead);
        }

        [HttpPost]
        public IActionResult Edit(SalesHead salesHead)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveSalesHead(salesHead);
                return RedirectToAction("Index");
            }

            salesHead.ListItem = _repository.GetItemList();
            return View(salesHead);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var salesHead = _repository.GetSalesHeadById(id);
            if (salesHead == null)
                return NotFound();

            return View(salesHead);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.DeleteSalesHead(id);
            return RedirectToAction("Index");
        }

        private string GeneratePrefix()
        {
            return "INV" + DateTime.Now.ToString("yyyy");
        }

        private int GetNextSequenceNumber()
        {
            return _repository.GetMaxSequenceNumber() + 1;
        }
    }
}
