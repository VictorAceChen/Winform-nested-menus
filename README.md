# MenuProject
MDI Interface using Tabs with Close Buttons

Purpose?


# References
[Tabbed MDI Child Forms](http://www.codeproject.com/Articles/17640/Tabbed-MDI-Child-Forms)

[IMPLEMENTING CLOSE BUTTON IN TAB PAGES](http://www.dotnetthoughts.net/implementing-close-button-in-tab-pages/)

Changes I made:

In the original example, MDI child forms had there window flicker during tab selection. Think this was due to FormWindowState.Maximized existing in Form1_MdiChildActivate. Child forms were being maximized after being selected. 

I took out that line of code and had all child forms derive an intermediary form class "_childForm".
There they will inerit the load method where 


NEED TO PUSH UP IMAGE EXAMPLE 



# changes from those ideas.

# additions
