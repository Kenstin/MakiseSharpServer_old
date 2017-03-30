#!/bin/bash
#courtesy of discord.js

#exit if any command fails
set -e

#Figure out the source of the build
if [ -n "$TRAVIS_TAG" ]; then
  echo -e "\e[36m\e[1mBuild triggered for tag \"${TRAVIS_TAG}\"."
else
  echo -e "\e[36m\e[1mBuild triggered for branch \"${TRAVIS_BRANCH}\"."
fi
#PR
if [ "$TRAVIS_PULL_REQUEST" != "false" ]; then
  echo -e "\e[36m\e[1mBuild triggered for PR #${TRAVIS_PULL_REQUEST} to branch\"${TRAVIS_BRANCH}\""
fi

#nuget sources Add -Name "Discord.NET 1.0" -Source "https://www.myget.org/F/discord-net/api/v3/index.json"
dotnet restore
#dotnet test
dotnet publish -c Release