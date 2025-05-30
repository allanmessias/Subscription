# Projects
PUBLISHER_PROJECT=Subscription.Publisher
CONSUMER_PROJECT=Subscription.Consumer
INFRA_PROJECT=Subscription.Infrastructure

# Docker Compose
DC=docker-compose

# Get the containers up
up:
	$(DC) up -d --build

down:
	$(DC) down

logs:
	$(DC) logs -f

logs-tail:
	$(DC) logs -f --tail=100

reset:
	$(DC) down
	$(DC) up -d --build	

# Build .net
build:
	dotnet build

migrate:
	dotnet ef database update --project $(INFRA_PROJECT) --startup-project $(CONSUMER_PROJECT)

migrations-list:
	dotnet ef migrations list --project $(INFRA_PROJECT) --startup-project $(CONSUMER_PROJECT)


# Create new migration, if needed
migration:
	ifndef name
		$(error You must provide a migration name: make migration name=MyMigration)
	endif
	    dotnet ef migrations add $(name) --project $(INFRA_PROJECT) --startup-project $(CONSUMER_PROJECT)

ps:
	$(DC) ps

bash-consumer:
	$(DC) exec consumer /bin/sh

bash-db:
	$(DC) exec db bash
