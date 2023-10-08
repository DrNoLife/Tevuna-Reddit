# Reddit User Analyzer
This repository contains a quick and dirty notebook designed for quickly analyzing someone's Reddit profile.

The primary goal of this project is to retrieve as many comments and posts as possible from a user's Reddit account, enabling us to gain insights into their online persona. By examining the subreddits where they are active, as well as the breakdown of their posts versus comments, we can make informed judgments about whether engaging with them is worthwhile.

The previous iteration of this repository was exclusively dedicated to the Jupyter Notebook implementation. All pertinent details regarding this version can be discovered within the README file located in the notebook directory.

## Table of Contents
* Features
* Prerequisites
* Setup
* Running the Program
* Contributing
* License

## Features
* **API**:

Exposes endpoints to fetch and analyze Reddit user data.
Located in the api folder.
* **Web UI**:

Provides a user-friendly interface to visualize user analysis.
Located in the web_ui folder.

## Prerequisites
* Docker
* Docker-compose
* Reddit ClientId and ClientSecret

To register a new app and acquire the necessary credentials, please visit: [Reddit Apps page](https://www.reddit.com/prefs/apps)

## Setup
Before running the application, you may create a settings.json file inside api/app with your Reddit app credentials:

```json
{
    "ClientId": "<your-reddit-client-id>",
    "ClientSecret": "<your-reddit-client-secret>"
}
```

## Running the Program
You can start the Reddit User Analyzer using either the startup.sh script for a simplified setup, or by using Docker Compose directly for more control.

### Using the Startup Script
Make the startup.sh script executable:
```bash
chmod +x startup.sh
```
Run the startup.sh script:
```bash
./startup.sh
```
The script will check for the settings.json file, prompt you for Reddit credentials if necessary, and start both the API and Web UI using Docker Compose.

### Using Docker Compose
Starting the API
Navigate to the api directory and execute the following command:

```bash
cd api && docker-compose up --build
```
Starting the Web UI
Navigate to the web_ui directory and execute the following command:

```bash
cd web_ui && docker-compose up --build
```

## Usage
* Access the API at http://localhost:5000.
* Access the Web UI at http://localhost:5090.

## Contributing
Feel free to open issues or pull requests to contribute to this project.
