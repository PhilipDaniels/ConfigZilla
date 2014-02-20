function Get-SolutionDir
{
    if ($dte.Solution -and $dte.Solution.IsOpen) {
        return Split-Path $dte.Solution.Properties.Item("Path").Value
    }
    else {
        throw "Solution not available"
    }
}

function ConfigZillaDestFolder
{
	$solutionDir = Get-SolutionDir
	$destProjectDir = $solutionDir + "\ConfigZilla"
	return $destProjectDir
}

function ConfigZillaProjectName
{
	return "{0}\ConfigZilla.csproj" -f (ConfigZillaDestFolder)
}

function ConfigZillaDestFolderExists
{
	# Test to see whether the ConfigZilla folder exists on disk.
	Write-Host ("Testing for folder {0}" -f (ConfigZillaDestFolder))
	if (test-path -pathtype container (ConfigZillaDestFolder))
	{
		return $true
	} 
	else
	{
		return $false
	}
}

function ConfigZillaProjectReference
{
	return $DTE.Solution.Projects | where-object {$_.Name -eq "ConfigZilla"}
}

function ConfigZillaProjectExistsInSolution
{
	return (ConfigZillaProjectReference) -ne $null
}

function CopyConfigZillaFolder
{
	$scriptPath = split-path $script:MyInvocation.MyCommand.Path
	$srcProjectDir = $scriptPath + "\ConfigZilla"

	# Copy the starter ConfigZilla project up to the root of the solution.
	Write-Host ("Copying {0} to {1}" -f $srcProjectDir, (ConfigZillaDestFolder))

	copy-item $srcProjectDir -destination $ConfigZillaDestFolder -recurse -container
}

function AddConfigZillaProjectToSolution
{
	$solution = Get-Interface $dte.Solution ([EnvDTE80.Solution2])
	$projectFile = $solution.AddFromFile((ConfigZillaProjectName), $false)
	Write-Host "ConfigZilla added to the solution."
}

function RemoveConfigZillaProjectFromSolution
{
	$solution = Get-Interface $dte.Solution ([EnvDTE80.Solution2])
	$project = (ConfigZillaProjectReference)
	$solution.Remove($project)
}

function UpdateAssemblyReferenceInConfigZillaProject
{
	param(
        [parameter(Position = 0, Mandatory = $true)]
        [string]$ToolsPath
    )

	$projectFile = "{0}\ConfigZillaCreateXslt.targets" -f (ConfigZillaDestFolder)
	Write-Host "Processing file $projectFile"

	(Get-Content $projectFile) -replace "ASSEMBLYHERE","$ToolsPath\ConfigZilla.Tasks.dll" | Out-File $projectFile
}

function AddConfigZillaProjectAsReference
{
	param(
        [parameter(Position = 0, Mandatory = $true)]
        $CurrentProject
    )

	$ConfigZillaProject = ConfigZillaProjectReference
	$CurrentProject.Object.References.AddProject($ConfigZillaProject)
}

function RemoveConfigZillaProjectReference
{
	param(
        [parameter(Position = 0, Mandatory = $true)]
        $CurrentProject
    )

	$CurrentProject.Object.References | Where-Object { $_.Name -eq 'ConfigZilla' } | ForEach-Object { $_.Remove() }
}

Export-ModuleMember Get-SolutionDir, ConfigZillaDestFolder, ConfigZillaProjectName, ConfigZillaDestFolderExists, `
	ConfigZillaProjectReference, ConfigZillaProjectExistsInSolution, CopyConfigZillaFolder, `
	AddConfigZillaProjectToSolution, RemoveConfigZillaProjectFromSolution, UpdateAssemblyReferenceInConfigZillaProject, `
	AddConfigZillaProjectAsReference, RemoveConfigZillaProjectReference

