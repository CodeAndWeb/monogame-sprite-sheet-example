param($installPath, $toolsPath, $package, $project) 
$path = [System.IO.Path]
$readmefile = $path::Combine($installPath, "readme.txt") 
$DTE.ItemOperations.OpenFile($readmefile) 