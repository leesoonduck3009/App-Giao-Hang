CREATE DATABASE	APP_GIAO_HANG	
GO
USE APP_GIAO_HANG
GO
SET DATEFORMAT DMY
CREATE TABLE Employee(
	EmployeeID BIGINT IDENTITY,
	EmployeeCode as 'EMP' + right(RIGHT(YEAR(DateJoin), 2) + '00000' + cast(EmployeeID as varchar(5)), 5) persisted ,
	EmployeeName NVARCHAR(max),
	DateJoin DATETIME,
	PhoneNumber VARCHAR(10),
	IdentityNumber VARCHAR(12),
	Birthday DATETIME,
	CONSTRAINT PKEmployee_ID PRIMARY KEY(EmployeeID)
)
GO
CREATE TABLE Account(
	AccountID BIGINT IDENTITY,
	UserName VARCHAR(50) UNIQUE,
	Password VARCHAR(max),
	AccountCreateTime DATETIME,
	Roles VARCHAR(max),
	EmployeeID BIGINT,
	CONSTRAINT PKAccount_ID PRIMARY KEY(AccountID),
	CONSTRAINT FKAccount_EmployeeID FOREIGN KEY(EmployeeID) REFERENCES Employee
)
GO
CREATE TABLE Customer(
	CustomerID BIGINT IDENTITY,
	CustomerCode as 'CUS' + right(RIGHT(YEAR(DateCreate), 2) + '00000' + cast(CustomerID as varchar(5)), 5) persisted ,	
	DateCreate DATETIME,
	CustomerName NVARCHAR(max),
	Birthday DATETIME,
	CustomerRank NVARCHAR(max),
	CONSTRAINT PKCustomer_ID PRIMARY KEY(CustomerID)
)
GO
CREATE TABLE CustomerOrderInformation(
	CustomerOrderInformationID BIGINT IDENTITY,
	CustomerID BIGINT,
	PhoneNumber VARCHAR(10),
	Address NVARCHAR(max),
	CONSTRAINT PKCustomerOrderInformation_ID PRIMARY KEY(CustomerOrderInformationID),
	CONSTRAINT FKCustomerOrderInformation_CustomerID FOREIGN KEY(CustomerID) REFERENCES Customer(CustomerID)
)
CREATE TABLE Product(
	ProductID BIGINT IDENTITY,
	ProductName NVARCHAR(max),
	Description NVARCHAR(max),
	Price FLOAT,
	CONSTRAINT PKProduct_ID PRIMARY KEY(ProductID)
)
CREATE TABLE CustomerOrder(
	CustomerOrderID BIGINT IDENTITY,
	OrderDate DATETIME,
	CustomerOrderInformationID BIGINT,
	EmployeeID BIGINT,
	TotalPrice FLOAT Default 0,
	OrderStatus NVARCHAR(max),
	Constraint PKCustomerOrder_ID PRIMARY KEY(CustomerOrderID),
	Constraint FKCustomerOrder_CustomerOrderInformationID FOREIGN KEY(CustomerOrderInformationID) REFERENCES CustomerOrderInformation(CustomerOrderInformationID),
	Constraint FKCustomerOrder_EmployeeID FOREIGN KEY(EmployeeID) REFERENCES Employee(EmployeeID)
)

CREATE TABLE CustomerOrderDetail(
	CustomerOrderDetailID BIGINT IDENTITY,
	ProductID BIGINT,
	CustomerOrderID BIGINT,
	Quantity INT,
	OrderDetailPrice FLOAT,
	CONSTRAINT PKCustomerOrderDetail_ID PRIMARY KEY(CustomerOrderDetailID),
	CONSTRAINT FKCustomerOrderDetail_ProductID FOREIGN KEY(ProductID) REFERENCES Product(ProductID),
	CONSTRAINT FKCustomerOrderDetail_CustomerOrderID FOREIGN KEY(CustomerOrderID) REFERENCES CustomerOrder(CustomerOrderID),

)
GO
CREATE TRIGGER TrUpdateTotalPriceOfCustomerOrder ON CustomerOrderDetail FOR INSERT

AS
BEGIN
	DECLARE @CustomerOrderID BIGINT,
	 @ProductPrice FLOAT,
	 @TotalPrice FLOAT,
	 @Quantity INT,
	 @PreviousPrice FLOAT
	 SELECT @CustomerOrderID = CustomerOrderID FROM inserted;
	 SELECT @ProductPrice = Product.Price FROM inserted JOIN Product ON inserted.ProductID = Product.ProductID
	 SELECT @Quantity = inserted.Quantity FROM inserted
	 SELECT @TotalPrice = CustomerOrder.TotalPrice FROM inserted JOIN CustomerOrder ON CustomerOrder.CustomerOrderID = inserted.CustomerOrderID
	 IF EXISTS (SELECT * FROM inserted)
	 BEGIN
		UPDATE CustomerOrder
		SET TotalPrice = @TotalPrice + @Quantity*@ProductPrice
		WHERE CustomerOrderID = @CustomerOrderID
	 END
END
GO
--CREATE TRIGGER TrUpdateTotalPriceOfCustomerOrderUpdate ON CustomerOrderDetail FOR UPDATE

--AS
--BEGIN
--	DECLARE @CustomerOrderID BIGINT,
--	 @ProductPrice FLOAT,
--	 @TotalPrice FLOAT,
--	 @Quantity INT,
--	 @PreviousPrice FLOAT
--	 SELECT @CustomerOrderID = CustomerOrderID FROM inserted;
--	 SELECT @ProductPrice = Product.Price FROM inserted JOIN Product ON inserted.ProductID = Product.ProductID
--	 SELECT @Quantity = inserted.Quantity FROM inserted
--	 SELECT @TotalPrice = CustomerOrder.TotalPrice FROM inserted JOIN CustomerOrder ON CustomerOrder.CustomerOrderID = inserted.CustomerOrderID
--	 SELECT @PreviousPrice = OrderDetailPrice FROM deleted


--		UPDATE CustomerOrder
--		SET TotalPrice = @TotalPrice -@PreviousPrice + @Quantity*@ProductPrice
--		WHERE CustomerOrderID = @CustomerOrderID
--END
--GO
--CREATE TRIGGER TrUpdateTotalPriceOfCustomerOrderDELETE ON CustomerOrderDetail FOR DELETE

--AS
--BEGIN
--	DECLARE @CustomerOrderID BIGINT,
--	 @ProductPrice FLOAT,
--	 @TotalPrice FLOAT,
--	 @Quantity INT,
--	 @PreviousPrice FLOAT
--	 SELECT @CustomerOrderID = CustomerOrderID FROM inserted;
--	 SELECT @ProductPrice = Product.Price FROM inserted JOIN Product ON inserted.ProductID = Product.ProductID
--	 SELECT @Quantity = inserted.Quantity FROM inserted
--	 SELECT @TotalPrice = CustomerOrder.TotalPrice FROM inserted JOIN CustomerOrder ON CustomerOrder.CustomerOrderID = inserted.CustomerOrderID
--	 IF EXISTS (SELECT * FROM deleted)
--	 BEGIN
--		 SELECT @TotalPrice = CustomerOrder.TotalPrice FROM inserted JOIN CustomerOrder ON CustomerOrder.CustomerOrderID = inserted.CustomerOrderID
--		 SELECT @PreviousPrice = OrderDetailPrice FROM deleted
--		 UPDATE CustomerOrder
--		 SET TotalPrice = @TotalPrice - @PreviousPrice
--		 WHERE CustomerOrderID = @CustomerOrderID
--	 END
--END
--GO
CREATE TRIGGER TrUSetOrderPriceOfCustomerOrderDetail ON CustomerOrderDetail FOR INSERT,UPDATE 
AS
BEGIN
	DECLARE @CustomerOrderDetailID BIGINT,
	 @ProductPrice FLOAT,
	 @Quantity INT
	 SELECT @CustomerOrderDetailID = CustomerOrderDetailID FROM inserted;
	 SELECT @ProductPrice = Product.Price FROM inserted JOIN Product ON inserted.ProductID = Product.ProductID
	 SELECT @Quantity = inserted.Quantity FROM inserted

	 UPDATE CustomerOrderDetail
	 SET OrderDetailPrice = @Quantity *@ProductPrice 
	 WHERE CustomerOrderDetailID = @CustomerOrderDetailID
END
GO
INSERT INTO Employee (EmployeeName, DateJoin, PhoneNumber, IdentityNumber, Birthday) VALUES (N'Nguyễn Văn A', GETDATE(),'0123456789','012345678901','30/05/2000')
INSERT INTO Employee (EmployeeName, DateJoin, PhoneNumber, IdentityNumber, Birthday) VALUES (N'Nguyễn Phúc Bình', GETDATE(),'0123666789','632345678901','30/05/2003')

INSERT INTO Account (UserName,Password,AccountCreateTime, EmployeeID, Roles) VALUES (N'admin','1234',GETDATE(), '1', 'admin')
INSERT INTO Account (UserName,Password,AccountCreateTime, EmployeeID, Roles) VALUES (N'nhanvien','1234',GETDATE(), '2', 'nhanvien')

INSERT INTO Customer (CustomerName,CustomerRank,DateCreate,Birthday) VALUES(N'Nguyễn Văn B', 'Đồng', GETDATE(),'01/01/1999')
INSERT INTO CustomerOrderInformation (CustomerID,Address,PhoneNumber) VALUES('1',N'211 Phạm Văn Đồng', '0123123123'),
																		('1',N'241 HVT', '01112312')
INSERT INTO CustomerOrder(CustomerOrderInformationID,EmployeeID,OrderDate,OrderStatus) VALUES('1','1',GETDATE(),'Đang giao')
INSERT INTO Product(ProductName,Price,Description) VALUES (N'Sản phẩm A', 100000, N'Sản phẩm A có tính năng BC')
INSERT INTO Product(ProductName,Price,Description) VALUES (N'Sản phẩm B', 500000, N'Sản phẩm B có tính năng CD')
INSERT INTO CustomerOrderDetail(CustomerOrderID,ProductID,Quantity) VALUES('1','2',10)

SELECT * FROM CustomerOrder
SELECT * FROM CustomerOrderDetail
UPDATE CustomerOrderDetail
SET Quantity = 10
WHERE CustomerOrderDetailID = 1