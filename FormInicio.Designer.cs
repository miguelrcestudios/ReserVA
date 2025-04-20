using ReserVA.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ReserVA
{
    partial class FormInicio
    {
        private Button btnIniciarSesion;

        private void InitializeComponent()
        {
            this.ClientSize = new Size(1200, 600);
            this.btnMinimizar.Location = new Point(this.ClientSize.Width - 80, 0);
            this.btnCerrar.Location = new Point(this.ClientSize.Width - 40, 0);

            this.btnIniciarSesion = new Button();

            this.panelSuperior.Controls.Add(this.btnIniciarSesion);

            this.btnIniciarSesion.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnIniciarSesion.FlatStyle = FlatStyle.Flat;
            this.btnIniciarSesion.FlatAppearance.BorderSize = 0;
            this.btnIniciarSesion.Font = new Font("Trebuchet MS", 9.75F, FontStyle.Bold);
            this.btnIniciarSesion.ForeColor = Color.White;
            this.btnIniciarSesion.Size = new Size(160, 40);
            this.btnIniciarSesion.Location = new Point(this.Width - 240, 0);
            this.btnIniciarSesion.Text = "👤 Iniciar sesión";
            this.btnIniciarSesion.BackColor = Settings.Default.ColorPrimario;
            this.btnIniciarSesion.Click += new EventHandler(this.BtnIniciarSesion_Click);            
        }
    }
}
