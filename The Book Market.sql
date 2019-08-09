Use Master
CREATE DAtabase [The_Book_Market]



Use [The_Book_Market]
Go
CREATE TABLE Inventory_Level_Status
(
InvLevel_Status_ID int Identity (5,5) Primary Key Not Null,
InvLevel_Description varchar (30) ,
)
GO


CREATE TABLE Inventory_Type
(
InventoryType_ID int Identity (50,10) Primary Key Not Null,
InventoryType_Name varchar (30) ,
InventoryType_Description varchar(50)
)
GO

CREATE TABLE Stock_Turns
(
StockTurn_ID int Identity (1,3) Primary Key Not Null,
Turn_Over_Ratio varchar (10),
)
GO

CREATE TABLE Inventory 
(
[Inventory_ID] int Identity  (100,1) Primary Key Not Null,
InventoryType_ID int references Inventory_Type (InventoryType_ID),
Inventory_Name varchar (30),
Inventory_Description varchar (150), 
Inventory_Quantity int ,
Minimum_Quantity int ,
StockTurn_ID int references Stock_Turns (StockTurn_ID)
)
GO

CREATE TABLE Inventory_Level
(
[Inventory_ID] int references Inventory ([Inventory_ID]) ,
InvLevel_Status_ID int references Inventory_Level_Status (InvLevel_Status_ID),
InvLevel_Description varchar (30),
Primary Key(Inventory_ID,InvLevel_Status_ID)
)
GO

CREATE TABLE Inventory_Price
(
Price_ID int Identity (55,1) Primary Key Not Null,
Inventory_ID int references Inventory (Inventory_ID),
DateActive DATE 
)
GO 

CREATE TABLE Payment_Type
(
PaymentType_ID int Identity (3000,100) primary key Not Null,
PaymentType_Name varchar(30) ,
PaymentType_Description  varchar(50)
)
GO

CREATE TABLE [User_Role]
(
UserRole_ID int Identity (800,2) Primary Key Not Null,
UserRole_Description varchar(50),

)
GO

CREATE TABLE [User]
(
[User_ID] int Identity (900,2) Primary Key Not Null,
UserRole_ID int references [User_Role] (UserRole_ID) ,
UserName varchar(30) ,
UserPassword varchar(30),
[GUID] varchar(30),
GUIDExpiry DATETIME 
)
GO

CREATE TABLE Employee_Title
(
EmpTitle_ID int Identity (200,5) Primary Key Not Null,
Title_Description varchar (10),
)
GO 

CREATE TABLE Employee_Gender
(
EmpGender_ID int Identity (69000,27000) Primary Key Not Null,
Gender_Description varchar (10),
)
GO

CREATE TABLE Employee
(
Employee_ID int Identity (1000,1) Primary Key Not Null,
[User_ID]  int references [User] ([User_ID]),
Employee_Name varchar(35) ,
Employee_Surname varchar(35), 
Employee_Address varchar(50),
Emp_Phone int , 
Emp_Email varchar(30), 
ID_Number bigint,
EmpTitle_ID int references Employee_Title (EmpTitle_ID),
EmpGender_ID int references Employee_Gender (EmpGender_ID),
ImageData image
)
GO

CREATE TABLE Customer
(
Customer_ID int Identity (2000,1) Primary Key Not Null,
Customer_Name varchar(50) ,
Customer_Surname varchar(50), 
Customer_Email varchar(30),
Customer_Contact bigint, 
)
GO

CREATE TABLE TaxRate
(
TaxRate_ID int Identity (400,5) Primary Key Not Null,
Tax_Description varchar(30), 
Tax_Percent int  
)
GO

CREATE TABLE Sale 
(
Sale_ID int Identity (411,1) PRIMARY KEY  Not Null,
Customer_ID int references Customer (Customer_ID) Not Null,
PaymentType_ID int references Payment_Type (PaymentType_ID),
Employee_ID int references Employee (Employee_ID), 
TaxRate_ID int references TaxRate (TaxRate_ID )
 )
GO  

CREATE TABLE Sale_Line
(
Sale_ID int references Sale(Sale_ID),
Inventory_ID int references Inventory (Inventory_ID),
Customer_ID int references Customer(Customer_ID),
Quanity int ,
Sale_Total FLOAT, 
Sale_Date DATE,
PRIMARY KEY (Sale_ID, Customer_ID,Inventory_ID)
)
GO  

CREATE TABLE Return_Sale
(
Return_ID int Identity (4000,1) Primary Key Not Null,
Inventory_ID int references Inventory (Inventory_ID),
)
GO 

CREATE TABLE Return_Sale_Line
(
Return_ID int  references Return_Sale (Return_ID) Not Null,
Sale_ID int references Sale (Sale_ID), 
Return_Quantity int,
Return_Date DATE,
Reason_For_Return varchar(200)
Primary key(Return_ID,Sale_ID)
)
GO 

CREATE TABLE Write_Off_Stock
(
Write_Off_ID int Identity (4600,2) Primary Key NOt null,
Inventory_ID int references Inventory(Inventory_ID), 
)
GO	

CREATE TABLE Write_Off_Line
(
Write_Off_ID int references Write_Off_Stock (Write_Off_ID),
Inventory_ID int references Inventory (Inventory_ID) , 
[Write_Off_Date] DATE ,
Write_Off_Reason varchar(225),
Primary key(Inventory_ID,Write_Off_ID)
)
GO	

CREATE TABLE Book_Status
(
BookStatus_ID int Identity (10,10) Primary Key Not Null,
BookStatus_Description varchar (35) ,
)
GO

CREATE TABLE Condition
(
Condition_ID int Identity (6000,500) Primary Key Not Null,
Condition_Description varchar (35) ,
)
GO 

CREATE TABLE Book_Supplier 
(
BookSupplier_ID int Identity (10000,1) Primary Key ,
BookSupplier_Name varchar(35),
BookSupplier_Surname varchar(35), 
BookSupplier_Email varchar(30),
BookSupplier_Address varchar(50),
BookSupplier_Type varchar(30),
)
GO

CREATE TABLE Prospective_Book
(
ProsBook_ID int Identity (11000,1) Primary Key Not Null,
BookSupplier_ID int references Book_Supplier (BookSupplier_ID),
ProsBook_Date DATE
)
GO 

CREATE TABLE Book
(
Book_ID int Identity (12000,1) Primary Key Not Null,
Book_Title varchar(50),
Book_Author varchar(50),
ISBN varchar(100), 
Book_Edition int, 
BookStatus_ID int references Book_Status(BookStatus_ID),
)
GO 

CREATE TABLE Book_Condition
(
Condition_ID int   references Condition(Condition_ID) Not Null,
Book_ID int   references Book(Book_ID),
Quantity int,
Price int, 
PRIMARY KEY(Condition_ID,Book_ID)
)
GO

CREATE TABLE Purchase
(
Purchase_ID int Identity (15000,1) Primary Key Not Null,
Purchase_Amount int,
Purchase_Date int,
BookSupplier_ID int references Book_Supplier(BookSupplier_ID)
)
GO 

CREATE TABLE Purchase_Line
(
Purchase_ID int references Purchase(Purchase_ID) Not Null,
Book_ID int references Book(Book_ID),
Quantity int 
PRIMARY KEY(Purchase_ID,Book_ID)
)
GO 

CREATE TABLE Book_Request
(
Book_Request_ID int Identity (16000,1) Primary Key Not Null,
Customer_ID int references Customer(Customer_ID),
 
)

CREATE TABLE Book_Request_Line
(
Book_Request_ID int references Book_Request(Book_Request_ID)  Not Null,
Book_ID int references Book(Book_ID),
Quantity int 
Primary Key(Book_Request_ID,Book_ID)
)

CREATE TABLE Inventory_Supplier 
(
InvSupplier_ID int Identity (20000,1) Primary Key ,
InvSupp_Name varchar(50),
InvSupp_Address varchar(35),
InvSupp_Email varchar(30),
InvSupp_Phone int,
)
GO

CREATE TABLE Order_Status
(
SuppOrder_Status_ID int Identity (15000,100) Primary Key Not Null,
Order_Status_Description varchar (35) ,
)
GO

CREATE TABLE Inventory_Supplier_Order
(
InvSuppOrder_ID int Identity (18000,1) Primary Key Not Null,
InvSupplier_ID int references Inventory_Supplier(InvSupplier_ID),
SuppOrder_Status_ID int references Order_Status(SuppOrder_Status_ID),
Order_Date DATE 
)
GO

CREATE TABLE Order_Line
(
InvSuppOrder_ID int  references Inventory_Supplier_Order(InvSuppOrder_ID) Not Null,
Inventory_ID int references Inventory(Inventory_ID),
SuppOrder_Status_ID int references Order_Status(SuppOrder_Status_ID),
Quanity int, 
Line_Total FLOAT,
[Date] DATE,
Primary Key(InvSuppOrder_ID,Inventory_ID)
)
GO  

CREATE TABLE Received_Supplier_Order
(
RecSupp_Order_ID int Identity (23000,1)  Primary Key Not Null,
InvSuppOrder_ID int references Inventory_Supplier_Order(InvSuppOrder_ID),
InvSupplier_ID int references Inventory_Supplier(InvSupplier_ID),
SuppOrder_Status_ID int references Order_Status(SuppOrder_Status_ID),
[Date] DATE,
Quanity int, 
 )
GO

CREATE TABLE Return_Supplier_Order
(
ReturnSupp_Order_ID int Identity (10,1)  Primary Key Not Null,
InvSuppOrder_ID int references Inventory_Supplier_Order(InvSuppOrder_ID),
Quanity int, 
Reason_For_Return varchar(200) 
 )
GO

CREATE TABLE Return_Line
(
ReturnSupp_Order_ID int references Return_Supplier_Order(ReturnSupp_Order_ID) Not Null, 
InvSuppOrder_ID int references Inventory_Supplier_Order(InvSuppOrder_ID)  ,
Quanity int, 
Return_Date DATE
Primary Key(ReturnSupp_Order_ID,InvSuppOrder_ID)
 )
GO



-- Insert into Inventory_Level_Status

INSERT INTO Inventory_Level_Status  values ('Optimal level')
INSERT INTO Inventory_Level_Status  values ('Adequate level')
INSERT INTO Inventory_Level_Status  values ('Low level')
INSERT INTO Inventory_Level_Status  values ('Critical Reorder')
Go

-- Insert into Stock_Turns

INSERT INTO Stock_Turns  values ('12')
INSERT INTO Stock_Turns  values ('15')
INSERT INTO Stock_Turns  values ('25')
INSERT INTO Stock_Turns  values ('55')
Go

---Insert into Inventory_Type Table
Insert into Inventory_Type values ('Stationery','Stationery related items')
Insert into Inventory_Type values ('Book','Academic Textbooks and Literature titles')
Insert into Inventory_Type values ('Accessories','Miscellaneous vital student items')
go


----Insert into Inventory

INSERT INTO Inventory values('50','0.5mm Ball Pen','0.5mm fine point ball pen','135','30',1)
INSERT INTO Inventory values('50','0.7mm Led Refills','0.7mm Graphite Led Stencils','100','30',4)
INSERT INTO Inventory values('50','7pt Eraser','7pt Eraser Hardnose 7x6 Square','202','25',4)
INSERT INTO Inventory values('70','Impresario Earphones','1m Long In-Ear Earphones','87','10',7)
INSERT INTO Inventory values('70','Deadpool Noise Earphones','Passive Noise Cancelling Earphones','58','15',10)
go


----Insert Into Employee_Title
INSERT Into Employee_Title values('Mr')
INSERT Into Employee_Title values('Ms')
INSERT Into Employee_Title values('Mrs')
INSERT Into Employee_Title values('Dr')
INSERT Into Employee_Title values('Prof')
go

---Insert into Employee_Gender
insert into Employee_Gender values('Male')
insert into Employee_Gender values('Female')
insert into Employee_Gender values('Other')
go 


---Insert into User_Role
insert into User_Role values('Manager')
insert into User_Role values('Clerk')
go

---Insert into [User]
insert into [User] values('800','Bond','123456','jd85988465','04-05-2019')
insert into [User] values('802','James','123456','aboiji88465','04-05-2019')
go


---Insert into TaxRate
insert into TaxRate values('State Product Tax','15')
go

---Insert into Employee
INSERT INTO Employee values('900','Marell','Vapong','78 Hatfiled Drive, Pretoria','0733267855','mvapong@mailme.com','8605459488759','205','96000','0x010203040506')
INSERT INTO Employee values('902','Mavis','Mavresh','256 Stanza Bo Timer, Arcadia','0785565684','mavresh@mailme.com','778954256315','210','96000','0x010203040507')
Go

---Insert into Customer
Insert Into Customer Values ('John Wesly Phuphuta', 'Phuphuta', 'jw411@gmail.com','0732257848')
Insert Into Customer Values ('Mama', 'Afrika', 'mamakajumayma@gmail.com','0865658758')
Insert Into Customer Values ('Jolyn', 'Palmer', 'formulajumpjp@gmail.com','07985865854')
Insert Into Customer Values ('Zack', 'Snyder', 'ezisnacks@gmail.com','01285796584')
Go


---Insert into Book_Status
insert into Book_Status values('Available')
insert into Book_Status values('Requested')
insert into Book_Status values('Prospective')
insert into Book_Status values('in demand') --if requested more than once
go

---Insert into Book
insert into Book values('1 million words','Brian Griffin','978-0-639-0126-5','3',10)
insert into Book values('Assurance: Audit Perspective','Ehi Oka Moka','878-0-677-2580-5','4',10)
insert into Book values('OBS 321: Final Observation','Gaba Absa','8625-0-877-2950-5','4',20)
insert into Book values('Riding Dirty For Dummies','Gofer Modal','565-5-785-652-5','1',20)
Go

---Insert into Condition
insert into Condition values('Excellent')
insert into Condition values('Good')
insert into Condition values('Bad')
GO

---Insert into Book_Condition
insert into Book_Condition values('6000','12000','1','255')
insert into Book_Condition values('6000','12001','2','250')
insert into Book_Condition values('6000','12002','1','260')
insert into Book_Condition values('6500','12003','1','185')
GO

---Insert into Inventory_Supplier
insert into Inventory_Supplier values('TDK Wholesalers','106 Industry drive, Auckland Park','tdksuppliers@mail1.com','0112548569')
insert into Inventory_Supplier values('StationAir','23 Barry Lane, HydePark','station1air@mail1.com','0132548985')
go