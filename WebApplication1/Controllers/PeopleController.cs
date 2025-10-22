using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PeopleController : Controller
    {
        private const string ClaveSessionPersonas = "listadoPersonas";

        // Obtiene la lista de personas desde la sesión
        private List<PersonModel> ObtenerListaPersonas()
        {
            if (Session[ClaveSessionPersonas] == null)
            {
                var listaInicial = new List<PersonModel>
                {
                    new PersonModel { id = 1, Nombre = "John", Apellido = "Doe" },
                    new PersonModel { id = 2, Nombre = "Jane", Apellido = "Smith" },
                    new PersonModel { id = 3, Nombre = "Michael", Apellido = "Johnson" }
                };
                Session[ClaveSessionPersonas] = listaInicial;
            }

            return (List<PersonModel>)Session[ClaveSessionPersonas];
        }

        // GET: People
        public ActionResult Index()
        {
            var listaPersonas = ObtenerListaPersonas();
            return View(listaPersonas);
        }

        // GET: People/Details/5
        public ActionResult Details(int id)
        {
            var listaPersonas = ObtenerListaPersonas();
            var personaEncontrada = listaPersonas.FirstOrDefault(p => p.id == id);
            if (personaEncontrada == null)
                return HttpNotFound();

            return View(personaEncontrada);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var listaPersonas = ObtenerListaPersonas();

            // Calcular el siguiente ID disponible
            int idSiguiente = 1;
            if (listaPersonas.Any())
            {
                idSiguiente = listaPersonas.Max(p => p.id) + 1;
            }
            model.id = idSiguiente;

            listaPersonas.Add(model);

            return RedirectToAction("Index");
        }

        // GET: People/Edit/5
        public ActionResult Edit(int id)
        {
            var listaPersonas = ObtenerListaPersonas();
            var personaParaEditar = listaPersonas.FirstOrDefault(p => p.id == id);
            if (personaParaEditar == null)
                return HttpNotFound();

            return View(personaParaEditar);
        }

        // POST: People/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PersonModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var listaPersonas = ObtenerListaPersonas();
            var personaExistente = listaPersonas.FirstOrDefault(p => p.id == id);
            if (personaExistente == null)
                return HttpNotFound();

            // Actualizar campos de la persona
            personaExistente.Nombre = model.Nombre;
            personaExistente.Apellido = model.Apellido;

            return RedirectToAction("Index");
        }

        // GET: People/Delete/5
        public ActionResult Delete(int id)
        {
            var listaPersonas = ObtenerListaPersonas();
            var personaAEliminar = listaPersonas.FirstOrDefault(p => p.id == id);
            if (personaAEliminar == null)
                return HttpNotFound();

            return View(personaAEliminar);
        }

        // POST: People/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var listaPersonas = ObtenerListaPersonas();
            var personaABorrar = listaPersonas.FirstOrDefault(p => p.id == id);
            if (personaABorrar == null)
                return HttpNotFound();

            listaPersonas.Remove(personaABorrar);

            return RedirectToAction("Index");
        }

        // Mantener el mapeo del POST Delete para compatibilidad
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            return DeleteConfirmed(id);
        }
    }
}