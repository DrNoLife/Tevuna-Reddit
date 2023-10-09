import praw
from collections import defaultdict
from red_wrap import RedditWrapper
from plotting import Plotting
from settings import Settings
from flask import Flask, jsonify, request, send_file, g
from flask_cors import CORS
import matplotlib.pyplot as plt
import os
import io
import json
import logging

logging.basicConfig(level=logging.DEBUG)

app = Flask(__name__)
CORS(app)  # This will enable CORS for all routes

@app.route('/get-user-activity', methods=['GET'])
def get_user_activity():
    username = request.args.get('username')

    # 0. Initialize settings
    settings = Settings("settings.json")
    
    # 1. Retrieve Data
    reddit_wrapper = RedditWrapper(settings)
    user_activity = reddit_wrapper.retrieve_stats(username)
    
    # 2. Generate Plot
    plotting = Plotting()
    # Modify the plot_top_subreddits method to return the plot instead of saving it
    img_bytes_io = plotting.plot_top_subreddits(user_activity, None, None)
    
    # 3. Return the Plot as a PNG Image
    return send_file(img_bytes_io, mimetype='image/png')

if __name__ == "__main__":
    app.run(host='0.0.0.0', debug=True)  # Set host to '0.0.0.0'