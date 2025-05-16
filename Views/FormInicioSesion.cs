using ReserVA.Controller;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ReserVA
{
    public partial class FormInicioSesion : FormBase
    {
        public Usuario Usuario {  get; set; }

        public FormInicioSesion()
        {
            InitializeComponent();
            ClientSize = new Size(400, 300);
            btnMinimizar.Location = new Point(this.ClientSize.Width - 80, 0);
            btnCerrar.Location = new Point(this.ClientSize.Width - 40, 0);
        }

        private void BtnIniciarSesion_Click(object sender, EventArgs e)
        {
            btnIniciarSesion.Enabled = false;
            btnIniciarSesion.Text = "Iniciando sesión...";
            btnIniciarSesion.Cursor = Cursors.WaitCursor;

            if (string.IsNullOrWhiteSpace(tbxUsuario.Text) || string.IsNullOrWhiteSpace(tbxContraseña.Text))
            {
                MessageBox.Show("Debe rellenar los campos usuario y contraseña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnIniciarSesion.Enabled = true;
                btnIniciarSesion.Text = "Iniciar sesión";
                btnIniciarSesion.Cursor = Cursors.Hand;
                return;
            }

            if (!UsuarioController.ValidarFormatoEmail(tbxUsuario.Text))
            {
                MessageBox.Show($"El formato del email no es valido.\nEjemplo: nombre@domino.com", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnIniciarSesion.Enabled = true;
                btnIniciarSesion.Text = "Iniciar sesión";
                btnIniciarSesion.Cursor = Cursors.Default;
                return;
            }

            Usuario usuarioIniciado = UsuarioController.IniciarSesion(tbxUsuario.Text, tbxContraseña.Text);

            if (usuarioIniciado != null)
            {
                Usuario = usuarioIniciado;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                btnIniciarSesion.Enabled = true;
                btnIniciarSesion.Text = "Iniciar sesión";
                btnIniciarSesion.Cursor = Cursors.Default;
            }
        }

        private void BtnRegistro_Click(object sender, EventArgs e)
        {
            Close();
            Form registro = new FormRegistro();
            registro.ShowDialog();
        }
    }
}