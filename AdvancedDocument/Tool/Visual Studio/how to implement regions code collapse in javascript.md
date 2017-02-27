[how to implement regions/code collapse in javascript](http://stackoverflow.com/questions/1921628/how-to-implement-regions-code-collapse-in-javascript)

## answer1

You have to use Visual Studio 2003/2005/2008 Macros.


Copy + Paste from Blog entry for fidelity sake:

    Open Macro Explorer
    Create a New Macro
    Name it OutlineRegions
    Click Edit macro and paste the following VB code:

```vb
Option Strict Off
Option Explicit Off

Imports System
Imports EnvDTE
Imports EnvDTE80
Imports System.Diagnostics
Imports System.Collections

Public Module JsMacros

    Sub OutlineRegions()
        Dim selection As EnvDTE.TextSelection = DTE.ActiveDocument.Selection

        Const REGION_START As String = "//#region"
        Const REGION_END As String = "//#endregion"

        selection.SelectAll()
        Dim text As String = selection.Text
        selection.StartOfDocument(True)

        Dim startIndex As Integer
        Dim endIndex As Integer
        Dim lastIndex As Integer = 0
        Dim startRegions As Stack = New Stack()

        Do
            startIndex = text.IndexOf(REGION_START, lastIndex)
            endIndex = text.IndexOf(REGION_END, lastIndex)

            If startIndex = -1 AndAlso endIndex = -1 Then
                Exit Do
            End If

            If startIndex <> -1 AndAlso startIndex < endIndex Then
                startRegions.Push(startIndex)
                lastIndex = startIndex + 1
            Else
                ' Outline region ...
                selection.MoveToLineAndOffset(CalcLineNumber(text, CInt(startRegions.Pop())), 1)
                selection.MoveToLineAndOffset(CalcLineNumber(text, endIndex) + 1, 1, True)
                selection.OutlineSection()

                lastIndex = endIndex + 1
            End If
        Loop

        selection.StartOfDocument()
    End Sub

    Private Function CalcLineNumber(ByVal text As String, ByVal index As Integer)
        Dim lineNumber As Integer = 1
        Dim i As Integer = 0

        While i < index
            If text.Chars(i) = vbCr Then
                lineNumber += 1
                i += 1
            End If

            i += 1
        End While

        Return lineNumber
    End Function

End Module

```

    Save the Macro and Close the Editor
    Now let's assign shortcut to the macro. Go to Tools->Options->Environment->Keyboard and search for your macro in "show commands containing" textbox
    now in textbox under the "Press shortcut keys" you can enter the desired shortcut. I use Ctrl+M+E. I don't know why - I just entered it first time and use it now :)

## answer2

[JScript Editor Extensions](https://marketplace.visualstudio.com/items?itemName=VisualStudioProductTeam.JScriptEditorExtensions)


## answer3

Thats easy!

Mark the section you want to collapse and,

```
Ctrl+M+H
```

And to expand use '+' mark on its left.
