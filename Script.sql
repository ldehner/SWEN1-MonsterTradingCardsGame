drop table if exists data;
CREATE TABLE DATA(
	username varchar(20) not null primary key,
	password varchar(200) not null,
	stack jsonb not null,
	deck jsonb not null,
	stats jsonb not null,
	elo int not null,
	userdata jsonb not null,
	coins int not null,
	battles jsonb
);

drop table if exists trades;
create table TRADES(
	id varchar(200) not null primary key,
	username varchar(20) not null,
	card jsonb not null,
	minDmg float not null,
	cardType int not null
);

drop table if exists packages;
create table packages(
	id varchar(200) not null primary key,
	package jsonb not null,
	counter SERIAL
);

select * from data;
select * from packages;
select * from trades;

