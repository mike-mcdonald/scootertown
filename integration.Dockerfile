FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY scootertown.integration/*.csproj ./scootertown.integration/
COPY scootertown.infrastructure/*.csproj ./scootertown.infrastructure/
COPY scootertown.data/*.csproj ./scootertown.data/
COPY scootertown.api/*.csproj ./scootertown.api/
WORKDIR /app/scootertown.integration
RUN dotnet restore

# copy everything else and build app
WORKDIR /app
COPY scootertown.integration/. ./scootertown.integration/
COPY scootertown.infrastructure/. ./scootertown.infrastructure
COPY scootertown.data/. ./scootertown.data/
COPY scootertown.api/. ./scootertown.api/
ADD ./appsettings.json .
ADD ./neighborhoods.json .
ADD ./pattern_areas.json .
WORKDIR /app/scootertown.integration
RUN dotnet publish -c Release -o out


FROM microsoft/dotnet:2.1-runtime AS runtime
WORKDIR /app
ENV TZ=America/Los_Angeles
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
COPY --from=build /app/scootertown.integration/out ./
ENTRYPOINT ["dotnet", "PDX.PBOT.Scootertown.Integration.dll"]
