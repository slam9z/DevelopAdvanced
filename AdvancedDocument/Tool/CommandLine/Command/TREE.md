[TREE](http://ss64.com/nt/tree.html)

TREE.com
Display the folder structure of a drive or path as a graphical tree.
Syntax
      TREE [drive:][path] [/F] [/A]
Key
   [drive:][path]  The startng directory for the tree listing.

   /F   Display the names of the files in each folder.

   /A   Use ASCII instead of extended characters.
Like the DIR command, the first two lines of output from Tree are the Volume Label and Serial Number, in Windows 10 the Serial number is prefixed with the Device ID.
Examples
tree "C:\program files"
tree /a "C:\program files" > c:\demo\treelist.txt
“The significance of the cherry blossom tree in Japanese culture goes back hundreds of years. In their country, the cherry blossom represents the fragility and the beauty of life. It's a reminder that life is almost overwhelmingly beautiful but that it is also tragically short” ~Homaro Cantu

Related:

DIR - Display a list of files and folders
VOL - Display a disk label
