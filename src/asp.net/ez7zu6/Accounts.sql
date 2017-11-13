use ez7zu6
go

if  exists (select 1 from sys.objects o (nolock) where o.object_id = object_id(N'Accounts') and o.type in (N'U'))
drop table ez7zu6.dbo.Accounts
go

begin transaction
GO
/****** Object:  Table ez7zu6.dbo.Accounts    Script Date: 11/12/2017 11:08:24 PM ******/
set ansi_nulls on
go
set quoted_identifier on
go
create table ez7zu6.dbo.Accounts (
	UserId uniqueidentifier not null,
	Username nvarchar(50) not null,
	UserPassword binary(64) not null,
	IsActive bit not null constraint DF_Accounts_IsActive default 1,
 constraint PK_Accounts primary key clustered 
(
	UserId asc
)with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on) on [primary]
) on [primary]

go

insert ez7zu6.dbo.Accounts (UserId, Username, UserPassword, IsActive) 
values 
	(newid(), N'john@test.com', Convert(binary, ''), 1)

go
commit

