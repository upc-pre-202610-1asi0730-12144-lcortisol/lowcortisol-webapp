param(
  [string]$OutputFile = "frontend-structure-clean.txt"
)

$ErrorActionPreference = "Stop"

$ProjectRoot = if ($PSScriptRoot) { $PSScriptRoot } else { (Get-Location).Path }
$ProjectRoot = [System.IO.Path]::GetFullPath($ProjectRoot)
$outputPath = Join-Path $ProjectRoot $OutputFile

$excludedDirectories = @(
  "node_modules",
  "dist",
  "dist-ssr",
  "bin",
  "obj",
  ".tmp",
  ".git",
  ".idea",
  ".vscode",
  "TestResults",
  "coverage",
  "logs",
  "cypress/videos",
  "cypress/screenshots",
  "__screenshots__"
)

$excludedFileNames = @(
  "package-lock.json",
  "pnpm-lock.yaml",
  "yarn.lock",
  "frontend-code.txt",
  "frontend-code-clean.txt",
  "frontend-structure.txt",
  "frontend-structure-clean.txt",
  "export-frontend-code.ps1",
  "export-frontend-structure.ps1"
)

function Get-RelativePath {
  param([string]$Path)

  $fullPath = [System.IO.Path]::GetFullPath($Path)
  $rootPath = $ProjectRoot.TrimEnd("\", "/")

  if ($fullPath.StartsWith($rootPath, [System.StringComparison]::OrdinalIgnoreCase)) {
    return $fullPath.Substring($rootPath.Length).TrimStart("\", "/")
  }

  return $fullPath
}

function Test-IsExcludedPath {
  param([string]$Path)

  $relativePath = (Get-RelativePath $Path).Replace("\", "/")
  $wrappedPath = "/$relativePath/"

  foreach ($directory in $excludedDirectories) {
    $normalizedDirectory = $directory.Trim("/", "\").Replace("\", "/")

    if ($wrappedPath -like "*/$normalizedDirectory/*") {
      return $true
    }
  }

  return $false
}

$utf8NoBom = New-Object System.Text.UTF8Encoding($false)
$writer = [System.IO.StreamWriter]::new($outputPath, $false, $utf8NoBom)

try {
  $writer.WriteLine("============================================================")
  $writer.WriteLine("LOWCORTISOL FRONTEND STRUCTURE EXPORT")
  $writer.WriteLine("Stack: Vue + JavaScript + Vite")
  $writer.WriteLine("Generated at: $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')")
  $writer.WriteLine("Root: $ProjectRoot")
  $writer.WriteLine("Output: $OutputFile")
  $writer.WriteLine("============================================================")
  $writer.WriteLine("")

  Get-ChildItem -LiteralPath $ProjectRoot -Recurse -Force |
    Where-Object {
      $_.FullName -ne $outputPath -and
      -not (Test-IsExcludedPath $_.FullName) -and
      -not ($excludedFileNames -contains $_.Name)
    } |
    Sort-Object FullName |
    ForEach-Object {
      $relativePath = Get-RelativePath $_.FullName

      if ($_.PSIsContainer) {
        $writer.WriteLine("[DIR]  $relativePath")
      } else {
        $writer.WriteLine("[FILE] $relativePath")
      }
    }
}
finally {
  $writer.Dispose()
}

Write-Host "Frontend structure exported to $outputPath"
