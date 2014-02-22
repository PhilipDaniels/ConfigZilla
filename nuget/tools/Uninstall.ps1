param($installPath, $toolsPath, $package, $project)

# Write-Host "ConfigZilla Uninstall.ps1 (project level un-initialisation) is running."
# Import-Module (Join-Path $toolsPath "ConfigZilla.psm1")

# Actually, it's debatable whether to do this. I think I will let the user do it manually
# because if they are using any of the classes from ConfigZilla removing the reference
# will stop their project from compiling.
# RemoveConfigZillaProjectReference($project)
