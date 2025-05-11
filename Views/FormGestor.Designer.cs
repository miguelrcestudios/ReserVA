using ReserVA.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace ReserVA
{
    partial class FormGestor
    {
        private TabControl tabControl;
        private TabPage tabReservas, tabRecintos, tabEspacios, tabGestores;

        private Button btnIniciarSesion, btnFiltrarReservas, btnFiltrarRecintos, btnFiltrarEspacios, btnCrear_Recinto, btnEditar_Recinto, btnEliminar_Recinto, btnRestablecer_Recinto, btnCrearEspacio, btnEditarEspacio, btnEliminarEspacio, btnRestablecerEspacio;
        private Label lblFiltroRecinto_Reservas, lblFiltroBarrio_Recintos, lblNombre_Recinto, lblDireccion_Recinto, lblSubzona_Recinto, lblFiltroRecinto_Espacios, lblNombreEspacio, lblTipoEspacio, lblDescripcionEspacio, lblRecintoEspacio;
        private ComboBox cbxFiltroRecinto_Reservas, cbxFiltroBarrio_Recintos, cbxSubzona_Recinto, cbxFiltroRecinto_Espacios, cbxRecintoEspacio;
        private DataGridView dgvReservas, dgvRecintos, dgvEspacios;
        private TextBox tbxNombreRecinto, tbxDireccionRecinto, tbxNombreEspacio, tbxTipoEspacio, tbxDescripcionEspacio;

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
                BackColor = Settings.Default.ColorPrimario,
                ForeColor = Color.White,
                Size = new Size(160, 40),
                Location = new Point(this.Width - 240, 0),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "👤 Iniciar sesión",
            };

            panelSuperior.Controls.Add(this.btnIniciarSesion);

            #region Pestañas
            tabControl = new TabControl
            {
                Dock = DockStyle.None,
                Location = new Point(5, 45),
                Size = new Size(Width - 10, Height - 50),
            };

            tabReservas = new TabPage("Reservas");
            tabRecintos = new TabPage("Recintos");
            tabEspacios = new TabPage("Espacios");
            tabGestores = new TabPage("Gestores");

            tabControl.TabPages.Add(tabReservas);
            tabControl.TabPages.Add(tabRecintos);
            tabControl.TabPages.Add(tabEspacios);
            if (Usuario != null && Usuario.IdRol == 3)
            {
                tabControl.TabPages.Add(tabGestores);
            }

            Controls.Add(tabControl);
            #endregion

            #region Pestaña Reservas
            lblFiltroRecinto_Reservas = new Label
            {
                Text = "Recinto:",
                Location = new Point(10, 10),
                AutoSize = true
            };

            cbxFiltroRecinto_Reservas = new ComboBox
            {
                Location = new Point(10, 30),
                Size = new Size(200, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
            };

            btnFiltrarReservas = new Button
            {
                Text = "▼ Filtrar",
                Location = new Point(230, 30),
                BackColor = Color.White,
                ForeColor = Settings.Default.ColorPrimario,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(100, 30),
            };
            btnFiltrarReservas.Click += BtnFiltrarReservas_Click;

            dgvReservas = new DataGridView 
            {
                Location = new Point(10, 70),
                Size = new Size(tabControl.Width - 30, tabControl.Height - 110),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                DefaultCellStyle = { SelectionBackColor = Settings.Default.ColorPrimario, SelectionForeColor = Color.White },
                ReadOnly = true,
                Columns =
                {
                    new DataGridViewTextBoxColumn { Name = "NumeroReserva", HeaderText = "Nº de reserva" },
                    new DataGridViewTextBoxColumn { Name = "Fecha", HeaderText = "Fecha" },
                    new DataGridViewTextBoxColumn { Name = "HoraInicio", HeaderText = "Inicio" },
                    new DataGridViewTextBoxColumn { Name = "HoraFin", HeaderText = "Fin" },
                    new DataGridViewTextBoxColumn { Name = "Espacio", HeaderText = "Espacio" },
                    new DataGridViewTextBoxColumn { Name = "Recinto", HeaderText = "Recinto" },
                    new DataGridViewTextBoxColumn { Name = "Usuario", HeaderText = "Usuario" }
                }
            };

            tabReservas.Controls.Add(lblFiltroRecinto_Reservas);
            tabReservas.Controls.Add(cbxFiltroRecinto_Reservas);
            tabReservas.Controls.Add(btnFiltrarReservas);
            tabReservas.Controls.Add(dgvReservas);
            #endregion

            #region Pestaña Recintos
            lblFiltroBarrio_Recintos = new Label
            {
                Text = "Barrio:",
                Location = new Point(10, 10),
                AutoSize = true
            };

            cbxFiltroBarrio_Recintos = new ComboBox
            {
                Location = new Point(10, 30),
                Size = new Size(200, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
            };

            btnFiltrarRecintos = new Button
            {
                Text = "▼ Filtrar",
                Location = new Point(230, 30),
                Size = new Size(100, 30),
                BackColor = Color.White,
                ForeColor = Settings.Default.ColorPrimario,
                FlatStyle = FlatStyle.Flat,
            };
            btnFiltrarRecintos.Click += BtnFiltrarRecintos_Click;

            dgvRecintos = new DataGridView
            {
                Location = new Point(10, 70),
                Size = new Size(880, tabControl.Height - 110),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                DefaultCellStyle = {SelectionBackColor = Settings.Default.ColorPrimario, SelectionForeColor = Color.White},
                ReadOnly = true,
                Columns =
                {
                    new DataGridViewTextBoxColumn { Name = "IdRecinto", HeaderText = "IdRecinto", Visible = false },
                    new DataGridViewTextBoxColumn { Name = "Nombre", HeaderText = "Nombre" },
                    new DataGridViewTextBoxColumn { Name = "Dirección", HeaderText = "Dirección" },
                    new DataGridViewTextBoxColumn { Name = "IdSubzona", HeaderText = "IdSubzona", Visible = false },
                    new DataGridViewTextBoxColumn { Name = "Subzona", HeaderText = "Subzona" },
                    new DataGridViewTextBoxColumn { Name = "Barrio", HeaderText = "Barrio" }
                }
            };
            dgvRecintos.SelectionChanged += DgvRecintos_SelectionChanged;

            lblNombre_Recinto = new Label 
            { 
                Text = "Nombre", 
                Location = new Point(930, 70), 
                Width = 230 
            };
            tbxNombreRecinto = new TextBox 
            { 
                Location = new Point(930, 95), 
                Width = 230 
            };

            lblDireccion_Recinto = new Label 
            { 
                Text = "Dirección", 
                Location = new Point(930, 130), 
                Width = 230 
            };
            tbxDireccionRecinto = new TextBox 
            { 
                Location = new Point(930, 155), 
                Width = 230 
            };

            lblSubzona_Recinto = new Label 
            { 
                Text = "Subzona", 
                Location = new Point(930, 190), 
                Width = 230 
            };
            cbxSubzona_Recinto = new ComboBox 
            { 
                Location = new Point(930, 215), 
                Width = 230,
                FlatStyle = FlatStyle.Flat,
            };

            btnCrear_Recinto = new Button 
            { 
                Text = "➕ Crear", 
                Location = new Point(930, 260), 
                Width = 70,
                Size = new Size(110, 40),
                BackColor = Color.White,
                ForeColor = Settings.Default.ColorPrimario,
                FlatStyle = FlatStyle.Flat
            };
            btnCrear_Recinto.Click += BtnCrearRecinto_Click;

            btnEditar_Recinto = new Button
            {
                Text = "✏️ Editar",
                Location = new Point(1050, 260),
                Size = new Size(110, 40),
                BackColor = Color.White,
                ForeColor = Settings.Default.ColorPrimario,
                FlatStyle = FlatStyle.Flat
            };
            btnEditar_Recinto.Click += BtnEditarRecinto_Click;

            btnEliminar_Recinto = new Button
            {
                Text = "🗑️ Eliminar",
                Location = new Point(930, 310),
                Width = 70,
                Size = new Size(110, 40),
                BackColor = Color.White,
                ForeColor = Settings.Default.ColorPrimario,
                FlatStyle = FlatStyle.Flat
            };
            btnEliminar_Recinto.Click += BtnEliminarRecinto_Click;

            btnRestablecer_Recinto = new Button
            {
                Text = "⭮ Restablecer",
                Location = new Point(1050, 310),
                Width = 70,
                Size = new Size(110, 40),
                BackColor = Color.White,
                ForeColor = Settings.Default.ColorPrimario,
                FlatStyle = FlatStyle.Flat,
                Enabled = false,
            };

            tabRecintos.Controls.Add(lblFiltroBarrio_Recintos);
            tabRecintos.Controls.Add(cbxFiltroBarrio_Recintos);
            tabRecintos.Controls.Add(btnFiltrarRecintos);
            tabRecintos.Controls.Add(dgvRecintos);
            tabRecintos.Controls.Add(lblNombre_Recinto);
            tabRecintos.Controls.Add(tbxNombreRecinto);
            tabRecintos.Controls.Add(lblDireccion_Recinto);
            tabRecintos.Controls.Add(tbxDireccionRecinto);
            tabRecintos.Controls.Add(lblSubzona_Recinto);
            tabRecintos.Controls.Add(cbxSubzona_Recinto);
            tabRecintos.Controls.Add(btnCrear_Recinto);
            tabRecintos.Controls.Add(btnEditar_Recinto);
            tabRecintos.Controls.Add(btnEliminar_Recinto);
            tabRecintos.Controls.Add(btnRestablecer_Recinto);
            #endregion

            #region Pestaña Espacios
            lblFiltroRecinto_Espacios = new Label
            {
                Text = "Recinto:",
                Location = new Point(10, 10),
                AutoSize = true
            };

            cbxFiltroRecinto_Espacios = new ComboBox
            {
                Location = new Point(10, 30),
                Size = new Size(200, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
            };

            btnFiltrarEspacios = new Button
            {
                Text = "▼ Filtrar",
                Location = new Point(230, 30),
                Size = new Size(100, 30),
                BackColor = Color.White,
                ForeColor = Settings.Default.ColorPrimario,
                FlatStyle = FlatStyle.Flat,
            };
            btnFiltrarEspacios.Click += BtnFiltrarEspacios_Click;

            dgvEspacios = new DataGridView
            {
                Location = new Point(10, 70),
                Size = new Size(880, tabControl.Height - 110),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                DefaultCellStyle = { SelectionBackColor = Settings.Default.ColorPrimario, SelectionForeColor = Color.White },
                ReadOnly = true,
                Columns =
                {
                    new DataGridViewTextBoxColumn { Name = "IdEspacio", HeaderText = "IdEspacio", Visible = false },
                    new DataGridViewTextBoxColumn { Name = "Nombre", HeaderText = "Nombre" },
                    new DataGridViewTextBoxColumn { Name = "IdRecinto", HeaderText = "IdRecinto", Visible = false },
                    new DataGridViewTextBoxColumn { Name = "Recinto", HeaderText = "Recinto" },
                    new DataGridViewTextBoxColumn { Name = "Tipo", HeaderText = "Tipo" },
                    new DataGridViewTextBoxColumn { Name = "Descripcion", HeaderText = "Descripcion" },
                }
            };
            dgvEspacios.SelectionChanged += DgvEspacios_SelectionChanged;

            lblNombreEspacio = new Label 
            { 
                Text = "Nombre", 
                Location = new Point(930, 70), 
                Width = 230 
            };
            tbxNombreEspacio = new TextBox { 
                Location = new Point(930, 95), 
                Width = 230 };

            lblTipoEspacio = new Label 
            { 
                Text = "Tipo", 
                Location = new Point(930, 130), 
                Width = 230 
            };
            tbxTipoEspacio = new TextBox 
            { 
                Location = new Point(930, 155), 
                Width = 230 
            };
            
            lblDescripcionEspacio = new Label 
            { 
                Text = "Descripcion", 
                Location = new Point(930, 190), 
                Width = 230,
            };
            tbxDescripcionEspacio = new TextBox
            {
                Location = new Point(930, 215),
                Width = 230,
                Height = 50,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
            };

            lblRecintoEspacio = new Label 
            { 
                Text = "Recinto", 
                Location = new Point(930, 280), 
                Width = 230 
            };
            cbxRecintoEspacio = new ComboBox 
            { 
                Location = new Point(930, 305), 
                Width = 230,
                FlatStyle = FlatStyle.Flat,
            };

            btnCrearEspacio = new Button
            {
                Text = "➕ Crear",
                Location = new Point(930, 350),
                Width = 70,
                Size = new Size(110, 40),
                BackColor = Color.White,
                ForeColor = Settings.Default.ColorPrimario,
                FlatStyle = FlatStyle.Flat
            };
            btnCrearEspacio.Click += BtnCrearEspacio_Click;

            btnEditarEspacio = new Button
            {
                Text = "✏️ Editar",
                Location = new Point(1050, 350),
                Size = new Size(110, 40),
                BackColor = Color.White,
                ForeColor = Settings.Default.ColorPrimario,
                FlatStyle = FlatStyle.Flat
            };
            btnEditarEspacio.Click += BtnEditarEspacio_Click;

            btnEliminarEspacio = new Button
            {
                Text = "🗑️ Eliminar",
                Location = new Point(930, 400),
                Width = 70,
                Size = new Size(110, 40),
                BackColor = Color.White,
                ForeColor = Settings.Default.ColorPrimario,
                FlatStyle = FlatStyle.Flat
            };
            btnEliminarEspacio.Click += BtnEliminarEspacio_Click;

            btnRestablecerEspacio = new Button
            {
                Text = "⭮ Restablecer",
                Location = new Point(1050, 400),
                Width = 70,
                Size = new Size(110, 40),
                BackColor = Color.White,
                ForeColor = Settings.Default.ColorPrimario,
                FlatStyle = FlatStyle.Flat,
                Enabled = false,
            };

            tabEspacios.Controls.Add(lblFiltroRecinto_Espacios);
            tabEspacios.Controls.Add(cbxFiltroRecinto_Espacios);
            tabEspacios.Controls.Add(btnFiltrarEspacios);
            tabEspacios.Controls.Add(dgvEspacios);
            tabEspacios.Controls.Add(lblNombreEspacio);
            tabEspacios.Controls.Add(tbxNombreEspacio);
            tabEspacios.Controls.Add(lblTipoEspacio);
            tabEspacios.Controls.Add(tbxTipoEspacio);
            tabEspacios.Controls.Add(lblDescripcionEspacio);
            tabEspacios.Controls.Add(tbxDescripcionEspacio);
            tabEspacios.Controls.Add(lblRecintoEspacio);
            tabEspacios.Controls.Add(cbxRecintoEspacio);
            tabEspacios.Controls.Add(btnCrearEspacio);
            tabEspacios.Controls.Add(btnEditarEspacio);
            tabEspacios.Controls.Add(btnEliminarEspacio);
            tabEspacios.Controls.Add(btnRestablecerEspacio);
            #endregion
        }
    }
}