using ReserVA.Controller;
using ReserVA.Controllers;
using ReserVA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ReserVA
{
    public partial class FormGestor : FormBase
    {
        public Usuario Usuario { get; set; } = null;
        public Recinto Recinto { get; set; } = null;
        public Espacio Espacio { get; set; } = null;
        public Usuario Gestor { get; set; } = null;

        public FormGestor(Usuario usuario)
        {
            Usuario = usuario;            
            InitializeComponent();
            
            var nombreYApellido = (Usuario.Nombre + " " + Usuario.Apellidos).Length > 18
            ? (Usuario.Nombre + " " + Usuario.Apellidos).Substring(0, 15) + "..."
            : (Usuario.Nombre + " " + Usuario.Apellidos);
            btnIniciarSesion.Text = "👤 " + nombreYApellido;

            btnIniciarSesion.Click += BtnIniciarSesion_CerrarSesion_Click;

            CargarReservas(-1);
            CargarRecintos(-1);
            CargarEspacios(-1);
            CargarGestores(null);
            CargarFiltroRecinto();
            CargarFiltroBarrio();
            CargarCbxSubzonas();
            CargarCbxRecintos();
        }

        private void BtnIniciarSesion_CerrarSesion_Click(object sender, EventArgs e)
        {
            bool cerrarSesion = UsuarioController.CerrarSesion();
            if (cerrarSesion)
            {
                Usuario = null;
                Close();
            }
        }



        #region Pestaña Reservas
        private void CargarFiltroRecinto()
        {
            using (var context = new ReserVAEntities())
            {
                cbxFiltroRecinto_Reservas.Items.Clear();
                cbxFiltroRecinto_Espacios.Items.Clear();

                var recintoVacio = new {IdRecinto = -1, Nombre = "" };

                var recintos = context.Recinto.Select(e => new
                {
                    e.IdRecinto,
                    e.Nombre
                })
                .OrderBy(o => o.Nombre)
                .ToList();

                recintos.Insert(0, recintoVacio);
                foreach (var recinto in recintos)
                {
                    cbxFiltroRecinto_Reservas.Items.Add(recinto);
                }
                cbxFiltroRecinto_Reservas.DisplayMember = "Nombre";
                cbxFiltroRecinto_Reservas.ValueMember = "Id";
                cbxFiltroRecinto_Reservas.SelectedItem = null;

                foreach (var recinto in recintos)
                {
                    cbxFiltroRecinto_Espacios.Items.Add(recinto);
                }
                cbxFiltroRecinto_Espacios.DisplayMember = "Nombre";
                cbxFiltroRecinto_Espacios.ValueMember = "Id";
                cbxFiltroRecinto_Espacios.SelectedItem = null;
            }
        }

        private void CargarReservas(int idRecinto)
        {
            List<ReservaDTO> reservas;

            if (idRecinto == -1)
            {
                reservas = ReservaController.ObtenerTodas();
            }
            else
            {
                reservas = ReservaController.ObtenerFiltradas(idRecinto);
            }            

            dgvReservas.Rows.Clear();

            foreach (var espacio in reservas)
            {
                dgvReservas.Rows.Add(espacio.NumeroReserva, espacio.Fecha, espacio.HoraInicio, espacio.HoraFin, espacio.Espacio, espacio.Recinto, espacio.Usuario);
            }            
        }

        private void BtnFiltrarReservas_Click(object sender, EventArgs e)
        {
            var recintoSeleccionado = cbxFiltroRecinto_Reservas.SelectedItem as dynamic;
            int idRecintoSeleccionado = recintoSeleccionado != null ? recintoSeleccionado.IdRecinto : -1;

            CargarReservas(idRecintoSeleccionado);
        }
        #endregion

        #region Pestaña Recintos
        private void CargarFiltroBarrio()
        {
            using (var context = new ReserVAEntities())
            {
                var barrioVacio = new { IdBarrio = -1, Nombre = "" };

                var barrios = context.Barrio.Select(e => new
                {
                    e.IdBarrio,
                    e.Nombre
                }).OrderBy(o => o.Nombre).ToList();

                barrios.Insert(0, barrioVacio);
                foreach (var barrio in barrios)
                {
                    cbxFiltroBarrio_Recintos.Items.Add(barrio);
                }
                cbxFiltroBarrio_Recintos.DisplayMember = "Nombre";
                cbxFiltroBarrio_Recintos.ValueMember = "IdBarrio";
                cbxFiltroBarrio_Recintos.SelectedItem = null;
            }
        }

        private void CargarRecintos(int idBarrio)
        {
            List<RecintoDTO> recintos;

            if (idBarrio == -1)
            {
                recintos = RecintoController.ObtenerTodos();
            }
            else
            {
                recintos = RecintoController.ObtenerFiltrados(idBarrio);
            }

            dgvRecintos.Rows.Clear();

            foreach (var recinto in recintos)
            {
                dgvRecintos.Rows.Add(recinto.IdRecinto, recinto.Nombre, recinto.Direccion, recinto.IdSubzona, recinto.Subzona, recinto.Barrio);
            }
        }

        private void CargarCbxSubzonas()
        {
            using (var context = new ReserVAEntities())
            {
                var subzonas = context.Subzona.Select(e => new
                {
                    e.IdSubzona,
                    e.Nombre,
                    NombreBarrio = e.Barrio.Nombre,
                    Display = e.Nombre + " (" + e.Barrio.Nombre + ")" 
                }).OrderBy(o => o.Nombre).ToList();

                foreach (var subzona in subzonas)
                {
                    cbxSubzona_Recinto.Items.Add(subzona);
                }
                cbxSubzona_Recinto.DisplayMember = "Display";
                cbxSubzona_Recinto.ValueMember = "IdSubzona";
                cbxSubzona_Recinto.SelectedItem = null;
            }
        }

        private void LimpiarCamposRecinto()
        {
            Recinto = null;
            tbxNombreRecinto.Text = null;
            tbxDireccionRecinto.Text = null;
            cbxSubzona_Recinto.SelectedIndex = 0;

            btnEditarRecinto.Enabled = false;
            btnEliminarRecinto.Enabled = false;
        }
        
        private void DgvRecintos_SelectionChanged(object sender, EventArgs e)
        {            
            if (dgvRecintos.SelectedRows.Count == 1)
            {
                DataGridViewRow filaSeleccionada = dgvRecintos.SelectedRows[0];

                Recinto = new Recinto {
                    IdRecinto = Convert.ToInt32(filaSeleccionada.Cells["IdRecinto"].Value),
                    Nombre = filaSeleccionada.Cells["Nombre"].Value?.ToString(),
                    Direccion = filaSeleccionada.Cells["Dirección"].Value?.ToString(),
                    IdSubzona = Convert.ToInt32(filaSeleccionada.Cells["IdSubzona"].Value)
                };

                btnEditarRecinto.Enabled = true;
                btnEliminarRecinto.Enabled = true;

                tbxNombreRecinto.Text = Recinto.Nombre;
                tbxDireccionRecinto.Text = Recinto.Direccion;

                if (filaSeleccionada.Cells["Subzona"].Value != null)
                {
                    foreach (var item in cbxSubzona_Recinto.Items)
                    {
                        dynamic obj = item;
                        if (obj.IdSubzona == Recinto.IdSubzona)
                        {
                            cbxSubzona_Recinto.SelectedItem = item;
                            return;
                        }
                    }

                    MessageBox.Show($"No se encontró el IdSubzona {Recinto.IdSubzona} en el ComboBox.");
                }                
            }
            else
            {
                LimpiarCamposRecinto();
            }
        }
        
        private void BtnFiltrarRecintos_Click(object sender, EventArgs e)
        {
            var barrioSeleccionado = cbxFiltroBarrio_Recintos.SelectedItem as dynamic;
            int idBarrioSeleccionado = barrioSeleccionado != null ? barrioSeleccionado.IdBarrio : -1;

            CargarRecintos(idBarrioSeleccionado);
        }

        private void BtnCrearRecinto_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbxNombreRecinto.Text) || string.IsNullOrWhiteSpace(tbxDireccionRecinto.Text))
            {
                MessageBox.Show("Debe rellenar los campos nombre y dirección.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;            
            }

            var subzonaSeleccionada = cbxSubzona_Recinto.SelectedItem as dynamic;
            int idSubzonaSeleccionada = subzonaSeleccionada.IdSubzona;

            Recinto = new Recinto
            {
                Nombre = tbxNombreRecinto.Text,
                Direccion = tbxDireccionRecinto.Text,
                IdSubzona = idSubzonaSeleccionada
            };

            bool creado = RecintoController.CrearRecinto(Recinto);
            if (creado)
            {
                var barrioSeleccionado = cbxFiltroBarrio_Recintos.SelectedItem as dynamic;
                int idBarrioSeleccionado = barrioSeleccionado != null ? barrioSeleccionado.IdBarrio : -1;

                CargarRecintos(idBarrioSeleccionado);
                CargarCbxRecintos();
                CargarFiltroRecinto();
            }
        }

        private void BtnEditarRecinto_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbxNombreRecinto.Text) || string.IsNullOrWhiteSpace(tbxDireccionRecinto.Text))
            {
                MessageBox.Show("Debe rellenar los campos nombre y dirección.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var subzonaSeleccionada = cbxSubzona_Recinto.SelectedItem as dynamic;
            int idSubzonaSeleccionada = subzonaSeleccionada.IdSubzona; 

            Recinto.Nombre = tbxNombreRecinto.Text;
            Recinto.Direccion = tbxDireccionRecinto.Text;            
            Recinto.IdSubzona = idSubzonaSeleccionada;

            bool editado = RecintoController.EditarRecinto(Recinto);
            if (editado)
            {
                var barrioSeleccionado = cbxFiltroBarrio_Recintos.SelectedItem as dynamic;
                int idBarrioSeleccionado = barrioSeleccionado != null ? barrioSeleccionado.IdBarrio : -1;

                CargarRecintos(idBarrioSeleccionado);
                CargarCbxRecintos();
                CargarFiltroRecinto();
            }
        }

        private void BtnEliminarRecinto_Click(object sender, EventArgs e)
        {
            DialogResult eliminarRecinto = MessageBox.Show($"¿Estás seguro de que deseas eliminar el recinto {Recinto.Nombre}?", "Eliminar recinto", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            
            if (eliminarRecinto == DialogResult.Yes)
            {
                bool eliminado = RecintoController.EliminarRecinto(Recinto);
                if (eliminado)
                {
                    var barrioSeleccionado = cbxFiltroBarrio_Recintos.SelectedItem as dynamic;
                    int idBarrioSeleccionado = barrioSeleccionado != null ? barrioSeleccionado.IdBarrio : -1;

                    CargarRecintos(idBarrioSeleccionado);
                    CargarCbxRecintos();
                    CargarFiltroRecinto();
                }
            }
        }

        private void BtnLimpiarCamposRecinto_Click(object sender, EventArgs e)
        {
            LimpiarCamposRecinto();
        }
        #endregion

        #region Pestaña Espacios
        private void CargarEspacios(int idRecinto)
        {
            List<Espacio> espacios;

            if (idRecinto == -1)
            {
                espacios = EspacioController.ObtenerTodos();
            }
            else
            {
                espacios = EspacioController.ObtenerFiltradosPorRecinto(idRecinto);
            }

            dgvEspacios.Rows.Clear();

            foreach (var espacio in espacios)
            {
                dgvEspacios.Rows.Add(espacio.IdEspacio, espacio.Nombre, espacio.IdRecinto, espacio.Recinto.Nombre, espacio.Tipo, espacio.Descripcion);
            }
        }

        private void CargarCbxRecintos()
        {
            using (var context = new ReserVAEntities())
            {
                cbxRecintoEspacio.Items.Clear();
                var recintos = context.Recinto.Select(e => new
                {
                    e.IdRecinto,
                    e.Nombre
                }).OrderBy(o => o.Nombre).ToList();

                foreach (var recinto in recintos)
                {
                    cbxRecintoEspacio.Items.Add(recinto);
                }
                cbxRecintoEspacio.DisplayMember = "Nombre";
                cbxRecintoEspacio.ValueMember = "IdRecinto";
                cbxRecintoEspacio.SelectedItem = null;
            }
        }

        private void LimpiarCamposEspacio()
        {
            Espacio = null;
            tbxNombreEspacio.Text = null;
            tbxTipoEspacio.Text = null;
            tbxDescripcionEspacio.Text = null;
            cbxRecintoEspacio.SelectedIndex = 0;

            btnEditarEspacio.Enabled = false;
            btnEliminarEspacio.Enabled = false;
        }
        
        private void DgvEspacios_SelectionChanged(object sender, EventArgs e)
        {            
            if (dgvEspacios.SelectedRows.Count == 1)
            {
                DataGridViewRow filaSeleccionada = dgvEspacios.SelectedRows[0];

                Espacio = new Espacio {
                    IdEspacio = Convert.ToInt32(filaSeleccionada.Cells["IdEspacio"].Value),
                    Nombre = filaSeleccionada.Cells["Nombre"].Value?.ToString(),
                    Tipo = filaSeleccionada.Cells["Tipo"].Value?.ToString(),
                    Descripcion = filaSeleccionada.Cells["Descripcion"].Value?.ToString(),
                    IdRecinto = Convert.ToInt32(filaSeleccionada.Cells["IdRecinto"].Value)
                };

                btnEditarEspacio.Enabled = true;
                btnEliminarEspacio.Enabled = true;

                tbxNombreEspacio.Text = Espacio.Nombre;
                tbxTipoEspacio.Text = Espacio.Tipo;
                tbxDescripcionEspacio.Text = Espacio.Descripcion;

                if (filaSeleccionada.Cells["Recinto"].Value != null)
                {
                    foreach (var item in cbxRecintoEspacio.Items)
                    {
                        dynamic obj = item; // Usamos 'dynamic' para acceder a las propiedades
                        if (obj.IdRecinto == Espacio.IdRecinto)
                        {
                            cbxRecintoEspacio.SelectedItem = item;
                            return;
                        }
                    }

                    MessageBox.Show($"No se encontró el IdRecinto {Espacio.IdRecinto} en el ComboBox.");
                }
            }
            else
            {
                LimpiarCamposEspacio();
            }
        }
        
        private void BtnFiltrarEspacios_Click(object sender, EventArgs e)
        {
            var recintoSeleccionado = cbxFiltroRecinto_Espacios.SelectedItem as dynamic;
            int idRecintoSeleccionado = recintoSeleccionado != null ? recintoSeleccionado.IdRecinto : -1;

            CargarEspacios(idRecintoSeleccionado);
        }

        private void BtnCrearEspacio_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbxNombreEspacio.Text) || string.IsNullOrWhiteSpace(tbxTipoEspacio.Text))
            {
                MessageBox.Show("Debe rellenar los campos nombre y tipo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var recintoSeleccionado = cbxRecintoEspacio.SelectedItem as dynamic;
            int idRecintoSeleccionado = recintoSeleccionado.IdRecinto;

            Espacio = new Espacio
            {
                Nombre = tbxNombreEspacio.Text,
                Tipo = tbxTipoEspacio.Text,
                Descripcion = tbxDescripcionEspacio.Text,
                IdRecinto = idRecintoSeleccionado,
            };

            bool creado = EspacioController.CrearEspacio(Espacio);
            if (creado)
            {
                var recintoFiltroSeleccionado = cbxFiltroRecinto_Espacios.SelectedItem as dynamic;
                int idRecintoFiltroSeleccionado = recintoFiltroSeleccionado != null ? recintoFiltroSeleccionado.IdRecinto : -1;

                CargarEspacios(idRecintoFiltroSeleccionado);
            }
        }

        private void BtnEditarEspacio_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbxNombreEspacio.Text) || string.IsNullOrWhiteSpace(tbxTipoEspacio.Text))
            {
                MessageBox.Show("Debe rellenar los campos nombre y tipo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var recintoSeleccionado = cbxRecintoEspacio.SelectedItem as dynamic;
            int idRecintoSeleccionado = recintoSeleccionado.IdRecinto;

            Espacio.Nombre = tbxNombreEspacio.Text;
            Espacio.Tipo = tbxTipoEspacio.Text;
            Espacio.Descripcion = tbxDescripcionEspacio.Text;
            Espacio.IdRecinto = idRecintoSeleccionado;

            bool editado = EspacioController.EditarEspacio(Espacio);
            if (editado)
            {
                var recintoFiltroSeleccionado = cbxFiltroRecinto_Espacios.SelectedItem as dynamic;
                int idRecintoFiltroSeleccionado = recintoFiltroSeleccionado != null ? recintoFiltroSeleccionado.IdRecinto : -1;

                CargarEspacios(idRecintoFiltroSeleccionado);
            }
        }

        private void BtnEliminarEspacio_Click(object sender, EventArgs e)
        {
            DialogResult eliminarRecinto = MessageBox.Show($"¿Estás seguro de que deseas eliminar el espacio {Espacio.Nombre}?", "Eliminar espacio", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            
            if (eliminarRecinto == DialogResult.Yes)
            {
                bool eliminado = EspacioController.EliminarEspacio(Espacio);
                if (eliminado)
                {
                    var recintoFiltroSeleccionado = cbxFiltroRecinto_Espacios.SelectedItem as dynamic;
                    int idRecintoFiltroSeleccionado = recintoFiltroSeleccionado != null ? recintoFiltroSeleccionado.IdRecinto : -1;

                    CargarEspacios(idRecintoFiltroSeleccionado);
                }
            }
        }

        private void BtnLimpiarCamposEspacio_Click(object sender, EventArgs e)
        {
            LimpiarCamposEspacio();
        }
        #endregion

        #region Pestaña Gestores
        private void CargarGestores(string filtro)
        {
            List<Usuario> gestores;

            if (filtro == null || filtro.Trim() == "")
            {
                gestores = UsuarioController.ObtenerGestores();
            }
            else
            {
                gestores = UsuarioController.ObtenerGestoresFiltrados(filtro);
            }

            dgvGestores.Rows.Clear();

            foreach (var gestor in gestores)
            {
                dgvGestores.Rows.Add(gestor.IdUsuario, gestor.Nombre, gestor.Apellidos, gestor.DocumentoIdentidad, gestor.Telefono, gestor.CorreoElectronico);
            }
        }

        private void LimpiarCamposGestor()
        {
            Gestor = null;
            tbxNombreGestor.Text = null;
            tbxApellidosGestor.Text = null;
            tbxDocumentoIdentidadGestor.Text = null;
            tbxTelefonoGestor.Text = null;
            tbxCorreoElectronicoGestor.Text = null;
            tbxContrasenaGestor.Text = null;

            btnEditarGestor.Enabled = false;
            btnEliminarGestor.Enabled = false;
        }
        
        private void DgvGestores_SelectionChanged(object sender, EventArgs e)
        {            
            if (dgvGestores.SelectedRows.Count == 1)
            {
                DataGridViewRow filaSeleccionada = dgvGestores.SelectedRows[0];

                Gestor = new Usuario {
                    IdUsuario = Convert.ToInt32(filaSeleccionada.Cells["IdUsuario"].Value),
                    Nombre = filaSeleccionada.Cells["Nombre"].Value?.ToString(),
                    Apellidos = filaSeleccionada.Cells["Apellidos"].Value?.ToString(),
                    DocumentoIdentidad = filaSeleccionada.Cells["DocumentoIdentidad"].Value?.ToString(),
                    Telefono = filaSeleccionada.Cells["Telefono"].Value?.ToString(),
                    CorreoElectronico = filaSeleccionada.Cells["CorreoElectronico"].Value?.ToString(),
                };

                btnEditarGestor.Enabled = true;
                btnEliminarGestor.Enabled = true;

                tbxNombreGestor.Text = Gestor.Nombre;
                tbxApellidosGestor.Text = Gestor.Apellidos;
                tbxDocumentoIdentidadGestor.Text = Gestor.DocumentoIdentidad;
                tbxTelefonoGestor.Text = Gestor.Telefono;
                tbxCorreoElectronicoGestor.Text = Gestor.CorreoElectronico;
                tbxContrasenaGestor.Text = null;
                Gestor.IdRol = 2;
            }
            else
            {
                LimpiarCamposGestor();
            }
        }
        
        private void BtnBuscarGestores_Click(object sender, EventArgs e)
        {
            CargarGestores(tbxFiltroGestores.Text);
        }
        
        private void BtnCrearGestor_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbxNombreGestor.Text) || string.IsNullOrWhiteSpace(tbxApellidosGestor.Text)
                || string.IsNullOrWhiteSpace(tbxDocumentoIdentidadGestor.Text) || string.IsNullOrWhiteSpace(tbxTelefonoGestor.Text)
                || string.IsNullOrWhiteSpace(tbxCorreoElectronicoGestor.Text) || string.IsNullOrEmpty(tbxContrasenaGestor.Text))
            {
                MessageBox.Show("Debe rellenar todos los campos para crear un gestor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string documento = UsuarioController.ValidarDocumentoIdentidad(tbxDocumentoIdentidadGestor.Text);
            if (documento != null)
            {
                MessageBox.Show($"El {documento} no es válido. Solo se acepta DNI, NIE o pasaporte español.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!UsuarioController.ValidarFormatoTelefono(tbxTelefonoGestor.Text))
            {
                MessageBox.Show($"El número de teléfono no es valido.\nEjemplo: 600112233", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!UsuarioController.ValidarFormatoEmail(tbxCorreoElectronicoGestor.Text))
            {
                MessageBox.Show($"El formato del email no es valido.\nEjemplo: nombre@domino.com", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!UsuarioController.ValidarFormatoContrasena(tbxContrasenaGestor.Text))
            {
                MessageBox.Show($"La contraseña debe cumplir los siguientes requisitos mínimos:\n" +
                                $"- Longitud mínima de 6 caracteres\n" +
                                $"- Una minúscula\n" +
                                $"- Una mayúscula\n" +
                                $"- Un dígito o símbolo",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            Gestor = new Usuario
            {
                Nombre = tbxNombreGestor.Text,
                Apellidos = tbxApellidosGestor.Text,
                DocumentoIdentidad = tbxDocumentoIdentidadGestor.Text,
                Telefono = tbxTelefonoGestor.Text,
                CorreoElectronico = tbxCorreoElectronicoGestor.Text,
                Contraseña = BCrypt.Net.BCrypt.HashPassword(tbxContrasenaGestor.Text),
                IdRol = 2,
            };

            bool creado = UsuarioController.RegistrarUsuario(Gestor);
            if (creado)
            {
                CargarGestores(tbxFiltroGestores.Text);
            }
        }

        private void BtnEditarGestor_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbxNombreGestor.Text) || string.IsNullOrWhiteSpace(tbxApellidosGestor.Text)
                || string.IsNullOrWhiteSpace(tbxDocumentoIdentidadGestor.Text) || string.IsNullOrWhiteSpace(tbxTelefonoGestor.Text)
                || string.IsNullOrWhiteSpace(tbxCorreoElectronicoGestor.Text))
            {
                MessageBox.Show("Debe rellenar los campos Nombre, Apellidos, DNI, NIE o Pasaporte, Teléfono y Correo electrónico para editar un gestor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string documento = UsuarioController.ValidarDocumentoIdentidad(tbxDocumentoIdentidadGestor.Text);
            if (documento != null)
            {
                MessageBox.Show($"El {documento} no es válido. Solo se acepta DNI, NIE o pasaporte español.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!UsuarioController.ValidarFormatoTelefono(tbxTelefonoGestor.Text))
            {
                MessageBox.Show($"El número de teléfono no es valido.\nEjemplo: 600112233", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!UsuarioController.ValidarFormatoEmail(tbxCorreoElectronicoGestor.Text))
            {
                MessageBox.Show($"El formato del email no es valido.\nEjemplo: nombre@domino.com", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }            

            Gestor.Nombre = tbxNombreGestor.Text;
            Gestor.Apellidos = tbxApellidosGestor.Text;
            Gestor.DocumentoIdentidad = tbxDocumentoIdentidadGestor.Text;
            Gestor.Telefono = tbxTelefonoGestor.Text;
            Gestor.CorreoElectronico = tbxCorreoElectronicoGestor.Text;
            if (!string.IsNullOrWhiteSpace(tbxContrasenaGestor.Text))
            {
                if (!UsuarioController.ValidarFormatoContrasena(tbxContrasenaGestor.Text))
                {
                    MessageBox.Show($"La contraseña debe cumplir los siguientes requisitos mínimos:\n" +
                                    $"- Longitud mínima de 6 caracteres\n" +
                                    $"- Una minúscula\n" +
                                    $"- Una mayúscula\n" +
                                    $"- Un dígito o símbolo",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else 
                {
                    DialogResult cambiarContraseña = MessageBox.Show($"Vas a cambiar la contraseña del usuario {Gestor.Nombre} {Gestor.Apellidos} con correo electrónico {Gestor.CorreoElectronico}. Esta acción dejará al gestor sin acceso hasta que se le informe de la nueva contraseña.\n¿Estás seguro?", "Cambiar contraseña", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (cambiarContraseña == DialogResult.Yes)
                    {
                        Gestor.Contraseña = BCrypt.Net.BCrypt.HashPassword(tbxContrasenaGestor.Text);
                    }
                    else
                    {
                        return;
                    }
                }
            }
            Gestor.IdRol = 2;

            bool editado = UsuarioController.EditarGestor(Gestor);
            if (editado)
            {
                CargarGestores(tbxFiltroGestores.Text);
            }
        }

        private void BtnEliminarGestor_Click(object sender, EventArgs e)
        {
            DialogResult eliminarRecinto = MessageBox.Show($"¿Estás seguro de que deseas eliminar al gestor {Gestor.Nombre} {Gestor.Apellidos} con correo electrónico {Gestor.CorreoElectronico}?", "Eliminar gestor", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            
            if (eliminarRecinto == DialogResult.Yes)
            {
                bool eliminado = UsuarioController.EliminarGestor(Gestor);
                if (eliminado)
                {
                    CargarGestores(tbxFiltroGestores.Text);
                }
            }
        }
        
        private void BtnLimpiarCamposGestor_Click(object sender, EventArgs e)
        {
            LimpiarCamposGestor();
        }
        #endregion
    }
}
