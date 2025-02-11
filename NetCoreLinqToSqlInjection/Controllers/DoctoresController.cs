using Microsoft.AspNetCore.Mvc;
using NetCoreLinqToSqlInjection.Models;
using NetCoreLinqToSqlInjection.Repositories;

namespace NetCoreLinqToSqlInjection.Controllers
{
    public class DoctoresController : Controller
    {
        IRepositoryDoctores repo;

        public DoctoresController(IRepositoryDoctores repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Doctor> doctores = this.repo.GetDoctores();
            return View(doctores);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Doctor doc)
        {
            this.repo.InsertDoctor(doc.IdDoctor, doc.Apellido
                , doc.Especialidad, doc.Salario, doc.IdHospital);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int iddoctor)
        {
            this.repo.DeleteDoctor(iddoctor);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int iddoctor)
        {
            Doctor d = this.repo.FindDoctor(iddoctor);
            return View(d);
        }

        [HttpPost]
        public IActionResult Edit(Doctor d)
        {
            this.repo.UpdateDoctor(d.IdHospital, d.IdDoctor, d.Apellido, d.Especialidad, d.Salario);
            return RedirectToAction("Index");
        }

        public IActionResult BuscadorDoctores()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BuscadorDoctores(string especialidad)
        {
            List<Doctor> d = this.repo.GetDoctoresEspecialidad(especialidad);
            return View(d);
        }
    }
}
