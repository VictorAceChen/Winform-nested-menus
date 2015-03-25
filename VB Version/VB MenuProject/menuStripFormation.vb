Imports System.Text.RegularExpressions
Imports System.Reflection

Namespace MenuProject
    Public Module menuStripFormation

#Region "menu hierarchy"
        'MenuStrip Tree
        'Each impelemented interface becomes child item in menuStrip
        Public Interface IMenuBase
            Inherits IMenuFile, IMenuUser, IMenuTesting
        End Interface
        Public Interface IMenuFile
        End Interface
        Public Interface IMenuUser
        End Interface
        Public Interface IMenuTesting
            Inherits IMenuTesting1
        End Interface
        Public Interface IMenuTesting1
            Inherits IMenuTesting2
        End Interface
        Public Interface IMenuTesting2
        End Interface
#End Region


        Public Sub buildMenuStrip(ByVal mainForm As MenuProject.mainForm)
            Dim itemFormList As List(Of MenuItemForChildForm) = MenuItemsForChildForm(mainForm)

            'Create menu item for each interface within MenuStripTree
            For Each item As Type In immediateInterfaces(GetType(IMenuBase))
                mainForm.MainMenuStrip.Items.Add(menuItemForInterface(item, itemFormList))
            Next
        End Sub

        'Get the types of all non-inherited interfaces
        Private Function immediateInterfaces(ByVal targetType As Type) As Type()
            Dim baseInterfaces As Type() = targetType.GetInterfaces()

            Return baseInterfaces.Except(baseInterfaces.SelectMany(Function(t) t.GetInterfaces())).ToArray()
        End Function

        Private Function menuItemForInterface(ByVal typeInterface As Type, ByVal FormItemList As List(Of MenuItemForChildForm)) As ToolStripMenuItem
            'create menu item for interface in parameter 
            Dim item As New ToolStripMenuItem()
            item.Name = typeInterface.Name
            Dim replacement As String = ""
            Dim rgx As New Regex("^IMenu")
            item.Text = rgx.Replace(typeInterface.Name, replacement)

            Dim ArrImmediateInterfaces As Type() = immediateInterfaces(typeInterface)

            'Recursively add related interfaces to DropDownItems
            If ArrImmediateInterfaces.Count() > 0 OrElse ArrImmediateInterfaces IsNot Nothing Then
                For Each relatedInterface As Type In ArrImmediateInterfaces
                    item.DropDownItems.Add(menuItemForInterface(relatedInterface, FormItemList))
                Next
            End If

            'Attach items from FormItemList to drop down of current item
            For Each childFormitem As MenuItemForChildForm In FormItemList
                Dim childFormInterfaces As Type() = immediateInterfaces(childFormitem.childForm)
                If Array.Exists(childFormInterfaces, Function(element) element = typeInterface) Then
                    item.DropDownItems.Add(childFormitem)
                End If
            Next
            Return item
        End Function


        Private Function MenuItemsForChildForm(ByVal inputMainForm As mainForm) As List(Of MenuItemForChildForm)
            Dim FormItemList As New List(Of MenuItemForChildForm)()

            'Collect all classes that derive from _ChildClasses

            Dim childForms = AppDomain.CurrentDomain.GetAssemblies().SelectMany(Function(x) x.GetTypes()).Where(Function(y) y.IsSubclassOf(GetType(_childForm)))

            For Each child In childForms
                'Create menu item for each Form and add it to parameter DropDown
                Dim item As New ToolStripMenuItem()
                item.Text = child.Name

                FormItemList.Add(New MenuItemForChildForm(inputMainForm, child))
            Next
            Return FormItemList
        End Function




    End Module
End Namespace



