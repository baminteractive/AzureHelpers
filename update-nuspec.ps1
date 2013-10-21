git status

$file = 'AzureHelpers.nuspec'
$x = [xml] (Get-Content $file)
Select-Xml -xml $x  -XPath //package/metadata/releaseNotes |
    % { $_.Node.'#text' = 'asdf' }
$x.Save($file)