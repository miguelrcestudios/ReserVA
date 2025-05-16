using ReserVA.Models;
using ReserVA.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ReserVA;

namespace ReserVA.Controllers
{
    public static class ReservaController
    {
        public static Reserva Reservar(Espacio espacio, Usuario usuario, DateTime fecha, DateTime horaInicio, DateTime horaFin)
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
                Fecha = fecha,
                HoraInicio = new TimeSpan(horaInicio.Hour, horaInicio.Minute, 0),
                HoraFin = new TimeSpan(horaFin.Hour, horaFin.Minute, 0)
            };

            using (var context = new ReserVAEntities())
            {
                List<Reserva> reservasFecha = context.Reserva.Where(w => w.IdEspacio == espacio.IdEspacio && w.Fecha.Equals(fecha)).ToList();
                bool reservaExistente = false;

                foreach (var reserva in reservasFecha)
                {
                    var reserva_horaInicio = fecha.AddHours(reserva.HoraInicio.Hours);
                    var reserva_horaFin = fecha.AddHours(reserva.HoraFin.Hours);

                    if (horaInicio < reserva_horaFin && horaFin > reserva_horaInicio)
                    {
                        reservaExistente = true;
                        break;
                    }
                }

                if (reservaExistente)
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
                        r.Espacio.Recinto.IdRecinto,
                        Recinto = r.Espacio.Recinto.Nombre,
                        Usuario = r.Usuario.Nombre + " " + r.Usuario.Apellidos
                    })
                    .AsEnumerable()
                    .Where(w => w.Fecha >= DateTime.Today && (w.Fecha > DateTime.Today || w.HoraFin >= DateTime.Now.AddHours(-1).TimeOfDay))
                    .OrderBy(o => o.Fecha)
                    .ThenBy(o => o.HoraInicio)
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
                        r.Espacio.Recinto.IdRecinto,
                        Recinto = r.Espacio.Recinto.Nombre,
                        Usuario = r.Usuario.Nombre + " " + r.Usuario.Apellidos
                    })
                    .AsEnumerable()
                    .Where(w => w.IdRecinto == recinto && (w.Fecha >= DateTime.Today && (w.Fecha > DateTime.Today || w.HoraFin >= DateTime.Now.AddHours(-1).TimeOfDay)))
                    .OrderBy(o => o.Fecha)
                    .ThenBy(o => o.HoraInicio)
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

        public static List<ReservaDTO> ObtenerProximas(int idUsuario)
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
                        r.Espacio.Recinto.IdRecinto,
                        Recinto = r.Espacio.Recinto.Nombre,
                        r.Usuario.IdUsuario,
                        Usuario = r.Usuario.Nombre + " " + r.Usuario.Apellidos
                    })
                    .AsEnumerable()
                    .Where(w => w.IdUsuario == idUsuario && (w.Fecha >= DateTime.Today && (w.Fecha > DateTime.Today || w.HoraFin >= DateTime.Now.AddHours(-1).TimeOfDay)))
                    .OrderBy(o => o.Fecha)
                    .ThenBy(o => o.HoraInicio)
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
        
        public static List<ReservaDTO> ObtenerHistorial(int idUsuario)
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
                        r.Espacio.Recinto.IdRecinto,
                        Recinto = r.Espacio.Recinto.Nombre,
                        r.Usuario.IdUsuario,
                        Usuario = r.Usuario.Nombre + " " + r.Usuario.Apellidos
                    })
                    .AsEnumerable()
                    .Where(w => w.IdUsuario == idUsuario && (w.Fecha <= DateTime.Today && (w.Fecha < DateTime.Today || w.HoraFin <= DateTime.Now.AddHours(-1).TimeOfDay)))
                    .OrderByDescending(o => o.Fecha)
                    .ThenByDescending(o => o.HoraInicio)
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