using ReserVA.Controller;
using ReserVA.Controllers;
using ReserVA.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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
                            Show();
                            
                            Usuario = null;
                            btnIniciarSesion.Text = "👤 Iniciar sesión";
                            btnIniciarSesion.Click -= BtnIniciarSesion_CerrarSesion_Click;
                            btnIniciarSesion.Click += BtnIniciarSesion_IniciarSesion_Click;                            
                        };
                        Hide();
                        gestor.Show();
                    }

                    var nombreYApellido = (formInicioSesion.Usuario.Nombre + " " + formInicioSesion.Usuario.Apellidos).Substring(0, 10).Length > 10
                    ? (formInicioSesion.Usuario.Nombre + " " + formInicioSesion.Usuario.Apellidos).Substring(0, 10) + "..."
                    : (formInicioSesion.Usuario.Nombre + " " + formInicioSesion.Usuario.Apellidos);

                    btnIniciarSesion.Text = "👤 " + nombreYApellido;
                    formInicioSesion.Close();

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
            }
        }
        
        private void BtnFiltrar_Click(object sender, EventArgs e)
        {
            var barrioSeleccionado = cbxFiltroBarrios.SelectedItem as dynamic;
            int idBarrioSeleccionado = barrioSeleccionado != null ? barrioSeleccionado.Id : -1;
            
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

                reserva.ShowDialog();
            }            
        }

        private void CargarFiltroBarrios()
        {
            using (var context = new ReserVAEntities())
            {
                var barrioVacio = new { Id = -1, Nombre = "" };

                var barrios = context.Barrio
                    .Select(s => new
                    {
                        Id = s.IdBarrio,
                        Nombre =  s.Nombre
                    })
                    .OrderBy(o => o.Nombre)
                    .ToList();

                barrios.Insert(0, barrioVacio);
                foreach (var barrio in barrios)
                {
                    cbxFiltroBarrios.Items.Add(barrio);
                }
                cbxFiltroBarrios.DisplayMember = "Nombre";
                cbxFiltroBarrios.ValueMember = "Id";
                cbxFiltroBarrios.SelectedItem = null;
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
                espacios = EspacioController.ObtenerFiltrados(idBarrio);
            }

            foreach (var espacio in espacios)
            {
                var panel = new Panel
                {
                    Width = panelRecintos.ClientSize.Width - 30,
                    Height = 100,
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
                    Location = new Point(panel.Width - 140, (panel.Height / 2) - 20),
                    BackColor = Settings.Default.ColorPrimario,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    FlatAppearance = { BorderSize = 0 },
                    //Anchor = AnchorStyles.Right | AnchorStyles.Top,
                    Tag = espacio
                };
                btnReservar.Click += BtnReserva_Click;

                var lblNombre = new Label
                {
                    Text = espacio.Nombre + " (" + espacio.Recinto.Nombre + ")",
                    Font = new Font("Trebuchet MS", 12F, FontStyle.Bold),
                    AutoSize = false,
                    Height = 25,
                    Width = panel.Width - btnReservar.Width - 20,
                    Dock = DockStyle.Top
                };

                var lblTipo = new Label
                {
                    Text = espacio.Tipo,
                    Font = new Font("Trebuchet MS", 10F, FontStyle.Regular),
                    ForeColor = Color.Gray,
                    AutoSize = false,
                    Height = 20,
                    Width = panel.Width - btnReservar.Width - 20,
                    Dock = DockStyle.Top
                };

                var descripcionReducida = espacio.Descripcion?.Length > 150
                    ? espacio.Descripcion.Substring(0, 150) + "..."
                    : espacio.Descripcion;

                var lblDescripcion = new Label
                {
                    Text = descripcionReducida,
                    Font = new Font("Trebuchet MS", 9F, FontStyle.Regular),
                    ForeColor = Color.DimGray,
                    AutoSize = false,
                    Height = 40,
                    Width = panel.Width - btnReservar.Width - 20,
                    Dock = DockStyle.Top
                };

                panel.Controls.Add(btnReservar);
                panel.Controls.Add(lblDescripcion);
                panel.Controls.Add(lblTipo);
                panel.Controls.Add(lblNombre);

                panelRecintos.Controls.Add(panel);
            }
        }
    }
}
