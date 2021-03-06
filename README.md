# powerplant-coding-challenge

Calculate how much power each of a multitude of different [powerplants](https://en.wikipedia.org/wiki/Power_station) need 
to produce (a.k.a. the production-plan) when the [load](https://en.wikipedia.org/wiki/Load_profile) is given
and taking into account the cost of the underlying energy sources (gas,  kerosine) and the Pmin and Pmax of each powerplant.

## The ProductionPlan solution

### Acceptance criteria
- [x] contain a README.md explaining how to build and launch the API
- [x] expose the API on port `8888`
- [x] return a result where the sum of the power generated by each of the different powerplants is
  exactly equal to the load specified in the payload for at least the example payloads provided.

### Optionally
- [x] Docker
- [x] CO2
- [x] Websocket (Implementation based on [article](https://radu-matei.com/blog/aspnet-core-websockets-middleware/))

### Link
- http://localhost:8888/api/v1/productionplan

### Example

- Postman: [colletion](powerplant-coding-challenge.postman_collection.json)

### Images

<p align="center">
  <img src="img/img-03-evidencia.png?style=centerme" />
</p>
<p>
  <img src="img/img-04-evidencia.png?style=centerme" />
</p>

## Build & Run (REST API)

### Microsoft Visual Studio 2022

- Open **PowerplantChallenge.sln** into src folder;
- Select project **Powerplant.API**;
- Click on **Development** to **build and run** the solution;
  > The Solution starts with the *Powerplant.API profile* defined in the **launchSettings.json**.

### Command Line

- **build:** folder src; dotnet build PowerplantChallenge.sln
- **run 1:** folder src\Powerplant.API; dotnet run Powerplant.API.csproj
- **run 2:** folder src\Powerplant.API\bin\Debug\net6.0; dotnet Powerplant.API.dll
  > Set Enviroment before to run 2:
  > - PowerShell -----> $Env:ASPNETCORE_ENVIRONMENT = "Development"
  > - Command line -> setx ASPNETCORE_ENVIRONMENT "Development"

### Docker

- **create image:** docker build -t api-powerplant-image -f Dockerfile .
- **run container:** docker run -it --rm --name api-powerplant -p 8888:80 -v "D:\\_source\\Docker_File_Sharing:/app/Logs" api-powerplant-image
  > Param -v allows access files that exist on the host file system outside of the Docker container

<p align="center">
  <img src="img/img-02-docker.png?style=centerme" />
</p>

## Build & Run (Websocket Client)

To make it more realistic, run many instances.

### Microsoft Visual Studio 2022

- Open **PowerplantChallenge.sln** into src folder;
- Click right mouse on project **Powerplant.WebsocketClient** and build;
- Open folder **src\Powerplant.WebsocketClient\bin\Debug\net6.0**, double click in **Powerplant.WebsocketClient.exe**;

### Command Line

- **build:** folder src; dotnet build PowerplantChallenge.sln
- **run:** folder src\Powerplant.WebsocketClient\bin\Debug\net6.0, **Powerplant.WebsocketClient.exe**


## Swagger (Documentation)

- http://localhost:8888/swagger

<p align="center">
  <img src="img/img-01-swagger.png?style=centerme" />
</p>

  > Only available **environment *Development***