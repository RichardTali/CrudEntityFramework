//AGREGAR ESTAS DOS REFERENCIAS
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using CRUDPRODUCTO.Models;
using CRUDPRODUCTO.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CRUDPRODUCTO.Controllers
{
    public class HomeController : Controller
    {
        private readonly CrudproductopracticaContext _DBContext;

        public HomeController(CrudproductopracticaContext context)
        {
            _DBContext = context;
        }

        public IActionResult Index()
        {
            List<Producto>lista=_DBContext.Productos.Include(c =>c.oCategoria).ToList();    
            return View(lista);
        }

        //MÉTODO PARA QUE DESPLIEGE LOS CARGOS EL SELECCIONADOR

        [HttpGet]
        public IActionResult Producto_Detalle(int idProducto)
        {
            ProductoVM oProductoVM = new ProductoVM()
            {
                oProducto = new Producto(),
                oListaCategoria = _DBContext.Categoria.Select(categoria => new SelectListItem()
                {
                    Text = categoria.NombreCategoria,
                    Value = categoria.IdCategoria.ToString()
                }).ToList()

            };


            //Este if es para el editar con los parametros que se reciben 
            if (idProducto != 0)
            {
                oProductoVM.oProducto = _DBContext.Productos.Find(idProducto);
            }


            return View(oProductoVM);

        }

        //MÉTODO PARA AGREGAR UN PRODUCTO
        [HttpPost]
        public IActionResult Producto_Detalle(ProductoVM oProductoVM)
        {
            if (oProductoVM.oProducto.IdProducto == 0)
            {
                _DBContext.Productos.Add(oProductoVM.oProducto);
            }
            else
            {
                _DBContext.Productos.Update(oProductoVM.oProducto);
            }
            

            _DBContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }




        // Método GET para mostrar la vista de confirmación de eliminación
        // Método GET para mostrar la vista de confirmación de eliminación
        [HttpGet]
        public IActionResult Eliminar(int idProducto)
        {
            Producto oProducto = _DBContext.Productos.Find(idProducto);

            if (oProducto == null)
            {
                return NotFound();
            }

            return View(oProducto);
        }

        // Método POST para eliminar el producto
        [HttpPost]
        public IActionResult EliminarConfirmado(int idProducto)
        {
            // Cargar la entidad desde la base de datos usando su IdProducto
            var producto = _DBContext.Productos.Find(idProducto);
            if (producto != null)
            {
                _DBContext.Productos.Remove(producto);
                _DBContext.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }



    }
}
