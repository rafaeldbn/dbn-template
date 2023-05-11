#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0.4-focal-amd64 AS base
RUN apt update && apt install --allow-unauthenticated -y tzdata locales curl libgdiplus libc6-dev ca-certificates
ENV TIME_ZONE="America/Sao_Paulo"
ENV TZ="America/Sao_Paulo"
ENV LANG="pt_BR.UTF-8"
ENV LANGUAGE="pt_BR:pt"
ENV LC_ALL="pt_BR.UTF-8"
RUN sed -i -e 's/# pt_BR.UTF-8 UTF-8/pt_BR.UTF-8 UTF-8/' /etc/locale.gen
RUN dpkg-reconfigure --frontend=noninteractive locales
RUN locale-gen
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /$ext_safeprojectname$
COPY ["$ext_safeprojectname$/$ext_safeprojectname$.Web/$ext_safeprojectname$.Web.csproj", "src/$ext_safeprojectname$.Web/"]
COPY ["$ext_safeprojectname$/$ext_safeprojectname$.Infrastructure/$ext_safeprojectname$.Infrastructure.csproj", "src/$ext_safeprojectname$.Infrastructure/"]
COPY ["$ext_safeprojectname$/$ext_safeprojectname$.SharedKernel/$ext_safeprojectname$.SharedKernel.csproj", "src/$ext_safeprojectname$.SharedKernel/"]
COPY ["$ext_safeprojectname$/$ext_safeprojectname$.Core/$ext_safeprojectname$.Core.csproj", "src/$ext_safeprojectname$.Core/"]
RUN dotnet restore "src/$ext_safeprojectname$.Web/$ext_safeprojectname$.Web.csproj"
COPY . .
WORKDIR "/$ext_safeprojectname$/$ext_safeprojectname$/$ext_safeprojectname$.Web"
RUN dotnet build "$ext_safeprojectname$.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "$ext_safeprojectname$.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "$ext_safeprojectname$.Web.dll"]