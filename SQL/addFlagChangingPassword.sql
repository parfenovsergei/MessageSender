use MessageSender

alter table Users
add IsChangingPassword bit default 0 not null