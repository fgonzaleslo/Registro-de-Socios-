CREATE TABLE socio (
id_socio INT NOT NULL PRIMARY KEY IDENTITY,
nombre_socio VARCHAR (100) NOT NULL,
apellido_socio VARCHAR (100) NOT NULL,
correo_socio VARCHAR (255) NOT NULL UNIQUE,
celular VARCHAR(20) NULL,
direccion VARCHAR(MAX) NULL,
creacion_socio DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

INSERT INTO socio ( nombre_socio,apellido_socio,correo_socio,celular,direccion) VALUES 
('Luis', 'Gonzáles Prieto','luis.gonzales93@hotmail.com','+51925830481','Primera etapa, avenida las palmeras, mz b4 lote 12-13')

select * from socio
CREATE PROCEDURE Listar_socio 
AS
select id_socio,nombre_socio,apellido_socio,correo_socio,celular,direccion,creacion_socio from socio
ORDER BY id_socio ASC

EXEC Listar_socio