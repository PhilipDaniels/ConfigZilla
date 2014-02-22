param($installPath, $toolsPath, $package, $project)

Write-Host "ConfigZilla Install.ps1 (project level initialisation) is running."

Import-Module (Join-Path $toolsPath "ConfigZilla.psm1")


function Main
{
	if (ConfigZillaDestFolderExists)
	{
		Write-Host ("The ConfigZilla folder already exists at {0}, NOT copying again." -f (ConfigZillaDestFolder))
	}
	else
	{
		CopyConfigZillaFolder
		# This is no longer necessary because the ConfigZilla.Tasks.dll assembly is
		# copied into the ConfigZilla folder during the NuGet build process.
		# UpdateAssemblyReferenceInConfigZillaProject($toolsPath)
	}

	if (ConfigZillaProjectExistsInSolution)
	{
		Write-Host "The ConfigZilla project is already installed in this solution, NOT adding again."
	}
	else
	{
		AddConfigZillaProjectToSolution
	}


	AddConfigZillaProjectAsReference($project)
}

Main
