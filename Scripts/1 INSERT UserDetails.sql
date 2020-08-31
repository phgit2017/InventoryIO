SET IDENTITY_INSERT UserDetails ON
INSERT INTO UserDetails(
UserID
,UserName
,Password
,UserRoleID
,IsActive
,CreatedBy
,CreatedTime
,ModifiedBy
,ModifiedTime
)
select UserID,UserName,'password',1,1,NULL,DateCreated,NULL,NULL
from ChainSawDB.dbo.Users

SET IDENTITY_INSERT UserDetails OFF

select * from UserDetails