@echo off
setlocal
:PROMPT
SET /P SURE=Are you sure you want to RESTORE? (y/[n])?: 
IF /I "%SURE%" NEQ "y" GOTO END

dotnet schemazen create --server DESKTOP-HMO05CP\SQLEXPRESS --database LFZ_TruckPark_W --verbose --overwrite --scriptDir ./DB_Dump

PAUSE
:END
endlocal