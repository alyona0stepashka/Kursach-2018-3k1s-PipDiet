use PipDietDB;

select ProductID from [tblKkal];
select [UserID],[Kkal] from [tblKkal] where [UserID]=8 and [Kkal]<100;
drop index [tblKkal].[KkalId_index];
drop index [tblKkal].[KkalName_index];
drop index [tblKkal].[KkalSost_index];
drop index [tblKkal].[KkalKk_index];
drop index [tblKkal].[KkalUserId_index];



ALTER INDEX ALL ON [tblKkal]
rebuild;

------------------------------------------------------------
--tblKkal---------------------------------------------------
------------------------------------------------------------
create nonclustered index [KkalId_index] on [dbo].[tblKkal] (ProductID); 
create nonclustered index [KkalName_index] on [dbo].[tblKkal] (Name);
create nonclustered index [KkalKk_index] on [dbo].[tblKkal] (Kkal);
create nonclustered index [KkalUserId_index] on [dbo].[tblKkal] (UserID);
create nonclustered index [KkalSost_index] on [dbo].[tblKkal] (UserID, Kkal);
------------------------------------------------------------
--tblMeal---------------------------------------------------
------------------------------------------------------------
create nonclustered index [MealMId_index] on [dbo].[tblMeal] (MealID);
create nonclustered index [MealProdId_index] on [dbo].[tblMeal] (ProductID);
create nonclustered index [MealAmount_index] on [dbo].[tblMeal] (Amount);
-------------------------------------------------------------
--tblDiet----------------------------------------------------
-------------------------------------------------------------
create nonclustered index [DietDId_index] on [dbo].[tblDiet] (NumberID);
create nonclustered index [DietUserId_index] on [dbo].[tblDiet] (UserID);
create nonclustered index [DietMealId_index] on [dbo].[tblDiet] (MealID);
create nonclustered index [DietKkal_index] on [dbo].[tblDiet] (Kkal);
create nonclustered index [DietMealN_index] on [dbo].[tblDiet] (MealNumber);
create nonclustered index [DietDayN_index] on [dbo].[tblDiet] (DayNumber);




