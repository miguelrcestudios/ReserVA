using ReserVA.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace ReserVA
{
    partial class FormInicio
    {
        private TabControl tabControlUsuario;
        private TabPage tabPageReservar, tabPageProximasReservas, tabPageHistorialReservas;
        private Button btnIniciarSesion, btnFiltrar;
        private Panel panelRecintos;
        private Label lblFiltroBarrio;
        private ComboBox cbxFiltroBarrios;
        private DataGridView dgvProximasReservas, dgvHistoriaReservas;

        private void InitializeComponent()
        {
            ClientSize = new Size(1200, 600);
            btnMinimizar.Location = new Point(this.ClientSize.Width - 80, 0);
            btnCerrar.Location = new Point(this.ClientSize.Width - 40, 0);

            btnIniciarSesion = new Button
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Size = new Size(160, 40),
                Location = new Point(this.Width - 240, 0),
                Text = "👤 Iniciar sesión",
                Font = new Font("Trebuchet MS", 9.75F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                BackColor = Settings.Default.ColorPrimario,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Cursor = Cursors.Hand
            };
            btnIniciarSesion.Click += BtnIniciarSesion_IniciarSesion_Click;

            panelSuperior.Controls.Add(this.btnIniciarSesion);

            #region Pestañas
            tabControlUsuario = new TabControl
            {
                Dock = DockStyle.None,
                Location = new Point(5, 45),
                Size = new Size(Width - 10, Height - 50),
            };

            tabPageReservar = new TabPage("Reservar");
            tabPageProximasReservas = new TabPage("Mis próximas reservas");
            tabPageHistorialReservas = new TabPage("Mi historial de reservas");

            tabControlUsuario.TabPages.Add(tabPageReservar);
            if (Usuario != null && Usuario.IdRol == 1)
            {
                tabControlUsuario.TabPages.Add(tabPageProximasReservas);
                tabControlUsuario.TabPages.Add(tabPageHistorialReservas);
            }

            Controls.Add(tabControlUsuario);
            #endregion

            #region Pestaña Reservar
            lblFiltroBarrio = new Label
            {
                Text = "Barrio:",
                Location = new Point(10, 10),
                AutoSize = true
            };

            cbxFiltroBarrios = new ComboBox
            {
                Location = new Point(10, 30),
                Size = new Size(200, 30),
                FlatStyle = FlatStyle.Flat,
                DropDownStyle = ComboBoxStyle.DropDownList,
                DropDownHeight = 200,
            };

            btnFiltrar = new Button
            {
                Text = Resources.Filtrar,
                Location = new Point(220, 30),
                Size = new Size(100, 30),
                BackColor = Color.White,
                ForeColor = Settings.Default.ColorPrimario,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnFiltrar.Click += BtnFiltrar_Click;            

            panelRecintos = new FlowLayoutPanel
            {
                Location = new Point(5, 70),
                Size = new Size(Width - 20, Height - 150),
                AutoScroll = true,
                //Margin = new Padding(15, 80, 15, 20),
                HorizontalScroll = { Visible = false, Enabled = false },
                VerticalScroll = { Visible = true, Enabled = true }
            };

            tabPageReservar.Controls.Add(lblFiltroBarrio);
            tabPageReservar.Controls.Add(cbxFiltroBarrios);
            tabPageReservar.Controls.Add(btnFiltrar);
            tabPageReservar.Controls.Add(panelRecintos);
            #endregion

            #region Pestaña Mis próximas reservas (sólo Usuarios registrados)
            dgvProximasReservas = new DataGridView
            {
                Location = new Point(10, 10),
                Size = new Size(tabControlUsuario.Width - 30, tabControlUsuario.Height - 50),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                DefaultCellStyle = { SelectionBackColor = Settings.Default.ColorPrimario, SelectionForeColor = Color.White },
                ReadOnly = true,
                AllowUserToAddRows = false,
                Columns =
                {
                    new DataGridViewTextBoxColumn { Name = "NumeroReserva", HeaderText = "Nº de reserva" },
                    new DataGridViewTextBoxColumn { Name = "Fecha", HeaderText = "Fecha" },
                    new DataGridViewTextBoxColumn { Name = "HoraInicio", HeaderText = "Inicio" },
                    new DataGridViewTextBoxColumn { Name = "HoraFin", HeaderText = "Fin" },
                    new DataGridViewTextBoxColumn { Name = "Espacio", HeaderText = "Espacio" },
                    new DataGridViewTextBoxColumn { Name = "Recinto", HeaderText = "Recinto" },
                }
            };

            tabPageProximasReservas.Controls.Add(dgvProximasReservas);
            #endregion
            
            #region Pestaña Mi historial de reservas (sólo Usuarios registrados)
            dgvHistoriaReservas = new DataGridView
            {
                Location = new Point(10, 10),
                Size = new Size(tabControlUsuario.Width - 30, tabControlUsuario.Height - 50),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                DefaultCellStyle = { SelectionBackColor = Settings.Default.ColorPrimario, SelectionForeColor = Color.White },
                ReadOnly = true,
                AllowUserToAddRows = false,
                Columns =
                {
                    new DataGridViewTextBoxColumn { Name = "NumeroReserva", HeaderText = "Nº de reserva" },
                    new DataGridViewTextBoxColumn { Name = "Fecha", HeaderText = "Fecha" },
                    new DataGridViewTextBoxColumn { Name = "HoraInicio", HeaderText = "Inicio" },
                    new DataGridViewTextBoxColumn { Name = "HoraFin", HeaderText = "Fin" },
                    new DataGridViewTextBoxColumn { Name = "Espacio", HeaderText = "Espacio" },
                    new DataGridViewTextBoxColumn { Name = "Recinto", HeaderText = "Recinto" },
                }
            };

            tabPageHistorialReservas.Controls.Add(dgvHistoriaReservas);
            #endregion
        }
    }
}
