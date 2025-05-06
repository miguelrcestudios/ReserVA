using ReserVA;
using ReserVA.Controller;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ReserVA
{
    public partial class FormRegistro : FormBase
    {
        public FormRegistro()
        {
            InitializeComponent();
            ClientSize = new Size(600, 450);
            btnMinimizar.Location = new Point(this.ClientSize.Width - 80, 0);
            btnCerrar.Location = new Point(this.ClientSize.Width - 40, 0);
        }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            btnRegistrar.Enabled = false;
            btnRegistrar.Text = "Registrando...";

            if (string.IsNullOrWhiteSpace(txtCorreo.Text) && string.IsNullOrWhiteSpace(txtContraseña.Text))
            {
                MessageBox.Show("Debe introducir un correo y una contraseña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (txtCorreo.Text != txtRepetirCorreo.Text)
            {
                MessageBox.Show("Los correos electrónicos no coinciden.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtContraseña.Text != txtRepetirContraseña.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Usuario nuevoUsuario = new Usuario()
            {
                Nombre = txtNombre.Text,
                Apellidos = txtApellidos.Text,
                DocumentoIdentidad = txtDocumentoIdentidad.Text,
                Telefono = txtTelefono.Text,
                CorreoElectronico = txtCorreo.Text,
                Contraseña = BCrypt.Net.BCrypt.HashPassword(txtContraseña.Text),
                IdRol = 1
            };

            bool usuarioCreado = UsuarioController.RegistrarUsuario(nuevoUsuario);

            if (usuarioCreado)
            {
                Close();
            }
            else
            {
                btnRegistrar.Enabled = true;
                btnRegistrar.Text = "Registrar";
            }
        }
    }
}
