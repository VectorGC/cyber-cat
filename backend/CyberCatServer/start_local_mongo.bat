rem set mongo_path=%~dp0\External\mongodb-win32-x86_64-2012plus-4.2.20
set mongo_path=%~dp0\External\mongodb-win32-x86_64-2008plus-ssl-4.0.4
mkdir %mongo_path%\data\db
%mongo_path%\bin\mongod.exe --dbpath %mongo_path%\data\db