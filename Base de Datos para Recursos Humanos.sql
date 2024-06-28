
--Creación de la Base de Datos para Recursos Humanos.

--Creación de la BD.
CREATE DATABASE BD_Recursos_Humanos;

--Usar la Base de Datos.
USE BD_Recursos_Humanos;

--Tabla Solicitud_nombramiento.

CREATE TABLE Solicitud_nombramiento (
    Id_solicitud INT IDENTITY (1, 1),
    Fecha DATE,
    Nombre VARCHAR (100) NOT NULL,
    Apellido VARCHAR (100) NOT NULL,
    Cedula VARCHAR (13) NOT NULL UNIQUE,	
    Sexo VARCHAR (10) NOT NULL,
    Id_Departamento INT,
    Id_Cargo INT,
    Grupo_ocupacional VARCHAR (10),
    Sustitucion VARCHAR (50),
    Salario MONEY NOT NULL,
    Direccion_de_residencia VARCHAR (100) NOT NULL,
    Autorizado VARCHAR (25) NOT NULL,
    Observaciones VARCHAR(30),
    Enviado_en_oficialNo VARCHAR(30),
    Pregunta VARCHAR(20),
    NoOficioyFecha VARCHAR(30),
    Archivo_Cedula_identidad VARBINARY(MAX),
    Extension_Cedula_identidad VARCHAR (50),
    Archivo_HojaVida_identidad VARBINARY(MAX),
    Extension_HojaVida_identidad VARCHAR (50),
    CONSTRAINT PK_Solicitud_nombramiento PRIMARY KEY (Id_solicitud),
    CONSTRAINT FK_Solicitud_Cargo FOREIGN KEY (Id_Cargo) REFERENCES Cargo(Id_Cargo),
    CONSTRAINT FK_Solicitud_Departamento FOREIGN KEY (Id_Departamento) REFERENCES Departamento(Id_Departamento)
);


drop table Solicitud_nombramiento

-- Tabla Cambio_designacion.
CREATE TABLE Cambio_designacion(
    Id_designacion INT IDENTITY (1, 1),
    Nombre_Completo VARCHAR (75) NOT NULL,
    Fecha DATE,
    Cedula VARCHAR (13) NOT NULL,
    Id_Cargo_Actual INT, -- Relación con Cargo Actual
    Id_Cargo_Nuevo INT,  -- Relación con Nuevo Cargo
    Salario_Propuesto MONEY NOT NULL,
    Id_Departamento_Propuesto INT, -- Relación con Departamento Propuesto
    Referido_por VARCHAR (50) NOT NULL,
    Observaciones VARCHAR (100),
    Estatus VARCHAR(20),
    Archivo VARBINARY(MAX),
    Extension VARCHAR (50),
    CONSTRAINT PK_Solicitud_designacion PRIMARY KEY (Id_designacion),
    CONSTRAINT FK_Cambio_Cargo_Actual FOREIGN KEY (Id_Cargo_Actual) REFERENCES Cargo(Id_Cargo),
    CONSTRAINT FK_Cambio_Cargo_Nuevo FOREIGN KEY (Id_Cargo_Nuevo) REFERENCES Cargo(Id_Cargo),
    CONSTRAINT FK_Cambio_Departamento FOREIGN KEY (Id_Departamento_Propuesto) REFERENCES Departamento(Id_Departamento)
);

drop table Cambio_designacion

--Tabla Usuarios
CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY,
    NombreUsuario NVARCHAR(50) NOT NULL,
    Contraseña NVARCHAR(50) NOT NULL,
)

--Tabla Cargos
CREATE TABLE Cargo (
    Id_Cargo INT IDENTITY(1, 1),
    Nombre_Cargo VARCHAR(50) NOT NULL,
    CONSTRAINT PK_Cargo PRIMARY KEY (Id_Cargo)
);

--Tabla Departamento
CREATE TABLE Departamento (
    Id_Departamento INT IDENTITY(1, 1),
    Nombre_Departamento VARCHAR(100) NOT NULL,
    CONSTRAINT PK_Departamento PRIMARY KEY (Id_Departamento)
);

--Tabla reajuste
CREATE TABLE Solicitud_reajuste(
    Id_reajuste INT IDENTITY (1, 1),
    Nombre_Completo VARCHAR (75) NOT NULL,
    Fecha DATE,
    Cedula VARCHAR (13) NOT NULL,
    Id_Cargo_Actual INT, -- Relación con Cargo Actual
    Salario_Propuesto MONEY NOT NULL,
    Id_Departamento_Propuesto INT, -- Relación con Departamento Propuesto
    Referido_por VARCHAR (50) NOT NULL,
    Observaciones VARCHAR (100),
    Archivo VARBINARY(MAX),
    Extension VARCHAR (50),
    CONSTRAINT PK_Solicitud_reajuste PRIMARY KEY (Id_reajuste),
    CONSTRAINT FK_Cargo_Actual FOREIGN KEY (Id_Cargo_Actual) REFERENCES Cargo(Id_Cargo),
    CONSTRAINT FK_Departamento FOREIGN KEY (Id_Departamento_Propuesto) REFERENCES Departamento(Id_Departamento)
);

drop table Departamento



insert into Usuarios(NombreUsuario, Contraseña) values ('gpaniagua','gp12345.')
insert into Usuarios(NombreUsuario, Contraseña) values ('prueba','123.')

select * from Solicitud_nombramiento
select * from Cargo
select * from Departamento
select * from Cambio_designacion
select * from Solicitud_reajuste


delete from Cargo

SELECT 
    sn.Id_solicitud,
    sn.Fecha,
    sn.Nombre,
    sn.Apellido,
    sn.Cedula,
    sn.Sexo,
    d.Nombre_Departamento AS Departamento,
    c.Nombre_Cargo AS Cargo,
    sn.Grupo_ocupacional,
    sn.Sustitucion,
    sn.Salario,
    sn.Direccion_de_residencia,
    sn.Autorizado,
    sn.Observaciones,
    sn.Enviado_en_oficialNo,
    sn.Pregunta,
    sn.NoOficioyFecha
FROM 
    Solicitud_nombramiento sn
LEFT JOIN 
    Departamento d ON sn.Id_Departamento = d.Id_Departamento
LEFT JOIN 
    Cargo c ON sn.Id_Cargo = c.Id_Cargo
ORDER BY 
    sn.Fecha DESC;
