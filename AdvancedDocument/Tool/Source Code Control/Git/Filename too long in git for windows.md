[Filename too long in git for windows](http://stackoverflow.com/questions/22575662/filename-too-long-in-git-for-windows)


This might help :

```
git config core.longpaths true
```

Basic Explanation : This answer suggest not to have such setting applied to global system (to all projects so avoiding --system or --global tag) configurations. This command only solve the problem by being specific to the current project.




> The specified path, file name, or both are too long. The fully qualified file name must be less than 260 characters, and the directory name must be less than 248 characters.