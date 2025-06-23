using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    public class CompanyController : Controller
    {
        private readonly CompanyRepository _repository;

        public CompanyController(IConfiguration configuration)
        {
            _repository = new CompanyRepository(configuration);
        }

        public IActionResult Index()
        {
            var Companys = _repository.GetListCompanies();
            return View(Companys);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveCompanies(company); 
                return RedirectToAction("Index");
            }
            return View(company);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var company = _repository.GetCompanyById(id);
            if (company == null)
                return NotFound();

            return View(company);
        }

        [HttpPost]
        public IActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveCompanies(company); 
                return RedirectToAction("Index");
            }
            return View(company);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var company = _repository.GetCompanyById(id);
            if (company == null)
                return NotFound();

            return View(company); 
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.DeleteCompany(id);
            return RedirectToAction("Index");
        }


    }

}

