#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PokemonApp/PokemonApp.csproj", "PokemonApp/"]
RUN dotnet restore "PokemonApp/PokemonApp.csproj"
COPY . .
WORKDIR "/src/PokemonApp"
RUN dotnet build "PokemonApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PokemonApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PokemonApp.dll"]