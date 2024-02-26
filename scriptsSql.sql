/*
tabla de tipos de contenedor
*/
CREATE TABLE [dbo].[tipo_contenedor](
	[in_idTipo] [int] IDENTITY(1,1) NOT NULL,
	[vc_descripcion] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[in_idTipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


/*
tabla de contenedores
*/
CREATE TABLE [dbo].[contenedores](
	[in_id] [int] IDENTITY(1,1) NOT NULL,
	[in_numero] [int] NOT NULL,
	[in_idTipo] [int] NOT NULL,
	[in_tamaño] [int] NOT NULL,
	[dc_peso] [decimal](12, 5) NOT NULL,
	[dc_tara] [decimal](12, 5) NOT NULL,
	[bt_estado] [bit] NULL,
	[dt_fechaRegistro] [datetime] NULL,
	[dt_fechaUpdate] [datetime] NULL,
	[dt_fechaDelete] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[in_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


ALTER TABLE [dbo].[contenedores] ADD  DEFAULT ((1)) FOR [bt_estado]
GO


ALTER TABLE [dbo].[contenedores] ADD  DEFAULT (getdate()) FOR [dt_fechaRegistro]
GO


ALTER TABLE [dbo].[contenedores]  WITH CHECK ADD FOREIGN KEY([in_idTipo])
REFERENCES [dbo].[tipo_contenedor] ([in_idTipo])
GO


/*
stored para registrar un contenedor 
*/
ALTER procedure [dbo].[sp_insertContenedor]
@in_numero int ,
@in_idTipo int,
@in_tamaño int,
@dc_peso decimal(12,5),
@dc_tara decimal(12,5),
@result int output
as
begin
	set @result=0


insert into
    contenedores (
        in_numero, in_idTipo, in_tamaño, dc_peso, dc_tara
    )
values (
        @in_numero, @in_idTipo, @in_tama ño, @dc_peso, @dc_tara
    )

set @result = SCOPE_IDENTITY ()

end
/*
stored para actualizar un contenedor
*/
ALTER procedure [dbo].[sp_updateContenedor]
@in_id int,
@in_numero int ,
@in_idTipo int,
@in_tamaño int,
@dc_peso decimal(12,5),
@dc_tara decimal(12,5),
@result int output
as
begin
	set @result=0


update contenedores
set
    in_numero = @in_numero,
    in_idTipo = @in_idTipo,
    in_tamaño = @in_tama ño,
    dc_peso = @dc_peso,
    dc_tara = @dc_tara,
    dt_fechaUpdate = GetDate ()
where
    in_id = @in_id
set
    @result = 1

end
/*
stored para obtener un contenedor en base a su identificador
*/
ALTER procedure [dbo].[sp_getContenedorById]  
@in_id int  
as  
begin  
  
 select   
   in_id,  
   in_numero,  
   in_idTipo,  
   in_tamaño,  
   dc_peso,  
   dc_tara  
 from contenedores with(nolock)  
 where in_id = @in_id  
	and bt_estado = 1
end 

/*
stored para obtener los tipos de contenedor
*/

ALTER procedure [dbo].[sp_getAllTiposContenedor]
as
begin
	select in_idTipo,vc_descripcion from tipo_contenedor with(nolock)
end

/*
stored para obtener los contenedores
*/
create procedure [dbo].[sp_getAllContenedores] as 
begin
	select c.in_id, c.in_numero, c.in_idTipo, tc.vc_descripcion, c.in_tamaño, c.dc_peso, c.dc_tara, C.bt_estado
	from contenedores c
	with (nolock)
	    right join tipo_contenedor tc on c.in_idTipo = tc.in_idTipo
	where
	    bt_estado = 1 end
/*
stored para eliminar contenedores
*/ 

Create procedure [dbo].[sp_DeleteContenedor] @in_id 
int, @result int output as 
begin
	set
	    @result = 0 declare @fechaDelete datetime
	update contenedores
	set
	    bt_estado = 0,
	    dt_fechaDelete = GETDATE ()
	where
	    in_id = @in_id
	set
	    @fechaDelete = (
	        select dt_fechaDelete
	        from contenedores
	        with (nolock)
	        where
	            in_id = @in_id
	    ) if(@fechaDelete <> '') begin
	set
	    @result = 1 end else begin
	set
	    @result = -1 end end
