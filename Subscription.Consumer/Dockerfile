FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS base
WORKDIR /app

RUN apt-get update && apt-get install -y postgresql-client

FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
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

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY Subscription.Consumer/entrypoint.sh ./entrypoint.sh
RUN chmod +x ./entrypoint.sh

ENTRYPOINT ["./entrypoint.sh"]
