$version="0.3.5"
$accessToken="72081693-2b21-4533-a5c8-4012bab562a9"
$paths=ls src | select Name
for($x=0;$x -lt $paths.length; $x=$x+1)
{
$fp=-join ("src\",$paths[$x].Name,"\bin\Release\",$paths[$x].Name,".",$version,".nupkg");

dotnet nuget push $fp -k $accessToken -s https://www.myget.org/F/objdesign/api/v3/index.json
}