using ReserVA.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ReserVA
{
    public partial class FormInicioSesion : FormBase
    {
        private Label lblUsuario, lblContraseña;
        private TextBox tbxUsuario, tbxContraseña;
        private Button btnIniciarSesion, btnRegistro;

        private void InitializeComponent()
        {
            ClientSize = new Size(400, 600);
            btnMinimizar.Location = new Point(this.ClientSize.Width - 80, 0);
            btnCerrar.Location = new Point(this.ClientSize.Width - 40, 0);

            lblUsuario = new Label 
            { 
                Text = "Correo electrónico", 
                Location = new System.Drawing.Point(100, 75), 
                Width = 200 
            };
            tbxUsuario = new TextBox 
            { 
                Location = new System.Drawing.Point(100, 100), 
                Width = 200 
            };

            lblContraseña = new Label 
            { 
                Text = "Contraseña", 
                Location = new System.Drawing.Point(100, 125), 
                Width = 200 
            };
            tbxContraseña = new TextBox 
            { 
                Location = new System.Drawing.Point(100, 150), 
                Width = 200, 
                UseSystemPasswordChar = true 
            };

            btnIniciarSesion = new Button 
            { 
                Text = "Iniciar sesión",
                Location = new Point(100, 200),
                Size = new Size(200, 40),
                BackColor = Settings.Default.ColorPrimario,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
            };
            btnIniciarSesion.Click += BtnIniciarSesion_Click;

            btnRegistro = new Button
            {
                Text = "Registrarse",
                Location = new Point(100, 250),
                Size = new Size(200, 40),
                BackColor = Color.White,
                ForeColor = Settings.Default.ColorPrimario,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
            };
            btnRegistro.Click += BtnRegistro_Click;

            Controls.Add(lblUsuario); Controls.Add(tbxUsuario);
            Controls.Add(lblContraseña); Controls.Add(tbxContraseña);
            Controls.Add(btnIniciarSesion);
            Controls.Add(btnRegistro);
        }
    }
}
