import openai
from openai import OpenAI

class OpenAIWrapper:
    def __init__(self, api_key):
        self.client = OpenAI(api_key=api_key)

    def analyze_comments(self, comments):
        comments_text = '\n'.join([comment['body'] for comment in comments])
        max_tokens = 16000  # Adjust as needed
        comments_text = comments_text[:max_tokens]

        # Yoink generation by Gippity.
        chat_completion = self.client.chat.completions.create(
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

        return chat_completion.choices[0].message.content.strip()