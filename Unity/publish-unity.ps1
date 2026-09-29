<#
.SYNOPSIS
	Publishes SkiaGum + GumCommon for netstandard2.1 into the Gum Demo Unity project's Assets/Gum/DLLs.

.DESCRIPTION
	Takes no parameters; run it directly.

	Runs `dotnet publish` on Runtimes/SkiaGum for netstandard2.1, which gathers SkiaGum, GumCommon and
	every managed NuGet dependency (System.Text.Json, SkiaSharp, Svg.Skia, ...). A plain `dotnet build`
	does not: a library's bin folder never holds its package dependencies. The publish goes to a temporary
	folder that is deleted afterwards.

	The files are copied into Unity/Demo/Gum Demo/Assets/Gum/DLLs, overwritten in place so Unity keeps
	their .meta files (GUIDs and plugin import settings). A DLL that a previous run copied but this
	publish no longer produces is removed along with its .meta.

	DLLs that Unity already provides are left out (see $unityProvidedAssemblies). Not handled yet:
	SkiaSharp's native libSkiaSharp is not part of a netstandard2.1 publish; add it to Unity per platform
	separately.

.EXAMPLE
	.\Unity\publish-unity.ps1
#>
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$repoRoot = Split-Path -Parent $PSScriptRoot
$project = Join-Path $repoRoot 'Runtimes/SkiaGum/SkiaGum.csproj'
$destination = Join-Path $PSScriptRoot 'Demo/Gum Demo/Assets/Gum/DLLs'
$publishDirectory = Join-Path ([System.IO.Path]::GetTempPath()) ('gum-unity-publish-' + [guid]::NewGuid().ToString('N'))

# Unity has no use for these: .deps.json is .NET host metadata. The .pdb/.xml stay, for debugging and
# IDE doc tooltips.
$excludedPatterns = @('*.deps.json')

# Unity 6 compiles editor code against its .NET Framework 4.8 reference assemblies
# (Editor/Data/UnityReferenceAssemblies/unity-4.8-api), which already contain these. Shipping our copies
# too fails compilation with CS1703 (multiple assemblies with equivalent identity). Unity's Mono runtime
# provides them at run time.
$unityProvidedAssemblies = @(
	'Microsoft.CSharp.dll',
	'System.Buffers.dll',
	'System.Data.DataSetExtensions.dll',
	'System.Memory.dll',
	'System.Numerics.Vectors.dll',
	'System.Threading.Tasks.Extensions.dll'
)

try {
	# GeneratePackageOnBuild=false: GumCommon and SkiaGum pack a .nupkg on every build otherwise.
	& dotnet publish $project -f netstandard2.1 -c Release -o $publishDirectory -p:GeneratePackageOnBuild=false
	if ($LASTEXITCODE -ne 0) {
		throw "dotnet publish failed with exit code $LASTEXITCODE."
	}

	foreach ($pattern in ($excludedPatterns + $unityProvidedAssemblies)) {
		Remove-Item -Path (Join-Path $publishDirectory $pattern) -Force
	}

	$published = @(Get-ChildItem -Path $publishDirectory -File)

	New-Item -ItemType Directory -Path $destination -Force | Out-Null

	foreach ($file in $published) {
		Copy-Item -Path $file.FullName -Destination $destination -Force
	}

	# Remove what an earlier run copied but this publish no longer produces, with its .meta. Everything
	# else in the folder (the .meta files of current DLLs) is left alone.
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
}
finally {
	if (Test-Path $publishDirectory) {
		Remove-Item -Path $publishDirectory -Recurse -Force
	}
}
