using ReserVA.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace ReserVA
{
    partial class FormRegistro
    {
        private Label lblNombre, lblApellidos, lblDocumentoIdentidad, lblTelefono, lblCorreo, lblRepetirCorreo, lblContraseña, lblRepetirContraseña;
        private TextBox tbxNombre, tbxApellidos, tbxDocumentoIdentidad, tbxTelefono, tbxCorreo, tbxRepetirCorreo, tbxContraseña, tbxRepetirContraseña;
        private Button btnRegistrar;

        private void InitializeComponent()
        {
            lblNombre = new Label
            {
                Text = "Nombre",
                Location = new Point(50, 80),
                AutoSize = true 
            };
            tbxNombre = new TextBox
            {
                Location = new Point(50, 100),
                Width = 200
            };

            lblApellidos = new Label
            {
                Text = "Apellidos",
                Location = new Point(300, 80),
                AutoSize = true
            };
            tbxApellidos = new TextBox
            {
                Location = new Point(300, 100),
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

            lblCorreo = new Label
            {
                Text = "Correo electrónico",
                Location = new Point(50, 220),
                AutoSize = true
            };
            tbxCorreo = new TextBox
            {
                Location = new Point(50, 240),
                Width = 200
            };

            lblRepetirCorreo = new Label
            {
                Text = "Repetir correo electrónico",
                Location = new Point(300, 220),
                AutoSize = true
            };
            tbxRepetirCorreo = new TextBox
            {
                Location = new Point(300, 240),
                Width = 200
            };

            lblContraseña = new Label
            {
                Text = "Contraseña",
                Location = new Point(50, 290),
                AutoSize = true
            };
            tbxContraseña = new TextBox
            {
                Location = new Point(50, 310),
                Width = 200,
                UseSystemPasswordChar = true
            };

            lblRepetirContraseña = new Label
            {
                Text = "Repetir contraseña",
                Location = new Point(300, 290),
                AutoSize = true
            };
            tbxRepetirContraseña = new TextBox
            {
                Location = new Point(300, 310),
                Width = 200,
                UseSystemPasswordChar = true
            };
            
            btnRegistrar = new Button
            {
                Text = "Registrarse",
                Location = new Point(200, 370),
                Size = new Size(200, 40),
                BackColor = Settings.Default.ColorPrimario,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnRegistrar.Click += BtnRegistrar_Click;
                        
            Controls.Add(lblNombre); Controls.Add(tbxNombre);
            Controls.Add(lblApellidos); Controls.Add(tbxApellidos);
            Controls.Add(lblDocumentoIdentidad); Controls.Add(tbxDocumentoIdentidad);
            Controls.Add(lblTelefono); Controls.Add(tbxTelefono);
            Controls.Add(lblCorreo); Controls.Add(tbxCorreo);
            Controls.Add(lblRepetirCorreo); Controls.Add(tbxRepetirCorreo);
            Controls.Add(lblContraseña); Controls.Add(tbxContraseña);
            Controls.Add(lblRepetirContraseña); Controls.Add(tbxRepetirContraseña);
            Controls.Add(btnRegistrar);
        }
    }
}
