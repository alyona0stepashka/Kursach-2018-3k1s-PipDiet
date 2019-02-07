Disable TRIGGER [dbo].[SizeS_INSERT_UPDATE]
ON [dbo].[tblSizeService];

------------------------------------------------------------------
--Data into Weight------------------------------------------------ 
------------------------------------------------------------------
GO
alter TRIGGER [dbo].[WeightS_INSERT_UPDATE]
ON [dbo].[tblWeightService]
AFTER INSERT, UPDATE
AS
begin
UPDATE [dbo].[tblWeightService]
SET [Date]=GetDate()
WHERE [ID] = (SELECT [ID] FROM inserted)
end;

enable TRIGGER [dbo].[WeightS_INSERT_UPDATE]
ON [dbo].[tblWeightService];
ENABLE  
------------------------------------------------------------------
--Data into Size--------------------------------------------------
------------------------------------------------------------------
GO
alter TRIGGER [dbo].[SizeS_INSERT_UPDATE]
ON [dbo].[tblSizeService]
AFTER INSERT, UPDATE
AS
begin
UPDATE [dbo].[tblSizeService]
SET [Date]=GetDate()
WHERE [ID] = (SELECT [ID] FROM inserted)
end;

------------------------------------------------------------------
--Data into Meal--------------------------------------------------
------------------------------------------------------------------
GO
alter TRIGGER [dbo].[MealS_INSERT_UPDATE]
ON [dbo].[tblMealService]
AFTER INSERT, UPDATE
AS
begin
UPDATE [dbo].[tblMealService]
SET [Date]=GetDate()
WHERE [ID] = (SELECT [ID] FROM inserted)
end;

 