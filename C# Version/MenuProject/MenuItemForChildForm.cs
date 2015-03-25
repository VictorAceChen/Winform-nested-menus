using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MenuProject
{
    class MenuItemForChildForm : ToolStripMenuItem
    {
        new mainForm Parent;
        Type _childForm;

        public MenuItemForChildForm(mainForm inputMainForm, Type inputChildForm)
        {
            Parent = inputMainForm;
            _childForm = inputChildForm;

            //set the menuItem properties
            this.Name = inputChildForm.Name;
            this.Text = inputChildForm.Name;
            this.Click += childFormToolStripMenuItem_Click;
        }

        public Type childForm
        {
            get { return _childForm; }
        }

        private void childFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;

            dynamic child = Activator.CreateInstance(this._childForm);
            child.MdiParent = Parent;
            child.Show();
        }
    }
}
