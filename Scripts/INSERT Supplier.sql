SET IDENTITY_INSERT Suppliers ON
INSERT INTO Suppliers(
SupplierID
,SupplierCode
,SupplierName
,IsActive
,CreatedBy
,CreatedTime
,ModifiedBy
,ModifiedTime
)
select SupplierID,SupplierCode,SupplierName,ISNULL(Activated,0),CreatedBy,DateCreated,
UpdatedBy,DateUpdated from ChainSawDB.dbo.Supplier
SET IDENTITY_INSERT Suppliers OFF