FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

RUN apt-get update && apt-get install -y ca-certificates && update-ca-certificates

RUN dotnet restore Subscription.Publisher/Subscription.Publisher.csproj

RUN dotnet publish Subscription.Publisher/Subscription.Publisher.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "Subscription.Publisher.dll"]
