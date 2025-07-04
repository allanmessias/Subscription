# Subscription Project Documentation

# Modules

- **Subscription.Core**: Data modeling and domain logic.
- **Subscription.Publisher**: API responsible of making the subscriptions using rabbitMQ.
- **Subscription.Consumer**: Worker that listens and consumes the events.
- **Subscription.Infrastructure**: Repository implementation, data layer, 3rd party connections and external services.
  

# Prerequisites & Development Environment

This project uses **Docker** and **Docker Compose** to spin up all required services: PostgreSQL database, RabbitMQ message broker, Publisher API, Consumer Worker, and auto-generated documentation using DocFX.

---

## ✅ Prerequisites

Before running the project, make sure the following tools are installed on your machine:

### 🐋 Docker

- **Minimum version:** 20.10+
- [Install Docker](https://docs.docker.com/get-docker/)

### 🛠 Docker Compose

- **Minimum version:** 1.29+
- Docker Compose is typically bundled with Docker Desktop (Windows/macOS)
- [Install Docker Compose](https://docs.docker.com/compose/install/)

---

## 📁 Expected Folder Structure

At the root of the project, the following folder structure is expected:

```md
📁 Subscription/
├── Subscription.Core/
├── Subscription.Publisher/
├── Subscription.Consumer/
├── Subscription.Infrastructure/
├── docs/
│ ├── docfx_project/ # Generated via docfx init
│ └── Dockerfile # For building the DocFX container
├── docker-compose.yml
├── entrypoint.sh # Script to build/run everything locally
```

---

## 🚀 Getting Started

To spin up all services (database, message queue, app services, and documentation), simply run:

```bash
./entrypoint.sh
```

This will:

1. **Stop and remove any running containers**

2. **Build all services from scratch (Publisher, Consumer, DocFX, etc.)**

3. **Run a migration container (subscription-migrator)**

4. **Launch the full application environment**

## 📚 Accessing the Documentation
Once the environment is running, you can access the 
generated documentation at:

    http://localhost:8090

⚠️ Make sure no other service is using port 8090 on your machine.

## 🗃️ Services Overview (from docker-compose.yml)

| Service        | Description                                 | Port(s)         |
| -------------- | ------------------------------------------- | --------------- |
| **PostgreSQL** | Database used by all microservices          | `5433 -> 5432`  |
| **RabbitMQ**   | Message broker with management UI           | `5672`, `15672` |
| **Publisher**  | API that publishes subscription events      | `5000 -> 80`    |
| **Consumer**   | Worker service that consumes messages       | `6000 -> 80`    |
| **DocFX**      | Static site generator for API documentation | `8090 -> 8090`  |

## 🧪 Health Checks
    PostgreSQL: via pg_isready

    RabbitMQ: via rabbitmqctl status

    Publisher and Consumer services: will wait for DB and RabbitMQ to be healthy before starting
