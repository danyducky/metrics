FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN apt-get update
RUN apt-get install -y curl
RUN apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx
RUN curl -sL https://deb.nodesource.com/setup_14.x | bash -
RUN apt-get install -y nodejs

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
RUN apt-get update
RUN apt-get install -y curl
RUN apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx
RUN curl -sL https://deb.nodesource.com/setup_14.x | bash -
RUN apt-get install -y nodejs
WORKDIR /src
COPY ["Metrics.Api/Metrics.Api.csproj", "Metrics.Api/"]
COPY ["Metrics.DataLayer/Metrics.DataLayer.csproj", "Metrics.DataLayer/"]
COPY ["Startup/Startup.csproj", "Startup/"]
RUN dotnet restore "Metrics.Api/Metrics.Api.csproj"
COPY . .
WORKDIR "/src/Metrics.Api"
RUN dotnet build "Metrics.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Metrics.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Metrics.Api.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Metrics.Api.dll