@echo off
setlocal
:PROMPT
SET /P SURE=Are you sure you want to DUMP? (y/[n])?: 
IF /I "%SURE%" NEQ "y" GOTO END

dotnet schemazen script --server DESKTOP-HMO05CP\SQLEXPRESS --database LFZ_TruckPark_W --dataTablesPattern=(.*) --verbose --overwrite --scriptDir ./DB_Dump

PAUSE
:END
endlocal