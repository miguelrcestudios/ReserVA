using System.Drawing;

namespace ReserVA
{
    public partial class FormRegistro : FormBase
    {
        public FormRegistro()
        {
            InitializeComponent();
            ClientSize = new System.Drawing.Size(600, 450);
            btnMinimizar.Location = new Point(this.ClientSize.Width - 80, 0);
            btnCerrar.Location = new Point(this.ClientSize.Width - 40, 0);
        }
    }
}
