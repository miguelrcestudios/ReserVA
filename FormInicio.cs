using System;
using System.Windows.Forms;

namespace ReserVA
{
    public partial class FormInicio : FormBase
    {
        public FormInicio()
        {
            InitializeComponent();
        }

        private void BtnIniciarSesion_Click(object sender, EventArgs e)
        {
            Form formInicioSesion = new FormInicioSesion();
            formInicioSesion.ShowDialog();
        }
    }
}
