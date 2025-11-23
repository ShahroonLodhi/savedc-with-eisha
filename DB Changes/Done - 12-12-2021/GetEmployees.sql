/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2017 (14.0.1000)
    Source Database Engine Edition : Microsoft SQL Server Enterprise Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/

USE [SaveDC]
GO
/****** Object:  StoredProcedure [dbo].[GetEmployees]    Script Date: 12/6/2021 3:04:18 AM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO





ALTER PROCEDURE [dbo].[GetEmployees]


/* with encryption */ 

AS

	Select * from tblEmployees
	WHERE isActive = 1
	order by EmployeeName 