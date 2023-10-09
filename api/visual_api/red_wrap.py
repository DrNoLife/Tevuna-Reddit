import praw
from settings import Settings
import json
from collections import defaultdict

class RedditWrapper:

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
    
    def __get_user_activity(self, username):
        user = self.__reddit.redditor(username)

        subreddit_activity = defaultdict(lambda: {"TotalPosts": 0, "TotalPostsKarma": 0, "TotalComments": 0, "TotalCommentsKarma": 0})

        # Iterate over the user's posts
        print(f"Fetching posts for user: {username}")
        post_count = 0
        for post in user.submissions.new(limit=None):
            subreddit_activity[post.subreddit.display_name]["TotalPosts"] += 1
            subreddit_activity[post.subreddit.display_name]["TotalPostsKarma"] += post.score
            post_count += 1
            if post_count % 100 == 0:
                print(f"Processed {post_count} posts")

        # Iterate over the user's comments
        print(f"Fetching comments for user: {username}")
        comment_count = 0
        for comment in user.comments.new(limit=None):
            subreddit_activity[comment.subreddit.display_name]["TotalComments"] += 1
            subreddit_activity[comment.subreddit.display_name]["TotalCommentsKarma"] += comment.score
            comment_count += 1
            if comment_count % 100 == 0:
                print(f"Processed {comment_count} comments")

        print(f"Finished processing. Total posts: {post_count}, Total comments: {comment_count}")

        # Calculate total activity and total karma for each subreddit
        for subreddit, data in subreddit_activity.items():
            data["TotalActivity"] = data["TotalPosts"] + data["TotalComments"]
            data["TotalKarma"] = data["TotalPostsKarma"] + data["TotalCommentsKarma"]

        return subreddit_activity

    def __write_to_json(self, username, data):
        # Sort data by total activity in descending order
        sorted_data = dict(sorted(data.items(), key=lambda item: item[1]['TotalActivity'], reverse=True))
        with open(f"{username}_activity.json", "w") as outfile:
            json.dump(sorted_data, outfile)

    def retrieve_stats(self, username):
        activity = self.__get_user_activity(username)
        self.__write_to_json(username, activity)
        return activity