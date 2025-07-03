# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Subscription.sln .
COPY Subscription.Core/Subscription.Core.csproj Subscription.Core/
COPY Subscription.Infrastructure/Subscription.Infrastructure.csproj Subscription.Infrastructure/
COPY Subscription.Consumer/Subscription.Consumer.csproj Subscription.Consumer/
COPY Subscription.Publisher/Subscription.Publisher.csproj Subscription.Publisher/
COPY Subscription.Tests/Subscription.Tests.csproj Subscription.Tests/

RUN dotnet restore Subscription.sln

COPY . .

WORKDIR /src/Subscription.Consumer
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

RUN apt-get update && apt-get install -y postgresql-client dos2unix ca-certificates && rm -rf /var/lib/apt/lists/*

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Subscription.Consumer.dll"]
