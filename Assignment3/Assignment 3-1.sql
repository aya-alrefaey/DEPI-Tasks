create database Company2;
create table Department(
DNum int primary key Identity(1,1),
DName nvarchar(50) unique not null,
Locations nvarchar(200) not null,
);
create table Employee(
SSN int primary key  ,
FName nvarchar(50) not null,
LName nvarchar(50)not null,
Gender char(1) check(Gender='M'or Gender='f' or Gender='m'or Gender='F'),
Birth_Date date not null,
DeptNum INT not null ,
Supervisor_ID INT,
 FOREIGN KEY (Supervisor_ID) REFERENCES Employee(SSN) on delete set null ,
);
ALTER TABLE Employee ADD CONSTRAINT empdef DEFAULT 4 FOR DeptNum;
alter table Employee add constraint empdeptfk FOREIGN KEY (DeptNum) REFERENCES Department(DNum) on delete set default ;

create table Project(
PNumber int primary key Identity(1000,1) ,
PName nvarchar(50) not null,
Location_City nvarchar(200) not null ,
DeptNum int not null,


);
ALTER TABLE project ADD CONSTRAINT prodef DEFAULT 4 FOR DeptNum;
alter table Project add constraint prodeptfk foreign key (DeptNum) references Department(DNum) on delete set default;



insert into Department (DName,Locations)
values('HR','Cairo'),
('IT','Alex'),
('Finance','Giza'),
('general','cairo');



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
Dependent_Name nvarchar(100) ,
Gender char(1) check(Gender='M'or Gender='f' or Gender='m'or Gender='F'),
Birth_Date date not null,
EmpSSN int not null,
primary key(EmpSSN,Dependent_Name),
foreign key (EmpSSN) references Employee(SSN) ON DELETE CASCADE ,

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
    Working_Hours INT not null,
    PRIMARY KEY (EmpSSN, PNumber),
    FOREIGN KEY (EmpSSN) REFERENCES Employee(SSN) on delete no action on update cascade,
    FOREIGN KEY (PNumber) REFERENCES Project(PNumber) on delete no action ,
);
create table manage(
EmpSSN int,
DeptNum int,
Hiring_Date date not null,
primary key(EmpSSN,DeptNum),
foreign key(EmpSSN)references Employee(SSN) on delete no action ,
foreign key(DeptNum)references Department(DNum),

);
SELECT Employee.*, Project.PName, Employee_projects.Working_Hours
FROM Employee , Project , Employee_projects 
WHERE Employee.SSN = Employee_projects.EmpSSN AND Project.PNumber = Employee_projects.PNumber;


