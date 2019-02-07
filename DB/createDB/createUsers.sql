USE [master]
GO
drop login [userdb];
drop login [admindb];

--connection login to user
CREATE LOGIN [userdb] WITH PASSWORD=N'userdb',
 DEFAULT_DATABASE=[PipDietDB], DEFAULT_LANGUAGE=[русский], 
 CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
GO
ALTER LOGIN [userdb] ENABLE
GO

--connection login to admin
CREATE LOGIN [admindb] WITH PASSWORD=N'admindb',
 DEFAULT_DATABASE=[PipDietDB], DEFAULT_LANGUAGE=[русский], 
 CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
GO
ALTER LOGIN [admindb] ENABLE
GO


use PipDietDB;

drop user [user];
drop user [admin];


CREATE USER [user] FOR LOGIN [userdb];  
GO  
CREATE USER [admin] FOR LOGIN [admindb];  
GO  

--@"Data Source=DESKTOP-FFV5E68\SQLEXPRESS;Integrated Security=False;User ID=userdb;Password=userdb;ApplicationIntent=ReadWrite;"))
--@"Data Source=DESKTOP-FFV5E68\SQLEXPRESS;Integrated Security=False;User ID=admindb;Password=admindb;ApplicationIntent=ReadWrite;"))


