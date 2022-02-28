using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.DAL;
using Blazor.Entidades;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Blazor.BLL
{
    public class ProductoBLL
    {
        public static bool Existe(int productoID)
        {
            Contexto context = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = context.Productos.Any(l => l.ProductoId == productoID);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                context.Dispose();
            }

            return encontrado;
        }

        public static bool Existe(string? descripcion)
        {
            Contexto context = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = context.Productos.Any(l => l.Descripcion == descripcion);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                context.Dispose();
            }

            return encontrado;
        }

        public static bool Insertar(Productos productos)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                contexto.Productos.Add(productos);
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        public static bool Guardar(Productos productos)
        {


            if (!Existe(productos.ProductoId))
                return Insertar(productos);
            else
                return Modificar(productos);
        }

        public static bool Modificar(Productos productos)
        {

            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Database.ExecuteSqlRaw($"Delete FROM DetalleProducto where ProductoId = {productos.ProductoId}");
                foreach (var anterior in productos.DetalleProducto)
                {
                    contexto.Entry(anterior).State = EntityState.Added;
                }

                contexto.Entry(productos).State = EntityState.Modified;
                paso = (contexto.SaveChanges() > 0);
            }

            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        public static bool Eliminar(int productoID)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                var adicionales = contexto.Productos.Find(productoID);

                if (adicionales != null)

                {

                    contexto.Productos.Remove(adicionales);

                    paso = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        public static Productos Buscar(int productoID)
        {
            Contexto contexto = new Contexto();
            Productos productos;

            try
            {
                productos = contexto.Productos.Include(x => x.DetalleProducto)
                        .Where(P => P.ProductoId == productoID)
                        .SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return productos;
        }

        public static List<Productos> GetList(Expression<Func<Productos, bool>> criterio)
        {
            Contexto contexto = new Contexto();
            List<Productos> lista = new List<Productos>();
            try
            {
                lista = contexto.Productos.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;
        }

        public static List<Productos> GetLista()
        {
            using (var contexto = new Contexto())
            {
                return contexto.Productos.ToList();
            }
        }
    }
}