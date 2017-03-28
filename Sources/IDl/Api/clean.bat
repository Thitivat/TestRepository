@echo off

echo About to recursively remove all bin, obj, subfolders... 
echo Remove the /q switch to disable quiet mode and require confirmation for deletion.
echo Also, try prefixing the command with start in case the command refuses to run in a batch file.
echo Deleting..

for /d /r . %%d in (bin, obj, packages) do @if exist "%%d" rd /s/q "%%d"

echo Successfully deleted bin obj packages folders
