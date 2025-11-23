  
  
  
  
  
Alter PROCEDURE [dbo].[GetApplicationsList]  
@StudentName varchar (255) = null,  
  
  
@PageNo int = 1,  
@PageSize int = 10,  
@TotalRecord int out  
  
/* with encryption */   
  
AS  
  
DECLARE @StartRow int , @EndRow int;  
  
If(@PageNo is null or @PageNo = 0)  
begin  
 SET @PageNo = 1;  
end   
  
If(@PageSize is null or @PageSize = 0)  
 SET @PageSize = (Select COUNT(*) from tblNewApplications);  
  
   
SET @StartRow = (@PageNo - 1) * @PageSize + 1;  
SET @EndRow = @PageNo * @PageSize  
  
  
Create Table #TempTable  
(  
IID int IDENTITY PRIMARY KEY,  
TableRecID int  
)  
  
Declare @TempSQL varchar(1024);  
  
  
Set @TempSQL = ' Insert into #TempTable(TableRecID)  select ApplicationId from tblNewApplications '  
  
Set @TempSQL = @TempSQL + ' where IsMoved = 0 ' ;  
   
 if(@StudentName is not null and @StudentName != '')  
 Set @TempSQL = @TempSQL + ' And StdFirstName + '' '' + StdLastName like  ''%'+ @StudentName +'%''';  
  
Set @TempSQL = @TempSQL + ' ORDER BY ActionDate desc' ;  
   
   
exec (@TempSQL);  
    
Set @TotalRecord = (select COUNT(*) from #TempTable);  
    
    
Set @TempSQL =  'Select tblNewApplications.*, [dbo].FormateDate(ReceivedOn, 1) as ReceivedOnDate1 from  tblNewApplications inner join #TempTable on #TempTable.TableRecID = tblNewApplications.ApplicationId '  
Set @TempSQL = @TempSQL + ' WHERE  IID >= '+ convert(nvarchar,@StartRow)+' AND IID <= ' + convert(nvarchar,@EndRow)  
Set @TempSQL = @TempSQL + ' ORDER BY CAST(dbo.tblNewApplications.ReceivedOn AS DATE) desc ' ;  
exec (@TempSQL);  
  