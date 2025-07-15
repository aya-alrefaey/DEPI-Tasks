------------------Assignment 5------------

use  StoreDB;

 --1

 select *,
 case 
 when list_price <300 then 'Economy'
 when list_price between 300 and 999 then 'Standard'
 when list_price between 1000 and 2499 then 'Premium'
 when list_price >= 2500 then 'Luxury'
 end as 'price category'
 from production.products;

 --2

 select *,
 case 
 when order_status=1 then 'Order Received'
 when order_status=2 then 'In Preparation'
 when order_status=3 then 'Order Cancelled'
 when order_status=4 then 'Order Delivered'
 end as 'order processing information',

 case
 when order_status=1 and datediff(day,order_date,GETDATE())>5 then 'URGENT'
 when order_status=2 and datediff(day,order_date,GETDATE())>3 then 'HIGH'
 else 'NORMAL'
 end as 'priority level'
 from sales.orders;

 --3

 select s.staff_id,s.first_name+' '+s.last_name as full_name,count(o.order_id) as 'no. of orders',
 case 
 WHEN COUNT(o.order_id) = 0 THEN 'New Staff'
 WHEN COUNT(o.order_id) BETWEEN 1 AND 10 THEN 'Junior Staff'
 WHEN COUNT(o.order_id) BETWEEN 11 AND 25 THEN 'Senior Staff'
 WHEN COUNT(o.order_id) >= 26 THEN 'Expert Staff'
 end as 'no. of orders'
 from sales.staffs s
 left join sales.orders o on s.staff_id=o.staff_id
 group by s.first_name,s.staff_id,s.last_name;

 --4

 select customer_id,first_name+' '+last_name as full_name ,
 isnull(phone,'phone not available') as 'phone number',email,
 coalesce (phone,email,'No Contact Method') as preferred_contact
 from sales.customers;

 --5

 select p.product_id ,p.product_name,s.quantity,p.list_price,isnull(p.list_price/nullif(quantity,0),0)as 'unit_price',
 case
 when s.quantity =0 then 'not exist'
 end as 'stock status'
 from production.products p
 left join production.stocks s on s.product_id=p.product_id and s.store_id=1;

 --6

 select customer_id,first_name+' '+last_name as full_name,phone ,email,
 coalesce(street,'no street')as street,
 coalesce(city,'no city')as city,
 coalesce(state,'no state')as state,
 coalesce(zip_code,'No Zip_Code')as Zip_code,
 coalesce(street,'')+' '+
 coalesce(city,'')+' '+
 coalesce(state,'')+' '+
 coalesce(zip_code,'')as 'formatted address'
 from sales.customers;

 --7


 with total_spending as(
 select  c.customer_id , sum( oi.list_price) as 'total spend'
 from sales.customers c
 join sales.orders o on c.customer_id = o.customer_id
 join sales.order_items oi on oi.order_id=o.order_id
 group by c.customer_id
 )
 select c.customer_id,c.first_name+' '+c.last_name as full_name,c.phone ,c.email,ts.[total spend]
 from total_spending ts
 join sales.customers c on c.customer_id=ts.customer_id
 where ts.[total spend]>1500
 order by [total spend] desc;

 --8

 with total_revenue as(
 select c.category_id,c.category_name,sum(oi.list_price) as 'total revenue'
 from production.categories c
 left join production.products p on p.category_id=c.category_id
 left join sales.order_items oi on oi.product_id=p.product_id
 group by c.category_id,c.category_name
 
 ),
 average_order_value as(
 select  c.category_id,count(o.order_id) as 'total no. of ordrs'
 from production.categories c
 left join production.products p on p.category_id=c.category_id
 left join sales.order_items oi on oi.product_id=p.product_id
 left join sales.orders o on o.order_id=oi.order_id
  group by c.category_id
)
select c.category_id,c.category_name,aov.[total no. of ordrs],isnull(tr.[total revenue],0)as 'total revenue' ,
ISNULL(tr.[total revenue] * 1.0 / NULLIF(aov.[total no. of ordrs], 0), 0) as 'average order value',
case
when tr.[total revenue]>50000 then 'Excellent'
when tr.[total revenue]>20000 then 'Good'
else 'Needs Improvement'
end as 'rate performance'
from production.categories c 
left join average_order_value aov on aov.category_id=c.category_id
left join total_revenue tr on tr.category_id=c.category_id;

--9

with monthly_sales as(
select month(order_date)as 'sales_month',year(order_date) as 'sales_year',
FORMAT(o.order_date, 'yyyy-MM') AS year_month,datename(month,order_date) as 'Month_name'
 ,count(o.order_id) as'no. of orders',SUM(oi.list_price) AS total_sales
from sales.orders o
JOIN sales.order_items oi ON o.order_id = oi.order_id
group by month(order_date),datename(month,order_date),year(order_date),FORMAT(o.order_date, 'yyyy-MM')
),
previous_month as(
select sales_month,sales_year,Month_name,year_month,[no. of orders],total_sales, 
LAG(total_sales) OVER (ORDER BY sales_year, sales_month) as 'prev month sales'
from monthly_sales 
)
select sales_month,sales_year,year_month,Month_name,[no. of orders],total_sales,[prev month sales],
case
when [prev month sales] is null then 0
else round( ((total_sales-[prev month sales])*100.0/[prev month sales]),2)
end as growth_percentage
from previous_month

--10

with rownum as(
select c.category_id,c.category_name,p.product_name,p.list_price,
row_number()over(partition by c.category_id order by p.list_price desc ) as'row number'
from production.categories c
left join production.products p on p.category_id=c.category_id
)
select *
from rownum
where [row number]<=3;

with rannk as(
select c.category_id,c.category_name,p.product_name,p.list_price,
rank()over( partition by c.category_id order by p.list_price desc  ) as 'rank'
from production.categories c
left join production.products p on p.category_id=c.category_id
)
select *
from rannk
where [rank]<=3;

with dense as(
select c.category_id,c.category_name,p.product_name,p.list_price,
dense_rank()over( partition by c.category_id order by p.list_price desc  ) as 'dense-rank'
from production.categories c
left join production.products p on p.category_id=c.category_id)
select *
from dense
where [dense-rank]<=3;

--11

with customer as(
select c.customer_id,c.first_name+' '+c.last_name as full_name,sum(oi.list_price) as 'total spending',
rank()over(order by sum(oi.list_price) desc) as 'customer rank',
ntile(5)over(order by sum(oi.list_price) desc) as 'group no.'
from sales.customers c
left join sales.orders o on o.customer_id=c.customer_id
left join sales.order_items oi on oi.order_id=o.order_id
group by c.customer_id,c.first_name,c.last_name
)
select *,
case 
when [group no.]=1 then 'VIP'
when [group no.]=2 then 'Gold'
when [group no.]=3 then 'Silver'
when [group no.]=4 then 'Bronze'
when [group no.]=5 then 'Standard'
else 'no group'
end as 'group'
from customer;

--12

with total_store_rev as(
select s.store_id,sum(p.list_price)as 'total revenue',
rank()over(order by sum(p.list_price) desc ) as 'rank by revenue'
from sales.stores s
left join production.stocks st on s.store_id=st.store_id
left join production.products p on p.product_id=st.product_id
group by s.store_id
),
 total_no_of_orders as(
select s.store_id,count(o.order_id) as 'no. of orders',
 rank()over(order by count(o.order_id) desc ) as 'rank by no of orders'
from sales.stores s
left join sales.orders o on o.store_id=s.store_id
group by s.store_id
)
select r.store_id,o.[no. of orders],r.[total revenue],r.[rank by revenue],o.[rank by no of orders],
PERCENT_RANK()over(order by r.[total revenue] desc) as'Revenue percentile performance',
PERCENT_RANK()over(order by o.[no. of orders] desc) as'Orders percentile performance'
from total_store_rev r
left join total_no_of_orders o on o.store_id=r.store_id

--13

select category_name ,[Electra],[Haro], [Trek], [Surly]
from(select category_name ,brand_name,product_id from production.categories c
left join production.products p on p.category_id=c.category_id
left join production.brands b on b.brand_id=p.brand_id) as src
pivot( count(product_id)for brand_name in ([Electra],[Haro], [Trek], [Surly]) ) as piv

--14

SELECT 
    store_name,[Jan],[Feb],[Mar],[Apr],[May],[Jun],[Jul],[Aug],[Sep],[Oct],[Nov],[Dec]
	from (
	    select s.store_name,
        DATENAME(MONTH, o.order_date) AS month_name,
        oi.list_price
    FROM sales.orders o
    JOIN sales.order_items oi ON o.order_id = oi.order_id
    JOIN sales.stores s ON s.store_id = o.store_id
	) as src
	pivot( sum(list_price) FOR month_name IN (
        [Jan], [Feb], [Mar], [Apr], [May], [Jun], 
        [Jul], [Aug], [Sep], [Oct], [Nov], [Dec]))as piv


--15

select store_name,[Order Received],[In Preparation], [Order Cancelled], [Order Delivered]
from (select s.store_name,order_id,
case 
 when order_status=1 then 'Order Received'
 when order_status=2 then 'In Preparation'
 when order_status=3 then 'Order Cancelled'
 when order_status=4 then 'Order Delivered'
 end as 'order processing information'
from sales.stores s
 join sales.orders o on o.store_id=s.store_id) as src
pivot(count(order_id) for [order processing information] 
in([Order Received],[In Preparation], [Order Cancelled], [Order Delivered]))as piv

--16

SELECT brand_name, [2016], [2017], [2018],
       ROUND(([2017] - [2016]) * 100.0 / NULLIF([2016], 0), 2) AS growth_16_to_17,
       ROUND(([2018] - [2017]) * 100.0 / NULLIF([2017], 0), 2) AS growth_17_to_18
FROM (
    SELECT b.brand_name,
           YEAR(o.order_date) AS sales_year,
           oi.list_price
    FROM sales.order_items oi
    JOIN sales.orders o ON oi.order_id = o.order_id
    JOIN production.products p ON oi.product_id = p.product_id
    JOIN production.brands b ON p.brand_id = b.brand_id
) AS src
PIVOT (
    SUM(list_price)
    FOR sales_year IN ([2016], [2017], [2018])
) AS piv;


--17

select product_name ,'In Stock' AS availability_status
from production.stocks s
join production.products p on s.product_id=p.product_id
where quantity>0
union
select product_name ,'Out Of Stock' AS availability_status
from production.stocks s
join production.products p on s.product_id=p.product_id
where quantity=0 or quantity is null
union
SELECT p.product_name, 'Discontinued' AS availability_status
FROM production.products p
LEFT JOIN production.stocks s ON p.product_id = s.product_id
WHERE s.product_id IS NULL;

--18

with customer as(
select distinct customer_id
from sales.orders 
where year(order_date)=2017
intersect
select distinct customer_id
from sales.orders 
where year(order_date)=2018)

SELECT c.customer_id, o.order_id, o.order_date, oi.product_id, oi.list_price
FROM customer c
JOIN sales.orders o ON o.customer_id = c.customer_id
JOIN sales.order_items oi ON oi.order_id = o.order_id
ORDER BY c.customer_id, o.order_date;

--19

SELECT product_id, 'Available in All Stores' AS status
FROM production.stocks
WHERE store_id = 1

INTERSECT

SELECT product_id, 'Available in All Stores'
FROM production.stocks
WHERE store_id = 2

INTERSECT

SELECT product_id, 'Available in All Stores'
FROM production.stocks
WHERE store_id = 3
union
SELECT product_id, 'Only in Store 1 (Not in 2)'
FROM production.stocks
WHERE store_id = 1

EXCEPT

SELECT product_id, 'Only in Store 1 (Not in 2)'
FROM production.stocks
WHERE store_id = 2;


--20

select distinct customer_id , 'Lost' AS status
from sales.orders 
where year(order_date)=2016
except

select distinct customer_id,'Lost' AS status
from sales.orders 
where year(order_date)=2017

union all

select distinct customer_id ,'New' AS status
from sales.orders 
where year(order_date)=2017
except

select distinct customer_id ,'New' AS status
from sales.orders 
where year(order_date)=2016

union all
select distinct customer_id,'Retained' AS status
from sales.orders 
where year(order_date)=2017
intersect

select distinct customer_id,'Retained' AS status
from sales.orders 
where year(order_date)=2016





  








