drop database Apptelink
/*Creacion de Base de Datos*/
create database Apptelink
go

use Apptelink
go

/*Tablas*/
-- Tabla de Login
create table userLogin(
	iduser int primary key identity,
	username varchar(50) not null,
	password varchar(50) not null,
)
go

-- Tabla Familia Productos
create table Familia_Productos(
	IdFamilia int primary key identity,
	Codigo varchar(15) UNIQUE not null,
	Nombre varchar(255) not null,
	Activo bit DEFAULT 1,
	FechaCreacion date not null
)
go

-- Tabla Producto
create table Producto(
	IdProducto int primary key identity,
	Codigo varchar(15) UNIQUE not null,
	Nombre varchar(255) not null,
	IdFamilia int not null,
	Precio decimal(10,2) not null,
	Stock int not null,
	Activo bit DEFAULT 1,
	FechaCreacion date not null
	Constraint fk_Familia_IdFamilia Foreign key (IdFamilia) references Familia_Productos(IdFamilia)
)
go

-- Tabla Cabecera Factura
create table Cabecera_Factura(
	IdFactura int Primary key identity,
	NroFacturas varchar(11) UNIQUE NOT NULL,
	RucCliente char(11) not null,
	RazonSocialCLiente varchar(255) not null,
	Subtotal decimal(10,2)DEFAULT 0 not null,
	PorcentajeIGV decimal not null,
)
go

-- Tabla Detalle Factura
create table Detalle_Factura(
	IdItem int primary key identity,
	IdFactura int not null,
	CodigoProducto varchar(15) not null,
	Cantidad int not null,
	Precio decimal not null,
	subtotal as (Cantidad * Precio ),
	Constraint fk_Factura_IdFactura Foreign Key (IdFactura) references Cabecera_Factura(IdFactura)
)
go


/** Procedimientos Almacenados*/

--Procedimiento de Login
create procedure sp_login
@username varchar(50),
@password varchar(50),
@ReturnValue int OUTPUT
as
begin 
	SET NOCOUNT ON;
    IF EXISTS (SELECT * FROM userLogin WHERE username = @username AND password = @password)
    BEGIN
        SET @ReturnValue = 1; -- Autenticación exitosa
    END
    ELSE
    BEGIN
        SET @ReturnValue = 0; -- Autenticación fallida
    END
end;

-- Procedimiento Listar Familia Productos
create procedure sp_listarFamilia
as 
begin
	select * from Familia_Productos
end;

-- Procedimiento Listar Una Familia Productos
create procedure sp_listarUnaFamilia
@id int 
as 
begin
	select * from Familia_Productos where IdFamilia = @id
end;

-- Procedimiento Crear Familia Productos
create procedure sp_crearFamilia
@Codigo varchar(15),
@Nombre varchar(255)
as
begin
	insert into Familia_Productos(Codigo,Nombre,Activo,FechaCreacion) values
	(@Codigo,@Nombre,1, GETDATE());
end;

-- Procedimiento Editar una Familia
create procedure sp_editarFamilia
@Id int,
@Codigo varchar(15),
@Nombre varchar(255),
@Activo bit,
@FechaCreacion date
as
begin
	
	update Familia_Productos set Codigo = @Codigo, Nombre = @Nombre, Activo = @Activo, FechaCreacion = @FechaCreacion
	where IdFamilia = @Id
end;

-- Procedimiento Eliminar una Familia Productos
create procedure sp_eliminarFamilia
@Id int
as
begin
	update Familia_Productos set Activo = 0
	where IdFamilia = @Id
end;

-- Procedimiento Listar Productos
create procedure sp_listarProducto
as
begin
	select * from Producto
end;


-- Procedimineto Listar un Producto
create procedure sp_listarUnProducto
@Id int
as 
begin
	select * from Producto where IdProducto = @Id;
end;

-- Procedimiento Crear un Producto
create procedure sp_crearProducto
@Codigo varchar(15),
@Nombre varchar(255),
@IdFamilia int,
@Precio decimal(10,2),
@Stock int
as
begin
	insert into Producto(Codigo, Nombre,IdFamilia,Precio,Stock,Activo,FechaCreacion)
	values(@Codigo,@Nombre,@IdFamilia,@Precio,@Stock,1,GETDATE());
end;

-- Procedimiento Editar un Producto
create procedure sp_editarProducto
@Id int,
@Codigo varchar(15),
@Nombre varchar(255),
@IdFamilia int,
@Precio decimal(10,2),
@Stock int,
@Activo bit,
@FechaCreacion date
as
begin 
	update Producto set Codigo = @Codigo, Nombre = @Nombre, IdFamilia = @IdFamilia,
	Precio = @Precio, Stock =@Stock, Activo = @Activo, FechaCreacion = @FechaCreacion
	where IdProducto = @Id
end;

-- Procedimiento Eliminar un Producto
create procedure sp_eliminarProducto
@Id int
as
begin
	update Producto set Activo = 0
	where IdProducto = @Id
end;

-- Procedimiento Listar Familia con Id y Nombnre
create procedure sp_listarFamiliasPorId
as 
begin
	select IdFamilia, Nombre from Familia_Productos 
end;


-- Procedimiento Listar DetalleFactura
create procedure sp_listarDetalle
as
begin
	select df.IdItem,df.IdFactura,df.CodigoProducto,p.Nombre, df.Precio,df.Cantidad,df.subtotal  from Detalle_Factura df inner join Producto p on df.CodigoProducto = p.Codigo;
end;

create procedure sp_listarUnDetalle
@Id int
as 
begin
	select cf.IdItem,cf.IdFactura,cf.CodigoProducto,p.Nombre, cf.Precio,cf.Cantidad, cf.subtotal from Detalle_Factura cf inner join Producto p 
	on cf.CodigoProducto = p.Codigo where IdItem = @Id;
end;

create procedure sp_crearDetalle
@IdFactura int,
@CodigoProducto varchar(250),
@Cantidad int,
@Precio decimal
as
begin
	Insert into Detalle_Factura(IdFactura,CodigoProducto,Cantidad,Precio)
	values(@IdFactura,@CodigoProducto,@Cantidad,@Precio);
end;


create procedure sp_eliminarDetalle
@Id int
as
begin
	delete from Detalle_Factura where IdItem = @Id
end;


create procedure sp_listarProductosPorCodyNombre
as
begin
	select Codigo, Nombre, Precio from Producto
end;

create procedure sp_listarPrecioProductoByCod
@Codigo varchar(15)
as 
begin
	select Precio from Producto where Codigo = @Codigo
end;
create procedure sp_listarDetallePorIdFactura
@Id int
as
begin
	select cf.IdItem,cf.IdFactura,cf.CodigoProducto,p.Nombre, cf.Precio,cf.Cantidad, cf.subtotal from Detalle_Factura cf inner join Producto p 
	on cf.CodigoProducto = p.Codigo where IdFactura = @Id;
end;



-- Procedimiento Listar Cabeceras
create procedure sp_listarCabecera
as
begin
	select * from Cabecera_Factura 
end;


-- Procedimiento Listar Una Cabecera 
create procedure sp_listarUnaCabecera
@Id int
as
begin
	select * from Cabecera_Factura where IdFactura = @Id
end;

/*En Pausa por el momento*/
create procedure sp_crearCabecera
@NroFacturas varchar(11),
@RucCliente char(11),
@RazonSocialCLiente varchar(255)
as
begin 
	insert into Cabecera_Factura(NroFacturas,RucCliente,RazonSocialCLiente,Subtotal,PorcentajeIGV)
	values(@NroFacturas,@RucCliente,@RazonSocialCLiente,0,18);
	select * from Cabecera_Factura where IdFactura = SCOPE_IDENTITY();
end;

create procedure sp_EliminarCabecera
@Id int
as
begin
	delete from Cabecera_Factura where IdFactura = @Id;
end;

Create trigger ActualizarSubtotal
on Detalle_Factura
After Insert
as
begin	
	Declare @IdFractura int;
	select @IdFractura = IdFactura
	from inserted;

	update cf
	Set Subtotal = COALESCE((select SUM(df.Subtotal)
	from Detalle_Factura df where df.IdFactura = @IdFractura),0)
	from Cabecera_Factura cf
	where cf.IdFactura = @IdFractura;
end;



create trigger ActualizarStock
on Detalle_Factura
After Insert
as 
begin
	DECLARE @CodigoProducto varchar(15);
    SELECT @CodigoProducto = CodigoProducto
    FROM INSERTED;

    UPDATE Producto
    SET Stock = Stock - df.Cantidad
    FROM Detalle_Factura df
    WHERE Producto.Codigo = @CodigoProducto;
end;

create trigger DetalleFacturaDelete
on Detalle_Factura
Instead of DELETE
as
begin
	DECLARE @CodigoProducto varchar(15);
	DECLARE @IdItem int;
	Declare @IdFactura int;
	select @IdFactura = IdFactura from deleted
	Select @IdItem =IdItem from deleted
    SELECT @CodigoProducto = CodigoProducto
    FROM deleted;

    UPDATE Producto
    SET Stock = Stock + df.Cantidad
    FROM Detalle_Factura df
    WHERE Producto.Codigo = @CodigoProducto;

	update Cabecera_Factura
	set Subtotal = Subtotal - (Select Subtotal from 
	deleted where IdItem = @IdItem)
	where IdFactura = @IdFactura;

	delete from Detalle_Factura
	where IdItem = @IdItem;
end;


/*Valores de Demostracion*/
-- Inserts de Tabla Login
INSERT INTO [dbo].[userLogin] ([username] ,[password])
VALUES('admin','admin')
GO
INSERT INTO [dbo].[userLogin] ([username] ,[password])
VALUES('developer','developer')
GO

-- Inserts de Tabla Familia Productos
INSERT INTO [dbo].[Familia_Productos]([Codigo],[Nombre],[Activo],[FechaCreacion])
     VALUES ('F001','jabones',1,'11/11/2023')
GO
INSERT INTO [dbo].[Familia_Productos]([Codigo],[Nombre],[Activo],[FechaCreacion])
     VALUES ('F002','Maquillaje',1,'11/12/2023')
GO
INSERT INTO [dbo].[Familia_Productos]([Codigo],[Nombre],[Activo],[FechaCreacion])
     VALUES ('F003','Champus',1,'11/13/2023')
GO

-- Inserts de Tabla Productos
INSERT INTO [dbo].[Producto]
           ([Codigo],[Nombre],[IdFamilia],[Precio],[Stock],[Activo],[FechaCreacion])
     VALUES ('A001','Producto 01',1, 3.00,50,1,'11/13/2023')
GO

INSERT INTO [dbo].[Producto]
           ([Codigo],[Nombre],[IdFamilia],[Precio],[Stock],[Activo],[FechaCreacion])
     VALUES ('A002','Producto 02',1, 3.00,50,1,'11/13/2023')
GO
INSERT INTO [dbo].[Cabecera_Factura]
           ([NroFacturas],[RucCliente],[RazonSocialCLiente],[PorcentajeIGV])
     VALUES('F003',11747630619,'Empresa de compra de verdudas', 30)
GO
INSERT INTO [dbo].[Cabecera_Factura]
           ([NroFacturas],[RucCliente],[RazonSocialCLiente],[PorcentajeIGV])
     VALUES('F004',11747630619,'Empresa de compra de verdudas', 30)
GO


INSERT INTO [dbo].[Detalle_Factura]
           ([IdFactura],[CodigoProducto] ,[Cantidad],[Precio])
     VALUES(1,'A001',20,13)
GO
INSERT INTO [dbo].[Detalle_Factura]
           ([IdFactura],[CodigoProducto] ,[Cantidad],[Precio])
     VALUES(1,'A002',20,13)
GO
INSERT INTO [dbo].[Detalle_Factura]
           ([IdFactura],[CodigoProducto] ,[Cantidad],[Precio])
     VALUES(1,'A001',20,13)
GO
select * from Detalle_Factura
select * from Producto
select * from Cabecera_Factura

