FROM python:3.9-slim

WORKDIR /app

# Install required packages and libraries
COPY requirements.txt requirements.txt
RUN pip install --no-cache-dir -r requirements.txt

# Copy the application code to the container
COPY . .

# Set the command to run your application
CMD ["gunicorn", "-b", "0.0.0.0:80", "app:app"]
