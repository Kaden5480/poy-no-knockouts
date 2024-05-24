#!/usr/bin/env bash

set -xe

cd ../

MOD_NAME="NoKnockouts"
VERSION="$(git describe --abbrev=0 | tr -d  "v")"

BP_NAME="$MOD_NAME-$VERSION-BepInEx"
ML_NAME="$MOD_NAME-$VERSION-MelonLoader"
BP_DIR="build/$BP_NAME"
ML_DIR="build/$ML_NAME"


dotnet build -c Release-BepInEx
dotnet build -c Release-MelonLoader

mkdir -p "$BP_DIR"/patchers
mkdir -p "$ML_DIR"/Plugins

# BepInEx
cp bin/patcher/release-bepinex/net472/*.dll \
    "$BP_DIR/patchers/"
cp build/README-BepInEx.txt "$BP_DIR/README.txt"

# MelonLoader
cp bin/patcher/release-melonloader/net472/*.dll \
    "$ML_DIR/Plugins/"
cp build/README-MelonLoader.txt "$ML_DIR/README.txt"

# Zip everything
pushd "$BP_DIR"
zip -r ../"$BP_NAME.zip" .
popd

pushd "$ML_DIR"
zip -r ../"$ML_NAME.zip" .
popd

# Remove directories
rm -rf "$BP_DIR" "$ML_DIR"