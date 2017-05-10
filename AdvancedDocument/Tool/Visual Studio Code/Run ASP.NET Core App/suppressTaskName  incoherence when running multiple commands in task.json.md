["suppressTaskName" incoherence when running multiple commands in task.json](https://github.com/Microsoft/vscode/issues/22099)

Current Behavior:

If suppressTaskName is true(default), the command line is `command args`
If suppressTaskName is false, it is `command taskname args`


竟然与version有关。

"version": "0.1.0 "->"version": "2.0.0"

version 0.1.0
If suppressTaskName is true(default), the command line is `command- args`
