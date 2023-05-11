#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["$ext_safeprojectname$.Worker/$ext_safeprojectname$.Worker.csproj", "$ext_safeprojectname$.Worker/"]
RUN dotnet restore "$ext_safeprojectname$.Worker/$ext_safeprojectname$.Worker.csproj"
COPY . .
WORKDIR "/src/$ext_safeprojectname$.Worker"
RUN dotnet build "$ext_safeprojectname$.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "$ext_safeprojectname$.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "$ext_safeprojectname$.Worker.dll"]