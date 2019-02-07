
 
---------------------------------------------------
--[SelectMealId] ------------------------------
--------------------------------------------------- 
IF OBJECT_ID('[dbo].[SelectMealId]') IS NOT NULL
BEGIN 
    DROP function [dbo].[SelectMealId] 
END 
go
create function [dbo].[SelectMealId]
(
@prodid int,
@amount decimal(18,0) 
) 
returns int
as begin 
declare  
@id int=0,
@kk int=0  
DECLARE @CURSOR CURSOR 
SET @CURSOR  = CURSOR SCROLL
FOR
SELECT [MealID] from [dbo].[tblMeal] where [ProductID]=@prodid and [Amount]=@amount;
OPEN @CURSOR 
FETCH NEXT FROM @CURSOR INTO @id
WHILE @@FETCH_STATUS = 0
BEGIN  
set @kk = @id;
FETCH NEXT FROM @CURSOR INTO @id
END
CLOSE @CURSOR
if @id=0 return 0; 
return @kk;
end;
---------------------------------------------------
--[SelectDietKkalAv] ------------------------------
---------------------------------------------------
--select * from [dbo].[SelectDietKkalAv](1);
IF OBJECT_ID('[dbo].[SelectDietKkalAv]') IS NOT NULL
BEGIN 
    DROP function [dbo].[SelectDietKkalAv] 
END 
go
alter function [dbo].[SelectDietKkalAv]
(
@userid int 
) 
returns int
as begin 
declare 
@dn int,
@k decimal,
@kkal int,
@kk int=0,
@count int=0;
--set @kk = select [Kkal] from [dbo].[tblDiet] where [UserId]=@userid;
DECLARE @CURSOR CURSOR 
SET @CURSOR  = CURSOR SCROLL
FOR
SELECT [Kkal],[DietNum] FROM  [dbo].[DietView]  WHERE [UserID]=@userid 
OPEN @CURSOR 
FETCH NEXT FROM @CURSOR INTO @kkal,@dn 
WHILE @@FETCH_STATUS = 0
BEGIN 
if @count<=@dn
begin
set @count=@dn;
end;
set @kk = @kk + @kkal;
FETCH NEXT FROM @CURSOR INTO @kkal,@dn 
END;
CLOSE @CURSOR;
if @count=0 return 0;
set @k = @kk/@count;
return @k;
end;
---------------------------------------------------
--[SelectKkalRes] ------------------------------
--------------------------------------------------- 
IF OBJECT_ID('[dbo].[SelectKkalRes]') IS NOT NULL
BEGIN 
    DROP function [dbo].[SelectKkalRes] 
END 
go
create function [dbo].[SelectKkalRes]
(
@prodid int,
@amount decimal(18,0) 
) 
returns decimal
as begin 
declare 
@kkal int, 
@id int=0,
@kk decimal =0  
DECLARE @CURSOR CURSOR 
SET @CURSOR  = CURSOR SCROLL
FOR
SELECT [Kkal] FROM  [dbo].[tblKkal] WHERE [ProductID]=@prodid 
OPEN @CURSOR 
FETCH NEXT FROM @CURSOR INTO @kkal 
WHILE @@FETCH_STATUS = 0
BEGIN  
set @kk = @kkal*@amount/100;
FETCH NEXT FROM @CURSOR INTO @kkal 
END
CLOSE @CURSOR
if @kk=0 return 0; 
return @kk;
end;
---------------------------------------------------
--[SelectDietId] ------------------------------
--------------------------------------------------- 
IF OBJECT_ID('[dbo].[SelectDietId]') IS NOT NULL
BEGIN 
    DROP function [dbo].[SelectDietId] 
END 
go
create function [dbo].[SelectDietId]
(
@userid int,
@mealid int
) 
returns int
as begin 
declare  
@id int=0,
@kk int=0  
DECLARE @CURSOR CURSOR 
SET @CURSOR  = CURSOR SCROLL
FOR
SELECT [NumberID] from [dbo].[tblDiet] where [UserID]=@userid and [MealID]=@mealid;
OPEN @CURSOR 
FETCH NEXT FROM @CURSOR INTO @id
WHILE @@FETCH_STATUS = 0
BEGIN  
set @kk = @id;
FETCH NEXT FROM @CURSOR INTO @id
END
CLOSE @CURSOR
if @id=0 return 0; 
return @kk;
end;
-----------------------------------------
--
-----------------------------------------
go
CREATE FUNCTION [dbo].[GetKkalByDay]
(  )
RETURNS 
@MyTable TABLE
(
[Date] Date ,
[KkalSum] decimal(18,0) 
)
AS
BEGIN
declare  



INSERT INTO @Table ( KK )
VALUES ( (CASE WHEN (NOT EXISTS(SELECT * FROM @Tbl)) THEN 0 ELSE 1 END))

RETURN 
END

---------------------------------------------------
--[SelectOstKkalUser] ------------------------------
-------------------------------------------------
select [dbo].[SelectOstKkalUser](7);
IF OBJECT_ID('[dbo].[SelectOstKkalUser]') IS NOT NULL
BEGIN 
    DROP function [dbo].[SelectOstKkalUser] 
END 
go
create function [dbo].[SelectOstKkalUser]
(
@userid int 
) 
returns decimal
as begin 
declare @am decimal, --кол-во продукта в граммах
@kkal int, --калорийность продукта
@sum decimal=0, --потребленные за текущий день калории
@prodid int, --идентификатор продукта
@ret decimal = 0, --итоговое значение 
@CURSOR CURSOR --курсор для обработки записей протокола текущего дня
SET @CURSOR  = CURSOR SCROLL
FOR
SELECT [ProductID],[Amount] FROM  [dbo].[tblMealService]  WHERE [UserID]= @userid and [Date]=convert(date,GetDate()); 
OPEN @CURSOR 
FETCH NEXT FROM @CURSOR INTO @prodid,@am
WHILE @@FETCH_STATUS = 0
BEGIN 
set @kkal = (select [Kkal] from [dbo].[tblKkal] where [ProductID]=@prodid);
set @sum = @sum + (@kkal*@am/100); --подсчет потребленных калорий
FETCH NEXT FROM @CURSOR INTO @prodid,@am
END;
CLOSE @CURSOR;   
set @ret=@sum;
return @ret;
end;
---------------------------------------------------
--[SelectMealId] ------------------------------
--select * from [dbo].[SelectKkalByProdID](1)------------------------------------------------- 
IF OBJECT_ID('[dbo].[SelectKkalByProdID]') IS NOT NULL
BEGIN 
    DROP function [dbo].[SelectKkalByProdID] 
END 
go
create function [dbo].[SelectKkalByProdID]
(
@prodid int 
) 
returns int
as begin 
declare @kk int;
set @kk = (select [Kkal] from [dbo].[tblKkal] where [ProductID]=@prodid);
return @kk;
end;

---------------------------------------------------
--[SelectMealId] ------------------------------
--select * from [dbo].[SelectKkByProd](1)------------------------------------------------- 
IF OBJECT_ID('[dbo].[SelectKkByProd]') IS NOT NULL
BEGIN 
    DROP function [dbo].[SelectKkByProd] 
END 
go
create function [dbo].[SelectKkByProd]
(
@prodid int 
) 
returns int
as begin 
declare @kk int;
set @kk = (select [Kkal] from [dbo].[tblKkal] where [ProductID]=@prodid);
return @kk;
end;