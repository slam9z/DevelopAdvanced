Exporler ContextMenu  Edit with  Notepad++

##ContextMenuHandlersHandlers

I Actuall have 2 Entries in there:
ANotepad++64:
[HKEY_CLASSES_ROOT*\shellex\ContextMenuHandlersHandlers\ANotepad++64]
@="{B298D29A-A6ED-11DE-BA8C-A68E55D89593}"
and Notepad++64:
[HKEY_CLASSES_ROOT*\shellex\ContextMenuHandlers\Notepad++64]
@="{B298D29A-A6ED-11DE-BA8C-A68E55D89593}"
But I don't have 2 "Edit with notepad++ entries" (which makes sense since it's the same GUID).
Another little thing. My system language is german but the Text still says "Edit with Notpead++" I honestly
can't say if this text was ever localized but I thought I should mention it.

##CLSID

Hi Robert (and everybody who can reproduce the problem),
can you find that same GUID in:
HKEY_CLASSES_ROOT\CLSID{B298D29A-A6ED-11DE-BA8C-A68E55D89593}
And check that it has this content:
Windows Registry Editor Version 5.00
[HKEY_CLASSES_ROOT\CLSID{B298D29A-A6ED-11DE-BA8C-A68E55D89593}\Settings]
"Title"="Edit with &Notepad++"
"Path"="C:\Program Files (x86)\Notepad++\notepad++.exe"
"Custom"=""
"ShowIcon"=dword:00000001
"Dynamic"=dword:00000001
"Maxtext"=dword:00000019
BR
Loreia
P.S.
32-bit GUID is:
{00F3C2EC-A6EE-11DE-A03A-EF8F55D89593}