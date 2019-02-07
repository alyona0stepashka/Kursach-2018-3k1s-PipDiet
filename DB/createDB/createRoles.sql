use PipDietDB;

-------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------[PipDietDB_user_Role]--------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------------------

DROP ROLE [PipDietDB_user_Role];
CREATE ROLE [PipDietDB_user_Role];
--GRANT EXECUTE ON [dbo].[AddNewUser]  TO [PipDietDB_user_Role]; --admin
GRANT EXECUTE ON [dbo].[AddNewWeightS]  TO [PipDietDB_user_Role];
GRANT EXECUTE ON [dbo].[AddNewMealS]  TO [PipDietDB_user_Role];
GRANT EXECUTE ON [dbo].[AddNewSizeS]  TO [PipDietDB_user_Role];
GRANT EXECUTE ON [dbo].[AddNewProduct]  TO [PipDietDB_user_Role];
GRANT EXECUTE ON [dbo].[AddNewDietRecord]  TO [PipDietDB_user_Role];
GRANT EXECUTE ON [dbo].[AddNewMeal]  TO [PipDietDB_user_Role];

--GRANT EXECUTE ON [dbo].[SelectUser]  TO [PipDietDB_user_Role]; --admin?? 
--GRANT EXECUTE ON [dbo].[SelectAllUser]  TO [PipDietDB_admin_Role]; --admin
GRANT EXECUTE ON [dbo].[SelectUsersProduct]  TO [PipDietDB_user_Role];
GRANT EXECUTE ON [dbo].[SelectKkal]  TO [PipDietDB_user_Role];
GRANT EXECUTE ON [dbo].[SelectAllProducts]  TO [PipDietDB_user_Role];

--GRANT EXECUTE ON [dbo].[DeleteUser]  TO [PipDietDB_user_Role]; --admin
--GRANT EXECUTE ON [dbo].[DeleteSizeS]  TO [PipDietDB_user_Role]; --admin
--GRANT EXECUTE ON [dbo].[DeleteWeightS]  TO [PipDietDB_user_Role]; --admin
--GRANT EXECUTE ON [dbo].[DeleteMealS]  TO [PipDietDB_user_Role]; --admin
GRANT EXECUTE ON [dbo].[DeleteMeal]  TO [PipDietDB_user_Role];
GRANT EXECUTE ON [dbo].[DeleteDiet]  TO [PipDietDB_user_Role];
--GRANT EXECUTE ON [dbo].[DeleteKkal]  TO [PipDietDB_user_Role]; --admin??
--GRANT EXECUTE ON [dbo].[DeleteKkalByID]  TO [PipDietDB_user_Role]; --admin??

GRANT EXECUTE ON [dbo].[SelectDietKkalAv] TO [PipDietDB_user_Role]; 

sp_addrolemember 'PipDietDB_user_Role', 'user';  

-------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------[PipDietDB_admin]---------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------------------


DROP ROLE [PipDietDB_admin_Role];
CREATE ROLE [PipDietDB_admin_Role];
GRANT EXECUTE ON [dbo].[AddNewUser]  TO [PipDietDB_admin_Role];  
GRANT EXECUTE ON [dbo].[AddNewWeightS]  TO [PipDietDB_admin_Role];
GRANT EXECUTE ON [dbo].[AddNewMealS]  TO [PipDietDB_admin_Role];
GRANT EXECUTE ON [dbo].[AddNewSizeS]  TO [PipDietDB_admin_Role];
GRANT EXECUTE ON [dbo].[AddNewProduct]  TO [PipDietDB_admin_Role];
GRANT EXECUTE ON [dbo].[AddNewDietRecord]  TO [PipDietDB_admin_Role];
GRANT EXECUTE ON [dbo].[AddNewMeal]  TO [PipDietDB_admin_Role];

GRANT EXECUTE ON [dbo].[SelectUser]  TO [PipDietDB_admin_Role]; 
GRANT EXECUTE ON [dbo].[SelectAllUser]  TO [PipDietDB_admin_Role]; 
GRANT EXECUTE ON [dbo].[SelectUsersProduct]  TO [PipDietDB_admin_Role];
GRANT EXECUTE ON [dbo].[SelectKkal]  TO [PipDietDB_admin_Role];
GRANT EXECUTE ON [dbo].[SelectAllProducts]  TO [PipDietDB_admin_Role];

GRANT EXECUTE ON [dbo].[DeleteUser]  TO [PipDietDB_admin_Role]; 
GRANT EXECUTE ON [dbo].[DeleteSizeS]  TO [PipDietDB_admin_Role];  
GRANT EXECUTE ON [dbo].[DeleteWeightS]  TO [PipDietDB_admin_Role];  
GRANT EXECUTE ON [dbo].[DeleteMealS]  TO [PipDietDB_admin_Role]; 
GRANT EXECUTE ON [dbo].[DeleteMeal]  TO [PipDietDB_admin_Role];
GRANT EXECUTE ON [dbo].[DeleteDiet]  TO [PipDietDB_admin_Role];
GRANT EXECUTE ON [dbo].[DeleteKkal]  TO [PipDietDB_admin_Role]; 
GRANT EXECUTE ON [dbo].[DeleteKkalByID]  TO [PipDietDB_admin_Role]; 
 
GRANT EXECUTE ON [dbo].[SelectDietKkalAv] TO [PipDietDB_user_Role];

GRANT EXECUTE ON SCHEMA::[dbo] TO [admin];

sp_addrolemember 'PipDietDB_admin_Role', 'admin';  
