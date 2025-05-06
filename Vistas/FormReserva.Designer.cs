using System;
using System.Drawing;
using System.Windows.Forms;
using ReserVA.Properties;

namespace ReserVA
{
    partial class FormReserva
    {
        private Label lblUsuario, lblEspacio, lblReserva;
        private Label lblNombre, lblApellidos, lblDocumentoIdentidad, lblTelefono, lblCorreoElectronico;
        private Label lblNombreEspacio, lblRecinto;
        private Label lblFecha, lblHoraInicio, lblHoraFin;
        private TextBox txtNombre, txtApellidos, txtDocumentoIdentidad, txtTelefono, txtCorreoElectronico;
        private TextBox txtNombreEspacio, txtRecinto;
        private DateTimePicker dtpFecha;
        private ComboBox cbxHoraInicio, cbxHoraFin;
        private Button btnReservar;
        
        DateTime Fecha, HoraInicio, HoraFin;

        private void InitializeComponent()
        {
            // Sección Usuario
            lblUsuario = new Label() { Text = "Usuario", Location = new Point(50, 60), AutoSize = true, Font = new Font("Trebuchet MS", 12F, FontStyle.Bold) };
            lblNombre = new Label() { Text = "Nombre", Location = new Point(50, 90), AutoSize = true };
            txtNombre = new TextBox() { Location = new Point(50, 110), Width = 200 };

            lblApellidos = new Label() { Text = "Apellidos", Location = new Point(300, 90), AutoSize = true };
            txtApellidos = new TextBox() { Location = new Point(300, 110), Width = 200 };

            lblDocumentoIdentidad = new Label() { Text = "DNI, NIE, o Pasaporte", Location = new Point(50, 150), AutoSize = true };
            txtDocumentoIdentidad = new TextBox() { Location = new Point(50, 170), Width = 200 };

            lblTelefono = new Label() { Text = "Teléfono", Location = new Point(300, 150), AutoSize = true };
            txtTelefono = new TextBox() { Location = new Point(300, 170), Width = 200 };

            lblCorreoElectronico = new Label() { Text = "Correo Electrónico", Location = new Point(50, 210), AutoSize = true };
            txtCorreoElectronico = new TextBox() { Location = new Point(50, 230), Width = 450 };

            // Sección Espacio
            lblEspacio = new Label() { Text = "Espacio", Location = new Point(50, 270), AutoSize = true, Font = new Font("Trebuchet MS", 12F, FontStyle.Bold) };
            lblNombreEspacio = new Label() { Text = "Nombre", Location = new Point(50, 300), AutoSize = true };
            txtNombreEspacio = new TextBox() { Location = new Point(50, 320), Width = 200 };

            lblRecinto = new Label() { Text = "Recinto", Location = new Point(300, 300), AutoSize = true };
            txtRecinto = new TextBox() { Location = new Point(300, 320), Width = 200 };

            // Sección Reserva
            lblReserva = new Label() { Text = "Reserva", Location = new Point(50, 360), AutoSize = true, Font = new Font("Trebuchet MS", 12F, FontStyle.Bold) };
            lblFecha = new Label() { Text = "Fecha", Location = new Point(50, 390), AutoSize = true };
            dtpFecha = new DateTimePicker()
            { 
                Location = new Point(50, 410),
                Format = DateTimePickerFormat.Short,
                MinDate = DateTime.Today,
                MaxDate = DateTime.Today.AddMonths(6)
            };
            Fecha = DateTime.Today;
            
            lblHoraInicio = new Label() { Text = "Hora Inicio", Location = new Point(50, 450), AutoSize = true };
            cbxHoraInicio = new ComboBox()
            {
                Location = new Point(50, 470),
                MaxDropDownItems = 10,
                Enabled = false
            };

            lblHoraFin = new Label() { Text = "Hora Fin", Location = new Point(300, 450), AutoSize = true };
            cbxHoraFin = new ComboBox()
            {
                Location = new Point(300, 470),
                MaxDropDownItems = 10,
                Enabled = false
            };

            //HoraInicio = AjustarBloqueHorario(DateTime.Now);
            //HoraFin = HoraInicio.AddMinutes(30);
            //AñadirItemsHoraInicio();

            btnReservar = new Button()
            {
                Text = "Reservar",
                Location = new Point(200, 510),
                Size = new Size(200, 40),
                BackColor = Settings.Default.ColorPrimario,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            btnReservar.Click += BtnReservar_Click;

            dtpFecha.ValueChanged += DtpFecha_ValueChanged;
            //cbxHoraInicio.SelectedIndexChanged += DtpHoraInicio_SelectedIndexChanged;

            // Agregar controles al formulario
            this.Controls.Add(lblUsuario);
            this.Controls.Add(lblNombre); this.Controls.Add(txtNombre);
            this.Controls.Add(lblApellidos); this.Controls.Add(txtApellidos);
            this.Controls.Add(lblDocumentoIdentidad); this.Controls.Add(txtDocumentoIdentidad);
            this.Controls.Add(lblTelefono); this.Controls.Add(txtTelefono);
            this.Controls.Add(lblCorreoElectronico); this.Controls.Add(txtCorreoElectronico);
            this.Controls.Add(lblEspacio);
            this.Controls.Add(lblNombreEspacio); this.Controls.Add(txtNombreEspacio);
            this.Controls.Add(lblRecinto); this.Controls.Add(txtRecinto);
            this.Controls.Add(lblReserva);
            this.Controls.Add(lblFecha); this.Controls.Add(dtpFecha);
            this.Controls.Add(lblHoraInicio); this.Controls.Add(cbxHoraInicio);
            this.Controls.Add(lblHoraFin); this.Controls.Add(cbxHoraFin);
            this.Controls.Add(btnReservar);
        }
    }
}
