# Imagen base de .NET SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copiar archivos y restaurar dependencias
COPY . ./
RUN dotnet restore

# Compilar el proyecto
RUN dotnet publish -c Release -o out

# Imagen base para ejecutar la app
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app

# Copiar archivos compilados
COPY --from=build /app/out .

# Exponer el puerto interno
EXPOSE 80

# Comando de inicio
ENTRYPOINT ["dotnet", "CompraGamer.Api.dll"]