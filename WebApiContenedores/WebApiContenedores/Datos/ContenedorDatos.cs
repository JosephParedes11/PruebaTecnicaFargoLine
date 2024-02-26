using CrudBasico.Datos;
using System.Data;
using System.Data.SqlClient;
using WebApiContenedores.Models;

namespace WebApiContenedores.Datos
{
    public class ContenedorDatos
    {
        public async Task<List<ContenedorModel>> GetAllContenedores()
        {
            var oLista = new List<ContenedorModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_getAllContenedores", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        oLista.Add(new ContenedorModel()
                        {
                            in_id = Convert.ToInt32(dr["in_id"]),
                            in_numero = Convert.ToInt32(dr["in_numero"]),
                            in_idTipo = Convert.ToInt32(dr["in_idTipo"]),
                            in_size = Convert.ToInt32(dr["in_tamaño"]),
                            dc_peso = Convert.ToDecimal(dr["dc_peso"]),
                            dc_tara = Convert.ToDecimal(dr["dc_tara"])
                            /*bt_estado = Convert.ToBoolean(dr["bt_estado"])*/
                        });
                    }
                }
            }
            return oLista;
            
        }

        public async Task<bool> GetContenedoreById(int idContenedor)
        {
            bool result = false;

            var contenedor = new ContenedorModel();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_getContenedorById", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@in_id", idContenedor);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        /* contenedor.Add(new ContenedorModel()
                         {
                             in_id = Convert.ToInt32(dr["in_id"]),
                             in_numero = Convert.ToInt32(dr["in_numero"]),
                             in_idTipo = Convert.ToInt32(dr["in_idTipo"]),
                             in_size = Convert.ToInt32(dr["in_tamaño"]),
                             dc_peso = Convert.ToInt32(dr["dc_peso"]),
                             dc_tara = Convert.ToInt32(dr["dc_tara"])
                             *//*bt_estado = Convert.ToBoolean(dr["bt_estado"])*//*
                         });*/
                        contenedor.in_id = Convert.ToInt32(dr["in_id"]);
                        contenedor.in_numero = Convert.ToInt32(dr["in_numero"]);
                        contenedor.in_idTipo = Convert.ToInt32(dr["in_idTipo"]);
                        contenedor.in_size = Convert.ToInt32(dr["in_tamaño"]);
                        contenedor.dc_peso = Convert.ToInt32(dr["dc_peso"]);
                        contenedor.dc_tara = Convert.ToInt32(dr["dc_tara"]);
                    }
                }
            }

            if (contenedor != null)
            {
                result = true;
            }

            return result;
        }

        public async Task<List<TipoContenedorModel>> GetAllTiposContenedor()
        {

            var oLista = new List<TipoContenedorModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_getAllTiposContenedor", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        oLista.Add(new TipoContenedorModel()
                        {
                            in_idTipo = Convert.ToInt32(dr["in_idTipo"]),
                            vc_descripcion = Convert.ToString(dr["vc_descripcion"])
                        });
                    }
                }
            }
            return oLista;
        }


        public async Task<int> CreateContenedor(ContenedorModel model)
        {
            int insertId = 0;
            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    await conexion.OpenAsync();

                    using (var cmd = new SqlCommand("sp_insertContenedore", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@in_numero", model.in_numero);
                        cmd.Parameters.AddWithValue("@in_idTipo", model.in_idTipo);
                        cmd.Parameters.AddWithValue("@in_tamaño", model.in_size);
                        cmd.Parameters.AddWithValue("@dc_peso", model.dc_peso);
                        cmd.Parameters.AddWithValue("@dc_tara", model.dc_tara);

                        var OutResult = new SqlParameter("@result", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(OutResult);

                        await cmd.ExecuteNonQueryAsync();

                        if (OutResult.Value != DBNull.Value)
                        {
                            insertId = Convert.ToInt32(OutResult.Value);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
            }

            return insertId;
        }

        public async Task<bool> UpdateContenedor(ContenedorModel model)
        {
            bool estado = false;
            int result = 0;
            
            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    await conexion.OpenAsync();

                    using (var cmd = new SqlCommand("sp_updateContenedor", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@in_id", model.in_id);
                        cmd.Parameters.AddWithValue("@in_numero", model.in_numero);
                        cmd.Parameters.AddWithValue("@in_idTipo", model.in_idTipo);
                        cmd.Parameters.AddWithValue("@in_tamaño", model.in_size);
                        cmd.Parameters.AddWithValue("@dc_peso", model.dc_peso);
                        cmd.Parameters.AddWithValue("@dc_tara", model.dc_tara);

                        var OutResult = new SqlParameter("@result", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(OutResult);

                        await cmd.ExecuteNonQueryAsync();

                        if (OutResult.Value != DBNull.Value)
                        {
                            result = Convert.ToInt32(OutResult.Value);
                        }
                    }
                }

                if (result != 0)
                {
                    estado = true;
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
            }

            return estado;
        }

        public async Task<bool> DeleteContenedor(int idContenedor)
        {
            bool estado = false;
            int result = 0;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    await conexion.OpenAsync();

                    using (var cmd = new SqlCommand("sp_DeleteContenedor", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@in_id", idContenedor);

                        var OutResult = new SqlParameter("@result", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(OutResult);

                        await cmd.ExecuteNonQueryAsync();

                        if (OutResult.Value != DBNull.Value)
                        {
                            result = Convert.ToInt32(OutResult.Value);
                        }
                    }
                }

                if (result != 0)
                {
                    estado = true;
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
                // Considera manejar el error de manera adecuada
                // rpta = false; // Asegúrate de que 'rpta' sea manejado adecuadamente
            }

            return estado;
        }
    }
}
