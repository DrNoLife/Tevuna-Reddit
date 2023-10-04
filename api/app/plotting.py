import matplotlib.pyplot as plt
import matplotlib as mpl
from scipy.signal import savgol_filter
import matplotlib.pyplot as plt
from matplotlib.patches import FancyBboxPatch
from matplotlib import font_manager
import numpy as np
from scipy.interpolate import make_interp_spline, BSpline
import os
import io

class Plotting:

    def __init__(self):
        # Define colors as hex codes
        self.post_color = '#f1c40f'  # Yellow
        self.comment_color = '#27ae60'  # Green
        self.background_color = '#2c3e50'  
        self.karma_line_color = '#e74c3c'  
        self.karma_text_color = '#ecf0f1'
        self.diagram_padding = 5
        self.y_axis_color = '#ffffff'
        self.circle_color = '#c0392b'
        self.circle_border_color = '#e74c3c'
        self.circle_radius = 1000

        # Set font
        font = {'family' : 'DejaVu Sans',
                'weight' : 'bold',
                'size'   : 10}

        mpl.rc('font', **font)

    def plot_top_subreddits(self, activity, filename, type):

        # Sort data by total activity in descending order
        sorted_data = dict(sorted(activity.items(), key=lambda item: item[1]['TotalActivity'], reverse=True))

        # Get the top 10 subreddits by total activity
        top_subreddits = list(sorted_data.keys())[:10]
        total_posts = [sorted_data[subreddit]["TotalPosts"] for subreddit in top_subreddits]
        total_comments = [sorted_data[subreddit]["TotalComments"] for subreddit in top_subreddits]
        post_karma = [sorted_data[subreddit]["TotalPostsKarma"] for subreddit in top_subreddits]
        comment_karma = [sorted_data[subreddit]["TotalCommentsKarma"] for subreddit in top_subreddits]
        total_karma = [sorted_data[subreddit]["TotalKarma"] for subreddit in top_subreddits]

        # Create a stacked bar chart
        fig, ax1 = plt.subplots(figsize=(34, 16))
        fig.patch.set_facecolor(self.background_color)
        ax1.set_facecolor(self.background_color)
        post_bars = ax1.bar(top_subreddits, total_posts, color=self.post_color, label='Posts')
        comment_bars = ax1.bar(top_subreddits, total_comments, bottom=total_posts, color=self.comment_color, label='Comments')

        # Add title.
        #plt.title(f'{filename} - [{type}]', color=self.y_axis_color)  # add title to the plot

        # Make x-labels diagonal
        plt.xticks(rotation=45)

        # Add grid lines
        ax1.grid(axis='y', color='grey', linestyle='--', linewidth=0.5)
        ax1.set_ylabel('Total Activity', color=self.y_axis_color)
        ax1.tick_params(axis='y', labelcolor=self.y_axis_color)
        ax1.tick_params(axis='x', labelcolor=self.y_axis_color)
        
        # Add second y-axis
        ax2 = ax1.twinx()
        karma_line = ax2.plot(top_subreddits, total_karma, color=self.karma_line_color, linewidth=2, label='Karma')
        karma_points = ax2.scatter(top_subreddits, total_karma, color=self.circle_color, s=self.circle_radius, edgecolors=self.circle_border_color, zorder=3)
        ax2.set_ylabel('Total Karma', color=self.y_axis_color)
        ax2.tick_params(axis='y', labelcolor=self.y_axis_color)
        
        # Add labels to bars
        for i, v in enumerate(total_posts):
            ax1.text(i, v/2, str(v), color='black', fontweight='bold', ha='center', va='center')
        for i, v in enumerate(total_comments):
            ax1.text(i, total_posts[i] + v/2, str(v), color='black', fontweight='bold', ha='center', va='center')

        # Add total karma to points
        for i, v in enumerate(total_karma):
            ax2.text(i, v, str(v), color=self.karma_text_color, fontweight='bold', ha='center', va='center', zorder=4)


        fig.tight_layout(pad=self.diagram_padding)
        
        plt.legend([post_bars, comment_bars, karma_line[0]], ['Posts', 'Comments', 'Karma'], loc="upper right")

        # Create directories if they don't exist
        #if not os.path.exists('diagrams'):
            #os.makedirs('diagrams')

        # Save the figure before showing it
        #plt.savefig(f'diagrams/{filename}.png', bbox_inches='tight')

        #plt.show()

        # Instead of saving the figure to a file, save it to a BytesIO object
        img_bytes_io = io.BytesIO()
        plt.savefig(img_bytes_io, format='png')
        img_bytes_io.seek(0)
        plt.close()

        # Return the BytesIO object
        return img_bytes_io