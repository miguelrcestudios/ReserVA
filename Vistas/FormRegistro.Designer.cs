using ReserVA.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace ReserVA
{
    partial class FormRegistro
    {
        private Label lblNombre, lblApellidos, lblDocumentoIdentidad, lblTelefono, lblCorreo, lblRepetirCorreo, lblContraseña, lblRepetirContraseña;
        private TextBox txtNombre, txtApellidos, txtDocumentoIdentidad, txtTelefono, txtCorreo, txtRepetirCorreo, txtContraseña, txtRepetirContraseña;
        private Button btnRegistrar;

        private void InitializeComponent()
        {
            lblNombre = new Label()
            {
                Text = "Nombre",
                Location = new Point(50, 80),
                AutoSize = true
            };
            txtNombre = new TextBox()
            {
                Location = new Point(50, 100),
                Width = 200
            };

            lblApellidos = new Label()
            {
                Text = "Apellidos",
                Location = new Point(300, 80),
                AutoSize = true
            };
            txtApellidos = new TextBox()
            {
                Location = new Point(300, 100),
                Width = 200
            };

            lblDocumentoIdentidad = new Label()
            {
                Text = "DNI, NIE, o Pasaporte",
                Location = new Point(50, 150),
                AutoSize = true
            };
            txtDocumentoIdentidad = new TextBox()
            {
                Location = new Point(50, 170),
                Width = 200
            };

            lblTelefono = new Label()
            {
                Text = "Teléfono",
                Location = new Point(300, 150),
                AutoSize = true
            };
            txtTelefono = new TextBox()
            {
                Location = new Point(300, 170),
                Width = 200
            };

            lblCorreo = new Label()
            {
                Text = "Correo electrónico",
                Location = new Point(50, 220),
                AutoSize = true
            };
            txtCorreo = new TextBox()
            {
                Location = new Point(50, 240),
                Width = 200
            };

            lblRepetirCorreo = new Label()
            {
                Text = "Repetir correo electrónico",
                Location = new Point(300, 220),
                AutoSize = true
            };
            txtRepetirCorreo = new TextBox()
            {
                Location = new Point(300, 240),
                Width = 200
            };

            lblContraseña = new Label()
            {
                Text = "Contraseña",
                Location = new Point(50, 290),
                AutoSize = true
            };
            txtContraseña = new TextBox()
            {
                Location = new Point(50, 310),
                Width = 200,
                UseSystemPasswordChar = true
            };

            lblRepetirContraseña = new Label()
            {
                Text = "Repetir contraseña",
                Location = new Point(300, 290),
                AutoSize = true
            };
            txtRepetirContraseña = new TextBox()
            {
                Location = new Point(300, 310),
                Width = 200,
                UseSystemPasswordChar = true
            };

            // Botón de registro
            btnRegistrar = new Button()
            {
                Text = "Registrarse",
                Location = new Point(200, 370),
                Size = new Size(200, 40),
                BackColor = Settings.Default.ColorPrimario,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            btnRegistrar.Click += BtnRegistrar_Click;

            // Agregar controles al formulario
            this.Controls.Add(lblNombre);
            this.Controls.Add(txtNombre);
            this.Controls.Add(lblApellidos);
            this.Controls.Add(txtApellidos);
            this.Controls.Add(lblDocumentoIdentidad);
            this.Controls.Add(txtDocumentoIdentidad);
            this.Controls.Add(lblTelefono);
            this.Controls.Add(txtTelefono);
            this.Controls.Add(lblCorreo);
            this.Controls.Add(txtCorreo);
            this.Controls.Add(lblRepetirCorreo);
            this.Controls.Add(txtRepetirCorreo);
            this.Controls.Add(lblContraseña);
            this.Controls.Add(txtContraseña);
            this.Controls.Add(lblRepetirContraseña);
            this.Controls.Add(txtRepetirContraseña);
            this.Controls.Add(btnRegistrar);
        }
    }
}
