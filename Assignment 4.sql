use StoreDB;

--1

select count(*) as 'No. of products'
from production.products;

--2
 select AVG(list_price) as 'average price',max(list_price) as 'maximum price',min(list_price) as 'minimum price'
 from production.products;

 --3

 select p.category_id,c.category_name, count(*) as'no. of products'
 from production.products p
 join production.categories c on p.category_id=c.category_id
 group by p.category_id , c.category_name;

 --4
 
 select o.store_id,s.store_name,count(*)as 'no. of orders'
 from sales.orders o
 join sales.stores s on s.store_id=o.store_id
 group by o.store_id ,s.store_name;

 --5

 select top(10) customer_id,UPPER(first_name)+' '+LOWER(last_name) as 'full name'
 from sales.customers;

 --6

 select top(10) product_name,len(product_name)as 'length of name'
 from production.products;

 --7

 select customer_id,LEFT(phone,3) as 'area code'
 from sales.customers
 where customer_id between 1 and 15 ;

 --8
 select order_date,year(order_date)as 'year' ,month(order_date) as 'month'
 from sales.orders
 WHERE order_id between 1 and 10;

 --9

 select top(10) p.product_name , c.category_name
 from production.products p
 join production.categories c on p.category_id=c.category_id;

 --10

 select top(10) first_name+' '+last_name as full_name ,o.order_date
 from sales.customers c
 join sales.orders o on c.customer_id=o.customer_id;

 --11

 select p.product_name,COALESCE(b.brand_name, 'No Brand') AS brand_name
 from production.products  p
 left join production.brands b on b.brand_id=p.brand_id;

 --12

 select *
 from production.products
 where list_price > (select AVG(list_price) from production.products)

 --13

 select customer_id, first_name+' '+last_name as full_name
 from sales.customers
 where customer_id in( select customer_id from sales.orders);

 --14

 select customer_id, first_name+' '+last_name as full_name,(select count(*)from sales.orders o where o.customer_id=c.customer_id) as 'total no. of orders'
 from sales.customers c;

 --15

create view easy_product_list
 as
 select p.product_name,c.category_name,p.list_price
 from production.products p
 join production.categories c on c.category_id=p.category_id;
 

select *
from easy_product_list
where list_price>100; 

--16

create view customer_info
 as
 select customer_id ,first_name+' '+last_name as full_name ,email, city +','+state as 'Location'
 from sales.customers;

 select *
 from customer_info
 where SUBSTRING (Location,CHARINDEX(',',Location)+1,LEN(Location))='CA';
 ---------------------------------or----------------------------
 
 select *
 from customer_info
 where right(location,2)='CA';
 ---------------------------------or-----------------------------
 SELECT *
FROM customer_info
WHERE location LIKE '%,CA';

--17

select product_name,list_price
from production.products
where list_price between 50 and 200
order by list_price;

--18

select state,count(*) as  'customer count'
from sales.customers
group by state
order by count(*) desc;

--19

SELECT 
    c.category_name,
    p.product_name,
    p.list_price
FROM production.products p
JOIN production.categories c 
    ON p.category_id = c.category_id
WHERE p.list_price = (
    SELECT MAX(p2.list_price)
    FROM production.products p2
    WHERE p2.category_id = p.category_id
);

--20

select s.store_name,s.city,count(o.order_id) as order_count
from sales.stores s
left join sales.orders  o on o.store_id=s.store_id
GROUP BY s.store_name, s.city




