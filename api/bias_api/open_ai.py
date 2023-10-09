import openai
import json

class OpenAI:

    def __init__(self, api_key):
        self.__api_key = api_key

    def analyze_comments(self, comments):
        # Concatenate comments into a single string
        comments_text = '\n'.join([comment['body'] for comment in comments])
        
        # Ensure the text is within the OpenAI API's maximum token limit
        max_tokens = 16000  # Adjust as needed
        comments_text = comments_text[:max_tokens]
        
        # Initialize OpenAI API client
        openai.api_key = self.__api_key
        
        # Send request to OpenAI API
        response = openai.ChatCompletion.create(
            model="gpt-3.5-turbo-16k",
            messages=[
                {
                    "role": "system",
                    "content": "You are a helpful assistant."
                },
                {
                    "role": "user",
                    "content": f"Please describe the individual based on these comments. "
                                f"All comments are created by the same person. "
                                f"Explain the person's political stance and potential biases, "
                                f"and also their interests.\n\n{comments_text}"
                }
            ],
            max_tokens=1000,
        )

        return response['choices'][0]['message']['content'].strip()