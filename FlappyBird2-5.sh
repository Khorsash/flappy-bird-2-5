#!/bin/sh
printf '\033c\033]0;%s\a' FlappyBird2-5
base_path="$(dirname "$(realpath "$0")")"
"$base_path/FlappyBird2-5.x86_64" "$@"
