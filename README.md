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
Adicionalmente hay que agregar la vista `SalesReportView`:

```sql
CREATE VIEW SalesReportView AS
SELECT 
    Sales.SalesOrderDetail.SalesOrderID AS OrderID,
    Sales.SalesOrderHeader.OrderDate,
    Sales.SalesOrderHeader.CustomerID,
    Sales.SalesOrderDetail.ProductID,
    Production.Product.Name AS ProductName,
    Production.ProductCategory.Name AS ProductCategory,
    Sales.SalesOrderDetail.UnitPrice,
    Sales.ShoppingCartItem.Quantity,
    Sales.SalesOrderDetail.LineTotal AS TotalPrice,
    Sales.SalesOrderHeader.SalesPersonID,
    Person.Person.FirstName AS SalesPersonName,
    Person.Address.AddressLine1 AS ShippingAddress,
    Person.Address.City AS BillingAddress
FROM 
    Sales.SalesOrderHeader
INNER JOIN
    Sales.SalesOrderDetail ON Sales.SalesOrderHeader.SalesOrderID = Sales.SalesOrderDetail.SalesOrderID
INNER JOIN
    Sales.SalesPerson ON Sales.SalesOrderHeader.SalesPersonID = Sales.SalesPerson.BusinessEntityID
INNER JOIN
    Production.Product ON Sales.SalesOrderDetail.ProductID = Production.Product.ProductID
INNER JOIN
    Sales.ShoppingCartItem ON Production.Product.ProductID = Sales.ShoppingCartItem.ProductID
INNER JOIN
    Person.Person ON Sales.SalesPerson.BusinessEntityID = Person.Person.BusinessEntityID
INNER JOIN
    Person.Address ON Sales.SalesOrderHeader.BillToAddressID = Person.Address.AddressID 
                    AND Sales.SalesOrderHeader.ShipToAddressID = Person.Address.AddressID
CROSS JOIN
    Production.ProductCategory;

````

