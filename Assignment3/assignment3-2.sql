select*
from production.products
where list_price>1000

select *
from sales.customers
where (state='CA'or state='NA')

select *
from sales.orders
where year(order_date)=2023

select *
from sales.customers
where  email like '%@gmail.com';

select *
from sales.staffs
where active=0;

select top(5)*
from production.products
order by list_price desc ;

select top(10)*
from sales.orders
order by order_date desc ;

select top(3)*
from sales.customers
order by last_name ;

select *
from sales.customers
where phone is null;

select *
from sales.staffs
where manager_id is not null ;

select category_id, count(*) as product_count
from production.Products
group by category_id;

select state, count(*) AS customer_count 
from sales.customers 
group by state;

select brand_id, avg(list_price) as avg_price 
from production.products 
group by brand_id;

select staff_id, count(*) as order_count 
from sales.orders 
group by staff_id;


select customer_id, count(*) as order_count 
from sales.orders 
group by customer_id 
having count(*) > 2;

select * 
from production.products 
where list_price between 500 and 1500;

select *
from sales.customers
where city like 'S%';

select *
from sales.orders
where order_status in (2, 4);

select * 
from production.products 
where category_id IN (1, 2, 3);

select * 
from sales.staffs 
where store_id = 1 or phone is null;













