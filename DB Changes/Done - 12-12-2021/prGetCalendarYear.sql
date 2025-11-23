USE [SaveDC]
GO
/****** Object:  StoredProcedure [dbo].[AddAnnualFee]    Script Date: 05/06/2014 12:30:08 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[prGetCalendarYear]
@MinDate int = NULL,
@MaxDate int = NULL

/* with encryption */ 

AS
	/*
		[dbo].[prGetCalendarYear] 1980,2022
	*/

	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	SELECT Distinct  date_year
	FROM    dbo.Calendar
	WHERE   date_year >= @MinDate
	AND     date_year <= @MaxDate
	ORDER BY date_year
GO
