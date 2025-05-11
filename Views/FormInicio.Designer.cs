using ReserVA.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace ReserVA
{
    partial class FormInicio
    {
        private Button btnIniciarSesion, btnFiltrar;
        private Panel panelRecintos;
        private Label lblFiltroBarrio;
        private ComboBox cbxFiltroBarrios;

        private void InitializeComponent()
        {
            ClientSize = new Size(1200, 600);
            btnMinimizar.Location = new Point(this.ClientSize.Width - 80, 0);
            btnCerrar.Location = new Point(this.ClientSize.Width - 40, 0);

            btnIniciarSesion = new Button
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Font = new Font("Trebuchet MS", 9.75F, FontStyle.Bold),
                ForeColor = Color.White,
                Size = new Size(160, 40),
                Location = new Point(this.Width - 240, 0),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "👤 Iniciar sesión",
                BackColor = Settings.Default.ColorPrimario
            };
            btnIniciarSesion.Click += BtnIniciarSesion_IniciarSesion_Click;

            panelSuperior.Controls.Add(this.btnIniciarSesion);

            lblFiltroBarrio = new Label
            {
                Text = "Barrio:",
                Location = new Point(20, 50),
                AutoSize = true
            };

            cbxFiltroBarrios = new ComboBox
            {
                Location = new Point(20, 70),
                Size = new Size(200, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            btnFiltrar = new Button
            {
                Text = "▼ Filtrar",
                Location = new Point(230, 70),
                Size = new Size(100, 30),
                BackColor = Color.White,
                ForeColor = Settings.Default.ColorPrimario,
            };
            btnFiltrar.Click += BtnFiltrar_Click;

            panelRecintos = new FlowLayoutPanel
            {
                Location = new Point(15, 110),
                Size = new Size(Width - 20, Height - 120),
                AutoScroll = true,
                Margin = new Padding(15, 80, 15, 20),
                HorizontalScroll = { Visible = false, Enabled = false },
                VerticalScroll = { Visible = true, Enabled = true }
            };

            foreach (Control control in panelRecintos.Controls)
            {
                control.MaximumSize = new Size(panelRecintos.Width - 200, control.Height);
            }

            Controls.Add(lblFiltroBarrio);
            Controls.Add(cbxFiltroBarrios);
            Controls.Add(btnFiltrar);
            Controls.Add(panelRecintos);
        }
    }
}
