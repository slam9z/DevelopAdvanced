[Redirection Operator ](https://www.lifewire.com/redirection-operator-2625979)


Tim Fisher 
Updated October 29, 2015 
What is a Redirection Operator?
A redirection operator is a special character that can be used with a command, like a Command Prompt 
command or DOS command, to either redirect the input to the command or the output from the command.

By default, when you execute a command, the input comes from the keyboard and the output is sent to 
the Command Prompt window. Command inputs and outputs are called command handles.

The table below lists all of the available redirection operators for commands in Windows and MS-DOS.
 However, the > and >> redirection operators are, by a considerable margin, the most commonly used.
 See How To Redirect Command Output to a File for detailed instructions on using those operators.


Redirection Operators in Windows and MS-DOS

| Redirection Operator| Explanation| Example|
|-----|----|----|
| >| The greater-than sign is used to send to a file, or even a printer or other device, whatever information from the command would have been displayed in the Command Prompt window had you not used the operator.| assoc > types.txt|
| >>| The double greater-than sign works just like the single greater-than sign but the information is appended to the end of the file instead of overwriting it.| ipconfig >> netdata.txt|
| <| The less-than sign is used to read the input for a command from a file instead of from the keyboard.| sort < data.txt|
| \| | The vertical pipe is used to read the output from one command and use if for the input of another.| dir \| sort|

Note: Two other redirection operators, >& and <&, also exist but deal mostly with more complicated
 redirection involving command handles.

Tip: The clip command is worth mentioning here as well. It's not a redirection operator but it is 
intended to be used with one, usually the vertical pipe, to redirect the output of the command before 
the pipe to the Windows clipboard.

For example, executing ping 192.168.1.1 | clip will copy the results of the ping command to
 the clipboard, which you can then paste into any program.

