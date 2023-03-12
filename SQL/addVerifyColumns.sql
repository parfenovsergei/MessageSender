use MessageSender

alter table Users
add 
IsVerifed bit default 0 not null,
VerifyCode int not null