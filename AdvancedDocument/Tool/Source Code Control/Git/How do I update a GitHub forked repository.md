[How do I update a GitHub forked repository?](http://stackoverflow.com/questions/7244321/how-do-i-update-a-github-forked-repository)

##answer

Starting in May 2014, it is possible to update a fork directly from GitHub. This still works as of October 2016, B
UT it will lead to a dirty commit history.

1. Open your fork on GitHub.
1. Click on `Pull Requests`.
1. Click on New Pull Request. By default, GitHub will compare the original with your fork, and there shouldn't be 
anything to compare if you didn't make any changes.
1. Click switching the base if you see that link. Otherwise, manually set the base fork drop down to your fork, 
and the head fork to the upstream. Now GitHub will compare your fork with the original, and you should see all the latest changes. 
1. Create pull request and assign a predictable name to your pull request (e.g., Update from original).
1. Scroll down to Merge pull request, but don't click anything yet.