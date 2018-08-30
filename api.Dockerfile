FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY scootertown.api/*.csproj ./scootertown.api/
COPY scootertown.data/*.csproj ./scootertown.data/
WORKDIR /app/scootertown.api
RUN dotnet restore

# copy everything else and build app
WORKDIR /app
COPY scootertown.api/. ./scootertown.api/
COPY scootertown.data/. ./scootertown.data/
WORKDIR /app/scootertown.api
RUN dotnet publish -c Release -o out


FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=build /app/scootertown.api/out ./
ENTRYPOINT ["dotnet", "PDX.PBOT.Scootertown.API.dll"]
