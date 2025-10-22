using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductosController : Controller
    {
        private const string ClaveSessionProductos = "inventarioProductos";

        private List<ProductModel> CargarProductosDeSession()
        {
            if (Session[ClaveSessionProductos] == null)
            {
                var productosIniciales = new List<ProductModel>
                {
                    new ProductModel { id = 1, nombreProducto = "Producto A", precioUnitario = 10.5m, cantidadStock = 5, categoriaProducto = "Categoria 1" },
                    new ProductModel { id = 2, nombreProducto = "Producto B", precioUnitario = 20m, cantidadStock = 10, categoriaProducto = "Categoria 2" },
                    new ProductModel { id = 3, nombreProducto = "Producto C", precioUnitario = 15m, cantidadStock = 0, categoriaProducto = "Categoria 1" }
                };
                Session[ClaveSessionProductos] = productosIniciales;
            }

            return (List<ProductModel>)Session[ClaveSessionProductos];
        }

        // GET: Productos
        public ActionResult Index()
        {
            var inventario = CargarProductosDeSession();
            return View(inventario);
        }

        // GET: Productos/Details/5
        public ActionResult Details(int id)
        {
            var inventario = CargarProductosDeSession();
            var productoSeleccionado = inventario.FirstOrDefault(p => p.id == id);
            if (productoSeleccionado == null)
                return HttpNotFound();

            return View(productoSeleccionado);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var inventario = CargarProductosDeSession();
            
            // Generar nuevo identificador
            int nuevoId = 1;
            if (inventario.Any())
            {
                nuevoId = inventario.Max(p => p.id) + 1;
            }
            model.id = nuevoId;
            inventario.Add(model);

            return RedirectToAction("Index");
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int id)
        {
            var inventario = CargarProductosDeSession();
            var productoModificar = inventario.FirstOrDefault(p => p.id == id);
            if (productoModificar == null)
                return HttpNotFound();

            return View(productoModificar);
        }

        // POST: Productos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var inventario = CargarProductosDeSession();
            var productoActual = inventario.FirstOrDefault(p => p.id == id);
            if (productoActual == null)
                return HttpNotFound();

            // Actualizar propiedades del producto
            productoActual.nombreProducto = model.nombreProducto;
            productoActual.precioUnitario = model.precioUnitario;
            productoActual.cantidadStock = model.cantidadStock;
            productoActual.categoriaProducto = model.categoriaProducto;

            return RedirectToAction("Index");
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int id)
        {
            var inventario = CargarProductosDeSession();
            var productoEliminar = inventario.FirstOrDefault(p => p.id == id);
            if (productoEliminar == null)
                return HttpNotFound();

            return View(productoEliminar);
        }

        // POST: Productos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var inventario = CargarProductosDeSession();
            var productoBorrar = inventario.FirstOrDefault(p => p.id == id);
            if (productoBorrar == null)
                return HttpNotFound();

            inventario.Remove(productoBorrar);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            return DeleteConfirmed(id);
        }
    }
}
