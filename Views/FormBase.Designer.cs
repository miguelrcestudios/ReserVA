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
        protected Button btnMinimizar;
        protected Button btnCerrar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.Icon = Resources.Icono_EscudoAyuntamientoValladolid;
            this.Text = Resources.Título;

            this.panelSuperior = new Panel();
            this.pbxIcono = new PictureBox();
            this.lblTitulo = new Label();
            this.btnMinimizar = new Button();
            this.btnCerrar = new Button();
            this.panelSuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxIcono)).BeginInit();
            this.SuspendLayout();

            this.panelSuperior.BackColor = Settings.Default.ColorVentana;
            this.panelSuperior.Controls.Add(this.pbxIcono);
            this.panelSuperior.Controls.Add(this.lblTitulo);
            this.panelSuperior.Controls.Add(this.btnMinimizar);
            this.panelSuperior.Controls.Add(this.btnCerrar);
            this.panelSuperior.Dock = DockStyle.Top;
            this.panelSuperior.Size = new Size(this.ClientSize.Width, 40);
            this.panelSuperior.MouseDown += new MouseEventHandler(this.PanelSuperior_MouseDown);
            this.panelSuperior.MouseMove += new MouseEventHandler(this.PanelSuperior_MouseMove);
            this.panelSuperior.MouseUp += new MouseEventHandler(this.PanelSuperior_MouseUp);

            this.pbxIcono.Image = Resources.EscudoAyuntamientoValladolid;
            this.pbxIcono.Location = new Point(2, 2);
            this.pbxIcono.Size = new Size(36, 36);
            this.pbxIcono.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pbxIcono.MouseDown += new MouseEventHandler(this.PanelSuperior_MouseDown);
            this.pbxIcono.MouseMove += new MouseEventHandler(this.PanelSuperior_MouseMove);
            this.pbxIcono.MouseUp += new MouseEventHandler(this.PanelSuperior_MouseUp);

            this.lblTitulo.Font = new Font("Trebuchet MS", 12F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.White;
            this.lblTitulo.Location = new Point(42, 0);
            this.lblTitulo.Size = new Size(200, 40);
            this.lblTitulo.Text = Resources.Título;
            this.lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
            this.lblTitulo.MouseDown += new MouseEventHandler(this.PanelSuperior_MouseDown);
            this.lblTitulo.MouseMove += new MouseEventHandler(this.PanelSuperior_MouseMove);
            this.lblTitulo.MouseUp += new MouseEventHandler(this.PanelSuperior_MouseUp);

            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatStyle = FlatStyle.Flat;
            this.btnCerrar.ForeColor = Color.White;
            this.btnCerrar.Size = new Size(40, 40);
            this.btnCerrar.Location = new Point(this.ClientSize.Width - 40, 0);
            this.btnCerrar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnCerrar.Text = "✕";
            this.btnCerrar.Click += new EventHandler(this.BtnCerrar_Click);
            this.btnCerrar.MouseEnter += new EventHandler(this.Button_MouseEnter);
            this.btnCerrar.MouseLeave += new EventHandler(this.Button_MouseLeave);
            this.btnCerrar.BackColor = Settings.Default.ColorVentana;

            this.btnMinimizar.FlatAppearance.BorderSize = 0;
            this.btnMinimizar.FlatStyle = FlatStyle.Flat;
            this.btnMinimizar.ForeColor = Color.White;
            this.btnMinimizar.Size = new Size(40, 40);
            this.btnMinimizar.Location = new Point(this.ClientSize.Width - 80, 0);
            this.btnMinimizar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnMinimizar.Text = "—";
            this.btnMinimizar.Click += new EventHandler(this.BtnMinimizar_Click);
            this.btnMinimizar.MouseEnter += new EventHandler(this.Button_MouseEnter);
            this.btnMinimizar.MouseLeave += new EventHandler(this.Button_MouseLeave);
            this.btnMinimizar.BackColor = Settings.Default.ColorVentana;

            this.Controls.Add(this.panelSuperior);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Font = new Font("Trebuchet MS", 9F);
            this.ResumeLayout(false);
        }
    }
}