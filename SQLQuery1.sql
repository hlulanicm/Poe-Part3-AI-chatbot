--creating the database called tasks_DB

--created now --
create database tasks_DB;

--use Tasks_DB


use [tasks_DB];

---POE Said add task name etc--

--collect user data--
--create a entity tabe
--coloumns are task_id task_date, description, task_status


--for (int i=0; i tasks.Count; i--


--creating table
create table all_tasks(
task_id int primary key identity(1,1),
task_name varchar(50), --task name must not exceed 50 chars--
description_task varchar(100),
task_date varchar(20),
task_status varchar(20),


);


--select all from the table--


select * from all_tasks;






