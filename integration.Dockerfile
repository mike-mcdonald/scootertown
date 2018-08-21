FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY scootertown.integration/*.csproj ./scootertown.integration/
COPY scootertown.data/*.csproj ./scootertown.data/
WORKDIR /app/scootertown.integration
RUN dotnet restore

# copy everything else and build app
WORKDIR /app
COPY scootertown.integration/. ./scootertown.integration/
COPY scootertown.data/. ./scootertown.data/
WORKDIR /app/scootertown.integration
RUN dotnet publish -c Release -o out


FROM microsoft/dotnet:2.1-runtime AS runtime
WORKDIR /app
COPY --from=build /app/scootertown.integration/out ./
ENTRYPOINT ["dotnet", "PDX.PBOT.Scootertown.Integration.dll"]
