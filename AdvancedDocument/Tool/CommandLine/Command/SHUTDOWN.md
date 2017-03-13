[SHUTDOWN](https://ss64.com/nt/shutdown.html)

## 常用用法

```
shutdown  /s  /t 7200 
```
两个小时后关机

```
shutdown /a
```

取消关机计划

昨天竟然写错了，很久没用了。


## 详细


SHUTDOWN.exe (for Terminal Services use: TsShutDn)

Shutdown the computer

Syntax
      SHUTDOWN [logoff_option]  [/m \\Computer] [options]

logoff_options:
    /i         Display the GUI (must be the first option)
    /l         Log off. This cannot be used with /m or /d option.
    /s         Shutdown.
    /r         Shutdown and Restart.
    /g         Shutdown and Restart, after restarting restart any registered applications.
    /a         Abort a system shutdown during the time-out period.
    /p         Turn off the local computer with no time-out or warning
               (only with /d)
    /h         Hibernate the local computer (can be used with /f )
    /e         Document the reason for an unexpected shutdown of a computer.

Options:

   /m \\Computer  A remote computer to shutdown.

   /t xxx         Time until system shutdown in seconds. 
                  The valid range is xxx=0-600 seconds. [default=30]

   /c "Msg"       An optional shutdown message [Max 127 chars]

   /f             Force running applications to close.
                  This will not prompt for File-Save in any open applications.
                  so will result in a loss of any unsaved data.

   /d u:xx:yy     List a USER (unplanned) reason code for the shutdown. 
   /d P:xx:yy     List a PLANNED reason code for the shutdown.
                    xx = The Major reason code (0-255)
                    yy = The Minor reason code (0-65536)

In Windows 7, the maximum /t timeout increased from 600 seconds to 315,360,000 seconds (10 years)
If you need to FORCE a shutdown, or target multiple computers at once; use PsShutdown instead. 

