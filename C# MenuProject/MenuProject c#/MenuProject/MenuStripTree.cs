using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Reflection;
using System.Linq;

namespace MenuProject
{
    static class MenuStripTree
    {

        #region menu hierarchy
        //MenuStrip Tree
        //impelemented interface becomes child item in menuStrip
        public interface IMenuBase : IMenuFiles, IMenuUser
        { }
        public interface IMenuFiles
        { }
        public interface IMenuUser : IMenuTesting, IMenuTesting1, IMenuTesting2
        { }
        public interface IMenuTesting : IMenuTesting0_1
        { }
        public interface IMenuTesting1
        { }
        public interface IMenuTesting2
        { }
        public interface IMenuTesting0_1
        { }

        #endregion


        public static void buildMenuStrip(MenuStrip mainMenuStrip)
    {

        Type treeBase = typeof(IMenuBase);

	    //Create menu item for each interface within MenuStripTree
        foreach (Type nestItem in immediateInterfaces(treeBase))
              {
                  mainMenuStrip.Items.Add(addInterfaceItem(nestItem));
	    }

    }


    private static ToolStripMenuItem addInterfaceItem(Type typeInterface)
    {
        //create menu item for interface in parameter 
        ToolStripMenuItem item = new ToolStripMenuItem();
        item.Name = typeInterface.Name;
        string replacement = "";
        Regex rgx = new Regex("^IMenu");
        item.Text = rgx.Replace(typeInterface.Name, replacement);

        //GetInterfaces() also returns parent's parent implementations
        //only need immediate implemented interfaces
        Type[] immediateInterfaces = typeInterface.GetInterfaces();
        if (immediateInterfaces.Count() != 0 || immediateInterfaces != null)
        {

            //make function for immediate interfaces
            var allInterfaces = typeof(IMenuBase).GetInterfaces();
            var MainItems = allInterfaces.Except
                            (allInterfaces.SelectMany(t => t.GetInterfaces()));

            foreach (Type relatedInterface in typeInterface.GetInterfaces())

                item.DropDownItems.Add(addInterfaceItem(relatedInterface));
        } 
            return item;
    }


    private static void addFormItem(ToolStripMenuItem itemInterface)
    {
	    //get interface type of parameter
	    Type typeInterface = Type.GetType("TabbedChildForms.MenuStripTree+" + itemInterface.Name);

	    //Collect All childForms that implement parameter 
	    dynamic matchedForms = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x => typeInterface.IsAssignableFrom(x)).Where(y => y.IsSubclassOf(typeof(ChildForms._childForm)));


	    foreach (var matchingForm in matchedForms) {
		    //Create menu item for each Form and add it to parameter DropDown
		    ToolStripMenuItem item = new ToolStripMenuItem();
		    item.Name = matchingForm.Name;
		    item.Text = matchingForm.Name;
		    itemInterface.DropDownItems.Add(item);

            //item.Click += childFormToolStripMenuItem_Click;
	    }

     }

    private static Type[] immediateInterfaces(object targetType)
    { 
        Type targetInterface = targetType.GetType();
        Type[] baseInterfaces = targetInterface.GetInterfaces();
        Type[] MainItems = baseInterfaces.Except
                        (baseInterfaces.SelectMany(t => t.GetInterfaces())).ToArray();
        return MainItems;
    }
    //private void childFormToolStripMenuItem_Click(object sender, EventArgs e)
    //{
    //    object cFormType = new object();
    //    cFormType = Activator.CreateInstance(Type.GetType("TabbedChildForms." + sender.Name));
    //    cFormType.MdiParent = TabbedChildForms.Form1;
    //    cFormType.Show();
    //}


    }
}