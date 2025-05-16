using ReserVA.Controller;
using ReserVA.Controllers;
using System;
using System.Drawing;
using System.Windows.Forms;
using ReserVA;

namespace ReserVA
{
    public partial class FormReserva : FormBase
    {
        private Espacio espacio;
        private Usuario usuario = null;        
        
        public FormReserva(Espacio espacio)
        {
            InitializeComponent();
            AñadirItemsHoraInicio();
            ClientSize = new Size(600, 560);
            btnMinimizar.Location = new Point(this.ClientSize.Width - 80, 0);
            btnCerrar.Location = new Point(this.ClientSize.Width - 40, 0);

            this.espacio = espacio;

            tbxNombreEspacio.Text = espacio.Nombre; tbxNombreEspacio.Enabled = false;
            tbxRecinto.Text = espacio.Recinto.Nombre; tbxRecinto.Enabled = false;
        }

        public FormReserva(Espacio espacio, Usuario usuario)
        {
            InitializeComponent();
            AñadirItemsHoraInicio();
            ClientSize = new Size(600, 560);
            btnMinimizar.Location = new Point(this.ClientSize.Width - 80, 0);
            btnCerrar.Location = new Point(this.ClientSize.Width - 40, 0);

            this.espacio = espacio;
            this.usuario = usuario;

            tbxNombre.Text = usuario.Nombre; tbxNombre.Enabled = false;
            tbxApellidos.Text = usuario.Apellidos; tbxApellidos.Enabled = false;
            tbxDocumentoIdentidad.Text = usuario.DocumentoIdentidad; tbxDocumentoIdentidad.Enabled = false;
            tbxTelefono.Text = usuario.Telefono; tbxTelefono.Enabled = false;
            tbxCorreoElectronico.Text = usuario.CorreoElectronico; tbxCorreoElectronico.Enabled = false;

            tbxNombreEspacio.Text = espacio.Nombre; tbxNombreEspacio.Enabled = false;
            tbxRecinto.Text = espacio.Recinto.Nombre; tbxRecinto.Enabled = false;
        }

        private void BtnReservar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbxNombre.Text) || string.IsNullOrWhiteSpace(tbxApellidos.Text)
                && string.IsNullOrWhiteSpace(tbxDocumentoIdentidad.Text) || string.IsNullOrWhiteSpace(tbxTelefono.Text)
                && string.IsNullOrWhiteSpace(tbxCorreoElectronico.Text))
            {
                MessageBox.Show("Debe rellenar todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string documento = UsuarioController.ValidarDocumentoIdentidad(tbxDocumentoIdentidad.Text);
            if (documento != null)
            {
                MessageBox.Show($"El {documento} no es válido. Solo se acepta DNI, NIE o pasaporte español.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!UsuarioController.ValidarFormatoEmail(tbxCorreoElectronico.Text))
            {
                MessageBox.Show($"El formato del email no es valido.\nEjemplo: nombre@domino.com", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Fecha < DateTime.Today)
            {
                MessageBox.Show("La fecha de la reserva es anterior a la fecha actual.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (HoraFin < HoraInicio)
            {
                MessageBox.Show("La hora de finalización de la reserva es anterior a la hora de inicio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (usuario == null)
            {
                Usuario nuevoUsuario = new Usuario()
                {
                    Nombre = tbxNombre.Text,
                    Apellidos = tbxApellidos.Text,
                    DocumentoIdentidad = tbxDocumentoIdentidad.Text,
                    Telefono = tbxTelefono.Text,
                    CorreoElectronico = tbxCorreoElectronico.Text,
                    Contraseña = null,
                    IdRol = 1
                };
                usuario = nuevoUsuario;
            }

            Reserva reserva = ReservaController.Reservar(espacio, usuario, Fecha, HoraInicio, HoraFin);
            if (reserva != null)
            {
                MessageBox.Show($"Reserva realizada correctamente.\nSu reserva en {espacio.Nombre} es la número {reserva.IdReserva}.", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);               
                Close();
            }
        }        

        private void AñadirItemsHoraInicio()
        {
            DateTime horaInicio = HoraInicio;

            cbxHoraInicio.Items.Clear();
            while (horaInicio < Fecha.AddHours(22))
            {
                cbxHoraInicio.Items.Add(horaInicio.ToString("HH:mm"));
                horaInicio = horaInicio.AddHours(1);
            }
            cbxHoraInicio.SelectedIndex = 0;
            AñadirItemsHoraFin();
        }

        private void AñadirItemsHoraFin()
        {
            DateTime horaFin = HoraFin;

            cbxHoraFin.Items.Clear();
            while (horaFin < Fecha.AddHours(23))
            {
                cbxHoraFin.Items.Add(horaFin.ToString("HH:mm"));
                horaFin = horaFin.AddHours(1);
            }            
            cbxHoraFin.SelectedIndex = 0;
        }

        private void DtpFecha_ValueChanged(object sender, EventArgs e)
        {
            Fecha = dtpFecha.Value.Date;

            if (Fecha == DateTime.Today)
            {
                HoraInicio = DateTime.Now.AddHours(1).AddMinutes(-DateTime.Now.Minute).AddSeconds(-DateTime.Now.Second);
            }
            else
            {
                HoraInicio = Fecha.AddHours(8);
            }
            HoraFin = HoraInicio.AddHours(1);
            
            AñadirItemsHoraInicio();
        }

        private void CbxHoraInicio_SelectedValueChanged(object sender, EventArgs e)
        {
            DateTime horaSeleccionada = DateTime.ParseExact(cbxHoraInicio.SelectedItem.ToString(), "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            HoraInicio = Fecha.AddHours(horaSeleccionada.Hour);
            HoraFin = HoraInicio.AddHours(1);
            AñadirItemsHoraFin();
        }
        private void CbxHoraFin_SelectedValueChanged(object sender, EventArgs e)
        {
            DateTime horaSeleccionada = DateTime.ParseExact(cbxHoraFin.SelectedItem.ToString(), "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            HoraFin = Fecha.AddHours(horaSeleccionada.Hour);
        }
    }
}
