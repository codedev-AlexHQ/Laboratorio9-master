using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace WebApplication1.Models
{
    public class ProductModel
    {
        public int id { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string nombreProducto { get; set; }

        [Required]
        [Display(Name = "Precio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal precioUnitario { get; set; }

        [Required]
        [Display(Name = "Stock")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
        public int cantidadStock { get; set; }

        [Required]
        [Display(Name = "Categoria")]
        public string categoriaProducto { get; set; }
    }
}
