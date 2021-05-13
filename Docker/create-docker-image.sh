#!/bin/bash

# Copy all files to this directory because of the build context
rsync -r ../../Shook ./

# Build new image
docker build -t shook-server .

# Delete all files
rm -r ./Shook