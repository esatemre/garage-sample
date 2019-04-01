$src = (Get-Item -Path ".\" -Verbose).FullName;

Get-ChildItem $src -directory | where {$_.PsIsContainer} | Select-Object -Property Name | ForEach-Object {
    $cdProjectDir = [string]::Format("cd /d {0}\{1}",$src, $_.Name);

    $projectDir = [string]::Format("{0}\{1}\appsettings.json",$src, $_.Name); 
    $fileExists = Test-Path $projectDir;
    
    if($fileExists -eq $true){
        $params=@("/C"; $cdProjectDir; " && dotnet run"; )
        Start-Process -Verb runas "cmd.exe" $params;
    }
} 