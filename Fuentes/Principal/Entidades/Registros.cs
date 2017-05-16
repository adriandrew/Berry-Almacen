using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class Registros
    {
          
        private int idUsuario;
        private string nombreEquipo;
        private bool esSesionIniciada;
        
        public int IdUsuario
        {
            get { return idUsuario; }
            set { idUsuario = value; }
        }
        public string NombreEquipo
        {
            get { return nombreEquipo; }
            set { nombreEquipo = value; }
        }
        public bool EsSesionIniciada
        {
            get { return esSesionIniciada; }
            set { esSesionIniciada = value; }
        }
        
        public void Guardar()
        {
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = BaseDatos.conexionConfiguracion;
                comando.CommandText = "INSERT INTO Registros (IdUsuario, NombreEquipo, EsSesionIniciada) VALUES (@idUsuario, @nombreEquipo, @esSesionIniciada)"; 
                comando.Parameters.AddWithValue("@idUsuario", this.IdUsuario);
                comando.Parameters.AddWithValue("@nombreEquipo", this.NombreEquipo);
                comando.Parameters.AddWithValue("@esSesionIniciada", this.EsSesionIniciada);
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
                comando.CommandText = "UPDATE Registros SET IdUsuario=@idUsuario, EsSesionIniciada=@esSesionIniciada WHERE NombreEquipo=@nombreEquipo";
                comando.Parameters.AddWithValue("@idUsuario", this.IdUsuario);
                comando.Parameters.AddWithValue("@nombreEquipo", this.NombreEquipo);
                comando.Parameters.AddWithValue("@esSesionIniciada", this.EsSesionIniciada);
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

        public void EditarSoloSesion()
        {
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = BaseDatos.conexionConfiguracion;
                comando.CommandText = "UPDATE Registros SET EsSesionIniciada=@esSesionIniciada WHERE NombreEquipo=@nombreEquipo";
                comando.Parameters.AddWithValue("@nombreEquipo", this.NombreEquipo);
                comando.Parameters.AddWithValue("@esSesionIniciada", this.EsSesionIniciada);
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
                comando.CommandText = "DELETE FROM Registros WHERE NombreEquipo=@nombreEquipo";
                comando.Parameters.AddWithValue("@nombreEquipo", this.NombreEquipo);
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

        public bool ValidarPorNombreEquipo()
        {

            try
            {
                bool resultado = false;
                SqlCommand comando = new SqlCommand();
                comando.Connection = BaseDatos.conexionConfiguracion;
                comando.CommandText = "SELECT * FROM Registros WHERE NombreEquipo=@nombreEquipo"; 
                comando.Parameters.AddWithValue("@nombreEquipo", this.NombreEquipo);
                BaseDatos.conexionConfiguracion.Open();
                SqlDataReader lectorDatos = default(SqlDataReader);
                lectorDatos = comando.ExecuteReader();
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

        public bool ValidarSesionPorNombreEquipo()
        {

            try
            {
                bool resultado = false;
                SqlCommand comando = new SqlCommand();
                comando.Connection = BaseDatos.conexionConfiguracion;
                comando.CommandText = "SELECT * FROM Registros WHERE NombreEquipo=@nombreEquipo"; 
                comando.Parameters.AddWithValue("@nombreEquipo", this.NombreEquipo);
                BaseDatos.conexionConfiguracion.Open();
                SqlDataReader lectorDatos = default(SqlDataReader);
                lectorDatos = comando.ExecuteReader();
                while (lectorDatos.Read())
                { 
                    resultado = Convert.ToBoolean(lectorDatos["EsSesionIniciada"].ToString()); 
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

        public List<Registros> ObtenerListadoPorNombreEquipo()
        {

            try
            {
                List<Registros> lista = new List<Registros>();
                SqlCommand comando = new SqlCommand();
                comando.Connection = BaseDatos.conexionConfiguracion;
                comando.CommandText = "SELECT * FROM Registros WHERE NombreEquipo=@nombreEquipo";
                comando.Parameters.AddWithValue("@nombreEquipo", this.NombreEquipo);
                BaseDatos.conexionConfiguracion.Open();
                SqlDataReader lectorDatos = default(SqlDataReader);
                lectorDatos = comando.ExecuteReader();
                Registros registro = new Registros();
                while (lectorDatos.Read())
                {
                    registro = new Registros();
                    registro.idUsuario = Convert.ToInt32(lectorDatos["IdUsuario"].ToString()); 
                    registro.nombreEquipo = lectorDatos["NombreEquipo"].ToString();
                    registro.esSesionIniciada = Convert.ToBoolean(lectorDatos["EsSesionIniciada"].ToString());
                    lista.Add(registro);
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
         
    }
}
