  
ALTER PROCEDURE [dbo].[GetSchoolExpenseStudents]   
@ExpenseId int,  
@SchoolId int  
/* with encryption */   
  
AS  
 /*
	[dbo].[GetSchoolExpenseStudents] 0,11
 */
  
select StudentId, FirstName, LastName ,ClassName from tblStudents left join tblClasses   
on tblStudents.ClassId = tblClasses.ClassId  
where StudentId in  
  
( select distinct StudentId from tblSchoolExpenseDetail where ExpenseId = @ExpenseId union  
  select StudentId from tblStudents where SchoolId = @SchoolId AND dbo.tblStudents.StatusId <> 6 )  
    
    