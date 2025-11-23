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
/****** Object:  StoredProcedure [dbo].[LoadUser]    Script Date: 12/7/2021 2:42:21 AM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[prDeleteEmployeeById] 
@EmployeeId int
/* with encryption */ 

AS


	UPDATE tblEmployees
	SET tblEmployees.isActive = 0
	WHERE tblEmployees.EmployeeId = @EmployeeId

	if @@error != 0
		return 0
	else
		return 1
		