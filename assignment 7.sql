-- Assignment 7


-- 1

create nonclustered index emailsearch
on sales.customers(email)
-- 2
create nonclustered index categorybrand
on production.products(category_id,brand_id)
include (product_name)
-- 3
create nonclustered index orders
on sales.orders(order_date)
include (customer_id,store_id,order_status)
-- 4

create trigger sales.welcomecus
on sales.customers
after insert
as
begin
insert into sales.customer_log(customer_id,action,log_date)
select customer_id,'ins',getdate()
from inserted
end

-- 5

create trigger price_changes
on production.products
after update
as
begin
insert into production.price_history(product_id,old_price,new_price,change_date,changed_by)
select i.product_id,d.list_price,i.list_price,getdate(),@@SERVERNAME --suser_sname()
from inserted i 
join deleted d on d.product_id=i.product_id
where d.list_price<>i.list_price
end

--6 

create trigger category_delete 
on production.categories
instead of delete
as
begin
declare @count int =0;
set @count =(select count(*) from production.products p join deleted d on d.category_id=p.category_id);
if @count>0
begin
print 'there are many products in this category, cannot be deleted'
end
else
begin
delete from production.categories
where category_id in (select category_id from deleted)
end
end

-- 7

create trigger reduce_quantity
on sales.order_items
after insert
as
begin
update s
set s.quantity=s.quantity-i.quantity
from production.stocks s
join inserted i on i.product_id=s.product_id
end

-- 8
create trigger neworders
on sales.orders 
after insert
as
begin
insert into sales.order_audit(order_id,customer_id,store_id,staff_id,order_date,audit_timestamp)
select order_id,customer_id,store_id,staff_id,order_date,getdate()
from inserted
end