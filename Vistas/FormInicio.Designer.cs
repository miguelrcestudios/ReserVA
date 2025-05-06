using ReserVA.Controllers;
using ReserVA.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ReserVA
{
    partial class FormInicio
    {
        private Button btnIniciarSesion;
        private Panel panelRecintos;

        private void InitializeComponent()
        {
            ClientSize = new Size(1200, 600);
            btnMinimizar.Location = new Point(this.ClientSize.Width - 80, 0);
            btnCerrar.Location = new Point(this.ClientSize.Width - 40, 0);

            btnIniciarSesion = new Button();            

            btnIniciarSesion.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnIniciarSesion.FlatStyle = FlatStyle.Flat;
            btnIniciarSesion.FlatAppearance.BorderSize = 0;
            btnIniciarSesion.Font = new Font("Trebuchet MS", 9.75F, FontStyle.Bold);
            btnIniciarSesion.ForeColor = Color.White;
            btnIniciarSesion.Size = new Size(160, 40);
            btnIniciarSesion.Location = new Point(this.Width - 240, 0);
            btnIniciarSesion.TextAlign = ContentAlignment.MiddleCenter;
            btnIniciarSesion.Text = "👤 Iniciar sesión";
            btnIniciarSesion.BackColor = Settings.Default.ColorPrimario;
            btnIniciarSesion.Click += new EventHandler(this.BtnIniciarSesion_IniciarSesion_Click);
            
            panelSuperior.Controls.Add(this.btnIniciarSesion);


            panelRecintos = new FlowLayoutPanel();
            panelRecintos.Location = new Point(15, 80);
            panelRecintos.Size = new Size(Width -30, Height - 100);
            panelRecintos.HorizontalScroll.Visible = false;
            panelRecintos.HorizontalScroll.Enabled = false;
            panelRecintos.VerticalScroll.Visible = true;
            panelRecintos.VerticalScroll.Enabled = true;
            panelRecintos.AutoScroll = true;
            panelRecintos.Margin = new Padding(15, 80, 15, 20);

            foreach (Control control in panelRecintos.Controls)
            {
                control.MaximumSize = new Size(panelRecintos.Width - 20, control.Height);
            }


            Controls.Add(panelRecintos);
        }

        private void CargarEspacios()
        {
            List<Espacio> espacios = EspacioController.ObtenerTodos(); // Datos simulados desde el controlador

            foreach (var espacio in espacios)
            {
                var panel = new Panel
                {
                    Width = panelRecintos.ClientSize.Width - 25,
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
                    BackColor = Settings.Default.ColorPrimario,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Anchor = AnchorStyles.Right | AnchorStyles.Top,
                    Tag = espacio
                };

                btnReservar.FlatAppearance.BorderSize = 0;
                btnReservar.Location = new Point(panel.Width - btnReservar.Width - 10, (panel.Height - btnReservar.Height) / 2);

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
