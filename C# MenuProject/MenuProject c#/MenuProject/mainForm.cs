using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing; 
using System.Text;
using System.Windows.Forms;
using System.IO;
using MenuProject.Properties;
 

namespace MenuProject
{
    public partial class mainForm : Form
    { 
        Rectangle closeButtonPosition;
        Rectangle rect; 
        Bitmap CloseImageDynamic = Images.Default;
        bool isMouseLeftDown = false;
         
        public mainForm()
        {
            InitializeComponent();
        }

        public void MainForm_Load(object sender, EventArgs e)
        { 
            this.tabControl1.Padding = new System.Drawing.Point(25, 0);
            menuStripFormation.buildMenuStrip(this);
        }

        private void MainForm_MdiChildActivate(object sender, EventArgs e)
        {
            // If there are no child form, hide tabControl
            if (this.ActiveMdiChild == null)    
                tabControl1.Visible = false;
            else 
            {
                tabControl1.Visible = true;

                // If child form is new
                if (this.ActiveMdiChild.Tag == null)
                {
                    //create tab page 
                    TabPage tp = new TabPage();
                    tp.Text = this.ActiveMdiChild.Text;
                    tp.Tag = this.ActiveMdiChild;
                    tabControl1.TabPages.Add(tp);

                    //connect child form
                    tp.Parent = tabControl1;
                    tabControl1.SelectedTab = tp;
                    this.ActiveMdiChild.Tag = tp;

                    focusCloseButton();
                }
            }

        }

        //focuses on selected tab
        private void focusCloseButton()
        { 
            rect = tabControl1.GetTabRect(tabControl1.SelectedIndex);
            closeButtonPosition =
                new Rectangle(rect.Right - 15
                , rect.Top + 4
                , CloseImageDynamic.Width,
                CloseImageDynamic.Height);
        }

        #region tabControl1 Events

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Bitmap CloseImageTarget = CloseImageDynamic;

            if (e.Index != tabControl1.SelectedIndex)
                CloseImageTarget = Images.Default;
            
            #region  DrawTabTitle

            e.Graphics.DrawString( 
                this.tabControl1.TabPages[e.Index].Text
                , e.Font, Brushes.Black
                , e.Bounds.Left + 5
                , e.Bounds.Top + 4
                );

            #endregion

            #region  DrawCloseButton

            e.Graphics.DrawImage(
                CloseImageTarget
                , e.Bounds.Right - CloseImageDynamic.Width - 3
                , e.Bounds.Top + 3
                , CloseImageDynamic.Width
                , CloseImageDynamic.Height
                );

            #endregion
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((tabControl1.SelectedTab != null) && (tabControl1.SelectedTab.Tag != null))
                (tabControl1.SelectedTab.Tag as Form).Select();

            if (tabControl1.SelectedIndex == -1) return;

            focusCloseButton();
        }

        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        { 

            if (closeButtonPosition.Contains(e.Location)
                && MessageBox.Show("Would you like to Close this Tab?"
                                , "Confirm"
                                , MessageBoxButtons.YesNo
                                , MessageBoxIcon.Question) == DialogResult.Yes
                )
            {
                (this.tabControl1.TabPages[tabControl1.SelectedIndex].Tag as Form).Dispose(); //dispose the Form
                this.tabControl1.TabPages.RemoveAt(tabControl1.SelectedIndex); //Remove the Tab

                //after closing a tab, bring focus to the last tab
                //The if statement prevents error when there is only a single tab left
                if (tabControl1.TabCount == 0)  return;
                
                tabControl1.SelectedTab = this.tabControl1.TabPages[tabControl1.TabCount - 1];    
                focusCloseButton();
                
            }
        }

        private void tabControl1_MouseLeave(object sender, EventArgs e)
        {
            CloseImageDynamic = Images.Default;
            tabControl1.Invalidate();
        }

        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseLeftDown = true;

            if (closeButtonPosition.Contains(e.Location))
                CloseImageDynamic = Images.Press;
            else
                CloseImageDynamic = Images.Default;

            tabControl1.Invalidate();
        }

        private void tabControl1_MouseMove(object sender, MouseEventArgs e)
        {
            //keep the press down image when moving
            if (isMouseLeftDown) return;

            //redraw to hover when moving onto the button
            if (closeButtonPosition.Contains(e.Location))
                if (CloseImageDynamic != Images.Hover)
                    CloseImageDynamic = Images.Hover;
                else return;

            //redraw to default when moving off the button
            else
                if (CloseImageDynamic != Images.Default)
                    CloseImageDynamic = Images.Default;
                else return;

            tabControl1.Invalidate(); 
        }

        private void tabControl1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseLeftDown = false;
        }
        #endregion

    }

    public static class Images
    {
        public static Bitmap Default = new Bitmap(Resources._default);
        public static Bitmap Hover = new Bitmap(Resources.hover);
        public static Bitmap Press = new Bitmap(Resources.press);
    }
}
