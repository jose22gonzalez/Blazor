using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;




namespace Blazor.Entidades
{
    public class Productos
    {
        
        [Key] public int ProductoId { get; set; }

         [Required(ErrorMessage ="Es obligatorio introducir la descricion")]
        public string? Descripcion { get; set; }

         [Range(1,int.MaxValue,ErrorMessage ="El costo sale del rango")]
        public double Costo { get; set; }
        public double Ganancia { get; set; }
        public double Precio { get; set; }



       [ForeignKey("ProductoId")]
        public virtual List<DetalleProducto> DetalleProducto {get; set;} = new List<DetalleProducto>();

       

    }
}