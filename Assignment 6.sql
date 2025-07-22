--1
declare @spend float =(
select sum(oi.list_price) as alll from sales.orders o 
join sales.order_items oi on o.order_id=oi.order_id where o.customer_id=1);
if @spend>5000
print 'vip customer';
else
print 'regular customer';

--2

declare @threshold  int =1500;
declare @number int =( select count(*) from production.products where list_price>@threshold );
print 'Threshold Price is '+cast(@Threshold as varchar)+' and no of products above Threshold is '+cast(@number as varchar);

--3
declare @staff_id int=2;
declare @year int=2017;
declare @total_sales float=(
select sum(oi.list_price)
from sales.staffs s
left join sales.orders o on s.staff_id=o.staff_id
left join sales.order_items oi on oi.order_id=o.order_id
where s.staff_id=@staff_id and year(o.order_date)=@year
)
select @staff_id as staff_id , @year as work_year , @total_sales as total_sales;
--4

select @@SERVERNAME as server_name,
@@VERSION as sql_version,
@@rowcount as 'number of rows affected by the last statement';

--5
declare @product int=1;
declare @store int=1;
declare @stock int=(
select quantity
from sales.stores s
join production.stocks ss on ss.store_id=s.store_id
where ss.product_id=@product and s.store_id=@store
);
if @stock>20
print 'Well stocked'
else if @stock>=10 and @stock<=20
print 'Moderate stock'
else if @stock<10
print 'Low stock - reorder needed'
else 
print'not found'

--6

select product_id
into production.lowstock
from production.stocks
where quantity<5;

declare @total int =( select count(*) from production.lowstock );
declare @i int =1;
while @i<=@total
begin

with top3 as(
select top(3) product_id
from production.lowstock
)
update  s
set s.quantity=s.quantity+10
from top3 t3
join production.stocks s on t3.product_id=s.product_id;

delete top(3) from production.lowstock;

print '3 low stock products are updated';
set @i=@i+3;
end
--7

select product_id,product_name,list_price,
case
when list_price<300 then 'Budget'
when list_price between 300 and 800 then 'Mid-Range'
when list_price between 801 and 2000 then 'Premium'
when list_price >2000 then 'Luxury'
end as 'price category'
from production.products;

--8

declare @id int =5;
declare @count int =0;
set @count =(select count(*) from sales.customers where customer_id=@id);
if @count>0
begin
declare @all int =0;
 set @all =(select count(*)
from sales.customers c
join sales.orders o on c.customer_id=o.customer_id
where o.customer_id=@id)
print 'customer is found and number of its total orders is '+ cast(@all as varchar(50));
end
else
print 'customer is not found';

--9
create function sales.CalculateShipping(@tot float)
returns float as
begin
declare @shipping float;
if @tot>100
set @shipping=0;
else if @tot between 50 and 99
set @shipping=5.99;
else if @tot<50
set @shipping=12.99;
return @shipping;
end

-- 10

create function GetProductsByPriceRange(@min money,@max money)
returns table as
return(
select p.product_id,p.product_name,c.category_name,b.brand_name
from production.products p
join production.categories c on c.category_id=p.category_id
join production.brands b on b.brand_id=p.brand_id
where p.list_price between @min and @max
);

-- 11

create function GetCustomerYearlySummary(@custid int)
returns @var table (orderyear int,total_orders int,total_spent money,average_for_year money) as
begin
insert into @var
select year(o.order_date) as 'order year',count(distinct o.order_id) as 'total orders',
sum(oi.list_price) as 'total spent',avg(oi.list_price) as 'average for year'
from sales.orders o
join sales.order_items oi on oi.order_id=o.order_id
join sales.customers c on c.customer_id=o.customer_id
where o.customer_id=@custid
group by year(o.order_date);
return;
end;
-- 12

create function production.CalculateBulkDiscount (@qty int)
returns float as
begin
declare @discount float=0;
if @qty between 1 and 2
set @discount=0;
else if @qty between 3 and 5
set @discount=0.05;
else if @qty between 6 and 9
set @discount=0.10;
else if @qty >=10
set @discount=0.15;
return @discount;
end

-- 13

create procedure sp_GetCustomerOrderHistory
@id int,
@startdate date=null,
@enddate date=null
as
begin
select o.order_id,o.order_date,count(o.order_id)as 'total orders',
sum(oi.quantity) as 'total quantity',sum(oi.list_price*oi.quantity) as 'total money'
from sales.orders o
join sales.order_items oi on oi.order_id=o.order_id
where o.customer_id=@id and (o.order_date=null or o.order_date>=@startdate)
and (o.order_date=null or o.order_date<=@enddate)
group by o.order_id,o.order_date
end

-- 14

create procedure sp_RestockProduct 
@storeid int ,
@productid int ,
@restockquantity int,
@oldquantity int output,
@newquantity int output,
@successstatus int output
as
begin
if exists (select 1 from production.stocks where store_id=@storeid and product_id=@productid)
begin
set @oldquantity=(select quantity from production.stocks where store_id=@storeid and product_id=@productid );
update production.stocks
set quantity=quantity+@restockquantity
where store_id=@storeid and product_id=@productid;
set @newquantity=@oldquantity+@restockquantity;
set @successstatus=1;
end
else
begin
set @oldquantity=0;
set @newquantity=0;
set @successstatus=0;
end
end

-- 15

create procedure sp_ProcessNewOrder
@customerid int,
@productid int,
@quantity int,
@storeid int
as
begin
begin try
begin transaction
insert into sales.orders(customer_id,store_id,order_date)
values(@customerid,@storeid,GETDATE());
 declare @OrderID int = SCOPE_IDENTITY();
insert into sales.order_items(order_id,product_id,quantity)
values(@OrderID,@productid,@quantity);
update production.stocks
set quantity=quantity-@quantity
where store_id=@storeid and product_id=@productid;
print 'order added successfully'
commit transaction
end try
begin catch
rollback transaction;
print 'an error occured '+error_message();
end catch
end

-- 16

create procedure sp_SearchProducts
@productname varchar(50)=null,
@catid int =null,
@minprice money =null,
@maxprice money =null,
@sort varchar(50)=null
as
begin
SELECT 
        product_id,
        product_name,
        category_id,
        list_price
    FROM 
        production.products
    WHERE 
        (@ProductName IS NULL OR product_name LIKE '%' + @ProductName + '%') AND
        (@CatID IS NULL OR category_id = @CatID) AND
        (@MinPrice IS NULL OR list_price >= @MinPrice) AND
        (@MaxPrice IS NULL OR list_price<= @MaxPrice)
    ORDER BY 
	  CASE WHEN @sort = 'product name' THEN product_name END,
        CASE WHEN @sort = 'category id' THEN category_id END,
        CASE WHEN @sort = 'price' THEN list_price END,
        product_id 
end
-- 16.2 -- after search for a better solution
CREATE PROCEDURE sp_SearchProducts2
@productname VARCHAR(50) = NULL,
@catid INT = NULL,
@minprice MONEY = NULL,
@maxprice MONEY = NULL,
@sort VARCHAR(50) = NULL
AS
BEGIN
    DECLARE @sql NVARCHAR(MAX) = '
    SELECT product_id, product_name, category_id, list_price
    FROM production.products
    WHERE 1 = 1';

    IF @productname IS NOT NULL
        SET @sql += ' AND product_name LIKE ''%' + @productname + '%''';

    IF @catid IS NOT NULL
        SET @sql += ' AND category_id = ' + CAST(@catid AS VARCHAR);

    IF @minprice IS NOT NULL
        SET @sql += ' AND list_price >= ' + CAST(@minprice AS VARCHAR);

    IF @maxprice IS NOT NULL
        SET @sql += ' AND list_price <= ' + CAST(@maxprice AS VARCHAR);

    SET @sql += ' ORDER BY ' + 
        CASE 
            WHEN @sort = 'product name' THEN 'product_name'
            WHEN @sort = 'category id' THEN 'category_id'
            WHEN @sort = 'price' THEN 'list_price'
            ELSE 'product_id'
        END;

    EXEC sp_executesql @sql;
END

-- 17

create procedure sp_addbonus as
begin
declare @startdate date='2022-01-1';
declare @enddate date='2022-03-30';
declare @bonus1 float = 0.05;
declare @bonus2 float = 0.10;
declare @bonus3 float = 0.15;

select o.staff_id,count(o.staff_id)as 'no of orders',sum(list_price) as'total revenue',
case
when sum(list_price)<1000 then sum(list_price)*@bonus1
when sum(list_price) between 1000 and 2000 then sum(list_price)*@bonus2
else sum(list_price)*@bonus1
end as bonus
from sales.orders o
join sales.order_items oi on oi.order_id=o.order_id
where o.order_date between @startdate and @enddate
group by o.staff_id;
end

-- 18

create procedure sp_restock 
 @catid int
as
begin
if @catid=1
begin
update s
set s.quantity=s.quantity+100
from production.stocks s
join production.lowstock ls on s.product_id=ls.product_id
end
else if @catid=2
begin
update s
set s.quantity=s.quantity+200
from production.stocks s
join production.lowstock ls on s.product_id=ls.product_id
end
else if @catid=3
begin
update s
set s.quantity=s.quantity+300
from production.stocks s
join production.lowstock ls on s.product_id=ls.product_id
end
else
begin
update s
set s.quantity=s.quantity+500
from production.stocks s
join production.lowstock ls on s.product_id=ls.product_id
end
end

-- 19

create procedure SP_customersrating as
begin
select c.customer_id,c.first_name+' '+c.last_name as full_name,isnull(sum(oi.list_price),0),
case
when isnull(sum(oi.list_price),0)<1000 then 'bronze'
when isnull(sum(oi.list_price),0)between 1000 and 2000 then 'silver'
when isnull(sum(oi.list_price),0)>2000 then 'gold'
else 'no category'
end as 'loyality'
from sales.customers c
left join sales.orders o on o.customer_id=c.customer_id
left join sales.order_items oi on oi.order_id=o.order_id
group by c.customer_id,c.first_name,c.last_name
end

-- 20 

create procedure sp_discontinueproduct
@productid int,
@newid int
as
begin
 if exists (
select 1
from sales.order_items oi
join sales.orders o ON o.order_id = oi.order_id
where oi.product_id = @productid and o.order_status in (1, 2, 3) 
    )
begin
print 'Cannot discontinue: Product has pending orders.';
return;
end
if @newid is not null
begin
update oi
set oi.product_id=@newid
from  sales.order_items oi 
join sales.orders o on o.order_id=oi.order_id
where oi.product_id=@productid and order_status=4;
print 'replacement occurs in orders'
end
else
begin
print 'No replacement product provided.';
end
 update production.stocks
    set quantity = 0
  where product_id = @productid;
  print'product stock now is 0'
end