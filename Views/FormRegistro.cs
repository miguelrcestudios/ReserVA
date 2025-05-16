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
            btnRegistrar.Cursor = Cursors.WaitCursor;

            if (string.IsNullOrWhiteSpace(tbxNombre.Text) || string.IsNullOrWhiteSpace(tbxApellidos.Text)
                || string.IsNullOrWhiteSpace(tbxDocumentoIdentidad.Text) || string.IsNullOrWhiteSpace(tbxTelefono.Text)
                || string.IsNullOrWhiteSpace(tbxCorreoElectronico.Text) || string.IsNullOrWhiteSpace(tbxContrasena.Text))
            {
                MessageBox.Show("Debe rellenar todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnRegistrar.Enabled = true;
                btnRegistrar.Text = "Registrar";
                btnRegistrar.Cursor = Cursors.Hand;
                return;
            }

            string documento = UsuarioController.ValidarDocumentoIdentidad(tbxDocumentoIdentidad.Text);
            if (documento != null) {
                MessageBox.Show($"El {documento} no es válido.\nSolo se acepta DNI, NIE o pasaporte español.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnRegistrar.Enabled = true;
                btnRegistrar.Text = "Registrar";
                btnRegistrar.Cursor = Cursors.Hand;
                return;
            }

            if (!UsuarioController.ValidarFormatoTelefono(tbxTelefono.Text))
            {
                MessageBox.Show($"El número de teléfono no es valido.\nEjemplo: 600112233", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnRegistrar.Enabled = true;
                btnRegistrar.Text = "Registrar";
                btnRegistrar.Cursor = Cursors.Hand;
                return;
            }

            if (!UsuarioController.ValidarFormatoEmail(tbxCorreoElectronico.Text))
            {
                MessageBox.Show($"El formato del email no es valido.\nEjemplo: nombre@domino.com", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnRegistrar.Enabled = true;
                btnRegistrar.Text = "Registrar";
                btnRegistrar.Cursor = Cursors.Hand;
                return;
            }

            if (!UsuarioController.ValidarFormatoContrasena(tbxContrasena.Text))
            {
                MessageBox.Show($"La contraseña debe cumplir los siguientes requisitos mínimos:\n" +
                                $"- Longitud mínima de 6 caracteres\n" +
                                $"- Una minúscula\n" +
                                $"- Una mayúscula\n" +
                                $"- Un dígito o símbolo",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnRegistrar.Enabled = true;
                btnRegistrar.Text = "Registrar";
                btnRegistrar.Cursor = Cursors.Hand;
                return;
            }
            
            if (!tbxCorreoElectronico.Text.Equals(tbxRepetirCorreoElectronico.Text))
            {
                MessageBox.Show("Los correos electrónicos no coinciden.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnRegistrar.Enabled = true;
                btnRegistrar.Text = "Registrar";
                btnRegistrar.Cursor = Cursors.Hand;
                return;
            }

            if (!tbxContrasena.Text.Equals(tbxRepetirContraseña.Text))
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnRegistrar.Enabled = true;
                btnRegistrar.Text = "Registrar";
                btnRegistrar.Cursor = Cursors.Hand;
                return;
            }

            Usuario nuevoUsuario = new Usuario()
            {
                Nombre = tbxNombre.Text,
                Apellidos = tbxApellidos.Text,
                DocumentoIdentidad = tbxDocumentoIdentidad.Text,
                Telefono = tbxTelefono.Text,
                CorreoElectronico = tbxCorreoElectronico.Text,
                Contraseña = BCrypt.Net.BCrypt.HashPassword(tbxContrasena.Text),
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
                btnRegistrar.Cursor = Cursors.Hand;
            }
        }
    }
}
