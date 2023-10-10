#!/bin/bash

# Function to prompt user for Reddit and OpenAI credentials
get_credentials() {
    echo "Please enter your Reddit Client ID:"
    read client_id
    echo "Please enter your Reddit Client Secret:"
    read client_secret
    echo "Please enter your OpenAI API Key:"
    read openai_key

    # Create the settings.json file with the provided credentials in both new locations
    for settings_path in ./api/bias_api/settings.json ./api/visual_api/settings.json
    do
        mkdir -p "$(dirname "$settings_path")"  # Ensure the directory exists
        cat > "$settings_path" <<EOL
{
    "ClientId": "$client_id",
    "ClientSecret": "$client_secret",
    "OpenAIKey": "$openai_key"
}
EOL
    done
}

# Check if the settings.json files exist in both new locations, if not, prompt user for credentials
settings_exist=true
for settings_path in ./api/bias_api/settings.json ./api/visual_api/settings.json
do
    if [[ ! -f "$settings_path" ]]; then
        settings_exist=false
        break
    fi
done

if [[ "$settings_exist" == false ]]; then
    echo "settings.json not found in one or both locations. Generating them now..."
    get_credentials
else
    echo "settings.json files found."
fi

# Start the API using Docker Compose
echo "Starting APIs..."
(cd api && docker-compose up --build) &

# Start the Web UI using Docker Compose
echo "Starting Web UI..."
(cd web_ui && docker-compose up --build) &
