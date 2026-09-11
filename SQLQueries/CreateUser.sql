-- ============================================================
-- CREAR USUARIO PARA SQL SERVER
-- ============================================================

-- Utilizamos la base de datos del sistema 'master'
-- porque el LOGIN pertenece al servidor de SQL Server.
USE master;
GO

-- Crea un LOGIN llamado 'biblioteca_user'.
-- Este usuario podr� autenticarse en SQL Server
-- utilizando usuario y contrase�a.
--
-- PASSWORD: establece la contrase�a del LOGIN.
CREATE LOGIN biblioteca_user
WITH PASSWORD = 'Biblioteca123!';
GO


-- ============================================================
-- CREAR EL USUARIO DENTRO DE LA BASE DE DATOS
-- ============================================================

-- Seleccionamos nuestra base de datos.
USE BibliotecaDB;
GO

-- Creamos un usuario dentro de BibliotecaDB
-- asociado al LOGIN que acabamos de crear.
--
-- LOGIN  -> permite iniciar sesi�n en SQL Server.
-- USER   -> permite trabajar dentro de una base de datos.
CREATE USER biblioteca_user
FOR LOGIN biblioteca_user;
GO


-- ============================================================
-- ASIGNAR PERMISOS
-- ============================================================

-- Permite al usuario consultar informaci�n de las tablas.
-- Por ejemplo: SELECT * FROM Categorias;
ALTER ROLE db_datareader
ADD MEMBER biblioteca_user;


-- Permite al usuario modificar informaci�n de las tablas.
-- Incluye operaciones como INSERT, UPDATE y DELETE.
ALTER ROLE db_datawriter
ADD MEMBER biblioteca_user;
GO