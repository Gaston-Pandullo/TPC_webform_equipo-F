use master
go
create database TPC_RESTAURANTE_DB
go
use TPC_RESTAURANTE_DB

go

CREATE TABLE USERS(
	id int primary key identity (1,1) not null,
	username varchar(25) not null,
	password varchar(25) not null, 
	name varchar(15) null,
	lastname varchar(15) null,
	admin bit default(0) not null,
	created_at DATETIME DEFAULT GETDATE() NOT NULL,
    updated_at DATETIME DEFAULT GETDATE() NOT NULL
)

go

CREATE TABLE PLATOS
(
	id_Plato int primary key identity (1,1) not null,
	nombre varchar(100) not null,
	descripcion varchar(500) null,
	precio money default(0) not null,
	preparable bit default(1) not null,
	estado bit default(1) not null
)

go

CREATE TABLE BEBIDAS
(
	id_Bebida int primary key identity (1,1) not null,
	nombre varchar(100) not null,
	precio money default(0) not null,
	alcoholica bit default(0) not null,
	stock int default(0) not null,
	estado bit default(1) not null
)

go 

CREATE TABLE INGREDIENTES
(
	id_Ingrediente int primary key identity (1,1) not null,
	nombre varchar(100) not null,
	precio money default(0) not null,
	stock int default(0) not null,
	estado bit default(1) not null
)

GO

CREATE TABLE INGREDIENTES_X_PLATOS
(
	id_Ingrediente int not null foreign key references INGREDIENTES(id_Ingrediente),
	id_Plato int not null foreign key references PLATOS(id_Plato),
	primary key(id_Ingrediente,id_Plato)
)

GO

create table IMAGENES
(
	id int primary key not null,
	idPlato int foreign key references platos(id_Plato) not null,
	imagen_url varchar(1000) not null
)

GO

-- Insertar datos en la tabla PLATOS
INSERT INTO PLATOS (nombre, descripcion, precio, preparable, estado) VALUES
('Spaghetti Carbonara', 'Pasta italiana con salsa de crema y tocino', 12.50, 1, 1),
('Ensalada César', 'Ensalada con lechuga, croutons y aderezo César', 8.00, 1, 1),
('Hamburguesa Clásica', 'Hamburguesa de carne con queso, lechuga y tomate', 10.00, 1, 1);
GO

-- Insertar datos en la tabla BEBIDAS
INSERT INTO BEBIDAS (nombre, precio, alcoholica, stock, estado) VALUES
('Coca Cola', 2.50, 0, 100, 1),
('Cerveza Artesanal', 5.00, 1, 50, 1),
('Agua Mineral', 1.50, 0, 200, 1);
GO

-- Insertar datos en la tabla INGREDIENTES
INSERT INTO INGREDIENTES (nombre, precio, stock, estado) VALUES
('Pasta', 1.00, 50, 1),
('Tocino', 2.00, 30, 1),
('Crema', 1.50, 20, 1),
('Lechuga', 0.50, 100, 1),
('Croutons', 0.75, 200, 1),
('Queso', 1.00, 150, 1),
('Pan de Hamburguesa', 0.50, 80, 1),
('Tomate', 0.50, 60, 1),
('Carne Picada', 1.00, 10, 1);
GO

-- Insertar datos en la tabla INGREDIENTES_X_PLATOS
INSERT INTO INGREDIENTES_X_PLATOS (id_Ingrediente, id_Plato) VALUES
-- Ingredientes para Spaghetti Carbonara (id_Plato = 1)
(1, 1), -- Pasta
(2, 1), -- Tocino
(3, 1), -- Crema
-- Ingredientes para Ensalada César (id_Plato = 2)
(4, 2), -- Lechuga
(5, 2), -- Croutons
(6, 2), -- Queso
-- Ingredientes para Hamburguesa Clásica (id_Plato = 3)
(7, 3), -- Pan de Hamburguesa
(6, 3), -- Queso
(8, 3), -- Tomate
(9,3);  -- Carne picada

INSERT INTO USERS (username, password, name, lastname, admin) VALUES ('pepito', 'asdasd', 'Pepito', 'Gonzalez', 1);


SELECT * FROM USERS;