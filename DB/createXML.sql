use PipDietDB;

exec AddNewWeightS 1,50;
------------------select * from tblWeightService---------------------------------------------
--[tblWeightService]------------------------------------------
---------------------------------------------------------------
go
alter PROCEDURE ExportW
AS
	SELECT [UserID], [Weight], [Date]
	FROM [dbo].[tblWeightService]
	FOR XML PATH('Weight'), ROOT('Weights');
	ExportW;

	--в помощь исследователю на сайте есть посмотреть 

alter PROCEDURE ImportW
	@xml XML = NULL
AS
Select  @xml  = 
CONVERT(XML,bulkcolumn,2) FROM OPENROWSET(BULK 'D:\Study\3k1s\Kursach\PipDiet\DB\1Weight.xml',SINGLE_BLOB) AS X
SET ARITHABORT ON
Insert into [dbo].[tblWeightService]
        (
          [UserID], [Weight], [Date]
        )
    Select 
        
        P.value('UserID[1]', 'int') AS [UserID],
        P.value('Weight[1]', 'decimal(18,0)') AS [Weight], 
        P.value('Date[1]', 'date') AS [Date]
       
    From @xml.nodes('/Weights/Weight') PropertyFeed(P) 
GO

ImportW;

---------------------------------------------------------------
--[tblSizeService]---------------------------------------------
---------------------------------------------------------------
go
alter PROCEDURE ExportS
AS
	SELECT  [UserID], [Size1], [Size2], [Size3], [Size4], [Date]
	FROM [dbo].[tblSizeService]
	FOR XML PATH('Size'), ROOT('Sizes');
	ExportS;



alter PROCEDURE ImportS
	@xml XML = NULL
AS
Select  @xml  = 
CONVERT(XML,bulkcolumn,2) FROM OPENROWSET(BULK 'D:\Study\3k1s\Kursach\PipDiet\DB\1Size.xml',SINGLE_BLOB) AS X
SET ARITHABORT ON
Insert into [dbo].[tblSizeService]
        (
           [UserID], [Size1], [Size2], [Size3], [Size4], [Date]
        )
    Select 

        P.value('UserID[1]', 'int') AS [UserID],
        P.value('Size1[1]', 'decimal(18,0)') AS [Weight],
        P.value('Size1[2]', 'decimal(18,0)') AS [Weight],
        P.value('Size1[3]', 'decimal(18,0)') AS [Weight],
        P.value('Size1[4]', 'decimal(18,0)') AS [Weight], 
        P.value('Date[1]', 'date') AS [Date]
       
    From @xml.nodes('/Sizes/Size') PropertyFeed(P) 
GO

exec [ImportS];