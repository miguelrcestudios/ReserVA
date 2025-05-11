using RerserVA.Models;
using ReserVA.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ReserVA.Controllers
{
    public static class ReservaController
    {
        public static Reserva Reservar(Espacio espacio, Usuario usuario, DateTime fecha)
        {
            Usuario usuarioRegistrado;

            if (usuario.IdUsuario == 0)
            {
                usuarioRegistrado = UsuarioController.RegistrarUsuarioNoRegistrado(usuario);
                if (usuarioRegistrado == null)
                {
                    MessageBox.Show($"Revise los datos del usuario. Se ha producido un error y su reserva no se ha realizado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            else
            {
                usuarioRegistrado = usuario;
            }

            Reserva nuevaReserva = new Reserva()
            {
                IdEspacio = espacio.IdEspacio,
                IdUsuario = usuarioRegistrado.IdUsuario,
                Fecha = fecha
            };

            using (var context = new ReserVAEntities())
            {
                Reserva reservaExitente = context.Reserva.Where(w => w.IdEspacio == espacio.IdEspacio && w.Fecha.Equals(fecha)).FirstOrDefault();
                if (reservaExitente != null)
                {
                    MessageBox.Show($"No se puede realizar una reserva porque ya existe un reserva en esa fecha.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                context.Reserva.Add(nuevaReserva);
                context.SaveChanges();

                return nuevaReserva;
            }
        }

        public static List<ReservaDTO> ObtenerTodas()
        {
            List<ReservaDTO> listaReservas;
            using (var contexto = new ReserVAEntities())
            {
                listaReservas = contexto.Reserva
                    .Select(r => new
                    {
                        NumeroReserva = r.IdReserva,
                        r.Fecha,
                        r.HoraInicio,
                        r.HoraFin,
                        Espacio = r.Espacio.Nombre,
                        IdRecinto = r.Espacio.Recinto.IdRecinto,
                        Recinto = r.Espacio.Recinto.Nombre,
                        Usuario = r.Usuario.Nombre + " " + r.Usuario.Apellidos
                    })
                    .OrderBy(o => o.Fecha)
                    .ThenBy(o => o.HoraInicio)
                    .AsEnumerable()
                    .Select(r => new ReservaDTO
                    {
                        NumeroReserva = r.NumeroReserva,
                        Fecha = r.Fecha.ToString("dd-MM-yyyy"),
                        HoraInicio = r.HoraInicio.ToString(@"hh\:mm"),
                        HoraFin = r.HoraFin.ToString(@"hh\:mm"),
                        Espacio = r.Espacio,
                        Recinto = r.Recinto,
                        Usuario = r.Usuario
                    })
                    .ToList();
            }

            return listaReservas;
        }
        
        public static List<ReservaDTO> ObtenerFiltradas(int recinto)
        {
            List<ReservaDTO> listaReservas;
            using (var context = new ReserVAEntities())
            {
                listaReservas = context.Reserva
                    .Select(r => new
                    {
                        NumeroReserva = r.IdReserva,
                        r.Fecha,
                        r.HoraInicio,
                        r.HoraFin,
                        Espacio = r.Espacio.Nombre,
                        IdRecinto = r.Espacio.Recinto.IdRecinto,
                        Recinto = r.Espacio.Recinto.Nombre,
                        Usuario = r.Usuario.Nombre + " " + r.Usuario.Apellidos
                    })
                    .Where(w => w.IdRecinto == recinto)
                    .OrderBy(o => o.Fecha)
                    .ThenBy(o => o.HoraInicio)
                    .AsEnumerable()
                    .Select(r => new ReservaDTO
                    {
                        NumeroReserva = r.NumeroReserva,
                        Fecha = r.Fecha.ToString("dd-MM-yyyy"),
                        HoraInicio = r.HoraInicio.ToString(@"hh\:mm"),
                        HoraFin = r.HoraFin.ToString(@"hh\:mm"),
                        Espacio = r.Espacio,
                        Recinto = r.Recinto,
                        Usuario = r.Usuario
                    })
                    .ToList();
            }
            return listaReservas;
        }
    }

}