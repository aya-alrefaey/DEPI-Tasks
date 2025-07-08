create database Company;
create table Department(
DNum int primary key Identity(1,1),
DName nvarchar(50),
Locations nvarchar(200),
);
create table Employee(
SSN int primary key  ,
FName nvarchar(50),
LName nvarchar(50),
Gender char(1),
Birth_Date date,
DeptNum INT,
Supervisor_ID INT,
 FOREIGN KEY (DeptNum) REFERENCES Department(DNum),
 FOREIGN KEY (Supervisor_ID) REFERENCES Employee(SSN),
);
create table Project(
PNumber int primary key Identity(1000,1) ,
PName nvarchar(50),
Location_City nvarchar(200) ,
DeptNum int,
foreign key (DeptNum) references Department(DNum),

);
insert into Department (DName,Locations)
values('HR','Cairo'),
('IT','Alex'),
('Finance','Giza');



insert into Employee (SSN,FName,LName,Gender,Birth_Date,DeptNum,Supervisor_ID)
values(12345,'Aya','Al-Refaey','f','1-6-2004',2,24680),
(678910,'Habiba','Al-Refaey','f','1-6-2004',1,13579),
(13579,'Doha','Harby','f','1-6-2004',2,24680),
(24680,'Ahmed','Al-Refaey','m','1-6-2004',3,12457),
(12457,'Mohamed','Al-Refaey','m','1-6-2004',3,678910);



update Employee 
set DeptNum=1
where SSN=12345;

create table Dependent(
Dependent_Name nvarchar(100),
Gender char(1),
Birth_Date date,
EmpSSN int,
primary key(EmpSSN,Dependent_Name),
foreign key (EmpSSN) references Employee(SSN),
);


insert into Dependent (Dependent_Name,Gender,Birth_Date,EmpSSN)
values('sahar','f','1-6-2004',12345),
('fatma','f','1-6-2004',678910),
('rawan','f','1-6-2004',13579),
('jomana','f','1-6-2004',12345);

DELETE FROM Dependent
WHERE EmpSSN = 13579 ;

select *
from Employee
where DeptNum=1;

create table Employee_projects(
    EmpSSN INT,
    PNumber INT,
    Working_Hours INT,
    PRIMARY KEY (EmpSSN, PNumber),
    FOREIGN KEY (EmpSSN) REFERENCES Employee(SSN),
    FOREIGN KEY (PNumber) REFERENCES Project(PNumber)
);
create table manage(
EmpSSN int,
DeptNum int,
Hiring_Date date,
primary key(EmpSSN,DeptNum),
foreign key(EmpSSN)references Employee(SSN),
foreign key(DeptNum)references Department(DNum),

);


SELECT Employee.*, Project.PName, Employee_projects.Working_Hours
FROM Employee , Project , Employee_projects 
WHERE Employee.SSN = Employee_projects.EmpSSN AND Project.PNumber = Employee_projects.PNumber;

--or

SELECT Employee.*, Project.PName, Employee_projects.Working_Hours
FROM Employee 
JOIN Employee_projects  ON Employee.SSN = Employee_projects.EmpSSN
JOIN Project  ON Employee_projects.PNumber = Project.PNumber;

