using ReserVA.Models;
using ReserVA;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Windows.Forms;

namespace ReserVA.Controllers
{
    public static class RecintoController
    {
        public static List<RecintoDTO> ObtenerTodos()
        {
            List<RecintoDTO> listaRecintos;
            using (var contexto = new ReserVAEntities())
            {

                listaRecintos = contexto.Recinto
                    .Select(r => new RecintoDTO
                    {
                        IdRecinto = r.IdRecinto,
                        Nombre = r.Nombre,
                        Direccion = r.Direccion,
                        IdSubzona = r.IdSubzona,
                        Subzona = r.Subzona.Nombre,
                        Barrio = r.Subzona.Barrio.Nombre
                    })
                    .OrderBy(o => o.Nombre)
                    .ToList();
            }
            return listaRecintos;
        }
        public static List<RecintoDTO> ObtenerFiltrados(int idBarrio)
        {
            using (var contexto = new ReserVAEntities())
            {
                List<Subzona> subzonas = contexto.Subzona.Where(w => w.IdBarrio == idBarrio).ToList();
                List<RecintoDTO> listaRecintosSubzona, listaRecintos = new List<RecintoDTO>();

                foreach (Subzona subzona in subzonas)
                {
                    listaRecintosSubzona = contexto.Recinto
                        .Include(i => i.Subzona)
                        .Include(i => i.Subzona.Barrio)
                        .Select(r => new
                        {
                            r.IdRecinto,
                            r.Nombre,
                            r.Direccion,
                            r.IdSubzona,
                            Subzona = r.Subzona.Nombre,
                            Barrio = r.Subzona.Barrio.Nombre
                        })
                        .Where(w => w.IdSubzona == subzona.IdSubzona)
                        .OrderBy(o => o.Nombre)
                        .AsEnumerable()
                        .Select(r => new RecintoDTO
                        {
                            IdRecinto = r.IdRecinto,
                            Nombre = r.Nombre,
                            Direccion = r.Direccion,
                            IdSubzona = r.IdSubzona,
                            Subzona = r.Subzona,
                            Barrio = r.Barrio
                        })
                        .ToList();

                    listaRecintos.AddRange(listaRecintosSubzona);
                }
                return listaRecintos;
            }
        }

        public static bool CrearRecinto(Recinto recinto)
        {
            using (var contexto = new ReserVAEntities())
            {
                if (recinto != null)
                {
                    contexto.Recinto.Add(recinto);
                    contexto.SaveChanges();
                    MessageBox.Show("Recinto creado correctamente.");
                    return true;
                }
                else
                {
                    MessageBox.Show("No se pudo crear el recinto.");
                    return false;
                }
            }
        }

        public static bool EditarRecinto(Recinto recinto)
        {
            using (var contexto = new ReserVAEntities())
            {
                Recinto recintoAEditar = contexto.Recinto.FirstOrDefault(r => r.IdRecinto == recinto.IdRecinto);

                if (recintoAEditar != null)
                {
                    recintoAEditar.Nombre = recinto.Nombre;
                    recintoAEditar.Direccion = recinto.Direccion;
                    recintoAEditar.IdSubzona = recinto.IdSubzona;

                    contexto.SaveChanges();
                    MessageBox.Show("Recinto editado correctamente.");
                    return true;
                }
                else
                {
                    MessageBox.Show("No se pudo editar el recinto.");
                    return false;
                }
            }
        }
        
        public static bool EliminarRecinto(Recinto recinto)
        {
            using (var contexto = new ReserVAEntities())
            {
                Recinto recintoAEliminar = contexto.Recinto.FirstOrDefault(r => r.IdRecinto == recinto.IdRecinto);

                if (recintoAEliminar != null)
                {
                    bool tieneEspacios = contexto.Espacio.Any(e => e.IdRecinto == recintoAEliminar.IdRecinto);

                    if(tieneEspacios)
                    {
                        MessageBox.Show("No se puede eliminar el recinto porque tiene espacios asociados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    
                    contexto.Recinto.Remove(recintoAEliminar);
                    contexto.SaveChanges();
                    MessageBox.Show("Recinto eliminado correctamente.");
                    return true;
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el recinto.");
                    return false;
                }
            }
        }
    }
}
