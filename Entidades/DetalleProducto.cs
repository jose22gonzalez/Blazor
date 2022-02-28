using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Blazor.Entidades
{
    public class DetalleProducto
    {
        [Key]
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public string? Descripcion { get; set; }
        public double Cantidad { get; set; }
        public double Precio { get; set; }

        public DetalleProducto()
        {
            Id = 0;
            ProductoId = 0;
            Descripcion = "";
            Cantidad = 0;
            Precio = 0;
        }

        public DetalleProducto(int productoId, string descricion, double cantidad, double precio)
        {
            Id = 0;
            ProductoId = productoId;
            Descripcion = descricion;
            Cantidad = cantidad;
            Precio = precio;
        }

        

    }
}