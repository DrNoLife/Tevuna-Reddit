import praw
from settings import Settings
import json
from collections import defaultdict

class Reddit:

    def __init__(self, settings):
        self.__settings = settings
        self.__reddit = self.__get_praw_instance__()

    def __get_praw_instance__(self):
        reddit = praw.Reddit(
            client_id = self.__settings.client_id,
            client_secret = self.__settings.client_secret,
            user_agent = "user_agent='reddit:my_subreddit_activity:v1.0 (by u/iamdrnolife)'",
            check_for_async = False)
        
        return reddit
    
    def fetch_comments(self, username):
        user = self.__reddit.redditor(username)
        comments = []

        for comment in user.comments.new(limit=None):
            comments.append({
                'body': comment.body,
                'created_utc': comment.created_utc,
                'subreddit': str(comment.subreddit)
            })

        return comments
        #self.__save_to_json(username, comments)

    def __save_to_json(self, username, comments):
        with open(f'{username}-comments.json', 'w', encoding='utf-8') as f:
            json.dump(comments, f, ensure_ascii=False, indent=4)