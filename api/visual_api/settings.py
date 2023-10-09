import os
import json

class Settings:

    def __init__(self, file_name):
        
        client_id, client_secret = self.__load_settings(file_name)

        self.client_id = client_id
        self.client_secret = client_secret

    # Retrieve the client ID and client secret from the configuration.
    def __load_settings(self, file_name):
        if not os.path.exists(file_name):
            with open(file_name, 'w') as file:
                file.write('{}')
            raise Exception("The settings.json file was created. Please fill it in with the required data.")

        with open(file_name, 'r') as file:
            settings = json.load(file)
            client_id = settings.get('ClientId')
            client_secret = settings.get('ClientSecret')

        if client_id is None or client_secret is None:
            raise Exception("The settings.json file is missing the required data.")
        
        return client_id, client_secret