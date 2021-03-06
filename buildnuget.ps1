# Running
# Normally you can just F5 this script from within the PowerShell ISE. That does
# not set the feed and hence pushes to LocalNuGetFeed. Otherwise you have to edit
# the script (see the end) to push to the pukka nuget site.

$buildfns = "$($MyInvocation.MyCommand.path | split-path)\buildnugetfns.ps1"
. $buildfns

$feed = $args[0]
if ($feed -eq $null) 
{
    $feed = "LocalNuGetFeed"
}


$scriptPath = split-path -parent $MyInvocation.MyCommand.Definition
$buildlog = "$scriptPath\buildlog.txt"
$config="Release"
$slndir = $scriptPath
$slnfile = "$slndir\ConfigZilla.sln"
$versionfile = "$slndir\VERSION.txt"
$msbuild = GetMSBuild
$verbosity = "normal"
$compileargs = "/v:$verbosity `"/p:Platform=AnyCPU;VisualStudioVersion=11.0;Configuration=$config;SolutionDir=$slndir\`" "


Message "Compiling with Config=$config, SlnDir=$slndir, feed=$feed"
rm $buildlog
BuildCleanTarget
CompileProject "$slndir\ConfigZilla\ConfigZilla.csproj"
CompileProject "$slndir\ConfigZilla.Tasks\ConfigZilla.Tasks.csproj"


# Create the nuget package structure.
rm -recurse -force "$slndir\nuget\tools\ConfigZilla"
cp -recurse "$slndir\ConfigZilla" "$slndir\nuget\tools\"
rm -recurse -force "$slndir\nuget\tools\ConfigZilla\bin"
rm -recurse -force "$slndir\nuget\tools\ConfigZilla\obj"
cp "$slndir\ConfigZilla.Tasks\bin\$config\ConfigZilla.Tasks.dll" "$slndir\nuget\build"
cp "$slndir\ConfigZilla.Tasks\ConfigZilla.targets" "$slndir\nuget\build"


# Auto-increment the version number and create the nuget package.
$version = get-content $versionfile | out-string
$version = $version.Trim()
Message "Old version = $version"
$version = IncrementVersionNumber $version
set-content -path $versionfile -value $version
Message "This version = $version"
nuget pack -version $version "$slndir\nuget\ConfigZilla.nuspec" -OutputDirectory $slndir >> $buildlog 2>&1
Message "nuget pack done."

# Push it to the feed.
$lastPkg = "$slndir\ConfigZilla.$version.nupkg"
#nuget push $lastPkg -Source $feed >> $buildlog 2>&1
# Swap the line above with this one to push. The one above will push to my local feed,
# the one below will push to the cloud.
nuget push $lastPkg >> $buildlog 2>&1
Message "nuget push done."

Message "All done."
notepad $buildlog
