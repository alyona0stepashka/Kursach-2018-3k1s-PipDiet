use PipDietDB;

delete from [dbo].[tblUser];
delete from [dbo].[tblKkal];
delete from [dbo].[tblMeal];
delete from [dbo].[tblDiet]; 
delete from [dbo].[tblMealService];
delete from [dbo].[tblWeightService];
delete from [dbo].[tblSizeService];

select * from [dbo].[tblUser];
select * from [dbo].[tblKkal];
select * from [dbo].[tblMeal];
select * from [dbo].[tblDiet]; 
select * from [dbo].[tblMealService];
select * from [dbo].[tblWeightService];
select * from [dbo].[tblSizeService];


exec [dbo].[insertDB];
delete from [dbo].[tblKkal] where [UserID]=8;
select * from [tblKkal] where [UserID]=8 and [Kkal]<100;
select [UserID],[Kkal] from [dbo].[tblKkal] where [Kkal]<100;

GO 
alter PROCEDURE [dbo].[insertDB] -- внесение данных 
AS 
BEGIN 
DECLARE 
@userID int,
@name nvarchar(50),
@kkal int, 
@count int; 
SET @count = 10000; 
set @userID = 8; 
begin tran
WHILE @count > 0 
BEGIN 
SET @name = CONVERT (nvarchar(50), @count); 
set @kkal = @count; 
INSERT INTO [tblKkal] ([UserID],[Name],[Kkal]) VALUES(@userID,@name,@kkal) 
SET @count = @count - 1 
END; 
commit;
END; 


----------------------------------------------------------------------------------------
--Users---------------------------------------------------------------------------------
----------------------------------------------------------------------------------------
exec [dbo].[AddNewUser] 'user1', 'user1',1,'Female',19,166,55,1700,1900,2100;
----------------------------------------------------------------------------------------
--Kkal----------------------------------------------------------------------------------
----------------------------------------------------------------------------------------
exec [dbo].[AddNewProduct] 1, 'Бутерброд с маслом и сыром', 310;
exec [dbo].[AddNewProduct] 1, 'Банан', 96;
exec [dbo].[AddNewProduct] 1, 'Бургер', 350;
exec [dbo].[AddNewProduct] 1, 'Борщ из свинины', 89;
exec [dbo].[AddNewProduct] 1, 'Бифштекс', 384;
exec [dbo].[AddNewProduct] 1, 'Баклажан', 24;
exec [dbo].[AddNewProduct] 1, 'Вафли', 211;
exec [dbo].[AddNewProduct] 1, 'Ветчина', 270;
exec [dbo].[AddNewProduct] 1, 'Грибы жареные', 162;
exec [dbo].[AddNewProduct] 1, 'Гречневая каша', 313;
exec [dbo].[AddNewProduct] 1, 'Грудка куриная вареная', 150;
exec [dbo].[AddNewProduct] 1, 'Зелёный чай', 0;
exec [dbo].[AddNewProduct] 1, 'Йогурт', 65;
exec [dbo].[AddNewProduct] 1, 'Красная рыба', 188;
exec [dbo].[AddNewProduct] 1, 'Картофель жареный', 192;
exec [dbo].[AddNewProduct] 1, 'Карп жареный', 200;
exec [dbo].[AddNewProduct] 1, 'Колбаса вареная', 257;
exec [dbo].[AddNewProduct] 1, 'Колбаса диетическая', 170;
exec [dbo].[AddNewProduct] 1, 'Колбаса салями', 568;
exec [dbo].[AddNewProduct] 1, 'Колбаса сервелат', 461;
exec [dbo].[AddNewProduct] 1, 'Кока-Кола', 42;
exec [dbo].[AddNewProduct] 1, 'Кофе с молоком', 58; 
exec [dbo].[AddNewProduct] 1, 'Капуста свежая', 27;
exec [dbo].[AddNewProduct] 1, 'Капуста квашеная', 19;
exec [dbo].[AddNewProduct] 1, 'Кукуруза конс.', 70;
exec [dbo].[AddNewProduct] 1, 'Кукуруза вареная', 123;
exec [dbo].[AddNewProduct] 1, 'Лимон', 34;
exec [dbo].[AddNewProduct] 1, 'Манная каша на молоке', 98;
exec [dbo].[AddNewProduct] 1, 'Маслины', 166;
exec [dbo].[AddNewProduct] 1, 'Макароны', 112;
exec [dbo].[AddNewProduct] 1, 'Мороженое', 247;
exec [dbo].[AddNewProduct] 1, 'Морковь', 132;
exec [dbo].[AddNewProduct] 1, 'Молоко 1,5%', 44;
exec [dbo].[AddNewProduct] 1, 'Молоко 3,2%', 59;
exec [dbo].[AddNewProduct] 1, 'Морковь по-корейски', 121;
exec [dbo].[AddNewProduct] 1, 'Мясо по-французски', 265;
exec [dbo].[AddNewProduct] 1, 'Огурец', 15;
exec [dbo].[AddNewProduct] 1, 'Оливье', 187;
exec [dbo].[AddNewProduct] 1, 'Пицца', 220;
exec [dbo].[AddNewProduct] 1, 'Пюре картофельное', 82;
exec [dbo].[AddNewProduct] 1, 'Печенье', 417;
exec [dbo].[AddNewProduct] 1, 'Пельмени', 275;
exec [dbo].[AddNewProduct] 1, 'Рыбные палочки', 230;
exec [dbo].[AddNewProduct] 1, 'Редиска', 19;
exec [dbo].[AddNewProduct] 1, 'Рисовая каша', 97;
exec [dbo].[AddNewProduct] 1, 'Рыба запеченная с лимоном', 90;
exec [dbo].[AddNewProduct] 1, 'Сало', 797;
exec [dbo].[AddNewProduct] 1, 'Сметана 20%', 205;
exec [dbo].[AddNewProduct] 1, 'Селедка соленая', 217;
exec [dbo].[AddNewProduct] 1, 'Сок', 45;
exec [dbo].[AddNewProduct] 1, 'Свекла вареная', 49;
exec [dbo].[AddNewProduct] 1, 'Сыр', 350;
exec [dbo].[AddNewProduct] 1, 'Томат', 20;
exec [dbo].[AddNewProduct] 1, 'Торт бисквитный', 221;
exec [dbo].[AddNewProduct] 1, 'Тыква', 28;
exec [dbo].[AddNewProduct] 1, 'Толстолобик жареный', 191;
exec [dbo].[AddNewProduct] 1, 'Творог 9%', 152;
exec [dbo].[AddNewProduct] 1, 'Хлеб чёрный', 200;
exec [dbo].[AddNewProduct] 1, 'Шаурма', 175;
exec [dbo].[AddNewProduct] 1, 'Шоколад', 544;
exec [dbo].[AddNewProduct] 1, 'Щи из свежей капусты', 31;
exec [dbo].[AddNewProduct] 1, 'Щавелевый суп', 40;
exec [dbo].[AddNewProduct] 1, 'Яйцо куриное сырое', 157;
exec [dbo].[AddNewProduct] 1, 'Яйцо куриное вареное', 160;
exec [dbo].[AddNewProduct] 1, 'Яйцо куриное жареное', 175;
exec [dbo].[AddNewProduct] 1, 'Яблоко', 52;



IF OBJECT_ID('[dbo].[Insert1000str]') IS NOT NULL
BEGIN r
    DROP PROC [dbo].[Insert1000str] 
END 
go
create procedure [dbo].[Insert1000str] 
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


----------------------------------------------------------------------------------------
--Meal----------------------------------------------------------------------------------
----------------------------------------------------------------------------------------
exec [dbo].[AddNewMeal] 1,200;
exec [dbo].[AddNewMeal] 2,80;
exec [dbo].[AddNewMeal] 3,150;
exec [dbo].[AddNewMeal] 4,220;

----------------------------------------------------------------------------------------
--Diet----------------------------------------------------------------------------------
----------------------------------------------------------------------------------------
exec [dbo].[AddNewDietRecord] 1,1,1,1;
exec [dbo].[AddNewDietRecord] 1,2,2,1;
exec [dbo].[AddNewDietRecord] 1,3,3,1;
exec [dbo].[AddNewDietRecord] 1,4,4,1;

exec [dbo].[AddNewDietRecord] 1,1,1,2;
exec [dbo].[AddNewDietRecord] 1,2,2,2;
exec [dbo].[AddNewDietRecord] 1,3,3,2;
exec [dbo].[AddNewDietRecord] 1,4,4,2;
----------------------------------------------------------------------------------------
--

