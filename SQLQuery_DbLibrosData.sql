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