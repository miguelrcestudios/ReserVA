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

            Usuario usuarioIniciado = UsuarioController.IniciarSesion(txtUsuario.Text, txtContrasena.Text);

            if (usuarioIniciado != null)
            {
                Usuario = usuarioIniciado;
                DialogResult = DialogResult.OK;
                Hide();
            }
            else
            {
                btnIniciarSesion.Enabled = true;
                btnIniciarSesion.Text = "Iniciar sesión";
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