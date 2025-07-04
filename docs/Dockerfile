FROM mcr.microsoft.com/dotnet/sdk:8.0

RUN dotnet tool install -g docfx

ENV PATH="$PATH:/root/.dotnet/tools"

WORKDIR /app

COPY . .

CMD ["docfx", "metadata", "&&", "docfx", "build"]
