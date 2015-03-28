# MenuProject
An MDI Interface using Tabs with Close Buttons<br />
![alt tag](https://raw.githubusercontent.com/VictorAceChen/MenuProject/master/SampleImage.png)

# References

For interaction between TabControl MDI Child Forms:<br />
[Tabbed MDI Child Forms](http://www.codeproject.com/Articles/17640/Tabbed-MDI-Child-Forms)

For drawing close buttons on tabs:<br />
[IMPLEMENTING CLOSE BUTTON IN TAB PAGES](http://www.dotnetthoughts.net/implementing-close-button-in-tab-pages/)

# Changes from Reference Code:

In the original example from "Tabbed MDI Child Forms", MDI child forms had thier window flicker during tab selection. Most likely this was due to "FormWindowState.Maximized" being called in "Form1_MdiChildActivate". I took out that line of code and had all child forms derive an intermediary form class "_childForm". There they will inherit a load method where "FormWindowState.Maximized" is called and ControlBox (minimize, maximize and close).

I didn't want to add menu items (through the Visual Studio Designer) each time a new MDI child form was introduced. Now they are generated for each "_childForm"(that exist within the assembly) and for the interfaces represented in an implementation tree in "menuStripFormation.cs". As long as a "_childForm" implements an interface within said tree, It will have a menu item made. Click events for menu items (derived from "_childForm") are generated within MenuItemForChildForm.

For "IMPLEMENTING CLOSE BUTTON IN TAB PAGES", I replaced the 'x' that represented the close button with an image. And instead of using a "for loop" to register a click event, I based it on "tabControl.SelectedIndex". To give it a button effect, I had the image redrawn from mouse events within the tabcontrol.<br /> 
