# Etapa 1: Construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos el archivo de proyecto y restauramos dependencias
COPY *.sln .
COPY UsuariosAPI/*.csproj UsuariosAPI/
RUN dotnet restore

# Copiamos el resto del código y construimos
COPY . .
WORKDIR /src/UsuariosAPI
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
EXPOSE 8081
ENTRYPOINT ["dotnet", "UsuariosAPI.dll"]
