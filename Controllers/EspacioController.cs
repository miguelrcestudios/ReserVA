using ReserVA.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace ReserVA.Controllers
{
    public static class EspacioController
    {
        public static List<Espacio> ObtenerTodos()
        {
            List<Espacio> listaEspacios;
            using (var contexto = new ReserVAEntities())
            {
                listaEspacios = contexto.Espacio
                    .Include(i => i.Recinto)
                    .Include(i => i.Recinto.Subzona)
                    .Include(i => i.Recinto.Subzona.Barrio)
                    .OrderBy(o => o.Recinto.Nombre)
                    .ThenBy(o => o.Nombre)
                    .ToList();
                
            }
            return listaEspacios;
        }

        public static List<Espacio> ObtenerFiltradosPorBarrio(int idBarrio)
        {
            using (var contexto = new ReserVAEntities())
            {
                List<Subzona> subzonas = contexto.Subzona.Where(w => w.IdBarrio == idBarrio).ToList();
                List<Espacio> listaEspaciosSubzona,listaEspacios = new List<Espacio>();

                foreach (Subzona subzona in subzonas)
                {
                    listaEspaciosSubzona = contexto.Espacio
                        .Include(i => i.Recinto)
                        .Include(i => i.Recinto.Subzona)
                        .Include(i => i.Recinto.Subzona.Barrio)
                        .Where(w => w.Recinto.IdSubzona == subzona.IdSubzona)
                        .OrderBy(o => o.Recinto.Nombre)
                        .ThenBy(o => o.Nombre)
                        .ToList();

                    listaEspacios.AddRange(listaEspaciosSubzona);
                }

                return listaEspacios;
            }
        }

        public static List<Espacio> ObtenerFiltradosPorRecinto(int idRecinto)
        {
            List<Espacio> listaEspacios;
            using (var contexto = new ReserVAEntities())
            {
                listaEspacios = contexto.Espacio
                    .Include(i => i.Recinto)
                    .Where(w => w.IdRecinto == idRecinto)
                    .OrderBy(o => o.Recinto.Nombre)
                    .ThenBy(o => o.Nombre)
                    .ToList();
            }

            return listaEspacios;
        }

        public static bool CrearEspacio(Espacio espacio)
        {
            using (var contexto = new ReserVAEntities())
            {
                if (espacio != null)
                {
                    contexto.Espacio.Add(espacio);
                    contexto.SaveChanges();
                    MessageBox.Show("Espacio creado correctamente.");
                    return true;
                }
                else
                {
                    MessageBox.Show("No se pudo crear el espacio.");
                    return false;
                }
            }
        }

        public static bool EditarEspacio(Espacio espacio)
        {
            using (var contexto = new ReserVAEntities())
            {
                Espacio espacioAEditar = contexto.Espacio.FirstOrDefault(e => e.IdEspacio == espacio.IdEspacio);

                if (espacioAEditar != null)
                {
                    espacioAEditar.Nombre = espacio.Nombre;
                    espacioAEditar.Tipo = espacio.Tipo;
                    espacioAEditar.Descripcion = espacio.Descripcion;
                    espacioAEditar.IdRecinto = espacio.IdRecinto;

                    contexto.SaveChanges();
                    MessageBox.Show("Espacio editado correctamente.");
                    return true;
                }
                else
                {
                    MessageBox.Show("No se pudo editar el espacio.");
                    return false;
                }
            }
        }

        public static bool EliminarEspacio(Espacio espacio)
        {
            using (var contexto = new ReserVAEntities())
            {
                Espacio espacioAEliminar = contexto.Espacio.FirstOrDefault(r => r.IdEspacio == espacio.IdEspacio);

                if (espacioAEliminar != null)
                {
                    bool tieneReservas = contexto.Reserva.Any(r => r.IdEspacio == espacioAEliminar.IdEspacio);

                    if (tieneReservas)
                    {
                        MessageBox.Show("No se puede eliminar el espacio porque tiene reservas asociadas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    contexto.Espacio.Remove(espacioAEliminar);
                    contexto.SaveChanges();
                    MessageBox.Show("Espacio eliminado correctamente.");
                    return true;
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el espacio.");
                    return false;
                }
            }
        }
    }
}
