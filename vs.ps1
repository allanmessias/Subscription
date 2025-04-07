# Caminhos possíveis do VS 2022
$possiblePaths = @(
    "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\devenv.exe",
    "C:\Program Files\Microsoft Visual Studio\2022\Professional\Common7\IDE\devenv.exe",
    "C:\Program Files\Microsoft Visual Studio\2022\Enterprise\Common7\IDE\devenv.exe"
)

# Pega o caminho do VS instalado
$vsPath = $possiblePaths | Where-Object { Test-Path $_ } | Select-Object -First 1

if (-not $vsPath) {
    Write-Host "Visual Studio 2022 não encontrado." -ForegroundColor Red
    exit 1
}

# Se nenhum argumento for passado, usa o diretório atual
$target = if ($args.Count -gt 0) { $args[0] } else { "." }

Start-Process $vsPath -ArgumentList $target