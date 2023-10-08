#!/bin/bash

# Function to prompt user for Reddit credentials
get_reddit_credentials() {
    echo "Please enter your Reddit Client ID:"
    read client_id
    echo "Please enter your Reddit Client Secret:"
    read client_secret
    
    # Create the settings.json file with the provided credentials
    cat > api/app/settings.json <<EOL
{
    "ClientId": "$client_id",
    "ClientSecret": "$client_secret"
}
EOL
}

# Check if the settings.json file exists, if not, prompt user for Reddit credentials
if [[ ! -f api/app/settings.json ]]; then
    echo "settings.json not found. Generating one now..."
    get_reddit_credentials
else
    echo "settings.json found."
fi

# Start the API using Docker Compose
echo "Starting API..."
(cd api && docker-compose up --build) &

# Start the Web UI using Docker Compose
echo "Starting Web UI..."
(cd web_ui && docker-compose up --build) &
