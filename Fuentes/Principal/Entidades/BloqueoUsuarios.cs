using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class BloqueoUsuarios
    {
         
        private int idUsuario;
        private int idModulo;
        private int idPrograma;
        private int idSubPrograma;
         
        public int IdUsuario
        {
            get { return idUsuario; }
            set { idUsuario = value; }
        }        
        public int IdModulo
        {
            get { return idModulo; }
            set { idModulo = value; }
        }
        public int IdPrograma
        {
            get { return idPrograma; }
            set { idPrograma = value; }
        }
        public int IdSubPrograma
        {
            get { return idSubPrograma; }
            set { idSubPrograma = value; }
        }

        public void Guardar()
        {

            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = BaseDatos.conexionConfiguracion;
                comando.CommandText = "INSERT INTO BloqueoUsuarios (IdUsuario, IdModulo, IdPrograma, IdSubPrograma) VALUES (@idUsuario, @idModulo, @idPrograma, @idSubPrograma)"; 
                comando.Parameters.AddWithValue("@idUsuario", this.IdUsuario);
                comando.Parameters.AddWithValue("@idModulo", this.IdModulo);
                comando.Parameters.AddWithValue("@idPrograma", this.IdPrograma);
                comando.Parameters.AddWithValue("@idSubPrograma", this.IdSubPrograma);
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

        public void Eliminar()
        {

            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = BaseDatos.conexionConfiguracion;
                string condiciones = string.Empty; 
                if (this.IdModulo > 0) 
                    condiciones += " AND IdModulo=@idModulo"; 
                if (this.IdPrograma > 0) 
                    condiciones += " AND IdPrograma=@idPrograma"; 
                if (this.IdSubPrograma > 0)
                    condiciones += " AND IdSubPrograma=@idSubPrograma";
                if (this.IdUsuario > 0)
                    condiciones += " AND IdUsuario=@idUsuario"; 
                comando.CommandText = "DELETE FROM BloqueoUsuarios WHERE 0=0 " + condiciones;
                comando.Parameters.AddWithValue("@idUsuario", this.IdUsuario);
                comando.Parameters.AddWithValue("@idModulo", this.IdModulo);
                comando.Parameters.AddWithValue("@idPrograma", this.IdPrograma);
                comando.Parameters.AddWithValue("@idSubPrograma", this.IdSubPrograma);
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

        public bool Obtener()
        {

            try
            {
                bool resultado = false;
                SqlCommand comando = new SqlCommand();
                comando.Connection = BaseDatos.conexionConfiguracion;
                string condiciones = string.Empty;
                if (this.IdModulo > 0)
                {
                    condiciones += " AND IdModulo=@idModulo ";
                }
                if (this.IdPrograma > 0)
                {
                    condiciones += " AND IdPrograma=@idPrograma ";
                }
                if (this.IdSubPrograma > 0)
                {
                    condiciones += " AND IdSubPrograma=@idSubPrograma ";
                }
                comando.CommandText = "SELECT * FROM BloqueoUsuarios WHERE IdUsuario=@idUsuario " + condiciones;
                comando.Parameters.AddWithValue("@idUsuario", this.IdUsuario);
                comando.Parameters.AddWithValue("@idModulo", this.IdModulo);
                comando.Parameters.AddWithValue("@idPrograma", this.IdPrograma);
                comando.Parameters.AddWithValue("@idSubPrograma", this.IdSubPrograma);
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

        public bool ValidarPorId()
        {

            try
            {
                bool resultado = false;
                SqlCommand comando = new SqlCommand();
                comando.Connection = BaseDatos.conexionConfiguracion;
                string condiciones = string.Empty;
                if (this.IdModulo > 0)
                {
                    condiciones += " AND IdModulo=@idModulo ";
                }
                if (this.IdPrograma > 0)
                {
                    condiciones += " AND IdPrograma=@idPrograma ";
                }
                if (this.IdSubPrograma > 0)
                {
                    condiciones += " AND IdSubPrograma=@idSubPrograma ";
                }
                comando.CommandText = "SELECT * FROM BloqueoUsuarios WHERE IdUsuario=@idUsuario " + condiciones;
                comando.Parameters.AddWithValue("@idUsuario", this.IdUsuario);
                comando.Parameters.AddWithValue("@idModulo", this.IdModulo);
                comando.Parameters.AddWithValue("@idPrograma", this.IdPrograma);
                comando.Parameters.AddWithValue("@idSubPrograma", this.IdSubPrograma);
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

    }
}
