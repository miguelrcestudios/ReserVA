using System;
using System.Data.Entity.Validation;
using System.Linq;
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
                    //bool usuarioExistente = context.Usuario
                    //    .Any(u => u.CorreoElectronico == nuevoUsuario.CorreoElectronico && !string.IsNullOrEmpty(u.Contraseña));

                    //if (usuarioExistente)
                    //{
                    //   MessageBox.Show("El correo electrónico ya está registrado.", "Usuario registrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}

                    // Agregar nuevo usuario
                    context.Usuario.Add(nuevoUsuario);
                    context.SaveChanges();
                    
                    return nuevoUsuario;
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var error in validationErrors.ValidationErrors)
                    {
                        Console.WriteLine($"Propiedad: {error.PropertyName}, Error: {error.ErrorMessage}");
                    }
                }
                return null;
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

                MessageBox.Show("Inicio de sesión exitoso.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return usuario;
            }
        }

        public static bool CerrarSesion()
        {
            DialogResult cerrarSesion = MessageBox.Show("¿Estás seguro de que deseas cerrar sesión?", "Cerrar sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (cerrarSesion == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}