using RerserVA.Controllers;
using RerserVA.Models;
using ReserVA.Controller;
using ReserVA.Controllers;
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

        public FormGestor(Usuario usuario)
        {
            Usuario = usuario;
            InitializeComponent();
            
            var nombreYApellido = (Usuario.Nombre + " " + Usuario.Apellidos).Substring(0, 10).Length > 10
            ? (Usuario.Nombre + " " + Usuario.Apellidos).Substring(0, 10) + "..."
            : (Usuario.Nombre + " " + Usuario.Apellidos);
            btnIniciarSesion.Text = "👤 " + nombreYApellido;

            btnIniciarSesion.Click += BtnIniciarSesion_CerrarSesion_Click;

            CargarReservas(-1);
            CargarRecintos(-1);
            CargarEspacios(-1);
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
                var barrioVacio = new { Id = -1, Nombre = "" };

                var barrios = context.Barrio.Select(e => new
                {
                    Id = e.IdBarrio,
                    Nombre = e.Nombre
                }).OrderBy(o => o.Nombre).ToList();

                barrios.Insert(0, barrioVacio);
                foreach (var barrio in barrios)
                {
                    cbxFiltroBarrio_Recintos.Items.Add(barrio);
                }
                cbxFiltroBarrio_Recintos.DisplayMember = "Nombre";
                cbxFiltroBarrio_Recintos.ValueMember = "Id";
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
                var barrios = context.Subzona.Select(e => new
                {
                    Id = e.IdSubzona,
                    Nombre = e.Nombre,
                    NombreBarrio = e.Barrio.Nombre,
                    Display = e.Nombre + " (" + e.Barrio.Nombre + ")" 
                }).OrderBy(o => o.Nombre).ToList();

                foreach (var barrio in barrios)
                {
                    cbxSubzona_Recinto.Items.Add(barrio);
                }
                cbxSubzona_Recinto.DisplayMember = "Display";
                cbxSubzona_Recinto.ValueMember = "Id";
                cbxSubzona_Recinto.SelectedItem = null;
            }
        }
        
        private void DgvRecintos_SelectionChanged(object sender, EventArgs e)
        {            
            if (dgvRecintos.SelectedRows.Count > 0)
            {
                DataGridViewRow filaSeleccionada = dgvRecintos.SelectedRows[0];

                Recinto = new Recinto {
                    IdRecinto = Convert.ToInt32(filaSeleccionada.Cells["IdRecinto"].Value),
                    Nombre = filaSeleccionada.Cells["Nombre"].Value?.ToString(),
                    Direccion = filaSeleccionada.Cells["Dirección"].Value?.ToString(),
                    IdSubzona = Convert.ToInt32(filaSeleccionada.Cells["IdSubzona"].Value)
                };

                tbxNombreRecinto.Text = Recinto.Nombre;
                tbxDireccionRecinto.Text = Recinto.Direccion;

                if (filaSeleccionada.Cells["Subzona"].Value != null)
                {
                    foreach (var item in cbxSubzona_Recinto.Items)
                    {
                        dynamic obj = item; // Usamos 'dynamic' para acceder a las propiedades
                        if (obj.Id == Recinto.IdSubzona)
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
                tbxNombreRecinto.Text = null;
                tbxDireccionRecinto.Text = null;
                cbxSubzona_Recinto.SelectedIndex = 0;
            }
        }
        
        private void BtnFiltrarRecintos_Click(object sender, EventArgs e)
        {
            var barrioSeleccionado = cbxFiltroBarrio_Recintos.SelectedItem as dynamic;
            int idBarrioSeleccionado = barrioSeleccionado != null ? barrioSeleccionado.Id : -1;

            CargarRecintos(idBarrioSeleccionado);
        }
        
        private void BtnCrearRecinto_Click(object sender, EventArgs e)
        {
            var subzonaSeleccionada = cbxSubzona_Recinto.SelectedItem as dynamic;
            int idSubzonaSeleccionada = subzonaSeleccionada.Id;

            Recinto.Nombre = tbxNombreRecinto.Text;
            Recinto.Direccion = tbxDireccionRecinto.Text;
            Recinto.IdSubzona = idSubzonaSeleccionada;

            bool creado = RecintoController.CrearRecinto(Recinto);
            if (creado)
            {
                var barrioSeleccionado = cbxFiltroBarrio_Recintos.SelectedItem as dynamic;
                int idBarrioSeleccionado = barrioSeleccionado != null ? barrioSeleccionado.Id : -1;

                CargarRecintos(idBarrioSeleccionado);
            }
        }

        private void BtnEditarRecinto_Click(object sender, EventArgs e)
        {
            var subzonaSeleccionada = cbxSubzona_Recinto.SelectedItem as dynamic;
            int idSubzonaSeleccionada = subzonaSeleccionada.Id; 

            Recinto.Nombre = tbxNombreRecinto.Text;
            Recinto.Direccion = tbxDireccionRecinto.Text;            
            Recinto.IdSubzona = idSubzonaSeleccionada;

            bool editado = RecintoController.EditarRecinto(Recinto);
            if (editado)
            {
                var barrioSeleccionado = cbxFiltroBarrio_Recintos.SelectedItem as dynamic;
                int idBarrioSeleccionado = barrioSeleccionado != null ? barrioSeleccionado.Id : -1;

                CargarRecintos(idBarrioSeleccionado);
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
                    int idBarrioSeleccionado = barrioSeleccionado != null ? barrioSeleccionado.Id : -1;

                    CargarRecintos(idBarrioSeleccionado);
                }
            }
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
                espacios = EspacioController.ObtenerFiltrados(idRecinto);
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
                var barrios = context.Recinto.Select(e => new
                {
                    Id = e.IdRecinto,
                    Nombre = e.Nombre
                }).OrderBy(o => o.Nombre).ToList();

                foreach (var barrio in barrios)
                {
                    cbxRecintoEspacio.Items.Add(barrio);
                }
                cbxRecintoEspacio.DisplayMember = "Nombre";
                cbxRecintoEspacio.ValueMember = "Id";
                cbxRecintoEspacio.SelectedItem = null;
            }
        }
        
        private void DgvEspacios_SelectionChanged(object sender, EventArgs e)
        {            
            if (dgvEspacios.SelectedRows.Count > 0)
            {
                DataGridViewRow filaSeleccionada = dgvEspacios.SelectedRows[0];

                Espacio = new Espacio {
                    IdEspacio = Convert.ToInt32(filaSeleccionada.Cells["IdEspacio"].Value),
                    Nombre = filaSeleccionada.Cells["Nombre"].Value?.ToString(),
                    Tipo = filaSeleccionada.Cells["Tipo"].Value?.ToString(),
                    Descripcion = filaSeleccionada.Cells["Descripcion"].Value?.ToString(),
                    IdRecinto = Convert.ToInt32(filaSeleccionada.Cells["IdRecinto"].Value)
                };

                tbxNombreEspacio.Text = Espacio.Nombre;
                tbxTipoEspacio.Text = Espacio.Tipo;
                tbxDescripcionEspacio.Text = Espacio.Descripcion;

                if (filaSeleccionada.Cells["Recinto"].Value != null)
                {
                    foreach (var item in cbxRecintoEspacio.Items)
                    {
                        dynamic obj = item; // Usamos 'dynamic' para acceder a las propiedades
                        if (obj.Id == Espacio.IdRecinto)
                        {
                            cbxRecintoEspacio.SelectedItem = item;
                            return;
                        }
                    }

                    MessageBox.Show($"No se encontró el IdRecinto {Recinto.IdSubzona} en el ComboBox.");
                }
            }
            else
            {
                tbxNombreRecinto.Text = null;
                tbxDireccionRecinto.Text = null;
                cbxSubzona_Recinto.SelectedIndex = 0;
            }
        }
        
        private void BtnFiltrarEspacios_Click(object sender, EventArgs e)
        {
            var recintoSeleccionado = cbxFiltroRecinto_Espacios.SelectedItem as dynamic;
            int idRecintoSeleccionado = recintoSeleccionado != null ? recintoSeleccionado.Id : -1;

            CargarRecintos(idRecintoSeleccionado);
        }
        
        private void BtnCrearEspacio_Click(object sender, EventArgs e)
        {
            var espacioSeleccionado = cbxRecintoEspacio.SelectedItem as dynamic;
            int idEspacioSeleccionado = espacioSeleccionado.Id;

            Espacio.Nombre = tbxNombreEspacio.Text;
            Espacio.Tipo = tbxTipoEspacio.Text;
            Espacio.Descripcion = tbxDescripcionEspacio.Text;
            Espacio.IdRecinto = idEspacioSeleccionado;

            bool creado = EspacioController.CrearEspacio(Espacio);
            if (creado)
            {
                var recintoSeleccionado = cbxFiltroBarrio_Recintos.SelectedItem as dynamic;
                int idRecintoSeleccionado = recintoSeleccionado != null ? recintoSeleccionado.Id : -1;

                CargarEspacios(idRecintoSeleccionado);
            }
        }

        private void BtnEditarEspacio_Click(object sender, EventArgs e)
        {
            var espacioSeleccionado = cbxRecintoEspacio.SelectedItem as dynamic;
            int idEspacioSeleccionado = espacioSeleccionado.Id;

            Espacio.Nombre = tbxNombreEspacio.Text;
            Espacio.Tipo = tbxTipoEspacio.Text;
            Espacio.Descripcion = tbxDescripcionEspacio.Text;
            Espacio.IdRecinto = idEspacioSeleccionado;

            bool editado = EspacioController.EditarEspacio(Espacio);
            if (editado)
            {
                var recintoSeleccionado = cbxFiltroBarrio_Recintos.SelectedItem as dynamic;
                int idRecintoSeleccionado = recintoSeleccionado != null ? recintoSeleccionado.Id : -1;

                CargarEspacios(idRecintoSeleccionado);
            }
        }

        private void BtnEliminarEspacio_Click(object sender, EventArgs e)
        {
            DialogResult eliminarRecinto = MessageBox.Show($"¿Estás seguro de que deseas eliminar el recinto {Recinto.Nombre}?", "Eliminar recinto", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            
            if (eliminarRecinto == DialogResult.Yes)
            {
                bool eliminado = EspacioController.EliminarEspacio(Espacio);
                if (eliminado)
                {
                    var recintoSeleccionado = cbxFiltroBarrio_Recintos.SelectedItem as dynamic;
                    int idRecintoSeleccionado = recintoSeleccionado != null ? recintoSeleccionado.Id : -1;

                    CargarEspacios(idRecintoSeleccionado);
                }
            }
        }
        #endregion
    }
}
