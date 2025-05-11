using System;
using System.Drawing;
using System.Windows.Forms;
using ReserVA.Properties;

namespace ReserVA
{
    public partial class FormBase : Form
    {
        private Point mouseOffset;
        private bool isMouseDown = false;

        public FormBase()
        {
            InitializeComponent();
            Font = new Font("Trebuchet MS", 9F);
            StartPosition = FormStartPosition.CenterScreen;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (Pen pen = new Pen(Settings.Default.ColorVentana, 1))
            {
                e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, this.ClientSize.Width - 1, this.ClientSize.Height - 1));
            }
        }

        private void PanelSuperior_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOffset = new Point(-e.X, -e.Y);
                isMouseDown = true;
            }
        }

        private void PanelSuperior_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
            }
        }

        private void PanelSuperior_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                isMouseDown = false;
        }

        protected void Button_MouseEnter(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn == btnCerrar)
                btn.BackColor = Color.DarkRed;
            else
                btn.BackColor = Settings.Default.ColorVentanaHover;
        }

        protected void Button_MouseLeave(object sender, EventArgs e)
        {
            var btn = sender as Button;
            btn.BackColor = Settings.Default.ColorVentana;
        }

        private void BtnCerrar_Click(object sender, EventArgs e) => Close();

        private void BtnMinimizar_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;
    }
}