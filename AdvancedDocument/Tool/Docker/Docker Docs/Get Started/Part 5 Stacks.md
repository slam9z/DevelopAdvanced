[Part 5: Stacks](https://docs.docker.com/get-started/part5/)

## Introduction

In part 4, you learned how to set up a swarm, which is a cluster of machines running Docker, and deployed an application to it, with containers running in concert on multiple machines.

Here in part 5, you’ll reach the top of the hierarchy of distributed applications: the stack. A stack is a group of interrelated services that share dependencies, and can be orchestrated and scaled together. A single stack is capable of defining and coordinating the functionality of an entire application (though very complex applications may want to use multiple stacks).

Some good news is, you have technically been working with stacks since part 3, when you created a Compose file and used docker stack deploy. But that was a single service stack running on a single host, which is not usually what takes place in production. Here, you’re going to take what you’ve learned and make multiple services relate to each other, and run them on 


## multiple machines.

This is the home stretch, so congratulate yourself!
Adding a new service and redeploying.