﻿use master
go
create database TPC_RESTAURANTE_DB
drop database TPC_RESTAURANTE_DB
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
	stock int default(50) not null,
	estado bit default(1) not null,
	categoria char not null
)

go

CREATE TABLE BEBIDAS
(
    id_Bebida int primary key identity (1,1) not null,
    nombre varchar(100) not null,
    descripcion varchar(500) null,
    precio money default(0) not null,
    alcoholica bit default(0) not null,
    stock int default(50) not null,
    estado bit default(1) not null,
	categoria char not null
);

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


CREATE TABLE MESERO (
   IDMESERO INT IDENTITY PRIMARY KEY,
   IDUSUARIO INT NOT NULL FOREIGN KEY REFERENCES USERS(id)
);

GO

CREATE TABLE MESA (
IDMESA INT IDENTITY PRIMARY KEY,
MESERO INT FOREIGN KEY REFERENCES MESERO(IDMESERO),
OCUPADA BIT DEFAULT 0
)


-- Alteramos la tabla MESA para que acepte nulls, asi podemos desasignar y asignar meseros
ALTER TABLE MESA
ALTER COLUMN MESERO INT NULL;

GO

-- Insertar datos en la tabla PLATOS
INSERT INTO PLATOS (nombre, descripcion, precio, preparable, estado, categoria) VALUES
('Spaghetti Carbonara', 'Pasta italiana con salsa de crema y tocino', 12.50, 1, 1, 'C'),
('Ensalada C�sar', 'Ensalada con lechuga, croutons y aderezo C�sar', 8.00, 1, 1, 'C'),
('Hamburguesa Cl�sica', 'Hamburguesa de carne con queso, lechuga y tomate', 10.00, 1, 1, 'C');
GO

-- Insertar datos en la tabla BEBIDAS
INSERT INTO BEBIDAS (nombre, descripcion, precio, alcoholica, stock, estado, categoria) VALUES
('Coca Cola', 'Refresco de tipo cola', 2.50, 0, 100, 1, 'B'),
('Cerveza Artesanal', 'Bebida alcoholica alta birrita', 5.00, 1, 50, 1, 'B'),
('Agua Mineral', 'Aguita bien saludable pa''', 1.50, 0, 200, 1, 'B');
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
-- Ingredientes para Ensalada C�sar (id_Plato = 2)
(4, 2), -- Lechuga
(5, 2), -- Croutons
(6, 2), -- Queso
-- Ingredientes para Hamburguesa Cl�sica (id_Plato = 3)
(7, 3), -- Pan de Hamburguesa
(6, 3), -- Queso
(8, 3), -- Tomate
(9,3);  -- Carne picada

INSERT INTO USERS (username, password, name, lastname, admin) VALUES 
('pepito', 'asdasd', 'Pepito', 'Gonzalez', 1),
('juanito', 'qwerty', 'Juanito', 'Perez', 0),
('maria', '123456', 'Maria', 'Lopez', 0),
('carlos', 'password', 'Carlos', 'Martinez', 0),
('ana', 'abc123', 'Ana', 'Sanchez', 0),
('luis', 'pass123', 'Luis', 'Rodriguez', 0);


INSERT INTO MESERO (IDUSUARIO) VALUES
(2),
(3), 
(4), 
(5),
(6);
-- Modificar la tabla PLATOS
ALTER TABLE PLATOS
ADD CONSTRAINT CK_PLATOS_Categoria CHECK (categoria IN ('C', 'B', 'P'));

ALTER TABLE PLATOS
ADD CONSTRAINT DF_PLATOS_Categoria DEFAULT 'C' FOR categoria;

-- Modificar la tabla BEBIDAS
ALTER TABLE BEBIDAS
ADD CONSTRAINT CK_BEBIDAS_Categoria CHECK (categoria IN ('C', 'B', 'P'));

ALTER TABLE BEBIDAS
ADD CONSTRAINT DF_BEBIDAS_Categoria DEFAULT 'B' FOR categoria;

INSERT INTO MESA (MESERO, OCUPADA) VALUES
(NULL, 0),
(NULL, 0),
(NULL, 0),
(NULL, 0),
(NULL, 0);

--SELECT id_Bebida AS Id, nombre AS Nombre, descripcion AS Descripcion, precio AS Precio, stock AS Stock, categoria FROM BEBIDAS UNION ALL SELECT id_Plato AS Id, nombre AS Nombre, descripcion AS Descripcion, precio AS Precio, stock AS Stock, categoria FROM PLATOS;
--UPDATE BEBIDAS SET stock = 101 WHERE id_Bebida = 2;

/*SELECT * FROM PLATOS;
SELECT * FROM BEBIDAS;
SELECT * FROM USERS;
SELECT * FROM MESA;
SELECT *  FROM MESERO M LEFT JOIN USERS U
ON U.id = M.IDUSUARIO*/


--SELECT * FROM USERS Where id = 1;

--insert into MESA(MESERO,OCUPADA) values(2,0)

--select * from MESERO m
--inner join USERS as u on u.id = m.IDUSUARIO

CREATE TABLE PEDIDOS(
IDPEDIDO INT IDENTITY PRIMARY KEY,
FECHAPEDIDO DATE NOT NULL DEFAULT GETDATE(),
TOTAL MONEY 
)

CREATE TABLE COMANDA(
IDCOMANDA INT IDENTITY PRIMARY KEY,
IDMESA INT FOREIGN KEY REFERENCES MESA(IDMESA),
IDPLATO INT FOREIGN KEY REFERENCES PLATOS(id_Plato)
)

ALTER TABLE PEDIDOS
ADD IDCOMANDA INT FOREIGN KEY REFERENCES COMANDA(IDCOMANDA);

ALTER TABLE PEDIDOS
ADD IDMESA INT FOREIGN KEY REFERENCES MESA(IDMESA);

ALTER TABLE COMANDA
ADD IDPEDIDO int foreign key references Pedidos(IDPEDIDO)

SELECT P.nombre, P.precio, COUNT(*) as cantidad
FROM COMANDA C
INNER JOIN PLATOS P ON C.idPlato = P.id_Plato
WHERE C.idPedido = @idPedido
GROUP BY P.nombre, P.precio;

SELECT * FROM MESA;

INSERT INTO MESA(MESERO, OCUPADA) VALUES (null, 0)


--DROP DATABASE TPC_RESTAURANTE_DB