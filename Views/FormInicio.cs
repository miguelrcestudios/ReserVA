using ReserVA.Models;
using ReserVA.Controller;
using ReserVA.Controllers;
using ReserVA.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Numerics;

namespace ReserVA
{
    public partial class FormInicio : FormBase
    {
        public Usuario Usuario { get; set; } = null;

        public FormInicio()
        {
            InitializeComponent();
            CargarEspacios(-1);
            CargarFiltroBarrios();
        }

        private void BtnIniciarSesion_IniciarSesion_Click(object sender, EventArgs e)
        {
            using (FormInicioSesion formInicioSesion = new FormInicioSesion())
            {
                if (formInicioSesion.ShowDialog() == DialogResult.OK)
                {
                    Usuario = formInicioSesion.Usuario;

                    if (Usuario.IdRol == 2 || Usuario.IdRol == 3)
                    {
                        Form gestor = new FormGestor(Usuario);
                        gestor.FormClosed += (s, args) =>
                        {
                            CargarEspacios(-1);
                            Show();
                            
                            Usuario = null;
                            btnIniciarSesion.Text = "👤 Iniciar sesión";
                            btnIniciarSesion.Click -= BtnIniciarSesion_CerrarSesion_Click;
                            btnIniciarSesion.Click += BtnIniciarSesion_IniciarSesion_Click;                            
                        };
                        Hide();
                        gestor.Show();
                    }

                    var nombreYApellido = (formInicioSesion.Usuario.Nombre + " " + formInicioSesion.Usuario.Apellidos).Length > 18
                    ? (formInicioSesion.Usuario.Nombre + " " + formInicioSesion.Usuario.Apellidos).Substring(0, 15) + "..."
                    : (formInicioSesion.Usuario.Nombre + " " + formInicioSesion.Usuario.Apellidos);

                    btnIniciarSesion.Text = "👤 " + nombreYApellido;
                    formInicioSesion.Close();

                    if (Usuario != null && Usuario.IdRol == 1)
                    {
                        tabControlUsuario.TabPages.Add(tabPageProximasReservas);
                        tabControlUsuario.TabPages.Add(tabPageHistorialReservas);
                        CargarProximasReservas();
                        CargarHistorialReservas();
                    }

                    btnIniciarSesion.Click -= BtnIniciarSesion_IniciarSesion_Click;
                    btnIniciarSesion.Click += BtnIniciarSesion_CerrarSesion_Click;                    
                }
            }
        }

        private void BtnIniciarSesion_CerrarSesion_Click(object sender, EventArgs e)
        { 
            bool cerrarSesion = UsuarioController.CerrarSesion();
            if (cerrarSesion)
            {
                Usuario = null;
                btnIniciarSesion.Text = "👤 Iniciar sesión";
                btnIniciarSesion.Click -= BtnIniciarSesion_CerrarSesion_Click;
                btnIniciarSesion.Click += BtnIniciarSesion_IniciarSesion_Click;
                tabControlUsuario.TabPages.Remove(tabPageProximasReservas);
                tabControlUsuario.TabPages.Remove(tabPageHistorialReservas);
            }
        }
        
        private void BtnFiltrar_Click(object sender, EventArgs e)
        {
            var barrioSeleccionado = cbxFiltroBarrios.SelectedItem as dynamic;
            int idBarrioSeleccionado = barrioSeleccionado != null ? barrioSeleccionado.IdBarrio : -1;
            
            CargarEspacios(idBarrioSeleccionado);
        }

        private void BtnReserva_Click(object sender, EventArgs e)
        {
            Button btnClicked = sender as Button;
            if (btnClicked != null && btnClicked.Tag != null)
            {
                Espacio espacio = (Espacio)btnClicked.Tag;
                Form reserva;
                if (Usuario == null)
                {
                    reserva = new FormReserva(espacio);
                }
                else
                {
                    reserva = new FormReserva(espacio, Usuario);
                }
                reserva.FormClosed += (s, args) =>
                {
                    if (Usuario != null && Usuario.IdRol == 1)
                    {
                        CargarProximasReservas();
                        CargarHistorialReservas();
                    };
                };
                reserva.ShowDialog();
            }            
        }

        private void CargarFiltroBarrios()
        {
            using (var context = new ReserVAEntities())
            {
                var barrioVacio = new BarrioDTO { IdBarrio = -1, Nombre = "" };

                var barrios = context.Barrio
                    .Select(s => new BarrioDTO
                    {
                        IdBarrio = s.IdBarrio,
                        Nombre =  s.Nombre
                    })
                    .OrderBy(o => o.Nombre)
                    .ToList();

                barrios.Insert(0, barrioVacio);
                cbxFiltroBarrios.DataSource = barrios;
                cbxFiltroBarrios.DisplayMember = "Nombre";
                cbxFiltroBarrios.ValueMember = "IdBarrio";
            }
        }

        private void CargarEspacios(int idBarrio)
        {
            List<Espacio> espacios;

            panelRecintos.Controls.Clear();

            if (idBarrio == -1)
            {
                espacios = EspacioController.ObtenerTodos();
            }
            else
            {
                espacios = EspacioController.ObtenerFiltradosPorBarrio(idBarrio);
            }

            foreach (var espacio in espacios)
            {
                int tamanoDescripcion = (int)Math.Ceiling((double)espacio.Descripcion.Length / 200);

                var panelEspacio = new Panel
                {
                    Size = new Size (panelRecintos.ClientSize.Width - 30, 60 + (tamanoDescripcion * 20)),
                    BackColor = Color.White,
                    Margin = new Padding(5),
                    Padding = new Padding(10),
                    BorderStyle = BorderStyle.FixedSingle
                };

                var btnReservar = new Button
                {
                    Text = "Reservar",
                    Font = new Font("Trebuchet MS", 10F, FontStyle.Bold),
                    Size = new Size(120, 40),
                    Location = new Point(panelEspacio.Width - 140, 10),
                    BackColor = Settings.Default.ColorPrimario,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand,
                    Tag = espacio
                };
                btnReservar.Click += BtnReserva_Click;

                var lblNombre = new Label
                {
                    Text = $"{espacio.Nombre} ({espacio.Recinto.Nombre})",
                    Font = new Font("Trebuchet MS", 12F, FontStyle.Bold),
                    Location = new Point(10, 10),
                    Size = new Size(panelEspacio.ClientSize.Width - btnReservar.Width - 20, 25),
                    Dock = DockStyle.Top
                };

                var lblTipoSubzona = new Label
                {
                    Text = $"{espacio.Tipo} \t|\t {espacio.Recinto.Subzona.Nombre} ({espacio.Recinto.Subzona.Barrio.Nombre})",
                    Font = new Font("Trebuchet MS", 10F, FontStyle.Regular),
                    Location = new Point(10, 40),
                    Size = new Size(panelEspacio.ClientSize.Width - btnReservar.Width - 20, 20),
                    Dock = DockStyle.Top
                };

                var lblDescripcion = new Label
                {
                    Text = espacio.Descripcion,
                    Font = new Font("Trebuchet MS", 9F, FontStyle.Regular),
                    ForeColor = Settings.Default.ColorVentanaHover,
                    Location = new Point(10, 70),
                    Margin = new Padding(0, 5, 160, 0),
                    Size = new Size(panelEspacio.ClientSize.Width - btnReservar.ClientSize.Width * 2 - 20, tamanoDescripcion * 20),
                    Dock = DockStyle.Top
                };

                panelEspacio.Controls.Add(btnReservar);
                panelEspacio.Controls.Add(lblDescripcion);
                panelEspacio.Controls.Add(lblTipoSubzona);
                panelEspacio.Controls.Add(lblNombre);

                panelRecintos.Controls.Add(panelEspacio);
            }
        }

        public void CargarProximasReservas()
        {
            List<ReservaDTO> reservas = ReservaController.ObtenerProximas(Usuario.IdUsuario);          

            dgvProximasReservas.Rows.Clear();

            foreach (var espacio in reservas)
            {
                dgvProximasReservas.Rows.Add(espacio.NumeroReserva, espacio.Fecha, espacio.HoraInicio, espacio.HoraFin, espacio.Espacio, espacio.Recinto);
            }
        }
        
        public void CargarHistorialReservas()
        {
            List<ReservaDTO> reservas = ReservaController.ObtenerHistorial(Usuario.IdUsuario);          

            dgvHistoriaReservas.Rows.Clear();

            foreach (var espacio in reservas)
            {
                dgvHistoriaReservas.Rows.Add(espacio.NumeroReserva, espacio.Fecha, espacio.HoraInicio, espacio.HoraFin, espacio.Espacio, espacio.Recinto);
            }
        }
    }
}
