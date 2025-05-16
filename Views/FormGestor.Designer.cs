using ReserVA.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace ReserVA
{
    partial class FormGestor
    {
        private TabControl tabControlGestor;
        private TabPage tabPageReservas, tabPageRecintos, tabPageEspacios, tabPageGestores;

        private Button btnIniciarSesion, btnFiltrarReservas, btnFiltrarRecintos, btnFiltrarEspacios, btnBuscarGestores, btnCrearRecinto, btnEditarRecinto, btnEliminarRecinto, btnLimpiarCamposRecinto,
            btnCrearEspacio, btnEditarEspacio, btnEliminarEspacio, btnLimpiarCamposEspacio, btnCrearGestor, btnEditarGestor, btnEliminarGestor, btnLimpiarCamposGestor;
        private Label lblFiltroRecinto_Reservas, lblFiltroBarrio_Recintos, lblNombre_Recinto, lblDireccion_Recinto, lblSubzona_Recinto,
            lblFiltroRecinto_Espacios, lblNombreEspacio,lblTipoEspacio, lblDescripcionEspacio, lblRecintoEspacio,
            lblFiltroGestores, lblNombreGestor, lblApellidosGestor, lblDocumentoIdentidadGestor, lblTelefonoGestor, lblCorreoElectronicoGestor, lblContraseñaGestor;
        private ComboBox cbxFiltroRecinto_Reservas, cbxFiltroBarrio_Recintos, cbxSubzona_Recinto, cbxFiltroRecinto_Espacios, cbxRecintoEspacio;
        private DataGridView dgvReservas, dgvRecintos, dgvEspacios, dgvGestores;
        private TextBox tbxNombreRecinto, tbxDireccionRecinto, tbxNombreEspacio, tbxTipoEspacio, tbxDescripcionEspacio,
            tbxFiltroGestores, tbxNombreGestor, tbxApellidosGestor, tbxDocumentoIdentidadGestor, tbxTelefonoGestor, tbxCorreoElectronicoGestor, tbxContrasenaGestor;

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
                Cursor = Cursors.Hand
            };

            panelSuperior.Controls.Add(this.btnIniciarSesion);

            #region Pestañas
            tabControlGestor = new TabControl
            {
                Dock = DockStyle.None,
                Location = new Point(5, 45),
                Size = new Size(Width - 10, Height - 50),
            };

            tabPageReservas = new TabPage("Reservas");
            tabPageRecintos = new TabPage("Recintos");
            tabPageEspacios = new TabPage("Espacios");
            tabPageGestores = new TabPage("Gestores");

            tabControlGestor.TabPages.Add(tabPageReservas);
            tabControlGestor.TabPages.Add(tabPageRecintos);
            tabControlGestor.TabPages.Add(tabPageEspacios);
            if (Usuario != null && Usuario.IdRol == 3)
            {
                tabControlGestor.TabPages.Add(tabPageGestores);
            }

            Controls.Add(tabControlGestor);
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
                FlatStyle = FlatStyle.Flat,
                DropDownStyle = ComboBoxStyle.DropDownList,
                DropDownHeight = 200,
            };

            btnFiltrarReservas = new Button
            {
                Text = Resources.Filtrar,
                Location = new Point(230, 30),
                Size = new Size(100, 30),
                BackColor = Color.White,
                ForeColor = Settings.Default.ColorPrimario,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnFiltrarReservas.Click += BtnFiltrarReservas_Click;

            dgvReservas = new DataGridView 
            {
                Location = new Point(10, 70),
                Size = new Size(tabControlGestor.Width - 30, tabControlGestor.Height - 110),
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
                    new DataGridViewTextBoxColumn { Name = "Usuario", HeaderText = "Usuario" }
                }
            };

            tabPageReservas.Controls.Add(lblFiltroRecinto_Reservas);
            tabPageReservas.Controls.Add(cbxFiltroRecinto_Reservas);
            tabPageReservas.Controls.Add(btnFiltrarReservas);
            tabPageReservas.Controls.Add(dgvReservas);
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
                FlatStyle = FlatStyle.Flat,
                DropDownStyle = ComboBoxStyle.DropDownList,
                DropDownHeight = 200,
            };

            btnFiltrarRecintos = new Button
            {
                Text = Resources.Filtrar,
                Location = new Point(230, 30),
                Size = new Size(100, 30),
                BackColor = Color.White,
                ForeColor = Settings.Default.ColorPrimario,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnFiltrarRecintos.Click += BtnFiltrarRecintos_Click;

            dgvRecintos = new DataGridView
            {
                Location = new Point(10, 70),
                Size = new Size(880, tabControlGestor.Height - 110),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                DefaultCellStyle = {SelectionBackColor = Settings.Default.ColorPrimario, SelectionForeColor = Color.White},
                ReadOnly = true,
                AllowUserToAddRows = false,
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
                DropDownStyle = ComboBoxStyle.DropDownList,
                DropDownHeight = 200,
            };

            btnCrearRecinto = new Button 
            { 
                Text = "➕ Crear", 
                Location = new Point(930, 260), 
                Width = 70,
                Size = new Size(110, 40),
                BackColor = Color.White,
                ForeColor = Settings.Default.ColorPrimario,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCrearRecinto.Click += BtnCrearRecinto_Click;

            btnEditarRecinto = new Button
            {
                Text = Resources.Editar,
                Location = new Point(1050, 260),
                Size = new Size(110, 40),
                BackColor = Color.White,
                ForeColor = Settings.Default.ColorPrimario,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnEditarRecinto.Click += BtnEditarRecinto_Click;

            btnEliminarRecinto = new Button
            {
                Text = Resources.Eliminar,
                Location = new Point(930, 310),
                Width = 70,
                Size = new Size(110, 40),
                BackColor = Color.White,
                ForeColor = Settings.Default.ColorPrimario,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnEliminarRecinto.Click += BtnEliminarRecinto_Click;

            btnLimpiarCamposRecinto = new Button
            {
                Text = Resources.Limpiar,
                Location = new Point(1050, 310),
                Width = 70,
                Size = new Size(110, 40),
                BackColor = Color.White,
                ForeColor = Settings.Default.ColorPrimario,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnLimpiarCamposRecinto.Click += BtnLimpiarCamposRecinto_Click;

            tabPageRecintos.Controls.Add(lblFiltroBarrio_Recintos);
            tabPageRecintos.Controls.Add(cbxFiltroBarrio_Recintos);
            tabPageRecintos.Controls.Add(btnFiltrarRecintos);
            tabPageRecintos.Controls.Add(dgvRecintos);
            tabPageRecintos.Controls.Add(lblNombre_Recinto);
            tabPageRecintos.Controls.Add(tbxNombreRecinto);
            tabPageRecintos.Controls.Add(lblDireccion_Recinto);
            tabPageRecintos.Controls.Add(tbxDireccionRecinto);
            tabPageRecintos.Controls.Add(lblSubzona_Recinto);
            tabPageRecintos.Controls.Add(cbxSubzona_Recinto);
            tabPageRecintos.Controls.Add(btnCrearRecinto);
            tabPageRecintos.Controls.Add(btnEditarRecinto);
            tabPageRecintos.Controls.Add(btnEliminarRecinto);
            tabPageRecintos.Controls.Add(btnLimpiarCamposRecinto);
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
                FlatStyle = FlatStyle.Flat,
                DropDownStyle = ComboBoxStyle.DropDownList,
                DropDownHeight = 200,
            };

            btnFiltrarEspacios = new Button
            {
                Text = Resources.Filtrar,
                Location = new Point(230, 30),
                Size = new Size(100, 30),
                BackColor = Color.White,
                ForeColor = Settings.Default.ColorPrimario,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnFiltrarEspacios.Click += BtnFiltrarEspacios_Click;

            dgvEspacios = new DataGridView
            {
                Location = new Point(10, 70),
                Size = new Size(880, tabControlGestor.Height - 110),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                DefaultCellStyle = { SelectionBackColor = Settings.Default.ColorPrimario, SelectionForeColor = Color.White },
                ReadOnly = true,
                AllowUserToAddRows = false,
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
                Width = 230
            };

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
                DropDownStyle = ComboBoxStyle.DropDownList,
                DropDownHeight = 200,
            };

            btnCrearEspacio = new Button
            {
                Text = "➕ Crear",
                Location = new Point(930, 350),
                Width = 70,
                Size = new Size(110, 40),
                BackColor = Color.White,
                ForeColor = Settings.Default.ColorPrimario,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCrearEspacio.Click += BtnCrearEspacio_Click;

            btnEditarEspacio = new Button
            {
                Text = Resources.Editar,
                Location = new Point(1050, 350),
                Size = new Size(110, 40),
                BackColor = Color.White,
                ForeColor = Settings.Default.ColorPrimario,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnEditarEspacio.Click += BtnEditarEspacio_Click;

            btnEliminarEspacio = new Button
            {
                Text = Resources.Eliminar,
                Location = new Point(930, 400),
                Width = 70,
                Size = new Size(110, 40),
                BackColor = Color.White,
                ForeColor = Settings.Default.ColorPrimario,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnEliminarEspacio.Click += BtnEliminarEspacio_Click;

            btnLimpiarCamposEspacio = new Button
            {
                Text = Resources.Limpiar,
                Location = new Point(1050, 400),
                Width = 70,
                Size = new Size(110, 40),
                BackColor = Color.White,
                ForeColor = Settings.Default.ColorPrimario,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnLimpiarCamposEspacio.Click += BtnLimpiarCamposEspacio_Click;

            tabPageEspacios.Controls.Add(lblFiltroRecinto_Espacios);
            tabPageEspacios.Controls.Add(cbxFiltroRecinto_Espacios);
            tabPageEspacios.Controls.Add(btnFiltrarEspacios);
            tabPageEspacios.Controls.Add(dgvEspacios);
            tabPageEspacios.Controls.Add(lblNombreEspacio);
            tabPageEspacios.Controls.Add(tbxNombreEspacio);
            tabPageEspacios.Controls.Add(lblTipoEspacio);
            tabPageEspacios.Controls.Add(tbxTipoEspacio);
            tabPageEspacios.Controls.Add(lblDescripcionEspacio);
            tabPageEspacios.Controls.Add(tbxDescripcionEspacio);
            tabPageEspacios.Controls.Add(lblRecintoEspacio);
            tabPageEspacios.Controls.Add(cbxRecintoEspacio);
            tabPageEspacios.Controls.Add(btnCrearEspacio);
            tabPageEspacios.Controls.Add(btnEditarEspacio);
            tabPageEspacios.Controls.Add(btnEliminarEspacio);
            tabPageEspacios.Controls.Add(btnLimpiarCamposEspacio);
            #endregion#region Pestaña Espacios

            #region Pestaña Gestores (sólo Administradores)
            lblFiltroGestores = new Label
            {
                Text = "Introduzca el texto a buscar:",
                Location = new Point(10, 10),
                AutoSize = true
            };

            tbxFiltroGestores = new TextBox
            {
                Location = new Point(10, 30),
                Size = new Size(200, 30),
            };

            btnBuscarGestores = new Button
            {
                Text = Resources.Buscar,
                Location = new Point(230, 30),
                Size = new Size(100, 30),
                BackColor = Color.White,
                ForeColor = Settings.Default.ColorPrimario,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnBuscarGestores.Click += BtnBuscarGestores_Click;

            dgvGestores = new DataGridView
            {
                Location = new Point(10, 70),
                Size = new Size(880, tabControlGestor.Height - 110),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                DefaultCellStyle = { SelectionBackColor = Settings.Default.ColorPrimario, SelectionForeColor = Color.White },
                ReadOnly = true,
                AllowUserToAddRows = false,
                Columns =
                {
                    new DataGridViewTextBoxColumn { Name = "IdUsuario", HeaderText = "IdUsuario", Visible = false },
                    new DataGridViewTextBoxColumn { Name = "Nombre", HeaderText = "Nombre" },
                    new DataGridViewTextBoxColumn { Name = "Apellidos", HeaderText = "Apellidos" },
                    new DataGridViewTextBoxColumn { Name = "DocumentoIdentidad", HeaderText = "DNI, NIE o Pasaporte" },
                    new DataGridViewTextBoxColumn { Name = "Telefono", HeaderText = "Teléfono" },
                    new DataGridViewTextBoxColumn { Name = "CorreoElectronico", HeaderText = "Correo electrónico" }
                }
            };
            dgvGestores.SelectionChanged += DgvGestores_SelectionChanged;

            lblNombreGestor = new Label 
            { 
                Text = "Nombre", 
                Location = new Point(930, 70), 
                Width = 230 
            };
            tbxNombreGestor = new TextBox { 
                Location = new Point(930, 95), 
                Width = 230 
            };

            lblApellidosGestor = new Label 
            { 
                Text = "Apellidos", 
                Location = new Point(930, 130), 
                Width = 230 
            };
            tbxApellidosGestor = new TextBox 
            { 
                Location = new Point(930, 155), 
                Width = 230 
            };
            
            lblDocumentoIdentidadGestor = new Label 
            { 
                Text = "DNI, NIE, ...", 
                Location = new Point(930, 190), 
                Width = 110,
            };
            tbxDocumentoIdentidadGestor = new TextBox
            {
                Location = new Point(930, 215),
                Width = 110,
            };

            lblTelefonoGestor = new Label 
            { 
                Text = "Teléfono", 
                Location = new Point(1050, 190), 
                Width = 110 
            };
            tbxTelefonoGestor = new TextBox
            {
                Location = new Point(1050, 215),
                Width = 110,
            };

            lblCorreoElectronicoGestor = new Label 
            { 
                Text = "Correo electrónico", 
                Location = new Point(930, 250), 
                Width = 230 
            };
            tbxCorreoElectronicoGestor = new TextBox
            {
                Location = new Point(930, 275),
                Width = 230,
            };

            lblContraseñaGestor = new Label 
            { 
                Text = "Contraseña", 
                Location = new Point(930, 310), 
                Width = 230 
            };
            tbxContrasenaGestor = new TextBox
            {
                Location = new Point(930, 335),
                Width = 230,
                UseSystemPasswordChar = true,
            };

            btnCrearGestor = new Button
            {
                Text = "➕ Crear",
                Location = new Point(930, 380),
                Width = 70,
                Size = new Size(110, 40),
                BackColor = Color.White,
                ForeColor = Settings.Default.ColorPrimario,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCrearGestor.Click += BtnCrearGestor_Click;

            btnEditarGestor = new Button
            {
                Text = Resources.Editar,
                Location = new Point(1050, 380),
                Size = new Size(110, 40),
                BackColor = Color.White,
                ForeColor = Settings.Default.ColorPrimario,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnEditarGestor.Click += BtnEditarGestor_Click;

            btnEliminarGestor = new Button
            {
                Text = Resources.Eliminar,
                Location = new Point(930, 430),
                Width = 70,
                Size = new Size(110, 40),
                BackColor = Color.White,
                ForeColor = Settings.Default.ColorPrimario,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnEliminarGestor.Click += BtnEliminarGestor_Click;

            btnLimpiarCamposGestor = new Button
            {
                Text = Resources.Limpiar,
                Location = new Point(1050, 430),
                Width = 70,
                Size = new Size(110, 40),
                BackColor = Color.White,
                ForeColor = Settings.Default.ColorPrimario,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnLimpiarCamposGestor.Click += BtnLimpiarCamposGestor_Click;

            tabPageGestores.Controls.Add(lblFiltroGestores);
            tabPageGestores.Controls.Add(tbxFiltroGestores);
            tabPageGestores.Controls.Add(btnBuscarGestores);
            tabPageGestores.Controls.Add(dgvGestores);
            tabPageGestores.Controls.Add(lblNombreGestor);
            tabPageGestores.Controls.Add(tbxNombreGestor);
            tabPageGestores.Controls.Add(lblApellidosGestor);
            tabPageGestores.Controls.Add(tbxApellidosGestor);
            tabPageGestores.Controls.Add(lblDocumentoIdentidadGestor);
            tabPageGestores.Controls.Add(tbxDocumentoIdentidadGestor);
            tabPageGestores.Controls.Add(lblTelefonoGestor);
            tabPageGestores.Controls.Add(tbxTelefonoGestor);
            tabPageGestores.Controls.Add(lblCorreoElectronicoGestor);
            tabPageGestores.Controls.Add(tbxCorreoElectronicoGestor);
            tabPageGestores.Controls.Add(lblContraseñaGestor);
            tabPageGestores.Controls.Add(tbxContrasenaGestor);
            tabPageGestores.Controls.Add(btnCrearGestor);
            tabPageGestores.Controls.Add(btnEditarGestor);
            tabPageGestores.Controls.Add(btnEliminarGestor);
            tabPageGestores.Controls.Add(btnLimpiarCamposGestor);
            #endregion
        }
    }
}