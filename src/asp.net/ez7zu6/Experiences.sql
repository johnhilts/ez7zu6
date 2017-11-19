use ez7zu6
go

if  exists (select 1 from sys.objects o (nolock) where o.object_id = object_id(N'Experiences') and o.type in (N'U'))
drop table ez7zu6.dbo.Experiences
go

/****** Object:  Table ez7zu6.dbo.Experiences    Script Date: 11/12/2017 11:08:24 PM ******/
set ansi_nulls on
go
set quoted_identifier on
go
create table ez7zu6.dbo.Experiences (
	ExperienceId int identity(1, 1) not null,
	UserId uniqueidentifier not null,
	Notes ntext not null,
	/*
	RatingId int not null,
	PhotoId int not null,
	LocationId int not null,
	WeatherId int not null,
	*/
	InputDateTime smalldatetime not null,
	Created smalldatetime not null constraint DF_Experiences_Created default getdate(),
	IsActive bit not null constraint DF_Experiences_IsActive default 1,
	constraint FK_Experiences_UserId foreign key (UserId) references dbo.Accounts (UserId),
 constraint PK_Experiences primary key clustered 
(
	ExperienceId asc
)with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on) on [primary]
) on [primary]

go
