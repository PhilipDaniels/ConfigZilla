# Write a message to the build log.
function Message($msg)
{
    add-content -path $buildlog -encoding Unicode "**** $msg`n"
    write-host "**** $msg"
}

# Build the "clean" target at solution level.
function BuildCleanTarget()
{
    $args = "$msbuild $slnfile /m /v:$verbosity /p:Configuration=$config /t:Clean >> $buildlog 2>&1"
    Invoke-Expression $args
}

function IncrementVersionNumber($version)
{
    $tokens = $version.Split(".")
    $last = [int]($tokens[$tokens.length - 1])
    $last += 1
    $tokens[$tokens.length - 1] = [string]$last
    return [string]::Join(".", $tokens)
}


# Compile (but do not publish) an individual project.
function CompileProject($projectfile)
{
    $args = "$msbuild $projectfile $compileargs >> $buildlog 2>&1"
    Invoke-Expression $args
    Message "Compiled project $projectfile"
}

# Compile and publish a project. No longer needed, we aren't using publication profiles any more.
#function PublishProject($projectfile)
#{
#    $args = "$msbuild $projectfile $compileargs /p:DeployOnBuild=true /p:PublishProfile=`"Release - local file`" >> $buildlog 2>&1"    
#    Invoke-Expression $args
#    Message "Published project $projectfile"
#}

function GetPublishFolderFromProject($projectfile)
{
    (split-path -leaf $projectfile).Replace(".csproj", "")
}

function PublishProject2($projectfile)
{
    $folder = GetPublishFolderFromProject $projectfile
    $args = "$msbuild $projectfile $compileargs /p:PublishDestination=$pubdir\$folder /t:PublishToFileSystem >> $buildlog 2>&1"    
    Invoke-Expression $args
    Message "Published project $projectfile to $pubdir\$folder"
}


# Return the folder that contains the .sln file.
function GetSolutionDir()
{
    (split-path -parent $scriptPath)
}

# Where we will be writing our published files.
function GetPublicationDir()
{
    "C:\work\publication\$config"
}

# Delete a file if it exists.
function SafeDeleteFile($file)
{
    if (Test-Path $file)
    {
	   rm $file -recurse
    }
}

# Get path to the MSBuild.exe.
function GetMSBuild
{
    # This returns v2.
    #$lib = [System.Runtime.InteropServices.RuntimeEnvironment]
    #$rtd = $lib::GetRuntimeDirectory()
    #Join-Path $rtd msbuild.exe
    
    "C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe"
}

# Ensure a folder exists, including all parent folders.
function EnsureFolder($folder)
{
    new-item -ItemType Directory -force -path $folder | out-null
}

function PublishAllToLocalDisk()
{
    # Delete the previous log file.
    SafeDeleteFile $buildlog
    Message "Starting solution clean."
    BuildCleanTarget
    Message "Solution clean done."

    # This has the nice side-effect of ensuring that the publication folder exists.
    EnsureFolder $log4netdir

    # Compile the various projects IN THE CORRECT ORDER.
    # MSBuild is broken, it can't figure out the correct order from the command line
    # so we have to do it manually.
    # For completeness, we are compiling everything even though not all are needed for
    # a successful publication.
    CompileProject "$slndir\ConfigZilla\ConfigZilla.csproj"
    CompileProject "$slndir\CommonLib\CommonLib.csproj"
    CompileProject "$slndir\CommonLib.Tests\CommonLib.Tests.csproj"
    CompileProject "$slndir\UserDataLayer\UserDataLayer.csproj"
    CompileProject "$slndir\CalneaDataLayer\CalneaDataLayer.csproj"
    CompileProject "$slndir\RouteDebugger\RouteDebugger.csproj"
    CompileProject "$slndir\Calnea.BusinessLayer\Calnea.BusinessLayer.csproj"
    CompileProject "$slndir\Calnea.BusinessLayer.Tests\Calnea.BusinessLayer.Tests.csproj"
    CompileProject "$slndir\XmlOrdersService\XmlOrdersService.csproj"
    CompileProject "$slndir\ServiceRequest.Utils\ServiceRequest.Utils.csproj"
    # These publications rely on a target in the project called PublishToFileSystem.
    PublishProject2 "$slndir\MailerWCFService\MailerWCFService.csproj"
    PublishProject2 "$slndir\CalneaV3\CalneaV3.csproj"
    PublishProject2 "$slndir\ErrorHandlerWebService\ErrorHandlerWebService.csproj"
    PublishProject2 "$slndir\XmlOrdersSite\XmlOrdersSite.csproj"

    # Copy the NT service into the publication folder.
    SafeDeleteFile "$pubdir\XmlOrdersService"
    EnsureFolder "$pubdir\XmlOrdersService"
    Copy-Item "$slndir\XmlOrdersService\bin\$config\*.*" "$pubdir\XmlOrdersService" -recurse
    Message "Copied XmlOrdersService to $pubdir\XmlOrdersService"
}

# Test to see whether a website exists (within IIS, not within the filesystem).
function WebSiteExists($name)
{
    $site = Get-WebSite | where { $_.Name -eq $name }
    if ($site -eq $null)
    {
        return $false
    }
    return $true
}

# Test to see whether an app pool exists.
function AppPoolExists($name)
{
    cd IIS:\AppPools\
    Test-Path $name -pathType container
}

# Create web sites if they do not exist.
function CreateAllWebSites()
{
}

# Allows the user to pick a configuration to build.
function PickConfiguration()
{
    Do {
        Write-Host "
        ------ Pick Configuration To Publish ------
        1 = Debug
        2 = Release
        -------------------------------------------"
        
        $choice = read-host -prompt "Select number or just type Foo to build the Foo configuration"
    } until ($choice -eq "1" -or $choice -eq "2" -or $choice -ne "")
    
    switch ($choice)
    {
        "1" { return "Debug" }
        "2" { return "Release" }
        default { return $choice }
    }
}
