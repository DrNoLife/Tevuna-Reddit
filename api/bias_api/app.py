from settings import Settings
from flask import Flask, jsonify, request, send_file, g
from flask_cors import CORS
import logging
from reddit import Reddit
from open_ai import OpenAIWrapper

logging.basicConfig(level=logging.DEBUG)

app = Flask(__name__)
CORS(app)  # This will enable CORS for all routes

@app.route('/get-user-analysis', methods=['GET'])
def get_user_activity():
    username = request.args.get('username')

    # 0. Initialize settings
    settings = Settings("settings.json")
    
    # 1. Retrieve Reddit data.
    reddit_client = Reddit(settings)
    reddit_data = reddit_client.fetch_comments(username)
   
    # 2. Get analysis by Gippity.
    openai_wrapper = OpenAIWrapper(settings.open_ai_key)
    analysis = openai_wrapper.analyze_comments(reddit_data)
    
    # 3. Return the analysis.
    return {
        "Username": username,
        "Analysis": analysis
    }

if __name__ == "__main__":
    app.run(host='0.0.0.0', debug=True)  # Set host to '0.0.0.0'