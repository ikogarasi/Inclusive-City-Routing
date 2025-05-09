FROM osrm/osrm-backend:latest

RUN echo "deb http://archive.debian.org/debian stretch main" > /etc/apt/sources.list && \
    apt-get update && \
    apt-get install -y python3 python3-pip python3-psycopg2 && \
    apt-get clean && rm -rf /var/lib/apt/lists/*