use PipDietDB;


DROP TABLE [dbo].[tblUser];
DROP TABLE [dbo].[tblKkal];
DROP TABLE [dbo].[tblMeal];
DROP TABLE [dbo].[tblDiet];
DROP TABLE [dbo].[tblHistoryService];
DROP TABLE [dbo].[tblMealService];
DROP TABLE [dbo].[tblWeightService];
DROP TABLE [dbo].[tblChainMeal];
DROP TABLE [dbo].[tblChainWeight];



------------------------------
--Table Users-----------------
------------------------------
CREATE TABLE [dbo].[tblUser] (
    [UserID]            INT           IDENTITY (1, 1) NOT NULL,
    [Username]          NVARCHAR (50) NULL,
    [Password]          NVARCHAR (50) NULL,
    [Admin]             tinyint default 1 NOT NULL,
    [Sex]               NVARCHAR(6) check( Sex in('Male','Female')),
    [Birthday]          INT          NULL,
    [Growth]            REAL          NULL,
    [IdealWeight]       REAL          NULL,
    [DailyCaloriesNormEasy] REAL          NULL,
    [DailyCaloriesNormNorm] REAL          NULL,
    [DailyCaloriesNormHard] REAL          NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC)
);
-----------------------------------
--Table Product Kkal---------------
-----------------------------------
create TABLE [dbo].[tblKkal] (
    [ProductID] INT        IDENTITY (1, 1) NOT NULL,
    [UserID]    INT        NOT NULL,
    [Name]      NVARCHAR (50) NULL,
    [Kkal]      REAL       NULL,
    PRIMARY KEY CLUSTERED ([ProductID] ASC),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[tblUser] ([UserID])
);

-----------------------------------
--Table Diet's Meals---------------
-----------------------------------
CREATE TABLE [dbo].[tblMeal] (
    [MealID]    INT     IDENTITY(1,1)      NOT NULL,
    [ProductID] INT           NOT NULL,
    [Amount]    REAL          NULL,
    [EI]        NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([MealID] ASC),
    FOREIGN KEY ([ProductID]) REFERENCES [dbo].[tblKkal] ([ProductID])
);

------------------------------
--Table Diet's----------------
------------------------------
CREATE TABLE [dbo].[tblDiet] (
    [NumberID] INT  IDENTITY(1,1) NOT NULL,
    [UserID]     INT NOT NULL,
    [MealID]     INT NOT NULL,
    [MealNumber] INT NULL,
    [DayNumber]  INT NULL,
    PRIMARY KEY CLUSTERED ([NumberID] ASC),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[tblUser] ([UserID]),
    FOREIGN KEY ([MealID]) REFERENCES [dbo].[tblMeal] ([MealID])
);
------------------------------------
--Table Weight----------------------
------------------------------------
CREATE TABLE [dbo].[tblWeightService] (
    [ID]     INT  IDENTITY (1, 1) NOT NULL,
    [UserID] INT NOT NULL,
    [Weight] REAL NULL,
    [Date]   DATE NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[tblUser] ([UserID])
);
------------------------------------
--Table Meal------------------------
------------------------------------
CREATE TABLE [dbo].[tblMealService] (
    [ID]        INT           IDENTITY (1, 1) NOT NULL,
    [UserID]    INT NOT NULL,
    [ProductID] INT           NOT NULL,
    [Amount]    REAL          NULL,
    [EI]        NVARCHAR (50) NULL,
    [Date]      DATE          NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([ProductID]) REFERENCES [dbo].[tblKkal] ([ProductID]),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[tblUser] ([UserID])
);
