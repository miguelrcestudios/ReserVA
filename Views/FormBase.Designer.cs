using System.Drawing;
using System.Windows.Forms;
using System;
using ReserVA.Properties;

namespace ReserVA
{
    partial class FormBase
    {
        private System.ComponentModel.IContainer components = null;
        protected Panel panelSuperior;
        protected PictureBox pbxIcono;
        protected Label lblTitulo;
        protected Button btnMinimizar, btnCerrar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            Icon = Resources.Icono_EscudoAyuntamientoValladolid;
            Text = Resources.Título;
            Font = new Font("Trebuchet MS", 9F);
            FormBorderStyle = FormBorderStyle.None;

            panelSuperior = new Panel
            {
                Size = new Size(this.ClientSize.Width, 40),
                Dock = DockStyle.Top,
                BackColor = Settings.Default.ColorVentana,
            };
            panelSuperior.MouseDown += PanelSuperior_MouseDown;
            panelSuperior.MouseMove += PanelSuperior_MouseMove;
            panelSuperior.MouseUp += PanelSuperior_MouseUp;
            

            pbxIcono = new PictureBox
            {
                Image = Resources.EscudoAyuntamientoValladolid,
                Location = new Point(2, 2),
                Size = new Size(36, 36),
                SizeMode = PictureBoxSizeMode.StretchImage,
            };
            pbxIcono.MouseDown += PanelSuperior_MouseDown;
            pbxIcono.MouseMove += PanelSuperior_MouseMove;
            pbxIcono.MouseUp += PanelSuperior_MouseUp;

            lblTitulo = new Label
            {
                Font = new Font("Trebuchet MS", 12F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(42, 0),
                Size = new Size(200, 40),
                Text = Resources.Título,
                TextAlign = ContentAlignment.MiddleLeft,
            };
            lblTitulo.MouseDown += PanelSuperior_MouseDown;
            lblTitulo.MouseMove += PanelSuperior_MouseMove;
            lblTitulo.MouseUp += PanelSuperior_MouseUp;

            btnMinimizar = new Button
            {
                Text = "—",
                Location = new Point(this.ClientSize.Width - 80, 0),
                Size = new Size(40, 40),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                ForeColor = Color.White,
                BackColor = Settings.Default.ColorVentana,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Cursor = Cursors.Hand,
            };
            btnMinimizar.Click += BtnMinimizar_Click;
            btnMinimizar.MouseEnter += Button_MouseEnter;
            btnMinimizar.MouseLeave += Button_MouseLeave;

            btnCerrar = new Button {
                Text = "✕",
                Location = new Point(this.ClientSize.Width - 40, 0),
                Size = new Size(40, 40),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0},
                BackColor = Settings.Default.ColorVentana,
                Cursor = Cursors.Hand,
            };
            btnCerrar.Click += BtnCerrar_Click;
            btnCerrar.MouseEnter += Button_MouseEnter;
            btnCerrar.MouseLeave += Button_MouseLeave;            

            panelSuperior.Controls.Add(pbxIcono);
            panelSuperior.Controls.Add(lblTitulo);
            panelSuperior.Controls.Add(btnMinimizar);
            panelSuperior.Controls.Add(btnCerrar);

            panelSuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(pbxIcono)).BeginInit();
            SuspendLayout();

            Controls.Add(panelSuperior);
            ResumeLayout(false);
        }
    }
}