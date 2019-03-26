FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["GeoIPMicroservice/GeoIPMicroservice.csproj", "GeoIPMicroservice/"]
RUN dotnet restore "GeoIPMicroservice/GeoIPMicroservice.csproj"
COPY . .
WORKDIR "/src/GeoIPMicroservice"
RUN dotnet build "GeoIPMicroservice.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "GeoIPMicroservice.csproj" -c Release -o /app

FROM base AS final
RUN apt-get update \
    && apt-get install -y wget
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "GeoIPMicroservice.dll"]