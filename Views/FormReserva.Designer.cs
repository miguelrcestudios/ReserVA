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
        private TextBox tbxNombre, tbxApellidos, tbxDocumentoIdentidad, tbxTelefono, tbxCorreoElectronico;
        private TextBox tbxNombreEspacio, tbxRecinto;
        private DateTimePicker dtpFecha;
        private ComboBox cbxHoraInicio, cbxHoraFin;
        private Button btnReservar;
        
        DateTime Fecha, HoraInicio, HoraFin;

        private void InitializeComponent()
        {
            Fecha = DateTime.Today;
            HoraInicio = DateTime.Now.AddHours(1).AddMinutes(-DateTime.Now.Minute).AddSeconds(-DateTime.Now.Second);
            HoraFin = HoraInicio.AddHours(1);

            // Sección Usuario
            lblUsuario = new Label
            {
                Text = "👤 Usuario",
                Location = new Point(50, 60),
                AutoSize = true,
                Font = new Font("Trebuchet MS", 12F, FontStyle.Bold)
            };

            lblNombre = new Label
            {
                Text = "Nombre",
                Location = new Point(50, 90),
                AutoSize = true
            };
            tbxNombre = new TextBox
            {
                Location = new Point(50, 110),
                Width = 200
            };

            lblApellidos = new Label
            {
                Text = "Apellidos",
                Location = new Point(300, 90),
                AutoSize = true
            };
            tbxApellidos = new TextBox
            { 
                Location = new Point(300, 110),
                Width = 200
            };

            lblDocumentoIdentidad = new Label
            {
                Text = "DNI, NIE, o Pasaporte",
                Location = new Point(50, 150),
                AutoSize = true
            };
            tbxDocumentoIdentidad = new TextBox
            {
                Location = new Point(50, 170),
                Width = 200
            };

            lblTelefono = new Label
            {
                Text = "Teléfono",
                Location = new Point(300, 150),
                AutoSize = true
            };
            tbxTelefono = new TextBox
            {
                Location = new Point(300, 170),
                Width = 200
            };

            lblCorreoElectronico = new Label
            {
                Text = "Correo Electrónico",
                Location = new Point(50, 210),
                AutoSize = true
            };
            tbxCorreoElectronico = new TextBox
            {
                Location = new Point(50, 230),
                Width = 450
            };

            // Sección Espacio
            lblEspacio = new Label
            {
                Text = "🏢 Espacio",
                Location = new Point(50, 270),
                AutoSize = true,
                Font = new Font("Trebuchet MS", 12F, FontStyle.Bold)
            };

            lblNombreEspacio = new Label
            {
                Text = "Nombre",
                Location = new Point(50, 300),
                AutoSize = true
            };
            tbxNombreEspacio = new TextBox
            {
                Location = new Point(50, 320),
                Width = 200 };

            lblRecinto = new Label
            { Text = "Recinto",
                Location = new Point(300, 300), AutoSize = true
            };
            tbxRecinto = new TextBox
            {
                Location = new Point(300, 320),
                Width = 200
            };

            // Sección Reserva
            lblReserva = new Label
            {
                Text = "🗓️ Reserva",
                Location = new Point(50, 360),
                AutoSize = true,
                Font = new Font("Trebuchet MS", 12F, FontStyle.Bold)
            };

            lblFecha = new Label
            {
                Text = "Fecha",
                Location = new Point(50, 390),
                AutoSize = true
            };
            dtpFecha = new DateTimePicker
            { 
                Location = new Point(50, 410),
                Format = DateTimePickerFormat.Short,
                MinDate = Fecha,
                MaxDate = Fecha.AddMonths(6)
            };
            dtpFecha.ValueChanged += DtpFecha_ValueChanged;            
            
            lblHoraInicio = new Label
            {
                Text = "Hora Inicio",
                Location = new Point(50, 450),
                AutoSize = true
            };
            cbxHoraInicio = new ComboBox
            {
                Location = new Point(50, 470),
                FlatStyle = FlatStyle.Flat,
                DropDownStyle = ComboBoxStyle.DropDownList,
                DropDownHeight = 100,
            };
            cbxHoraInicio.SelectedValueChanged += CbxHoraInicio_SelectedValueChanged;

            lblHoraFin = new Label
            {
                Text = "Hora Fin",
                Location = new Point(300, 450),
                AutoSize = true
            };
            cbxHoraFin = new ComboBox
            {
                Location = new Point(300, 470),
                FlatStyle = FlatStyle.Flat,
                DropDownStyle = ComboBoxStyle.DropDownList,
                DropDownHeight = 100,
            };
            cbxHoraFin.SelectedValueChanged += CbxHoraFin_SelectedValueChanged;

            btnReservar = new Button
            {
                Text = "Reservar",
                Location = new Point(200, 510),
                Size = new Size(200, 40),
                BackColor = Settings.Default.ColorPrimario,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnReservar.Click += BtnReservar_Click;
            
            //cbxHoraInicio.SelectedIndexChanged += DtpHoraInicio_SelectedIndexChanged;

            Controls.Add(lblUsuario);
            Controls.Add(lblNombre); Controls.Add(tbxNombre);
            Controls.Add(lblApellidos); Controls.Add(tbxApellidos);
            Controls.Add(lblDocumentoIdentidad); Controls.Add(tbxDocumentoIdentidad);
            Controls.Add(lblTelefono); Controls.Add(tbxTelefono);
            Controls.Add(lblCorreoElectronico); Controls.Add(tbxCorreoElectronico);
            Controls.Add(lblEspacio);
            Controls.Add(lblNombreEspacio); Controls.Add(tbxNombreEspacio);
            Controls.Add(lblRecinto); Controls.Add(tbxRecinto);
            Controls.Add(lblReserva);
            Controls.Add(lblFecha); Controls.Add(dtpFecha);
            Controls.Add(lblHoraInicio); Controls.Add(cbxHoraInicio);
            Controls.Add(lblHoraFin); Controls.Add(cbxHoraFin);
            Controls.Add(btnReservar);
        }        
    }
}
