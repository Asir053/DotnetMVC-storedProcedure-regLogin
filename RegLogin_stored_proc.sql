CREATE DATABASE RegLogin;


SET ANSI_NULLS ON  
GO  
  
SET QUOTED_IDENTIFIER ON  
GO  
  
SET ANSI_PADDING ON  
GO  
  
CREATE TABLE Enrollment2(  
    [ID] [int] IDENTITY(1,1) NOT NULL,  
    [Name] [varchar](50) NULL,  
    [Address] [nvarchar](50) NULL,  
	[Username] [varchar](50) NULL,
    [Password] [nvarchar](30) NULL,  
    [Role] [varchar](15) NULL,   
PRIMARY KEY CLUSTERED   
(  
    [ID] ASC  
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]  
) ON [PRIMARY]  
  
GO  
  
SET ANSI_PADDING OFF  
GO  




SET ANSI_NULLS ON  
GO  
SET QUOTED_IDENTIFIER ON  
GO  
  
create procedure SP_EnrollDetail2 
(  
@Name varchar(50)=NULL,  
@Address nvarchar(50)=NULL,  
@Username varchar(50)=NULL,  
@Password nvarchar(30)=NULL,  
@Role varchar(15)=NULL,
@status varchar(15)
)  
as  
begin  
if @status='Insert'  
begin  
insert into [RegLogin].[dbo].[Enrollment2](Name,Address,Username,Password,Role)values(@Name,@Address,@Username,@Password,@Role)  
end  
end 


SET ANSI_NULLS ON  
GO  
SET QUOTED_IDENTIFIER ON  
GO  
create procedure sp_GetEnrollmentDetails2
(@Username nvarchar(50))  
as  
begin  
select * from [RegLogin].[dbo].[Enrollment2] where Username=@Username  
end 

SET ANSI_NULLS ON  
GO  
SET QUOTED_IDENTIFIER ON  
GO  
create procedure sp_GetEnrollmentDetails3 
as  
begin  
select * from [RegLogin].[dbo].[Enrollment2] 
end 

DROP PROCEDURE [dbo].[SP_EnrollDetail2];
DROP PROCEDURE [dbo].[sp_GetEnrollmentDetails2];
DROP PROCEDURE [dbo].[sp_GetEnrollmentDetails3];
DROP TABLE [RegLogin].[dbo].[Enrollment2];
DELETE FROM [RegLogin].[dbo].[Enrollment2] where Password Like 'Aasirasir5?';
DELETE FROM [RegLogin].[dbo].[Enrollment2] where ID=45;