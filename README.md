<p id="readme-top" align="center">
  <img src="/images/logo.png" height="256">
  <h1  align="center">Green Tech Manager</h1>
</p>

Haven't you always dreamed of managing your own wind- or solarpark? Then this application is just perfect for you! With the Green Tech Manager, you can get any wind- or solarpark project up and running in no time!

## Table of Contents

- [Motivation](#motivation)
- [Architectur](#architectur)
- [Services](#services)
    - [Identity Service](#identityservice)
    - [Windpark Service](#windparkservice)
    - [Solarpark Service](#solarparkservice)
    - [Operator Service](#operatorservice)
- [Built with](#builtwith)
- [Getting started](#gettingstarted)
- [Disclaimer](#disclaimer)

## Motivation <a name="motivation"></a>

The young startup Green Tech Ltd. has the vision of realizing a software for the management of wind and solar parks. In this software it should be possible to record the basic information on individual wind turbines and photovoltaic arrays. In this GreenTechManager, the startup would also like to record the operators of the systems.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Architectur <a name="architectur"></a>

In order to decouple the individual functional units, the GreenTechManager is implemented using a microservice architecture. Each individual microservice can be addressed via a defined REST interface. Some of the advantages of this software architecture are the faster availability of new functions, a smaller code base of the individual services and better scalability.
<p align="center">
<img src="/images/architecture.png" height="400">
</p>
Data is stored according to the "share-nothing" or "share-as-little-as-possible" principle. This means each microservice keeps its data in its own database. The communication between the microservices is implemented using the choreography pattern. Information is exchanged asynchronously via a dedicated message broker. Since there is no point-to-point communication between the individual participants, this pattern helps to reduce the coupling between the microservices.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Services <a name="services"></a>

### Identity Service <a name="identityservice"></a>

### Windpark Service <a name="windparkservice"></a>

### Solarpark Service <a name="solarparkservice"></a>

### Operator Service <a name="operatorservice"></a>

### Auditing Service <a name="auditservice"></a>

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Built with <a name="builtwith"></a>

- [Visual Studio](https://visualstudio.microsoft.com/de/vs/community/)
- [Net Core](https://dotnet.microsoft.com/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [RabbitMQ](https://www.rabbitmq.com/)
- [Swagger](https://swagger.io/)
- [Docker](https://www.docker.com/)
- [Ocelot](https://github.com/ThreeMammals/Ocelot)
- [IdentityServer4](https://identityserver4.readthedocs.io/)

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Getting Started <a name="gettingstarted"></a>

## Disclaimer <a name="disclaimer"></a>
The scenario shown in this repository is a purely fictitious project and is intended to illustrate the various software components and aspects of a microservice architecture.

<p align="right">(<a href="#readme-top">back to top</a>)</p>