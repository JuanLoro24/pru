CREATE DATABASE LibreriaData

USE LibreriaData

-- Lineas de codigo para la creacion de la tabla autores con su llave primaria 
CREATE TABLE Autores (
    AutorID INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(255) NOT NULL
);

-- Lineas de codigo para la creacion de la tabla Libros con su llave primaria y foranea
CREATE TABLE Libros (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Titulo VARCHAR(255) NOT NULL,
    AutorID INT,
    FOREIGN KEY (AutorID) REFERENCES Autores(AutorID) ON DELETE CASCADE
);

SELECT * FROM Autores
SELECT * FROM Libros


--Creacion de los procedimientos almacenados
DROP PROCEDURE IF EXISTS sp_EditarLibro;

CREATE PROCEDURE sp_EditarLibro
    @ID INT,
    @Titulo VARCHAR(255),
    @AutorID INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Libros
    SET Titulo = @Titulo,
        AutorID = @AutorID
    WHERE ID = @ID;
END;

DROP PROCEDURE IF EXISTS sp_EliminarLibro;

CREATE OR ALTER PROCEDURE sp_EliminarLibro
    @Id INT
AS
BEGIN
    DELETE FROM Libros
    WHERE ID = @Id;
END;
