using System;
using System.Drawing;
using System.Windows.Forms;

namespace ReserVA
{
    public partial class FormInicioSesion : FormBase
    {
        public FormInicioSesion()
        {
            InitializeComponent();
            ClientSize = new Size(400, 300);
            btnMinimizar.Location = new Point(this.ClientSize.Width - 80, 0);
            btnCerrar.Location = new Point(this.ClientSize.Width - 40, 0);
        }

        private void BtnIniciarSesion_Click(object sender, EventArgs e)
        {
            // Lógica de autenticación
        }

        private void BtnRegistro_Click(object sender, EventArgs e)
        {
            Close();
            Form registro = new FormRegistro();
            registro.ShowDialog();
        }
    }
}