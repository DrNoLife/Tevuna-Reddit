# Tevuna Reddit
Tevuna Reddit is an insightful tool designed to delve into the activities and behaviors of specific Reddit users. By harnessing the power of the Reddit API, it unveils a user's active subreddits alongside a detailed breakdown of their karma, comments, and posts, all represented in a comprehensive visual graph. Taking a step further, Tevuna Reddit integrates the OpenAI API to generate a "bias report" for the user, offering a deeper understanding of their inclinations and interactions on the platform.

## Table of Contents
* [Features](#features)
    + [Showcase](#showcase)
    + [APIs](#apis)
    + [Web UI](#web-ui)
* [Previous Version](#previous-version)
* [Prerequisites](#prerequisites)
* [Setup](#setup)
* [Running the Program](#running-the-program)
    + [Using the Startup Script](#using-the-startup-script)
    + [Using Docker Compose](#using-docker-compose)
* [Usage](#usage)
* [Stopping the program](#stopping-the-program)
    + [Manual Shutdown](#manual-shutdown)
    + [Automated Shutdown](#automated-shutdown)
* [Roadmap](#roadmap)
    + [Potentiel Features, but no guarantee](#potentiel-features-but-no-guarantee)
* [Contributing](#contributing)


## Features

Tevuna Reddit includes the following:

* **Visual Representations**: Gain a clearer insight through visual graphs that encapsulate the user's Reddit activity.
* **Bias Report Generation**: Utilize the OpenAI API to generate a bias report, providing a more nuanced understanding of the user's behavior and tendencies on Reddit.
* **Web UI**: Use a clean and easy to understand web ui, that makes interacting with Tevuna Reddit a breeze.

### Showcase

The following video shows how to use the program. Starts out from the Web UI and shows searching and result. 

For a more full walkthrough, checkout the `./docs/videos` folder for a video showing how to use the startup script to run Tevuna Reddit.

https://github.com/DrNoLife/Tevuna-Reddit/assets/17468135/e1548887-8b7d-4b3a-a2c7-5dcd88f653b7

***Note**: Usernames used in the showcase are found at random. I visited a random post I saw on `r/Denmark` and just used the first name I found.*

### APIs

In Tevuna Reddit there are 2 APIs included. One for retrieving the graphical overview of a user, and one for retrieving the bias report. Here's an overview of each:

* **Overview API**

Exposes an endpoint to generate a visual overview of a Reddit user.

Default port is: ```5000```

Endpoint is ```get-user-activity``` and it expects a get parameter of ```username```.

Example call: ```localhost:5000/get-user-activity?username=iamdrnolife```

* **Bias Report API**

Exposes an endpoint to generate a text-based bias report of a Reddit user.

Default port is: ```5010```

Endpoint is ```get-user-analysis``` and it expects a get parameter of ```username```.

Example call: ```localhost:5010/get-user-analysis?username=iamdrnolife```

### Web UI

Tevuna Reddit also includes a Web UI that makes the program easy to interact with.

It's written in C# using Blazor Wasm. It's located in the ```web_ui``` folder.

Default port is: ```5090```

## Previous Version
The predecessor of this repository was solely focused on a Jupyter Notebook implementation. All relevant details regarding that version are preserved in the README file within the notebook directory, ensuring a seamless reference for those interested in the prior iteration.

## Prerequisites
* [Docker](https://docs.docker.com/get-docker/)
* [Docker-compose](https://docs.docker.com/compose/install/)
* Reddit ClientId and ClientSecret
* OpenAI API Key

To register a new app and acquire the necessary credentials, please visit: [Reddit Apps page](https://www.reddit.com/prefs/apps)

To get an OpenAI API Key, please visit: [OpenAI User Settings](https://platform.openai.com/account/api-keys)

## Setup
Before running the application, you need to create a settings.json file inside api/app with your Reddit app credentials and OpenAI API Key:

```json
{
    "ClientId": "<your-reddit-client-id>",
    "ClientSecret": "<your-reddit-client-secret>",
    "OpenAIKey": "<your-openai-api-key>"
}
```

However, you don't have to do this manually, you can also just use the ```startup.sh``` script, which will guide you through the process.

## Running the Program
You can start Tevuna Reddit using either the startup.sh script for a simplified setup, or by using Docker Compose directly for more control.

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
* Access the Visual Overview API at http://localhost:5000 (replace "localhost" with your server address if deployed remotely).
* Access the Bias Report API at http://localhost:5010 (replace "localhost" with your server address if deployed remotely).
* Access the Web UI at http://localhost:5090 (replace "localhost" with your server address if deployed remotely).


## Stopping the program

Tevuna Reddit ensures a smooth and hassle-free termination process through multiple avenues. You may opt for the manual approach or leverage our automated script for a swift shutdown.

### Manual Shutdown

1. Navigate to the `/api` and `/web_ui` folders.
2. Execute the following command to bring down the Docker containers:

```bash
docker compose down
```

### Automated Shutdown

Alternatively, a simplified shutdown can be achieved by executing the shutdown.sh script provided:
Make the startup.sh script executable:

```bash
chmod +x shutdown.sh
./shutdown.sh
```

This streamlined script encapsulates the necessary commands to safely terminate the program, offering a hassle-free shutdown experience.

## Roadmap

These are some of the enhancements I'm considering to improve the user experience and efficiency of the application.

* **Caching via Local Storage**: Store user data for 24 hours to minimize API calls.
* **Force-Refresh Button**: Manually update stored user data.
* **Instant Display**: Show local storage data on page load for a quicker user experience.
* **Incremental Data Display**: Show data as it arrives from each API, with a loading bar for pending information.

### Potentiel Features, but no guarantee

While these changes could significantly alter the project, they are not currently on the roadmap. They would offer more customization and ease of use, especially for hosted versions.

* **Database Support**: Option to use a database for more robust data management.
* **Custom API Keys**: Allow users to input their own Reddit and OpenAI API keys.
* **OpenAI Optional**: Make OpenAI feature optional if only a Reddit API key is supplied.
* **Database Caching**: Introduce server-side caching when a database is used.

*Note:* These changes would enable users to benefit from a hosted solution while still using their own API keys. 

## Contributing
Feel free to open issues or pull requests to contribute to this project.
