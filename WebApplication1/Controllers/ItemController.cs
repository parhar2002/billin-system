using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    public class ItemController : Controller
    {
        private readonly ItemRepository _repository;

        public ItemController(IConfiguration configuration)
        {
            _repository = new ItemRepository(configuration);
        }

        public IActionResult Index()
        {
            var Items = _repository.GetItemList();
            return View(Items);
        }
        public IActionResult Create()
        {
            var model = new Item
            {
                Companies = _repository.GetCompaniesList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(Item Item)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveItem(Item);
                return RedirectToAction("Index");
            }
            return View(Item);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = _repository.GetItemById(id);
            if (item == null)
                return NotFound();

            item.Companies = _repository.GetCompaniesList();

            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(Item Item)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveItem(Item);
                return RedirectToAction("Index");
            }
            return View(Item);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var Item = _repository.GetItemById(id);
            if (Item == null)
                return NotFound();

            return View(Item);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.DeleteItem(id);
            return RedirectToAction("Index");
        }

       
    }

}

