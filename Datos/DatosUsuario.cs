using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad;

namespace Datos
{
    public class DatosUsuario
    {
        public string Autentificacion(EntidadUsuario Usuario)
        {
            try
            {
                using (SqlConnection cnx = new SqlConnection(ConfigurationManager.ConnectionStrings["cnnString"].ToString()))
                {
                    //ABRIMOS LA CONEXION
                    cnx.Open();

                    //DECLARAMOS LA CONSULTA
                    string sqlQuery = "sp_Autentificacion";

                    //LE MANDAMOS LA CONSULTA A LA BASE DE DATOS
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, cnx))
                    {
                        //AGREGARON LOS PARAMETROS
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NombreUsuario", Usuario.NombreUsuario);
                        cmd.Parameters.AddWithValue("@Contrasena", Usuario.Contrasena);     

                        SqlDataReader dr = cmd.ExecuteReader();

                        if(dr.Read())
                        {
                            return dr["Nombre"].ToString();
                        }
                        else
                        {
                            return "";
                        }                        
                    }
                }
            }catch(Exception ex)          
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
