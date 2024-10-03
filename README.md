# Overview

Small ASP.NET application created as per instructions for a job interview.

It allows users to create posts and add comments to those posts.

I had planned to implement authentication and authorization using firebase, because we used it in
a previous project. However integrating the firebase API has no out of the box .NET integration
so I elected to not implement authorization. If I had known that this would be an issue when I
created the project I would have generated the project using the ASP template which includes auth
out of the box.

- [] Implement NUnit
- [] Implement mock adapter for postgres
- [] Create unit tests to verify functionality of HTTP methods.

# Getting started.

## Dotnet and EF

Set up a dotnet environment for ASP and install the EntityFramwork command line.

On windows dotnet and entity framework can be installed using the VisualStudio installer.

The EntityFramework command line can be installed using the following command.

```
dotnet tool install --global dotnet-ef
```

## Docker

This project relies on Docker and docker compose. Visit [docker.com](https://www.docker.com/) and
install docker desktop to get a docker environment running on windows. Then, to run the application
use the following command

```
docker-compose -f .\docker-compose.database.yml up
```

The other docker compose file is for the visual studio debugger to launch the application in a
docker container.

## Setting up the database

After the docker containers are running you can initialize the database with the following command.

```
dotnet ef database update
```

## Running the application

The application can be run from Visual Studio using the play button, or from the command line
with the following command.

```
dotnet run
```

It can also be attached to the debugger in vs code using the built in configuration in the
launch.json file.

## Running Scripts

This application contains several CS scripts that can be used to generate mock data for the database.
They can be run with the `--script` argument, followed by the name of the script.

```
dotnet run --script SeedUsers
```

A list of all scripts can be found in the scripts.cs file [here](./UserCommentApplication/Scripting/Scripts.cs).
