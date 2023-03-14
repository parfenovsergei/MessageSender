use MessageSender

insert into Users (Email, [Password], Salt, [Role], IsVerifed, VerifyCode, CreateAndVerifyTime)
values(
'parfenovserezhka@gmail.com', 
'28-B9-17-F1-15-93-09-DF-A6-FC-CE-F0-61-50-20-E5-E3-C5-9B-21-80-D1-F5-31-2A-6D-8F-73-C5-B5-86-BB-54-D5-14-F6-20-5F-2E-75-90-EF-BA-D8-15-B7-9D-D2-36-31-88-F9-DB-BB-6F-87-51-13-10-4C-04-EF-F2-DD', 
'81-5D-53-F3-DD-7E-20-66-BB-AB-13-D4-FF-2D-45-FA-88-AD-C3-46-21-23-86-C9-AE-48-2B-0B-52-BF-58-0C-59-42-F1-B0-DE-91-C9-49-60-7A-2C-CA-26-CD-6A-8F-79-E6-97-0F-AB-0E-F2-62-69-E4-45-82-78-63-48-9D',
0,
1,
0,
Getdate())