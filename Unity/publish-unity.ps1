<#
.SYNOPSIS
	Publishes SkiaGum + GumCommon for netstandard2.1 and optionally copies the DLLs into a Unity project.

.DESCRIPTION
	Runs `dotnet publish` on Runtimes/SkiaGum for netstandard2.1, which gathers SkiaGum, GumCommon and
	every managed NuGet dependency (System.Text.Json, SkiaSharp, Svg.Skia, ...) into one folder. A plain
	`dotnet build` does not: a library's bin folder never holds its package dependencies.

	With -UnityProject, the published files are copied into <UnityProject>/Assets/Plugins/Gum. Files are
	overwritten in place so Unity keeps their .meta files (GUIDs and plugin import settings). A DLL that a
	previous run copied but this publish no longer produces is removed along with its .meta.

	Not handled yet: DLLs that Unity itself already provides (System.Memory, System.Buffers,
	System.Runtime.CompilerServices.Unsafe, Microsoft.CSharp, ...) are copied anyway, and SkiaSharp's
	native libSkiaSharp is not part of a netstandard2.1 publish; add it to Unity per platform separately.

.PARAMETER Configuration
	Build configuration. Defaults to Release.

.PARAMETER OutputDirectory
	Where dotnet publish writes. Defaults to Unity/Build, next to this script (git-ignored). It is emptied
	first so no file from an earlier publish lingers.

.PARAMETER UnityProject
	Optional path to a Unity project (the folder containing Assets/). When set, the output is copied into
	Assets/Plugins/Gum.

.EXAMPLE
	.\Unity\publish-unity.ps1

.EXAMPLE
	.\Unity\publish-unity.ps1 -UnityProject D:\Unity\MyGame
#>
[CmdletBinding()]
param(
	[string]$Configuration = 'Release',
	[string]$OutputDirectory,
	[string]$UnityProject
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$repoRoot = Split-Path -Parent $PSScriptRoot
$project = Join-Path $repoRoot 'Runtimes/SkiaGum/SkiaGum.csproj'

if (-not $OutputDirectory) {
	$OutputDirectory = Join-Path $PSScriptRoot 'Build'
}

# Unity has no use for these: .deps.json is .NET host metadata. The .pdb/.xml stay, for debugging and
# IDE doc tooltips.
$excludedPatterns = @('*.deps.json')

if (Test-Path $OutputDirectory) {
	Remove-Item -Path (Join-Path $OutputDirectory '*') -Recurse -Force
}

# GeneratePackageOnBuild=false: GumCommon and SkiaGum pack a .nupkg on every build otherwise.
& dotnet publish $project -f netstandard2.1 -c $Configuration -o $OutputDirectory -p:GeneratePackageOnBuild=false
if ($LASTEXITCODE -ne 0) {
	throw "dotnet publish failed with exit code $LASTEXITCODE."
}

foreach ($pattern in $excludedPatterns) {
	Remove-Item -Path (Join-Path $OutputDirectory $pattern) -Force
}

$published = @(Get-ChildItem -Path $OutputDirectory -File)

Write-Host "Published $($published.Count) files to $OutputDirectory"

if (-not $UnityProject) {
	return
}

if (-not (Test-Path (Join-Path $UnityProject 'Assets'))) {
	throw "'$UnityProject' is not a Unity project: it has no Assets folder."
}

$destination = Join-Path $UnityProject 'Assets/Plugins/Gum'
New-Item -ItemType Directory -Path $destination -Force | Out-Null

foreach ($file in $published) {
	Copy-Item -Path $file.FullName -Destination $destination -Force
}

# Remove what an earlier run copied but this publish no longer produces, with its .meta. Everything else
# in the folder (the .meta files of current DLLs) is left alone.
foreach ($stale in (Get-ChildItem -Path $destination -File | Where-Object { $_.Extension -ne '.meta' })) {
	if ($published.Name -notcontains $stale.Name) {
		Remove-Item $stale.FullName -Force
		$meta = "$($stale.FullName).meta"
		if (Test-Path $meta) {
			Remove-Item $meta -Force
		}
		Write-Host "Removed stale $($stale.Name)"
	}
}

Write-Host "Copied $($published.Count) files to $destination"
