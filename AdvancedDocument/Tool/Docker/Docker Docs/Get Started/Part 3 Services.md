[Part 3: Services](https://docs.docker.com/get-started/part3/)

## Introduction

In part 3, we scale our application and enable load-balancing. To do this, we must go one level up in the hierarchy of a distributed application: the service.


* Stack

* Services (you are here)

* Container (covered in part 2)


## Understanding services

In a distributed application, different pieces of the app are called “services.” For example, if you imagine a video sharing site, there will probably be a service for storing application data in a database, a service for video transcoding in the background after a user uploads something, a service for the front-end, and so on.

A service really just means, “containers in production.” A service only runs one image, but it codifies the way that image runs – what ports it should use, how many replicas of the container should run so the service has the capacity it needs, and so on. Scaling a service changes the number of container instances running that piece of software, assigning more computing resources to the service in the process.

Luckily it’s very easy to define, run, and scale services with the Docker platform – just write a `docker-compose.yml` file.


## Your first docker-compose.yml File

A docker-compose.yml file is a YAML file that defines how Docker containers should behave in production.
docker-compose.yml

Save this file as docker-compose.yml wherever you want. Be sure you have pushed the image you created in Part 2 to a registry, and use that info to replace username/repo:tag:

```yml
version: "3"
services:
  web:
    image: username/repository:tag
    deploy:
      replicas: 5
      resources:
        limits:
          cpus: "0.1"
          memory: 50M
      restart_policy:
        condition: on-failure
    ports:
      - "80:80"
    networks:
      - webnet
networks:
  webnet:
```


## Recap and cheat sheet (optional)

Here’s a terminal recording of what was covered on this page:

To recap, while typing docker run is simple enough, the true implementation of a container in production is running it as a service. Services codify a container’s behavior in a Compose file, and this file can be used to scale, limit, and redeploy our app. Changes to the service can be applied in place, as it runs, using the same command that launched the service: docker stack deploy.

Some commands to explore at this stage:

```
docker stack ls              # List all running applications on this Docker host
docker stack deploy -c <composefile> <appname>  # Run the specified Compose file
docker stack services <appname>       # List the services associated with an app
docker stack ps <appname>   # List the running containers associated with an app
docker stack rm <appname>  
```