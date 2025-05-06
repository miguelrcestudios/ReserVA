using ReserVA.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ReserVA
{
    public partial class FormInicioSesion : FormBase
    {
        private Label lblUsuario, lblContrasena;
        private TextBox txtUsuario, txtContrasena;
        private Button btnIniciarSesion, btnRegistro;

        private void InitializeComponent()
        {
            this.ClientSize = new Size(400, 600);
            this.btnMinimizar.Location = new Point(this.ClientSize.Width - 80, 0);
            this.btnCerrar.Location = new Point(this.ClientSize.Width - 40, 0);


            lblUsuario = new Label() { Text = "Correo electrónico", Location = new System.Drawing.Point(100, 75), Width = 200 };
            txtUsuario = new TextBox() { Location = new System.Drawing.Point(100, 100), Width = 200 };

            lblContrasena = new Label() { Text = "Contraseña", Location = new System.Drawing.Point(100, 125), Width = 200 };
            txtContrasena = new TextBox() { Location = new System.Drawing.Point(100, 150), Width = 200, UseSystemPasswordChar = true };

            btnIniciarSesion = new Button() 
            { 
                Text = "Iniciar sesión",
                Location = new Point(100, 200),
                Size = new Size(200, 40),
                BackColor = Settings.Default.ColorPrimario,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnRegistro = new Button()
            {
                Text = "Registrarse",
                Location = new Point(100, 250),
                Size = new Size(200, 40),
                BackColor = Color.White,
                ForeColor = Settings.Default.ColorPrimario,
                FlatStyle = FlatStyle.Flat
            };

            btnIniciarSesion.Click += BtnIniciarSesion_Click;
            btnRegistro.Click += BtnRegistro_Click;

            this.Controls.Add(lblUsuario);
            this.Controls.Add(txtUsuario);
            this.Controls.Add(lblContrasena);
            this.Controls.Add(txtContrasena);
            this.Controls.Add(btnIniciarSesion);
            this.Controls.Add(btnRegistro);
        }
    }
}
