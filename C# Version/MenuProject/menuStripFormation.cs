using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace MenuProject
{
    static class menuStripFormation
    {

        #region menu hierarchy
        //MenuStrip Tree
        //Each impelemented interface becomes child item in menuStrip
        public interface IMenuBase : IMenuFile, IMenuUser, IMenuTesting
        { }
        public interface IMenuFile
        { }
        public interface IMenuUser
        { }
        public interface IMenuTesting : IMenuTesting1
        { }
        public interface IMenuTesting1 : IMenuTesting2
        { }
        public interface IMenuTesting2
        { }
        #endregion


        public static void buildMenuStrip(mainForm mainForm)
        {
            List<MenuItemForChildForm> itemFormList = menuItemsForChildForm(mainForm);

            //Create menu item for each interface within MenuStripTree
            foreach (Type item in immediateInterfaces(typeof(IMenuBase)))
            {
                mainForm.MainMenuStrip.Items.
                    Add(menuItemForInterface(item, itemFormList));
            }
        }

        //Get the types of all non-inherited interfaces
        private static Type[] immediateInterfaces(Type targetType)
        {
            Type[] baseInterfaces = targetType.GetInterfaces();
            
            return baseInterfaces
                    .Except(baseInterfaces.SelectMany(t => t.GetInterfaces()))
                    .ToArray();
        }


        private static ToolStripMenuItem menuItemForInterface(Type typeInterface, List<MenuItemForChildForm> FormItemList)
        {
            //create menu item for interface in parameter 
            ToolStripMenuItem item = new ToolStripMenuItem();
            item.Name = typeInterface.Name;
            string replacement = "";
            Regex rgx = new Regex("^IMenu");
            item.Text = rgx.Replace(typeInterface.Name, replacement);

            Type[] ArrImmediateInterfaces = immediateInterfaces(typeInterface);

            //Recursively add related interfaces to DropDownItems
            if (ArrImmediateInterfaces.Count() > 0 || ArrImmediateInterfaces != null)
            {
                foreach (Type relatedInterface in ArrImmediateInterfaces)
                    item.DropDownItems.Add(menuItemForInterface(relatedInterface, FormItemList));
            }

            //Attach items from FormItemList to drop down of current item
            foreach (MenuItemForChildForm childFormitem in FormItemList)
            {
                Type[] childFormInterfaces = immediateInterfaces(childFormitem.childForm);
                if (Array.Exists(childFormInterfaces, element => element == typeInterface))
                    item.DropDownItems.Add(childFormitem);
            }
            return item;
        }

        private static List<MenuItemForChildForm> menuItemsForChildForm(mainForm inputMainForm)
        {
            List<MenuItemForChildForm> FormItemList = new List<MenuItemForChildForm>();

            //Collect all classes that derive from _ChildClasses
            dynamic childForms =
                AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).
                Where(y => y.IsSubclassOf(typeof(ChildForms._ChildForm)));

            foreach (var child in childForms)
            {
                //Create menu item for each Form and add it to parameter DropDown
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = child.Name;

                FormItemList.Add(new MenuItemForChildForm(inputMainForm, child));

            }
            return FormItemList;
        }

    }
}