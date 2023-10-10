#!/bin/bash

# Function to stop Docker Compose projects
stop_docker_compose() {
    local dir=$1
    echo "Stopping Docker Compose project in $dir..."
    (cd "$dir" && docker-compose down)
}

# Stop the API Docker Compose project
stop_docker_compose api

# Stop the Web UI Docker Compose project
stop_docker_compose web_ui

echo "All Docker Compose projects have been stopped."
