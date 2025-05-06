using ReserVA.Controller;
using System;
using System.Windows.Forms;

namespace ReserVA
{
    public partial class FormInicio : FormBase
    {
        public Usuario Usuario { get; set; } = null;


        public FormInicio()
        {
            InitializeComponent();
            CargarEspacios();
        }

        private void BtnIniciarSesion_IniciarSesion_Click(object sender, EventArgs e)
        {
            using (FormInicioSesion formInicioSesion = new FormInicioSesion())
            {
                if (formInicioSesion.ShowDialog() == DialogResult.OK)
                {
                    Usuario = formInicioSesion.Usuario;
                    var nombreYApellido = (formInicioSesion.Usuario.Nombre + " " + formInicioSesion.Usuario.Apellidos).Substring(0, 10).Length > 10
                    ? (formInicioSesion.Usuario.Nombre + " " + formInicioSesion.Usuario.Apellidos).Substring(0, 10) + "..."
                    : (formInicioSesion.Usuario.Nombre + " " + formInicioSesion.Usuario.Apellidos);

                    btnIniciarSesion.Text = "👤 " + nombreYApellido;
                    formInicioSesion.Close();

                    btnIniciarSesion.Click -= BtnIniciarSesion_IniciarSesion_Click;
                    btnIniciarSesion.Click += BtnIniciarSesion_CerrarSesion_Click;

                    if (Usuario.IdRol == 2)
                    {
                        Form registro = new FormRegistro();
                        Hide();
                        registro.Show();
                    }
                }
            }
        }

        private void BtnIniciarSesion_CerrarSesion_Click(object sender, EventArgs e)
        { 
            bool cerrarSesion = UsuarioController.CerrarSesion();
            if (cerrarSesion)
            {
                Usuario = null;
                btnIniciarSesion.Text = "👤 Iniciar sesión";
                btnIniciarSesion.Click -= BtnIniciarSesion_CerrarSesion_Click;
                btnIniciarSesion.Click += BtnIniciarSesion_IniciarSesion_Click;
            }
        }

        private void BtnReserva_Click(object sender, EventArgs e)
        {
            Button btnClicked = sender as Button;
            if (btnClicked != null && btnClicked.Tag != null)
            {
                Espacio espacio = (Espacio)btnClicked.Tag;
                Form reserva;
                if (Usuario == null)
                {
                    reserva = new FormReserva(espacio);
                }
                else
                {
                    reserva = new FormReserva(espacio, Usuario);
                }

                reserva.ShowDialog();
            }            
        }
    }
}
