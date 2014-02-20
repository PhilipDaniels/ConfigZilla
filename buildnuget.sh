#!/bin/bash

# Build instructions
# ==================
# Compile ConfigZilla.Tasks and ConfigZilla.Encrypter in Release mode.
# Run this script to build a new nuget package. Remember to update the version number if going public.


# Create the nuget package structure.
cp ./ConfigZilla.Tasks/bin/Release/ConfigZilla.Tasks.dll ./nuget/tools
rm -rf ./nuget/tools/ConfigZilla
cp -R ./ConfigZilla ./nuget/tools
rm -rf ./nuget/tools/ConfigZilla/bin
rm -rf ./nuget/tools/ConfigZilla/obj



# Increment the build number...actually not necessary during testing cycle
# because you can do "Update-Package -reinstall ConfigZilla" from the
# package manager console in Visual Studio.
# vim ./nuget/ConfigZilla.nuspec

# Create the nuget package (creates a .nupkg file) in the current directory.
nuget pack ./nuget/ConfigZilla.nuspec

# Push the package to my local feed for testing.
LAST=`ls -t *.nupkg | head -n1`
# echo "LAST is $LAST"
nuget push $LAST -Source LocalNuGetFeed
