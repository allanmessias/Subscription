#!/bin/sh

set -e

# Waits for database to be up and healthy and respondos on 5432
until pg_isready -h db -p 5432 -U subscription_user -d subscription_db; do
	echo "Waiting for database to be configured and finished"
	sleep 2
done

echo "Database up and running"

exec dotnet Subscription.Consumer.dll