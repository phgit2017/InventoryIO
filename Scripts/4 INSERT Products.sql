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
GO

SET IDENTITY_INSERT PurchaseOrders ON

INSERT INTO PurchaseOrders(
PurchaseOrderID
,OrderTypeID
,TotalQuantity
,TotalAmount
,CreatedBy
,CreatedTime
,ModifiedBy
,ModifiedTime
)

select PO.PurchaseID,1,SUM(POD.Qty),0,PO.CreatedBy,PO.DateCreated,NULL,NULL
from ChainSawDB.dbo.PurchaseOrder PO
inner join ChainSawDB.dbo.PurchaseOrderDetails POD on PO.PurchaseID = POD.PurchaseID
GROUP BY  PO.PurchaseID,PO.CreatedBy,PO.DateCreated

SET IDENTITY_INSERT PurchaseOrders OFF

GO



INSERT INTO PurchaseOrderDetails(
PurchaseOrderID
,ProductID
,Quantity
,SupplierID
,CreatedBy
,CreatedTime
,ModifiedBy
,ModifiedTime
)

select PO.PurchaseID,POD.ProductID,POD.Qty,POD.SupplierID,PO.CreatedBy,PO.DateCreated,NULL,NULL
from ChainSawDB.dbo.PurchaseOrder PO
inner join ChainSawDB.dbo.PurchaseOrderDetails POD on POD.PurchaseID = PO.PurchaseID
inner join ChainSawDB.dbo.Supplier S on S.SupplierID = POD.SupplierID
inner join ChainSawDB.dbo.Product P ON P.ProductID = POD.ProductID

GO