---DDL---
CREATE DATABASE Filmes_Rebeca
USE Filmes_Rebeca

CREATE TABLE Genero
(
	IdGenero INT PRIMARY KEY IDENTITY,
	Nome VARCHAR(50)
)

CREATE TABLE Filme
(
	IdFilme INT PRIMARY KEY IDENTITY,
	IdGenero INT FOREIGN KEY REFERENCES Genero(IdGenero),
	Titulo VARCHAR(50)
)

CREATE TABLE Usuario
(
	IdUsuario INT PRIMARY KEY IDENTITY,
	Email VARCHAR(50) UNIQUE NOT NULL,
	Senha VARCHAR(50) NOT NULL,
	Nome VARCHAR(50) NOT NULL,
	Permissao BIT NOT NULL
);

---DML--
INSERT INTO Genero (Nome) VALUES ('Romance'), ('Drama');
INSERT INTO Filme (Titulo, IdGenero) VALUES ('Exterminador do Futuro', 1);
INSERT INTO Usuario(Email, Senha, Nome, Permissao)
VALUES ('rebeca@rebeca.com','2346','Rebeca',1),('carolina@carolina.com','8957','Carolina',0)

---DQL---
SELECT * FROM Genero
SELECT * FROM Filme
SELECT * FROM Usuario
