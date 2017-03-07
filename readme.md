DogetChat 
=====================

The DogetChat application is built and run using the [.NET Core Runtime Docker image](https://hub.docker.com/r/microsoft/dotnet/). It's a great option for creating a Docker image for production.

The instructions assume that you already have [.NET Core 1.0 RC4](https://github.com/dotnet/core/blob/master/release-notes/rc4-download.md) and [Git](https://git-scm.com/downloads) and [Docker](https://www.docker.com/products/docker) clients installed. They also assume you already know how to target Linux or Windows containers. Do try both image types. You need the latest Windows 10 or Windows Server 2016 to use [Windows containers](http://aka.ms/windowscontainers).

Instructions
------------

First, prepare your environment by cloning the repository and navigating to the sample:

```console
git clone https://github.com/nickgermyn/net-dogte-chat
cd net-dogte-chat
```

Follow these steps to run the sample locally:

```console
dotnet restore
dotnet run
```

Follow these steps to run this sample in a Linux container:

```console
dotnet restore
dotnet publish DogteChat\DogteChat.csproj -c Release -o ..\out
docker build -t net-dogte-chat .
docker run net-dogte-chat
```