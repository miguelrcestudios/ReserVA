using ReserVA.Controller;
using System;
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
    }
}