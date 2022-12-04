#!/bin/sh

targetDirectory="./AdventOfCode$1.Puzzle$2"
targetFileName="$targetDirectory/input.txt"

#if test -f $targetFileName; then
#  echo "Puzzle input already exists locally, skipping download!"
#  exit 0
#fi

# Login to adventofcode.com and use the developer tools to retrieve the session token.
token=$3
url="https://adventofcode.com/$1/day/$2/input"

curl --cookie "session=$token" --output $targetFileName $url

echo $url
