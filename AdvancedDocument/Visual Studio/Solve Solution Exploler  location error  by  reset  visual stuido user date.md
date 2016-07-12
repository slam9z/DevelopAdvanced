[Visual Studio solution explorer doesn't work](https://social.msdn.microsoft.com/Forums/vstudio/en-US/24887c32-5a3c-4e76-9b98-60903897af03/visual-studio-solution-explorer-doesnt-work?forum=vseditor )    



Thanks for your post.

I suggest to try

devenv /resetuserdata

devenv /resetallsettings

and then ran VS in safemode

devenv /safemode

If it doesn't help, be free to let me know.


好久没还原过数据了，都忘记还有这种方法，比重装好N倍。
