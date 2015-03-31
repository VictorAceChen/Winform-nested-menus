 
using System.Windows.Forms;

namespace MenuProject.ChildForms
{
    public partial class _ChildForm : Form
    {
        public _ChildForm()
        {
            InitializeComponent(); 
        }

        protected virtual void _childForm_Load(object sender, System.EventArgs e)
        {
            //Default settings for child forms
            this.ControlBox = false; 
            this.WindowState = FormWindowState.Maximized;
            //this.FormBorderStyle  = FormBorderStyle.None;
        }
    }
}
