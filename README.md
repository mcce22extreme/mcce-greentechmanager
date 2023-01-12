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
Provides user authentication and authorization functions. An access token can be obtained from this service via the login operation:
```
curl -X 'POST' \
  'http://localhost/identity/api/v1/Auth/login' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "clientId": "greentechclient",
  "userName": "operatoradmin",
  "password": "******************"
}'
```
This AccessToken is required for all other service calls of the application. Access to the various services is secured using a role system. Writing and deleting operations may only be performed by administrators. The following preconfigured administrator users are available:

- operatoradmin
- windparkadmin
- solarparkadmin

Reading persons may also be carried out by normal users. The following preconfigured user accounts are available:

- operatoruser
- windparkuser
- solarparkuser

A user's scope is included within a generated access token. Therefore a new access token is required for a scope change.

The operations of this services can be tested via swagger under http://localhost/identity/swagger/index.html.

### Windpark Service <a name="windparkservice"></a>
This service encapsulates functions to manage windparks. For each operation call, an access token with a suitable scope is required, which must be transferred as a bearer authentication header.

Retrieve a list of windparks:

```
curl -X 'GET' \
  'http://localhost/windparks/windparks/api/v1/Windpark' \
  -H 'accept: text/plain' \
  -H 'Authorization: Bearer {AccessToken}'
```
Create a new windpark:
```
curl -X 'POST' \
  'http://localhost/windparks/windparks/api/v1/windpark' \
  -H 'accept: text/plain' \
  -H 'Authorization: Bearer {AccessToken}' \
  -H 'Content-Type: application/json' \
  -d '{
  "name": "New windpark",
  "operatorId": 1,
  "numberOfTurbines": 5,
  "maxPowerOutput": 100,
  "startOfOperation": "2014-01-01T00:00:00",
  "location": "47° 49′ 48″ N, 17° 1′ 20″ O"
}'
```
Before adding a new windpark, be sure that the references operator exists. The operation will fail, if the referenced operator does not exist.

Delete a windpark:
```
curl -X 'DELETE' \
  'http://localhost/windparks/windparks/api/v1/WindPark/2' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer {AccessToken}'
```

The operations of this services can be tested via swagger under http://localhost/windparks/swagger/index.html.

### Solarpark Service <a name="solarparkservice"></a>

This service encapsulates functions to manage solarparks. For each operation call, an access token with a suitable scope is required, which must be transferred as a bearer authentication header.

Retrieve a list of solarparks:

```
curl -X 'GET' \
  'http://localhost/solarparks/api/v1/SolarPark' \
  -H 'accept: text/plain' \
  -H 'Authorization: Bearer {AccessToken}'
```

Create a new solarpark:

```
curl -X 'POST' \
  'http://localhost/solarparks/api/v1/SolarPark' \
  -H 'accept: text/plain' \
  -H 'Authorization: Bearer {AccessToken}' \
  -H 'Content-Type: application/json' \
  -d '{
  "name": "New solarpark",
  "operatorId": 1,
  "startOfOperation": "2023-01-03T11:13:19.811Z",
  "location": "49° 49′ 48″ N, 17° 1′ 20″ O"
}'
```

Before adding a new solarpark, be sure that the references operator exists. The operation will fail, if the referenced operator does not exist.

Delete an existing solarpark:

```
curl -X 'DELETE' \
  'http://localhost/solarparks/api/v1/SolarPark/2' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer {AccessToken}'
```

### Operator Service <a name="operatorservice"></a>

This service encapsulates functions to manage operators of solar- and windparks. For each operation call, an access token with a suitable scope is required, which must be transferred as a bearer authentication header.

Retrieve a list of operators:

```
curl -X 'GET' \
  'http://localhost/operators/api/v1/Operator' \
  -H 'accept: text/plain' \
  -H 'Authorization: Bearer {AccessToken}'
```

Create a new operator:

```
curl -X 'POST' \
  'http://localhost/operators/api/v1/Operator' \
  -H 'accept: text/plain' \
  -H 'Authorization: Bearer {AccessToken}' \
  -H 'Content-Type: application/json' \
  -d '{
  "name": "New operator",
  "address": "demo address",
  "city": "demo city",
  "zip": 1237,
  "country": "demp country"
}'
```

Delete an existing operator:

```
curl -X 'DELETE' \
  'http://localhost/operators/api/v1/Operator/2' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer {AccessToken}'
```

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
- [Nginx](https://www.nginx.com/)

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Getting Started <a name="gettingstarted"></a>

1. Install and execute docker desktop
2. Checkout the repository
3. Navigate to the root folder
4. Execute docker compose up

## Disclaimer <a name="disclaimer"></a>
The scenario shown in this repository is a purely fictitious project and is intended to illustrate the various software components and aspects of a microservice architecture.

<p align="right">(<a href="#readme-top">back to top</a>)</p>