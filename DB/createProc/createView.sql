----------------------------------------
--AllDietView---------------------------
----------------------------------------
go
 alter view [dbo].[DietView] 
 (UserID,DietID,MealID,DietNum,MealNum,ProdID,NameProd,Amount,Kkal)
 as select 
 a.UserID,a.NumberID,a.MealID,a.DayNumber,a.MealNumber,
 b.ProductID,c.Name,b.Amount,a.Kkal 
 from 
 [dbo].[tblDiet] a, [dbo].[tblMeal] b, [dbo].[tblKkal] c
 where
 a.MealID=b.MealID and b.ProductID=c.ProductID;

 --select * from [dbo].[DietView];
 --------------------------------------
 --MealSView-------------------------
 --------------------------------------
 go
 create view [dbo].[MealView] 
 (UserID,MealID,ProductID,ProductName,Amount,Kkal)
 as select 
 c.UserID,a.ID,a.ProductID,b.Name,a.Amount, (b.Kkal/100*a.Amount)
 from 
 [dbo].[tblMealService] a, [dbo].[tblKkal] b, [dbo].[tblUser] c
 where
 b.ProductID=a.ProductID and c.UserID=a.UserID;
 --select * from [dbo].[MealView];
 --------------------------------------
 --KkalView-------------------------
 --------------------------------------
 go
 alter view [dbo].[KkalView] 
 (ProductID,UserID,Username,ProductName,Kkal)
 as select 
 b.ProductID,b.UserID,c.Username,b.Name,b.Kkal 
 from 
 [dbo].[tblKkal] b, [dbo].[tblUser] c
 where
 b.UserID=c.UserID;
 --order by b.UserID asc;
 --select * from [dbo].[KkalView];
 --------------------------------------
 --MealSDateView-------------------------
 --------------------------------------
 go
 create view [dbo].[MealDateView] 
 ([UserID],[Date],[Kkal])
 as select 
 c.[UserID],a.[Date], Sum(b.Kkal/100*a.Amount)
 from 
 [dbo].[tblMealService] a join [dbo].[tblKkal] b
 on a. = b.
 join [dbo].[tblUser] c
 on c. = 
 where
 b.ProductID=a.ProductID and c.UserID=a.UserID;
 --select * from [dbo].[MealView];