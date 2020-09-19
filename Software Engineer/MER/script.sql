drop database dbtestdrive;

create database dbtestdrive;
use dbtestdrive;

create table tb_carro (
	id_carro int primary key auto_increment,
    nm_carro varchar(100),
    nm_marca varchar(100),
    ds_modelo varchar(100),
    nr_ano int,
    ds_placa varchar(100)
);

create table tb_login (
	id_login int primary key auto_increment,
    ds_email varchar(100),
    ds_senha varchar(100),
    ds_permissao varchar(100)
);

create table tb_cliente (
	id_cliente int primary key auto_increment,
    nm_cliente varchar(100),
    ds_cnh varchar(100),
    ds_cpf varchar(100),
    id_login int,
    foreign key (id_login) references tb_login (id_login) on delete cascade
);

create table tb_funcionario (
	id_funcionario int primary key auto_increment,
    nm_funcionario varchar(100),
    id_login int,
    foreign key (id_login) references tb_login (id_login) on delete cascade
);

create table tb_agendamento (
	id_agendamento int primary key auto_increment,
    id_cliente int,
    foreign key (id_cliente) references tb_cliente (id_cliente) on delete cascade,
    id_funcionario int,
    foreign key (id_funcionario) references tb_funcionario (id_funcionario) on delete cascade,
	id_carro int,
    foreign key (id_carro) references tb_carro (id_carro) on delete cascade,
    dt_test_drive datetime,
    ds_status varchar(100),
    nr_feedback int,
    ds_feedback varchar(100)
);