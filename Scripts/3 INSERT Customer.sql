SET IDENTITY_INSERT Customers ON
INSERT INTO Customers(
CustomerID
,CustomerCode
,Name
,Address
,IsActive
,CreatedBy
,CreatedTime
,ModifiedBy
)
select CustomerID,CustomerCode,Name,Address,Active,CreatedBy,DateCreated,UpdatedBy from ChainSawDB.dbo.Customer

SET IDENTITY_INSERT Customers OFF
