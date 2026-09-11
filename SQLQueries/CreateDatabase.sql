-- ============================================================
-- CREAR LA BASE DE DATOS
-- ============================================================

-- Crea una nueva base de datos llamada BibliotecaDB.
CREATE DATABASE BibliotecaDB;
GO

-- Selecciona BibliotecaDB para que las siguientes instrucciones
-- se ejecuten dentro de esta base de datos.
USE BibliotecaDB;
GO


-- ============================================================
-- CREAR LA TABLA CATEGORIAS
-- ============================================================

-- Crea una tabla llamada Categorias.
CREATE TABLE Categorias
(
    -- ID:
    -- Identificador �nico de cada categor�a.
    -- INT: almacena n�meros enteros.
    -- IDENTITY(1,1): comienza en 1 y aumenta autom�ticamente
    -- de uno en uno cada vez que se inserta un registro.
    -- PRIMARY KEY: establece este campo como clave primaria.
    ID INT IDENTITY(1,1) PRIMARY KEY,

    -- Nombre:
    -- Almacena el nombre de la categor�a.
    -- NVARCHAR(100): permite almacenar hasta 100 caracteres.
    -- NOT NULL: significa que este campo es obligatorio.
    Nombre NVARCHAR(100) NOT NULL,

    -- Descripcion:
    -- Almacena una descripci�n de la categor�a.
    -- NVARCHAR(250): permite almacenar hasta 250 caracteres.
    -- NULL: significa que este campo puede quedar vac�o.
    Descripcion NVARCHAR(250) NULL
);
GO


-- ============================================================
-- INSERTAR DATOS DE PRUEBA
-- ============================================================

-- Insertamos registros iniciales en la tabla Categorias.
-- No necesitamos indicar el ID porque SQL Server lo genera
-- autom�ticamente gracias a IDENTITY(1,1).

INSERT INTO Categorias (Nombre, Descripcion)
VALUES
    -- Primera categor�a
    ('Novela', 'Obras narrativas de ficci�n'),

    -- Segunda categor�a
    ('Ciencia Ficci�n', 'Obras relacionadas con ciencia y tecnolog�a'),

    -- Tercera categor�a
    ('Historia', 'Obras relacionadas con acontecimientos hist�ricos');
GO