# Adventure Works API

Este proyecto proporciona una API basada en las vistas creadas sobre la base de datos de Adventure Works. Las vistas ofrecen una visión analítica de las ventas, permitiendo el análisis de ventas por región y categoría de productos.

Para utilizar esta API, primero es necesario crear la vista en la base de datos. A continuación se muestra el script SQL para crear la vista `SaleAnalysisView`:

```sql
CREATE VIEW SaleAnalysisView
AS
WITH TotalSalesCTE
AS
(
    SELECT 
        st.TerritoryID, 
        st.Name AS 'RegionName', 
        pc.ProductCategoryID, 
        pc.Name AS 'CategoryName', 
        SUM(SUM(sod.LineTotal)) OVER(PARTITION BY st.TerritoryID) AS 'TotalSalesInRegion',
        SUM(sod.LineTotal) AS 'TotalCategorySalesInRegion'
    FROM 
        sales.SalesOrderDetail sod
    INNER JOIN 
        sales.SalesOrderHeader soh ON soh.SalesOrderID = sod.SalesOrderID
    INNER JOIN 
        sales.SalesTerritory st ON st.TerritoryID = soh.TerritoryID
    INNER JOIN 
        production.product p ON p.ProductID = sod.ProductID
    INNER JOIN 
        production.ProductSubcategory ps ON ps.ProductSubcategoryID = p.ProductSubcategoryID
    INNER JOIN 
        production.ProductCategory pc ON pc.ProductCategoryID = ps.ProductCategoryID
    GROUP BY 
        st.TerritoryID, st.Name, pc.ProductCategoryID, pc.Name
)
SELECT 
    CAST(ROW_NUMBER() OVER (ORDER BY s.PercentageOfTotalCategorySalesInRegion DESC) AS INT) AS Id,
    s.ProductName, 
    s.ProductCategory, 
    s.TotalSales,
    s.PercentageOfTotalSalesInRegion,
    s.PercentageOfTotalCategorySalesInRegion
FROM 
(
    SELECT TOP 20
        p.Name AS 'ProductName', 
        pc.Name AS 'ProductCategory', 
        sod.LineTotal AS 'TotalSales',
        CONVERT(decimal(10,2),ROUND((sod.LineTotal*100)/tsCTE.TotalSalesInRegion,2)) AS 'PercentageOfTotalSalesInRegion',
        CONVERT(decimal(10,2),ROUND((sod.LineTotal*100)/tsCTE.TotalCategorySalesInRegion,2)) AS 'PercentageOfTotalCategorySalesInRegion'
    FROM 
        sales.SalesOrderDetail sod
    INNER JOIN 
        sales.SalesOrderHeader soh ON soh.SalesOrderID = sod.SalesOrderID
    INNER JOIN 
        production.product p ON p.ProductID = sod.ProductID
    INNER JOIN 
        production.ProductSubcategory ps ON ps.ProductSubcategoryID = p.ProductSubcategoryID
    INNER JOIN 
        production.ProductCategory pc ON pc.ProductCategoryID = ps.ProductCategoryID
    INNER JOIN 
        sales.SalesTerritory st ON st.TerritoryID = soh.TerritoryID
    INNER JOIN 
        TotalSalesCTE tsCTE ON tsCTE.TerritoryID = st.TerritoryID AND tsCTE.CategoryName = pc.Name
) s;
````
Adicionalmente hay que agregar la vista `NewSalesView`:

```sql

CREATE VIEW NewSalesView AS
SELECT TOP 100
    CAST(ROW_NUMBER() OVER (ORDER BY soh.SalesOrderID ASC) AS INT) AS Id,
    soh.SalesOrderID 'OrderID',
    CAST(soh.OrderDate AS date) 'OrderDate',
    sod.ProductID,
    p.Name 'ProductName',
    pc.Name 'ProductCategory',
    sod.UnitPrice,
    sod.OrderQty,
    sod.LineTotal,
    soh.SalesPersonID,
    CONCAT(Per.FirstName, ' ', Per.LastName) 'SalesPersonName',
    CONCAT(sa.AddressLine1, ', ', sa.City) 'ShippingAddress',
    CONCAT(ba.AddressLine1, ', ', ba.City) 'BillingAddress'
FROM 
    Sales.SalesOrderHeader soh
INNER JOIN
    Sales.SalesOrderDetail sod ON soh.SalesOrderID = sod.SalesOrderID 
INNER JOIN
    Production.Product p ON sod.ProductID = p.ProductID 
INNER JOIN
    Production.ProductSubcategory psc ON p.ProductSubCategoryID = psc.ProductSubcategoryID
INNER JOIN
    Production.ProductCategory pc ON psc.ProductCategoryID = pc.ProductCategoryID
INNER JOIN
    Person.Person per ON sp.BusinessEntityID = per.BusinessEntityID
INNER JOIN
    Person.Address ba ON soh.BillToAddressID = ba.AddressID
INNER JOIN 
    Person.Address sa ON soh.ShipToAddressID = sa.AddressID;
INNER JOIN
    Sales.SalesPerson sp ON soh.SalesPersonID = sp.BusinessEntityID

````

