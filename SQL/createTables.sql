use MessageSender

create table Users
(
	Id int primary key identity(1,1),
	Email nvarchar(100) not null,
	[Password] char(128) not null,
	Salt char(128) not null,
	[Role] int not null
)

create table [Messages]
(
	Id int primary key identity(1,1),
	MessageTheme nvarchar(100) not null,
	MessageBody nvarchar(1000) not null,
	SenderId int references Users (Id) not null,
	ReceiverId int references Users (Id) not null,
	SendDate datetime not null
)