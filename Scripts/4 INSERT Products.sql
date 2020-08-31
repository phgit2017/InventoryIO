SET IDENTITY_INSERT Products ON
INSERT INTO Products(
ProductID
,ProductCode
,ProductDescription
,ProductExtension
,Quantity
,IsActive
,CreatedBy
,CreatedTime
,ModifiedBy
,ModifiedTime
)
select ProductID,Code,Description,Extension,Quantity,1,CreatedBy,DateCreated,UpdatedBy,DateUpdated 
from ChainSawDB.dbo.Product

SET IDENTITY_INSERT Products OFF