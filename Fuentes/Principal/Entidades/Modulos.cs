using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class Modulos : BloqueoUsuarios
    {

        int id;
        string nombre;
        string prefijo;

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
        public string Prefijo
        {
            get { return prefijo; }
            set { prefijo = value; }
        }

        public List<Modulos> ObtenerListado()
        {

            try
            {
                List<Modulos> lista = new List<Modulos>();
                SqlCommand comando = new SqlCommand();
                comando.Connection = BaseDatos.conexionConfiguracion;
                comando.CommandText = "SELECT * FROM Modulos"; 
                BaseDatos.conexionConfiguracion.Open();
                SqlDataReader lectorDatos = comando.ExecuteReader();
                Modulos modulos;
                while (lectorDatos.Read())
                {
                    modulos = new Modulos();
                    modulos.id = Convert.ToInt32(lectorDatos["Id"].ToString());
                    modulos.nombre = lectorDatos["Nombre"].ToString();
                    modulos.prefijo = lectorDatos["Prefijo"].ToString();
                    lista.Add(modulos);
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

        public List<Modulos> ObtenerListadoPorId()
        {

            try
            {
                List<Modulos> lista = new List<Modulos>();
                SqlCommand comando = new SqlCommand();
                comando.Connection = BaseDatos.conexionConfiguracion;
                comando.CommandText = "SELECT * FROM Modulos WHERE Id=@id";
                comando.Parameters.AddWithValue("@id", this.Id);
                BaseDatos.conexionConfiguracion.Open();
                SqlDataReader lectorDatos = comando.ExecuteReader();
                Modulos modulos;
                while (lectorDatos.Read())
                {
                    modulos = new Modulos();
                    modulos.id = Convert.ToInt32(lectorDatos["Id"].ToString());
                    modulos.nombre = lectorDatos["Nombre"].ToString();
                    modulos.prefijo = lectorDatos["Prefijo"].ToString();
                    lista.Add(modulos);
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

        public List<Modulos> ObtenerMenuListado()
        {

            try
            {
                List<Modulos> lista = new List<Modulos>();
                SqlCommand comando = new SqlCommand();
                comando.Connection = BaseDatos.conexionConfiguracion;
                comando.CommandText = "SELECT M.* FROM Modulos AS M LEFT JOIN (SELECT * FROM BloqueoUsuarios WHERE IdUsuario = @idUsuario) AS BU ON (M.Id = BU.IdModulo) WHERE BU.IdUsuario IS NULL";
                comando.Parameters.AddWithValue("@idUsuario", this.IdUsuario);
                comando.Parameters.AddWithValue("@id", this.Id);
                BaseDatos.conexionConfiguracion.Open();
                SqlDataReader lectorDatos = comando.ExecuteReader();
                Modulos modulos;
                while (lectorDatos.Read())
                {
                    modulos = new Modulos(); 
                    modulos.id = Convert.ToInt32(lectorDatos["Id"].ToString());
                    modulos.nombre = lectorDatos["Nombre"].ToString();
                    modulos.prefijo = lectorDatos["Prefijo"].ToString();
                    lista.Add(modulos);
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

        public DataTable ObtenerListadoReporte()
        {

            try
            {
                DataTable datos = new DataTable();
                SqlCommand comando = new SqlCommand();
                comando.Connection = BaseDatos.conexionConfiguracion;
                comando.CommandText = "SELECT Id, Nombre, Prefijo FROM Modulos";
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
                comando.CommandText = "SELECT * FROM Modulos WHERE Id=@id";
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
                comando.CommandText = "INSERT INTO Modulos (Id, Nombre, Prefijo) VALUES (@id, @nombre, @prefijo)";
                comando.Parameters.AddWithValue("@id", this.Id);
                comando.Parameters.AddWithValue("@nombre", this.Nombre);
                comando.Parameters.AddWithValue("@prefijo", this.Prefijo);
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
                comando.CommandText = "UPDATE Modulos SET Nombre=@nombre, Prefijo=@prefijo WHERE Id=@id"; 
                comando.Parameters.AddWithValue("@id", this.Id);
                comando.Parameters.AddWithValue("@nombre", this.Nombre);
                comando.Parameters.AddWithValue("@prefijo", this.Prefijo);
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
                if (this.Id > 0)
                    condiciones += " WHERE Id=@id";
                comando.CommandText = "DELETE FROM Modulos";
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
