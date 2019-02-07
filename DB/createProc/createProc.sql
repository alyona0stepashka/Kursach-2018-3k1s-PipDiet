use PipDietDB;


insert into [dbo].[tblWeightService]([UserID],[Weight],[Date])
values ( 7,60,'2018-12-04');

--exec DeleteUser 1;
--select * from tblUser;
--select * from tblKkal;
--select * from tblDiet;
--select * from tblMeal;
--select * from tblMealService;
--select * from tblWeightService;
--select * from tblSizeService;

--delete from tblMeal;
--delete from tblDiet;

---------------------------------------------------
--[SelectKkalMeal] ---------
--IF OBJECT_ID('[dbo].[SelectKkalMeal]') IS NOT NULL
--BEGIN 
--    DROP function [dbo].[SelectKkalMeal] 
--END 
--go
--CREATE FUNCTION SelectKkalMeal 
--(@prodid int, @amount int) returns int 
--begin 
--declare @kk int=0;
--set @kk = select [Kkal] from [dbo].[tblKkal] where [ProductID]=@prodid

--        DECLARE @addCosts DEC (14,2), @sumBudget DEC(16,2)
--        SELECT @sumBudget = SUM (Budget) FROM Project
--        SET @addCosts = @sumBudget * @percent/100
--        RETURN @addCosts
--END;
---------------------------
---------------------------------------------------
--IF OBJECT_ID('[dbo].[SelectKkalMeal1]') IS NOT NULL
--BEGIN 
--    DROP function [dbo].[SelectKkalMeal1] 
--END 
--go
--create function [dbo].[SelectKkalMeal1]
--(@prodid int, @amount int) returns int 
--begin 
--declare @kk int=0;
--declare @kk2 int =0;
--DECLARE @CURSOR CURSOR 
--SET @CURSOR  = CURSOR SCROLL
--FOR
--SELECT  [ProductID],[Name],[Kkal] FROM  [dbo].[tblKkal] WHERE  [ProductID]=@prodid 
--OPEN @CURSOR 
--FETCH NEXT FROM @CURSOR INTO @prodId, @Name, @kkal 
--WHILE @@FETCH_STATUS = 0
--BEGIN 
--set kk2 = select [Kkal]
--FETCH NEXT FROM @CURSOR INTO @prodId, @Name, @kkal 
--END
--CLOSE @CURSOR
--return @kk;
--end;
---------------------------------------------------
--[AddNewWeightS] ----------------------------------
------------------------------------------------------------------------
-----------------------------------------------------------------
--exec [dbo].[ReportSize] 7;
IF OBJECT_ID('[dbo].[ReportSize]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[ReportSize] 
END 
go
create procedure [dbo].[ReportSize]
(
@userid int 
) 
as begin
begin try
--begin tran 
 
select @userid as [UserIDRep], [Date] as [DateRep], [Size1] as [s1], [Size2] as [s2], [Size3] as [s3], [Size4] as [s4] from [dbo].[tblSizeService] where [UserID]=@userid; 
--commit;
end try
begin catch
print error_message()
 --rollback;
end catch
end;

---------------------------------------------------
--[AddNewWeightS] ----------------------------------
---------------------------------------------------
--select * from [dbo].[SelectKkByProd] (2);
--exec [dbo].[ReportDayAlreadyEatKkal] 7;
IF OBJECT_ID('[dbo].[ReportDayAlreadyEatKkal]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[ReportDayAlreadyEatKkal] 
END 
go
create procedure [dbo].[ReportDayAlreadyEatKkal]
(
@userid int 
) 
as begin
begin try
--begin tran 

declare @kkal decimal(18,0) =(select [dbo].[SelectOstKkalUser](@userid));
declare @kkalNorm decimal(18,0) = (select [DailyCaloriesNormNorm] from [dbo].[tblUser] where [UserID]=@userid);
declare @del decimal(18,0) = (@kkal/@kkalNorm)*100;
if (@del >100) set @del = 100;

select @userid as [UserIDReport],GetDate() as [DateReport],@kkal as [KkalReport],@kkalNorm as [KkalNorm],@del as [Result] ;

--commit;
end try
begin catch
print error_message()
 --rollback;
end catch
end;
---------------------------------------------------
--[AddNewWeightS] ----------------------------------
---------------------------------------------------

----select [Date],([Amount]*(select * from [dbo].[SelectKkalByProdID]([ProductID]))/100) from [dbo].[tblMealService] where [UserID]=7;
--exec [dbo].[ReportDayKkal] 7;
--select * from [dbo].[SelectKkByProd] (2); 
IF OBJECT_ID('[dbo].[ReportDayKKal]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[ReportDayKKal] 
END 
go
create procedure [dbo].[ReportDayKKal]
(
@userid int 
) 
as begin
begin try
--begin tran 
--select [Date] as [Date],([Amount]*( (select * from (select [Kkal] from [dbo].[tblKkal] where [ProductID]=@prodid))/100)) as [Amount] from [dbo].[tblMealService] where [UserID]=@userid;

--create table ##tbl
--(
--[UserID] int,
--[DateReport] date,
--[AmountKkalReport] decimal(18,0)
--);
--select [UserID] as [UserID],[Date] as [DateReport],([Amount]*( (select [dbo].[SelectKkByProd]([ProductID]))/100)) as [AmountKkalReport] into ##tbl from [dbo].[tblMealService] where [UserID]=@userid;
--select sum([AmountKkalReport]) as [SumKkal],[DateReport],[UserID] from ##tbl;

select [UserID],[Date],([Amount]*( (select [dbo].[SelectKkByProd]([ProductID]))/100)) as [AmountKkalReport] from [dbo].[tblMealService] where [UserID]=@userid;


--commit;
end try
begin catch
print error_message()
 --rollback;
end catch
end;
---------------------------------------------------
--[AddNewWeightS] ----------------------------------
---------------------------------------------------
--exec [dbo].[ReportWeight] 7;
IF OBJECT_ID('[dbo].[ReportWeight]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[ReportWeight] 
END 
go
create procedure [dbo].[ReportWeight]
(
@userid int 
) 
as begin
begin try
begin tran 

select [UserID],[Weight],convert(date,[Date]) as [DateR] from [dbo].[tblWeightService] where [UserID]=@userid; 
commit;
end try
begin catch
print error_message()
 
end catch
end
---------------------------------------------------
--[SelectAllUser] --------------grant-------------- 
---------------------------------------------------
IF OBJECT_ID('[dbo].[SelectAllUser]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[SelectAllUser] 
END 
go
create procedure [dbo].[SelectAllUser] 
as begin
begin try
begin tran
select [UserId],[Username],[Admin] from [dbo].[tblUser];
commit;
end try
begin catch
print error_message()
-- 
end catch;
end;
---------------------------------------------------
--[SelectUser] ------------------------------------
---------------------------------------------------
IF OBJECT_ID('[dbo].[SelectUser]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[SelectUser] 
END 
go
create procedure [dbo].[SelectUser]
(
@username nvarchar(50),
@password nvarchar(50)
)
as begin
begin try
--begin tran
select * from [dbo].[tblUser] where [Username]=@username and [Password]=@password;
--commit;
end try
begin catch
print error_message()
 
end catch;
end;
---------------------------------------------------
--[SelectUser'sProduct] ---------------------------
---------------------------------------------------
IF OBJECT_ID('[dbo].[SelectUsersProduct]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[SelectUsersProduct] 
END 
go
create procedure [dbo].[SelectUsersProduct]
(
@userid int 
)
as begin
begin try
--begin tran
select * from [dbo].[tblKkal] where [UserID]=@userid or [UserID]=1;
--commit;
end try
begin catch
print error_message()
 
end catch;
end;
---------------------------------------------------
--[SelectKoef]-------------------------------------
---------------------------------------------------
IF OBJECT_ID('[dbo].[SelectKkal]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[SelectKkal] 
END 
go
create procedure [dbo].[SelectKkal]
(
@product nvarchar(50) ,
@userid int
)
as begin
begin try
--begin tran
select * from [dbo].[tblKkal] where [Name]=@product and ([UserID]=@userid or [UserID]=1);
--commit;
end try
begin catch
print error_message()
 
end catch;
end;
---------------------------------------------------
--[SelectAllProducts]------------------------------
---------------------------------------------------
IF OBJECT_ID('[dbo].[SelectAllProducts]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[SelectAllProducts] 
END 
go
create procedure [dbo].[SelectAllProducts]
as begin
begin try
--begin tran
select [ProductID],[UserID],[Name],[Kkal] 
from [dbo].[tblKkal];
--commit;
end try
begin catch
print error_message()
 
end catch;
end;
---------------------------------------------------
--[AddNewUser] ------------------------------------
---------------------------------------------------
IF OBJECT_ID('[dbo].[AddNewUser]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[AddNewUser] 
END 
go
create procedure [dbo].[AddNewUser]
(
@username nvarchar(50),
@password nvarchar(50),
@admin int,
@sex nvarchar(6),
@birth int,
@growth decimal(18,0),
@idealweight decimal(18,0),
@calories1 int,
@calories2 int,
@calories3 int
)
as begin
begin try
--begin tran
insert into [dbo].[tblUser]([Username],[Password],[Admin],[Sex],[Birthday],[Growth],[IdealWeight],[DailyCaloriesNormEasy],[DailyCaloriesNormNorm],[DailyCaloriesNormHard])
values (@username,@password,@admin,@sex,@birth,@growth,@idealweight,@calories1,@calories2,@calories3);
--declare @userid int = (select top(1) [UserID] from [dbo].[tblUser] where [Username]=@username order by [UserID] desc);
--commit;
end try
begin catch
print error_message()
 
end catch
end

---------------------------------------------------
--[AddNewWeightS] ----------------------------------
---------------------------------------------------
IF OBJECT_ID('[dbo].[AddNewWeightS]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[AddNewWeightS] 
END 
go
create procedure [dbo].[AddNewWeightS]
(
@userid int,
@weight decimal(18,0)
) 
as begin
begin try
--begin tran
declare @date date = GetDate();
insert into [dbo].[tblWeightService]([UserID],[Weight],[Date])
values (@userid,@weight,@date);
--commit;
end try
begin catch
print error_message()
 
end catch
end
---------------------------------------------------
--[AddNewMealS]------------------------------------
---------------------------------------------------
IF OBJECT_ID('[dbo].[AddNewMealS]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[AddNewMealS] 
END 
go
create procedure [dbo].[AddNewMealS]
(
@userid int,
@prodid int,
@amount decimal(18,0)
)
as begin
begin try
--begin tran
declare @date date = GetDate();
insert into [dbo].[tblMealService]([UserID],[ProductID],[Amount],[Date])
values (@userid,@prodid,@amount,@date);
--commit;
end try
begin catch
print error_message()
 
end catch
end
---------------------------------------------------
--[AddNewSizeS]------------------------------------
---------------------------------------------------
IF OBJECT_ID('[dbo].[AddNewSizeS]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[AddNewSizeS] 
END 
go
create procedure [dbo].[AddNewSizeS]
(
@userid int,
@size1 decimal(18,0),
@size2 decimal(18,0),
@size3 decimal(18,0),
@size4 decimal(18,0)
)
as begin
begin try
begin tran
declare @date date = GetDate();
insert into [dbo].[tblSizeService]([UserID],[Size1],[Size2],[Size3],[Size4],[Date])
values (@userid,@size1,@size2,@size3,@size4,@date);
commit;
end try
begin catch
print error_message()
 
end catch
end
---------------------------------------------------
--[AddNewProduct]------------------------------------
---------------------------------------------------
IF OBJECT_ID('[dbo].[AddNewProduct]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[AddNewProduct] 
END 
go
create procedure [dbo].[AddNewProduct]
(
@userid int,
@name nvarchar(50),	
@kkal int 
) 
as begin
begin try
begin tran
insert into [dbo].[tblKkal]([UserID],[Name],[Kkal])
values (@userid,@name,@kkal);
commit;
end try
begin catch
print error_message()
 
end catch
end 
---------------------------------------------------
--[AddNewDietRecord]-------------------------------
---------------------------------------------------
IF OBJECT_ID('[dbo].[AddNewDietRecord]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[AddNewDietRecord] 
END 
go
create procedure [dbo].[AddNewDietRecord]
(
@userid int,
@mealid int,
@kkal int,
@mealnum int,
@daynum int
)
as begin
begin try
begin tran
insert into [dbo].[tblDiet]([UserID],[MealID],[Kkal],[MealNumber],[DayNumber])
values (@userid,@mealid,@kkal,@mealnum,@daynum);
commit;
end try
begin catch
print error_message()
 
end catch
end

---------------------------------------------------
--[AddNewDietMeal]---------------------------------
---------------------------------------------------
IF OBJECT_ID('[dbo].[AddNewMeal]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[AddNewMeal] 
END 
go
create procedure [dbo].[AddNewMeal]
(
@prodid int,
@amount decimal(18,0)
)
as begin
begin try
begin tran
insert into [dbo].[tblMeal]([ProductID],[Amount])
values (@prodid,@amount);
commit;
end try
begin catch
print error_message()
 
end catch
end
---------------------------------------------------
--[DeleteUser]-------------------------------------
--------------------------------------------------- 
IF OBJECT_ID('[dbo].[DeleteUser]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[DeleteUser] 
END 
go
create procedure [dbo].[DeleteUser]
(
@userid int
)
as begin
begin try
begin tran
exec [dbo].[DeleteSizeS] @userid;
exec [dbo].[DeleteWeightS] @userid;
exec [dbo].[DeleteMealS] @userid;
exec [dbo].[DeleteDiet] @userid;
exec [dbo].[DeleteKkal] @userid;

delete from [dbo].[tblUser] where [UserID]=@userid;
commit;
end try
begin catch
print error_message()
 
end catch;
end;
--------------------------------------------------------
--[DeleteSizeS]-----------------------------------------
--------------------------------------------------------
IF OBJECT_ID('[dbo].[DeleteSizeS]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[DeleteSizeS] 
END 
go
create procedure [dbo].[DeleteSizeS]
(
@userid int
)
as begin
begin try
begin tran
delete from [dbo].[tblSizeService] where [UserID]=@userid;
commit;
end try
begin catch
print error_message()
 
end catch;
end;
--------------------------------------------------------
--[DeleteWeightS]-----------------------------------------
--------------------------------------------------------
IF OBJECT_ID('[dbo].[DeleteWeightS]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[DeleteWeightS] 
END 
go
create procedure [dbo].[DeleteWeightS]
(
@userid int
)
as begin
begin try
begin tran
delete from [dbo].[tblWeightService] where [UserID]=@userid;
commit;
end try
begin catch
print error_message()
 
end catch;
end;
--------------------------------------------------------
--[DeleteMealS]-----------------------------------------
--------------------------------------------------------
IF OBJECT_ID('[dbo].[DeleteMealS]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[DeleteMealS] 
END 
go
create procedure [dbo].[DeleteMealS]
(
@userid int
)
as begin
begin try
begin tran
delete from [dbo].[tblMealService] where [UserID]=@userid;
commit;
end try
begin catch
print error_message()
 
end catch;
end;
--------------------------------------------------------
--[DeleteMeal]------------------------------------------
--------------------------------------------------------
IF OBJECT_ID('[dbo].[DeleteMeal]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[DeleteMeal] 
END 
go
create procedure [dbo].[DeleteMeal]
(
@userid int
)
as begin
begin try
begin tran
delete from [dbo].[tblMeal] where [ProductID] in (select [ProductID] from [dbo].[tblKkal] where [UserID] = @userid);
commit;
end try
begin catch
print error_message()
 
end catch;
end;
--------------------------------------------------------
--[DeleteDiet]------------------------------------------
--------------------------------------------------------
IF OBJECT_ID('[dbo].[DeleteDiet]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[DeleteDiet] 
END 
go
create procedure [dbo].[DeleteDiet]
(
@userid int
)
as begin
begin try
begin tran
delete from [dbo].[tblDiet] where [UserID]=@userid;
commit;
end try
begin catch
print error_message()
 
end catch;
end;
--------------------------------------------------------
--[DeleteKkal]-----------------------------------------
--------------------------------------------------------
--exec [dbo].[DeleteKkal] 6;
IF OBJECT_ID('[dbo].[DeleteKkal]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[DeleteKkal] 
END 
go
create procedure [dbo].[DeleteKkal]
(
@userid int
)
as begin
begin try
begin tran
--???
 exec [dbo].[DeleteMeal] @userid;
 exec [dbo].[DeleteMealS] @userid;
--delete from [dbo].[tblKkal] where [ProductID] in (select [ProductID] from [dbo].[tblMeal] where [ProductID] in (select [ProductID] from [dbo].[tblKkal] where [UserID] = @userid));

delete from [dbo].[tblKkal] where [UserID]=@userid;
commit;
end try
begin catch
print error_message()
 
end catch;
end;
--------------------------------------------------------
--[DeleteKkalByID]-----------------------------------------
--------------------------------------------------------
--exec [dbo].[DeleteKkal] 6;
IF OBJECT_ID('[dbo].[DeleteKkalByID]') IS NOT NULL
BEGIN 
    DROP PROC [dbo].[DeleteKkalByID] 
END 
go
create procedure [dbo].[DeleteKkalByID]
(
@prodid int
)
as begin
begin try
begin tran
delete from [dbo].[tblKkal] where [ProductID]=@prodid;
commit;
end try
begin catch
print error_message()
 
end catch;
end;









