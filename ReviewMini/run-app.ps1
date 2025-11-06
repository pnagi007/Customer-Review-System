$env:ASPNETCORE_URLS="http://localhost:5000"
$env:ASPNETCORE_ENVIRONMENT="Development"

# Start the web app in a new window
Start-Process powershell -ArgumentList "dotnet run" -WorkingDirectory (Get-Location)

# Wait a moment for the app to start
Start-Sleep -Seconds 3

# Open the browser
Start-Process "http://localhost:5000"