#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_ENVIRONMENT=Production

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["src/Powerplant.API/Powerplant.API.csproj", "Powerplant.API/"]
COPY ["src/Powerplant.Infra.WebsocketManager/Powerplant.Infra.WebsocketManager.csproj", "Powerplant.Infra.WebsocketManager/"]
COPY ["src/Powerplant.Infra.Mock/Powerplant.Infra.Mock.csproj", "Powerplant.Infra.Mock/"]
COPY ["src/Powerplant.Core.Domain/Powerplant.Core.Domain.csproj", "Powerplant.Core.Domain/"]
COPY ["src/Powerplant.Infra.CrossCutting/Powerplant.Infra.CrossCutting.csproj", "Powerplant.Infra.CrossCutting/"]
COPY ["src/Powerplant.Core.Service/Powerplant.Core.Service.csproj", "Powerplant.Core.Service/"]
COPY ["src/Powerplant.Infra.Data/Powerplant.Infra.Data.csproj", "Powerplant.Infra.Data/"]
COPY ["src/Powerplant.Infra.DependencyInjection/Powerplant.Infra.DependencyInjection.csproj", "Powerplant.Infra.DependencyInjection/"]

RUN dotnet restore "Powerplant.API/Powerplant.API.csproj"
COPY . .
WORKDIR "/src/src/Powerplant.API"
RUN dotnet build "Powerplant.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Powerplant.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Powerplant.API.dll"]