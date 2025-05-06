using ReserVA.Controllers;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ReserVA
{
    public partial class FormReserva : FormBase
    {
        private Espacio espacio;
        private Usuario usuario = null;

        public FormReserva()
        {
            InitializeComponent();
            ClientSize = new Size(600, 560);
            btnMinimizar.Location = new Point(this.ClientSize.Width - 80, 0);
            btnCerrar.Location = new Point(this.ClientSize.Width - 40, 0);
        }
        
        public FormReserva(Espacio espacio)
        {
            InitializeComponent();
            ClientSize = new Size(600, 560);
            btnMinimizar.Location = new Point(this.ClientSize.Width - 80, 0);
            btnCerrar.Location = new Point(this.ClientSize.Width - 40, 0);

            this.espacio = espacio;

            txtNombreEspacio.Text = espacio.Nombre; txtNombreEspacio.Enabled = false;
            txtRecinto.Text = espacio.Recinto.Nombre; txtRecinto.Enabled = false;
        }

        public FormReserva(Espacio espacio, Usuario usuario)
        {
            InitializeComponent();
            ClientSize = new Size(600, 560);
            btnMinimizar.Location = new Point(this.ClientSize.Width - 80, 0);
            btnCerrar.Location = new Point(this.ClientSize.Width - 40, 0);

            this.espacio = espacio;
            this.usuario = usuario;

            txtNombre.Text = usuario.Nombre; txtNombre.Enabled = false;
            txtApellidos.Text = usuario.Apellidos; txtApellidos.Enabled = false;
            txtDocumentoIdentidad.Text = usuario.DocumentoIdentidad; txtDocumentoIdentidad.Enabled = false;
            txtTelefono.Text = usuario.Telefono; txtTelefono.Enabled = false;
            txtCorreoElectronico.Text = usuario.CorreoElectronico; txtCorreoElectronico.Enabled = false;

            txtNombreEspacio.Text = espacio.Nombre; txtNombreEspacio.Enabled = false;
            txtRecinto.Text = espacio.Recinto.Nombre; txtRecinto.Enabled = false;
        }

        private void BtnReservar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCorreoElectronico.Text))
            {
                MessageBox.Show("Debe introducir un correo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);                
            }

            if (usuario == null)
            {
                Usuario nuevoUsuario = new Usuario()
                {
                    Nombre = txtNombre.Text,
                    Apellidos = txtApellidos.Text,
                    DocumentoIdentidad = txtDocumentoIdentidad.Text,
                    Telefono = txtTelefono.Text,
                    CorreoElectronico = txtCorreoElectronico.Text,
                    Contraseña = null,
                    IdRol = 1
                };
                usuario = nuevoUsuario;
            }

            Reserva reserva = ReservaController.Reservar(espacio, usuario, dtpFecha.Value);
            if (reserva == null)
            {
                //MessageBox.Show($"Se ha producido un error y su reserva no se ha realizado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show($"Reserva realizada correctamente. Su reserva es la número {reserva.IdReserva}.", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
        }

        private void DtpHoraInicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime horaConvertida = DateTime.ParseExact(cbxHoraInicio.SelectedItem.ToString(), "HH:mm", null);


            //HoraInicio = new DateTime(Fecha.Year, Fecha.Month, Fecha.Day, horaConvertida.Hour, horaConvertida.Minute, 0);
            //HoraFin.AddMinutes(30);
            //AñadirItemsHoraFin();
        }

        private void AñadirItemsHoraInicio()
           {
            while (HoraInicio <= Fecha.AddHours(21).AddMinutes(30))
            {
                cbxHoraInicio.Items.Add(HoraInicio.ToString("HH:mm"));
                HoraFin = HoraInicio.AddMinutes(30);
            }
            cbxHoraInicio.SelectedIndex = 0;
            AñadirItemsHoraFin() ;
        }

        private void AñadirItemsHoraFin()
        {
            while (HoraFin <= Fecha.AddHours(22))
            {
                cbxHoraFin.Items.Add(HoraFin.ToString("HH:mm"));
                HoraFin = HoraFin.AddMinutes(30);
            }
            if (cbxHoraFin.Items.Count > 1) 
            {
                cbxHoraFin.SelectedIndex = 1; 
            }
            else if (cbxHoraFin.Items.Count == 1)
            {
                cbxHoraFin.SelectedIndex = 0;
            }            
        }

        private void DtpFecha_ValueChanged(object sender, EventArgs e)
        {
            Fecha = new DateTime(dtpFecha.Value.Year, dtpFecha.Value.Month, dtpFecha.Value.Day);

            if (Fecha < DateTime.Today)
            {
                Fecha = DateTime.Today;
            }

            //if (Fecha == DateTime.Today)
            //{
            //    HoraInicio = AjustarBloqueHorario(DateTime.Now);
            //}
            //else
            //{
            //    HoraInicio = Fecha.AddHours(8); 
            //}
            //HoraFin = Fecha.AddHours(HoraInicio.Hour).AddMinutes(HoraInicio.Minute + 30);

            dtpFecha.Value = Fecha;
            //cbxHoraInicio.Items.Clear();
            //cbxHoraFin.Items.Clear();
            //AñadirItemsHoraInicio();
        }

        private DateTime AjustarBloqueHorario(DateTime fechaHora)
        {
            int hora = fechaHora.Hour;
            int minutos = fechaHora.Minute < 30 ? 30 : 0;
            if (minutos == 0) { hora++; }


            if (fechaHora > DateTime.Today.AddHours(21).AddMinutes(30))
            {                
                return new DateTime(fechaHora.Year, fechaHora.Month, fechaHora.Day + 1, 8, 0, 0);
            }

            if (fechaHora < DateTime.Today.AddHours(8))
            {
                return new DateTime(fechaHora.Year, fechaHora.Month, fechaHora.Day, 8, 0, 0);
            }

            return new DateTime(fechaHora.Year, fechaHora.Month, fechaHora.Day, hora, minutos, 0);
        }
    }
}
