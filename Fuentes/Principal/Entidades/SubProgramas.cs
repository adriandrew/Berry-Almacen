using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class SubProgramas : BloqueoUsuarios
    {

        private int idModulo;
        private int idPrograma; 
        private int id;
        private string nombre;

        public int IdModuloo
        {
            get { return idModulo; }
            set { idModulo = value; }
        }
        public int IdProgramaa
        {
            get { return idPrograma; }
            set { idPrograma = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
         
        public List<SubProgramas> ObtenerMenuListado()
        {

            List<SubProgramas> lista = new List<SubProgramas>();
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = BaseDatos.conexionConfiguracion;
                comando.CommandText = "SELECT SP.* FROM SubProgramas AS SP LEFT JOIN (SELECT * FROM BloqueoUsuarios WHERE IdUsuario = @idUsuario) AS BU ON (SP.IdModulo = BU.IdModulo AND SP.IdPrograma = BU.IdPrograma AND SP.Id = BU.IdSubPrograma) WHERE SP.IdModulo = @idModulo AND SP.IdPrograma = @idPrograma AND BU.IdUsuario IS NULL";
                comando.Parameters.AddWithValue("@idUsuario", this.IdUsuario);
                comando.Parameters.AddWithValue("@idModulo", this.IdModuloo);
                comando.Parameters.AddWithValue("@idPrograma", this.IdProgramaa);
                BaseDatos.conexionConfiguracion.Open();
                SqlDataReader lectorDatos = comando.ExecuteReader();
                SubProgramas subProgramas;
                while (lectorDatos.Read())
                {
                    subProgramas = new SubProgramas();
                    subProgramas.idModulo = Convert.ToInt32(lectorDatos["IdModulo"].ToString());
                    subProgramas.idPrograma = Convert.ToInt32(lectorDatos["IdPrograma"].ToString());
                    subProgramas.id = Convert.ToInt32(lectorDatos["Id"].ToString());
                    subProgramas.nombre = lectorDatos["Nombre"].ToString();
                    lista.Add(subProgramas);
                }
                BaseDatos.conexionConfiguracion.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                BaseDatos.conexionConfiguracion.Close();
            }

        }

        public bool ValidarPorIdModuloyIdPrograma()
        {

            try
            {
                bool resultado = false;
                SqlCommand comando = new SqlCommand();
                comando.Connection = BaseDatos.conexionConfiguracion;
                comando.CommandText = "SELECT * FROM SubProgramas WHERE IdModulo=@idModulo AND IdPrograma=@idPrograma"; 
                comando.Parameters.AddWithValue("@idModulo", this.IdModuloo);
                comando.Parameters.AddWithValue("@idPrograma", this.IdProgramaa); 
                BaseDatos.conexionConfiguracion.Open();
                SqlDataReader lectorDatos = comando.ExecuteReader();
                if (lectorDatos.HasRows) 
                    resultado = true; 
                else 
                    resultado = false; 
                BaseDatos.conexionConfiguracion.Close();
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                BaseDatos.conexionConfiguracion.Close();
            }

        }

        public DataTable ObtenerListadoReporte()
        {

            DataTable datos = new DataTable();
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = BaseDatos.conexionConfiguracion;
                string condicion = string.Empty;
                if (this.IdModuloo >= 0) 
                    condicion += " AND IdModulo=@idModulo";
                if (this.IdProgramaa >= 0) 
                    condicion += " AND IdPrograma=@idPrograma";
                comando.CommandText = "SELECT Id, Nombre FROM SubProgramas WHERE 0=0 " + condicion;
                comando.Parameters.AddWithValue("@idModulo", this.IdModuloo);
                comando.Parameters.AddWithValue("@idPrograma", this.IdProgramaa); 
                BaseDatos.conexionConfiguracion.Open();
                SqlDataReader lectorDatos = comando.ExecuteReader();
                datos.Load(lectorDatos);
                BaseDatos.conexionConfiguracion.Close();
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                BaseDatos.conexionConfiguracion.Close();
            }

        }

        new public bool ValidarPorId()
        {

            try
            {
                bool resultado = false;
                SqlCommand comando = new SqlCommand();
                comando.Connection = BaseDatos.conexionConfiguracion;
                string condiciones = string.Empty;
                if (this.IdModuloo > 0)
                {
                    condiciones += " AND IdModulo=@idModulo ";
                }
                if (this.IdProgramaa > 0)
                {
                    condiciones += " AND IdPrograma=@idPrograma";
                }
                if (this.Id > 0)
                {
                    condiciones += " AND Id=@id ";
                }
                comando.CommandText = "SELECT * FROM SubProgramas WHERE 0=0 " + condiciones;
                comando.Parameters.AddWithValue("@idModulo", this.IdModuloo);
                comando.Parameters.AddWithValue("@idPrograma", this.IdProgramaa);
                comando.Parameters.AddWithValue("@id", this.Id);
                BaseDatos.conexionConfiguracion.Open();
                SqlDataReader lectorDatos = comando.ExecuteReader();
                if (lectorDatos.HasRows)
                {
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }
                BaseDatos.conexionConfiguracion.Close();
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                BaseDatos.conexionConfiguracion.Close();
            }

        }

        new public void Guardar()
        {

            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = BaseDatos.conexionConfiguracion;
                comando.CommandText = "INSERT INTO SubProgramas (IdModulo, IdPrograma, Id, Nombre) VALUES (@idModulo, @idPrograma, @id, @nombre)";
                comando.Parameters.AddWithValue("@idModulo", this.IdModuloo);
                comando.Parameters.AddWithValue("@idPrograma", this.IdProgramaa);
                comando.Parameters.AddWithValue("@id", this.Id);
                comando.Parameters.AddWithValue("@nombre", this.Nombre);
                BaseDatos.conexionConfiguracion.Open();
                comando.ExecuteNonQuery();
                BaseDatos.conexionConfiguracion.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                BaseDatos.conexionConfiguracion.Close();
            }

        }

        public void Editar()
        {

            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = BaseDatos.conexionConfiguracion;
                comando.CommandText = "UPDATE SubProgramas SET Nombre=@nombre WHERE IdModulo=@idModulo AND IdPrograma=@idPrograma AND Id=@id";
                comando.Parameters.AddWithValue("@idModulo", this.IdModuloo);
                comando.Parameters.AddWithValue("@idPrograma", this.IdProgramaa);
                comando.Parameters.AddWithValue("@id", this.Id);
                comando.Parameters.AddWithValue("@nombre", this.Nombre);
                BaseDatos.conexionConfiguracion.Open();
                comando.ExecuteNonQuery();
                BaseDatos.conexionConfiguracion.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                BaseDatos.conexionConfiguracion.Close();
            }

        }

        new public void Eliminar()
        {

            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = BaseDatos.conexionConfiguracion;
                string condiciones = string.Empty;
                if (this.IdModuloo > 0) 
                    condiciones += " AND IdModulo=@idModulo";
                if (this.IdProgramaa > 0)
                    condiciones += " AND IdPrograma=@idPrograma";
                if (this.Id > 0)
                    condiciones += " AND Id=@id"; 
                comando.CommandText = "DELETE FROM SubProgramas WHERE 0=0 " + condiciones;
                comando.Parameters.AddWithValue("@idModulo", this.IdModuloo);
                comando.Parameters.AddWithValue("@idPrograma", this.IdProgramaa);
                comando.Parameters.AddWithValue("@id", this.Id);
                BaseDatos.conexionConfiguracion.Open();
                comando.ExecuteNonQuery();
                BaseDatos.conexionConfiguracion.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                BaseDatos.conexionConfiguracion.Close();
            }

        }

    }
}
