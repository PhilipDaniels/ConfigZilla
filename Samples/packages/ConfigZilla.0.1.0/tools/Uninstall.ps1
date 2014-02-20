param($installPath, $toolsPath, $package, $project)

Write-Host "ConfigZilla Uninstall.ps1 (project level un-initialisation) is running."

Import-Module (Join-Path $toolsPath "ConfigZilla.psm1")


function Main
{
	RemoveConfigZillaProjectReference($project)
}

Main
