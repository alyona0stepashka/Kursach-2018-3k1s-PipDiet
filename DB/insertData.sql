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
alter PROCEDURE [dbo].[insertDB] -- �������� ������ 
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
exec [dbo].[AddNewProduct] 1, '��������� � ������ � �����', 310;
exec [dbo].[AddNewProduct] 1, '�����', 96;
exec [dbo].[AddNewProduct] 1, '������', 350;
exec [dbo].[AddNewProduct] 1, '���� �� �������', 89;
exec [dbo].[AddNewProduct] 1, '��������', 384;
exec [dbo].[AddNewProduct] 1, '��������', 24;
exec [dbo].[AddNewProduct] 1, '�����', 211;
exec [dbo].[AddNewProduct] 1, '�������', 270;
exec [dbo].[AddNewProduct] 1, '����� �������', 162;
exec [dbo].[AddNewProduct] 1, '��������� ����', 313;
exec [dbo].[AddNewProduct] 1, '������ ������� �������', 150;
exec [dbo].[AddNewProduct] 1, '������ ���', 0;
exec [dbo].[AddNewProduct] 1, '������', 65;
exec [dbo].[AddNewProduct] 1, '������� ����', 188;
exec [dbo].[AddNewProduct] 1, '��������� �������', 192;
exec [dbo].[AddNewProduct] 1, '���� �������', 200;
exec [dbo].[AddNewProduct] 1, '������� �������', 257;
exec [dbo].[AddNewProduct] 1, '������� �����������', 170;
exec [dbo].[AddNewProduct] 1, '������� ������', 568;
exec [dbo].[AddNewProduct] 1, '������� ��������', 461;
exec [dbo].[AddNewProduct] 1, '����-����', 42;
exec [dbo].[AddNewProduct] 1, '���� � �������', 58; 
exec [dbo].[AddNewProduct] 1, '������� ������', 27;
exec [dbo].[AddNewProduct] 1, '������� ��������', 19;
exec [dbo].[AddNewProduct] 1, '�������� ����.', 70;
exec [dbo].[AddNewProduct] 1, '�������� �������', 123;
exec [dbo].[AddNewProduct] 1, '�����', 34;
exec [dbo].[AddNewProduct] 1, '������ ���� �� ������', 98;
exec [dbo].[AddNewProduct] 1, '�������', 166;
exec [dbo].[AddNewProduct] 1, '��������', 112;
exec [dbo].[AddNewProduct] 1, '���������', 247;
exec [dbo].[AddNewProduct] 1, '�������', 132;
exec [dbo].[AddNewProduct] 1, '������ 1,5%', 44;
exec [dbo].[AddNewProduct] 1, '������ 3,2%', 59;
exec [dbo].[AddNewProduct] 1, '������� ��-��������', 121;
exec [dbo].[AddNewProduct] 1, '���� ��-����������', 265;
exec [dbo].[AddNewProduct] 1, '������', 15;
exec [dbo].[AddNewProduct] 1, '������', 187;
exec [dbo].[AddNewProduct] 1, '�����', 220;
exec [dbo].[AddNewProduct] 1, '���� ������������', 82;
exec [dbo].[AddNewProduct] 1, '�������', 417;
exec [dbo].[AddNewProduct] 1, '��������', 275;
exec [dbo].[AddNewProduct] 1, '������ �������', 230;
exec [dbo].[AddNewProduct] 1, '�������', 19;
exec [dbo].[AddNewProduct] 1, '������� ����', 97;
exec [dbo].[AddNewProduct] 1, '���� ���������� � �������', 90;
exec [dbo].[AddNewProduct] 1, '����', 797;
exec [dbo].[AddNewProduct] 1, '������� 20%', 205;
exec [dbo].[AddNewProduct] 1, '������� �������', 217;
exec [dbo].[AddNewProduct] 1, '���', 45;
exec [dbo].[AddNewProduct] 1, '������ �������', 49;
exec [dbo].[AddNewProduct] 1, '���', 350;
exec [dbo].[AddNewProduct] 1, '�����', 20;
exec [dbo].[AddNewProduct] 1, '���� ����������', 221;
exec [dbo].[AddNewProduct] 1, '�����', 28;
exec [dbo].[AddNewProduct] 1, '����������� �������', 191;
exec [dbo].[AddNewProduct] 1, '������ 9%', 152;
exec [dbo].[AddNewProduct] 1, '���� ������', 200;
exec [dbo].[AddNewProduct] 1, '������', 175;
exec [dbo].[AddNewProduct] 1, '�������', 544;
exec [dbo].[AddNewProduct] 1, '�� �� ������ �������', 31;
exec [dbo].[AddNewProduct] 1, '��������� ���', 40;
exec [dbo].[AddNewProduct] 1, '���� ������� �����', 157;
exec [dbo].[AddNewProduct] 1, '���� ������� �������', 160;
exec [dbo].[AddNewProduct] 1, '���� ������� �������', 175;
exec [dbo].[AddNewProduct] 1, '������', 52;



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

