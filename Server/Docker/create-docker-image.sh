#!/bin/bash

# Clone project
git clone https://github.com/Samofan/Shook.git

# Build new image
docker build -t shook-server .

# Remove the repo
rm -r ./Shook
