use master
go
CREATE DATABASE TPC_RESTAURANTE_DB2;
DROP DATABASE TPC_RESTAURANTE_DB2;
GO
USE TPC_RESTAURANTE_DB2;
GO

CREATE TABLE USERS (
    id INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
    username VARCHAR(25) NOT NULL,
    password VARCHAR(25) NOT NULL,
    name VARCHAR(15) NULL,
    lastname VARCHAR(15) NULL,
    admin BIT DEFAULT 0 NOT NULL,
    created_at DATETIME DEFAULT GETDATE() NOT NULL,
    updated_at DATETIME DEFAULT GETDATE() NOT NULL
);
GO

CREATE TABLE ITEM_MENU (
    id INT PRIMARY KEY IDENTITY,
    nombre VARCHAR(100) NOT NULL,
    descripcion VARCHAR(500) NULL,
    precio MONEY DEFAULT 0 NOT NULL,
    stock INT DEFAULT 50 NOT NULL,
    cantidad INT DEFAULT 0 NOT NULL,
    estado BIT DEFAULT 1 NOT NULL,
    categoria CHAR(1) NOT NULL
);
GO

CREATE TABLE IMAGENES (
    id INT PRIMARY KEY NOT NULL,
    idItemMenu INT NOT NULL FOREIGN KEY REFERENCES ITEM_MENU(id),
    imagen_url VARCHAR(1000) NOT NULL
);
GO

CREATE TABLE MESERO (
    idMesero INT PRIMARY KEY IDENTITY,
    idUsuario INT NOT NULL FOREIGN KEY REFERENCES USERS(id)
);
GO

CREATE TABLE MESA (
    idMesa INT PRIMARY KEY IDENTITY,
    mesero INT NULL FOREIGN KEY REFERENCES MESERO(idMesero),
    ocupada BIT DEFAULT 0
);
GO

CREATE TABLE PEDIDOS (
    idPedido INT PRIMARY KEY IDENTITY,
    fechaPedido DATETIME NOT NULL DEFAULT GETDATE(),
    total MONEY,
    idMesa INT NOT NULL FOREIGN KEY REFERENCES MESA(idMesa)
);
GO

CREATE TABLE COMANDA (
    idComanda INT PRIMARY KEY IDENTITY,
    idPedido INT NOT NULL FOREIGN KEY REFERENCES PEDIDOS(idPedido),
    idItem INT NOT NULL FOREIGN KEY REFERENCES ITEM_MENU(id),
    cantidad INT NOT NULL DEFAULT 1
);
GO

INSERT INTO ITEM_MENU (nombre, descripcion, precio, stock, cantidad, estado, categoria) VALUES
('Spaghetti Carbonara', 'Pasta italiana con salsa de crema y tocino', 12.50, 50, 0, 1,'C'),
('Ensalada Cesar', 'Ensalada con lechuga, croutons y aderezo Cesar', 8.00, 50, 0, 1,'C'),
('Hamburguesa Clasica', 'Hamburguesa de carne con queso, lechuga y tomate', 10.00, 50, 0, 1,'C'),
('Coca Cola', 'Refresco de tipo cola', 2.50, 50, 0, 1, 'B'),
('Cerveza Artesanal', 'Bebida alcoholica alta birrita', 5.00, 50, 0, 1, 'B'),
('Agua Mineral', 'Aguita bien saludable pa', 1.50, 50, 0, 1, 'B'),
('Flan', 'Flancito mixto o solo DDL', 6.00, 50, 0, 1, 'P'),
('Tiramisu', 'Top tier de los postres', 8.00, 50, 0, 1, 'P'),
('Helado', 'Unas bochinis de helado', 4.50, 50, 0, 1, 'P');
GO

INSERT INTO USERS (username, password, name, lastname, admin) VALUES 
('pepito', 'asdasd', 'Pepito', 'Gonzalez', 1),
('juanito', 'qwerty', 'Juanito', 'Perez', 0),
('maria', '123456', 'Maria', 'Lopez', 0),
('carlos', 'password', 'Carlos', 'Martinez', 0),
('ana', 'abc123', 'Ana', 'Sanchez', 0),
('luis', 'pass123', 'Luis', 'Rodriguez', 0);
GO

INSERT INTO MESERO (idUsuario) VALUES
(2),
(3), 
(4), 
(5),
(6);
GO

INSERT INTO MESA (mesero, ocupada) VALUES
(NULL, 0),
(NULL, 0),
(NULL, 0),
(NULL, 0),
(NULL, 0);

GO

INSERT INTO MESA (mesero, ocupada) VALUES (NULL, 0);

--DROP DATABASE TPC_RESTAURANTE_DB


-- SELECT P.nombre, P.precio, COUNT(*) as cantidad
-- FROM COMANDA C
-- INNER JOIN PLATOS P ON C.idPlato = P.id_Plato
-- WHERE C.idPedido = @idPedido
-- GROUP BY P.nombre, P.precio;

--SELECT id_Bebida AS Id, nombre AS Nombre, descripcion AS Descripcion, precio AS Precio, stock AS Stock, categoria FROM BEBIDAS UNION ALL SELECT id_Plato AS Id, nombre AS Nombre, descripcion AS Descripcion, precio AS Precio, stock AS Stock, categoria FROM PLATOS;
--UPDATE BEBIDAS SET stock = 101 WHERE id_Bebida = 2;

SELECT * FROM USERS;
SELECT * FROM MESA;
SELECT *  FROM MESERO M LEFT JOIN USERS U
ON U.id = M.IDUSUARIO
SELECT * FROM PEDIDOS
SELECT * FROM COMANDA


--SELECT * FROM USERS Where id = 1;

--insert into MESA(MESERO,OCUPADA) values(2,0)

--select * from MESERO m
--inner join USERS as u on u.id = m.IDUSUARIO

SELECT p.idPedido, p.fechaPedido, p.total, p.idMesa FROM PEDIDOS p WHERE p.idPedido = 5;
SELECT c.idPedido, im.id AS idItemMenu, im.nombre,im.descripcion,
    im.precio,
    im.stock,
    SUM(c.cantidad) AS cantidadTotal,
    im.estado,
    im.categoria
FROM 
    COMANDA c
INNER JOIN 
    ITEM_MENU im ON c.idItem = im.id
WHERE 
    c.idPedido = 4
GROUP BY 
    c.idPedido,
    im.id,
    im.nombre,
    im.descripcion,
    im.precio,
    im.stock,
    im.estado,
    im.categoria;

SELECT COUNT(*) as cantidad FROM MESA WHERE MESA.ocupada = 1;

SELECT * FROM MESA;
DELETE FROM MESA WHERE idMesa = (SELECT MAX(idMesa) FROM MESA);