using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ReserVA;

namespace ReserVA.Controller
{
    public class UsuarioController
    {
        public bool UsuarioYaRegistrado(string correoElectronico)
        {
            using (var contexto = new ReserVAEntities())
            {
                return contexto.Usuario.Any(u =>
                    u.CorreoElectronico == correoElectronico &&
                    !string.IsNullOrWhiteSpace(u.Contraseña));
            }
        }

        public static bool RegistrarUsuario(Usuario nuevoUsuario)
        {
            try
            {
                using (var context = new ReserVAEntities())
                {
                    bool usuarioExistente = context.Usuario
                        .Any(u => u.CorreoElectronico == nuevoUsuario.CorreoElectronico && !string.IsNullOrEmpty(u.Contraseña));

                    if (usuarioExistente)
                    {
                        MessageBox.Show("El correo electrónico ya está registrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    // Agregar nuevo usuario
                    context.Usuario.Add(nuevoUsuario);
                    context.SaveChanges();

                    MessageBox.Show("Usuario registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al registrar el usuario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static Usuario RegistrarUsuarioNoRegistrado(Usuario nuevoUsuario)
        {
            try
            {
                using (var context = new ReserVAEntities())
                {
                    context.Usuario.Add(nuevoUsuario);
                    context.SaveChanges();

                    return nuevoUsuario;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al registrar el usuario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static Usuario IniciarSesion(string correo, string contraseña)
        {
            using (var context = new ReserVAEntities())
            {
                var usuario = context.Usuario
                    .FirstOrDefault(u => u.CorreoElectronico == correo && !string.IsNullOrEmpty(u.Contraseña));

                if (usuario == null)
                {
                    MessageBox.Show("El correo electrónico no está registrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                bool contraseñaValida = BCrypt.Net.BCrypt.Verify(contraseña, usuario.Contraseña);

                if (!contraseñaValida)
                {
                    MessageBox.Show("Correo electrónico o contraseña incorrecta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                return usuario;
            }
        }

        public static bool CerrarSesion()
        {
            DialogResult cerrarSesion = MessageBox.Show("¿Estás seguro de que deseas cerrar sesión?", "Cerrar sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            return cerrarSesion == DialogResult.Yes;
        }

        public static List<Usuario> ObtenerGestores()
        {
            List<Usuario> listaGestores;
            using (var contexto = new ReserVAEntities())
            {
                listaGestores = contexto.Usuario
                    .Where(w => w.IdRol == 2)
                    .OrderBy(o => o.Nombre)
                    .ThenBy(o => o.Apellidos)
                    .ToList();
            }
            return listaGestores;
        }
        public static List<Usuario> ObtenerGestoresFiltrados(string filtro)
        {
            List<Usuario> listaGestores;
            using (var contexto = new ReserVAEntities())
            {
                listaGestores = contexto.Usuario
                    .Where(w => w.IdRol == 2 &&
                            (w.Nombre.Contains(filtro)
                            || w.Apellidos.Contains(filtro)
                            || w.DocumentoIdentidad.Contains(filtro)
                            || w.Telefono.Contains(filtro)
                            || w.CorreoElectronico.Contains(filtro)))
                    .OrderBy(o => o.Nombre)
                    .ThenBy(o => o.Apellidos)
                    .ToList();
            }

            return listaGestores;
        }

        public static bool EditarGestor(Usuario gestor)
        {
            using (var contexto = new ReserVAEntities())
            {
                Usuario gestorAEditar = contexto.Usuario.FirstOrDefault(g => g.IdUsuario == gestor.IdUsuario);

                if (gestorAEditar != null)
                {
                    gestorAEditar.Nombre = gestor.Nombre;
                    gestorAEditar.Apellidos = gestor.Apellidos;
                    gestorAEditar.DocumentoIdentidad = gestor.DocumentoIdentidad;
                    gestorAEditar.Telefono = gestor.Telefono;
                    gestorAEditar.CorreoElectronico = gestor.CorreoElectronico;
                    gestorAEditar.Contraseña = gestor.Contraseña;

                    contexto.SaveChanges();
                    MessageBox.Show("Gestor editado correctamente.");
                    return true;
                }
                else
                {
                    MessageBox.Show("No se pudo editar el gestor.");
                    return false;
                }
            }
        }

        public static bool EliminarGestor(Usuario gestor)
        {
            using (var contexto = new ReserVAEntities())
            {
                Usuario espacioAEliminar = contexto.Usuario.FirstOrDefault(g => g.IdUsuario == gestor.IdUsuario);

                if (espacioAEliminar != null)
                {
                    contexto.Usuario.Remove(espacioAEliminar);
                    contexto.SaveChanges();
                    MessageBox.Show("Usuario eliminado correctamente.");
                    return true;
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el espacio.");
                    return false;
                }
            }
        }

        public static string ValidarDocumentoIdentidad(string documento)
        {
            string letrasDNINIE = "TRWAGMYFPDXBNJZSQVHLCKE";

            // Validación DNI (Documento Nacional de Identidad)
            if (Regex.IsMatch(documento, @"^\d{8}[A-Z]$"))
            {               
                int numeroDNI = int.Parse(documento.Substring(0, 8));
                char letraDNI = documento[8];
                char letraValidaDNI = letrasDNINIE[numeroDNI % 23];

                return letraDNI != letraValidaDNI ?  "DNI" : null;                
            }
            // Validación NIE (Número de Identidad de Extranjero)
            else if (Regex.IsMatch(documento, @"^[XYZ]\d{7}[A-Z]$"))
            {
                int numeroNIE = int.Parse(documento.Substring(0, 8).Replace('X', '0').Replace('Y', '1').Replace('Z', '2'));
                char letraNIE = documento[8];
                char letraValidaNIE = letrasDNINIE[numeroNIE % 23];

                return letraNIE != letraValidaNIE ? "NIE" : null;
            }
            // Validación pasaporte español
            else if (Regex.IsMatch(documento, @"^[A-Z]{3}\d{6}$"))
            {
                return null;
            }

            return "documento";
        }

        public static bool ValidarFormatoTelefono(string telefono)
        {
            return Regex.IsMatch(telefono, @"^\d{9}$");
        }

        public static bool ValidarFormatoEmail(string email)
        {
            return Regex.IsMatch(email, @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9._-]+\.[a-zA-Z]{2,}$");
        }

        public static bool ValidarFormatoContrasena(string contrasena)
        {
            return Regex.IsMatch(contrasena, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[\d\W]).{6,}$");
        }
    }
}