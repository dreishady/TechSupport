@echo off

rem Change directory to MS SQL Server 13.0 (v851)
cd "%ProgramFiles%\Microsoft SQL Server\130\LocalDB\Binn"

rem Stop the existing server and delete it
SqlLocalDB stop   "MSSQLLocalDB"
SqlLocalDB delete "MSSQLLocalDB"

rem Create new server using v851
SqlLocalDB create "MSSQLLocalDB"
