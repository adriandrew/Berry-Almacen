using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class Programas : BloqueoUsuarios
    {

        private int idModulo;
        private int id;
        private string nombre;
         
        public int IdModulo
        {
            get { return idModulo; }
            set { idModulo = value; }
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

        public List<Programas> ObtenerListado()
        {

            try
            {
                List<Programas> lista = new List<Programas>();
                SqlCommand comando = new SqlCommand();
                comando.Connection = BaseDatos.conexionConfiguracion;
                comando.CommandText = "SELECT * FROM Programas"; 
                BaseDatos.conexionConfiguracion.Open();
                SqlDataReader lectorDatos = comando.ExecuteReader();
                Programas programas;
                while (lectorDatos.Read())
                {
                    programas = new Programas();
                    programas.idModulo = Convert.ToInt32(lectorDatos["IdModulo"].ToString()); 
                    programas.id = Convert.ToInt32(lectorDatos["Id"].ToString());
                    programas.nombre = lectorDatos["Nombre"].ToString(); 
                    lista.Add(programas);
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

        public List<Programas> ObtenerMenuListado()
        {

            try
            {
                List<Programas> lista = new List<Programas>();
                SqlCommand comando = new SqlCommand();
                comando.Connection = BaseDatos.conexionConfiguracion;
                //comando.CommandText = "SELECT IdModulo, Id, Nombre FROM Programas WHERE IdModulo=@idModulo";
                comando.CommandText = "SELECT P.* FROM Programas AS P LEFT JOIN (SELECT * FROM BloqueoUsuarios WHERE IdUsuario = @idUsuario) AS BU ON (P.IdModulo = BU.IdModulo AND P.Id = BU.IdPrograma) WHERE P.IdModulo = @idModulo AND BU.IdUsuario IS NULL";
                comando.Parameters.AddWithValue("@idUsuario", this.IdUsuario);
                comando.Parameters.AddWithValue("@idModulo", this.IdModulo);
                BaseDatos.conexionConfiguracion.Open();
                SqlDataReader lectorDatos = comando.ExecuteReader();
                Programas programas;
                while (lectorDatos.Read())
                {
                    programas = new Programas();
                    programas.idModulo = Convert.ToInt32(lectorDatos["IdModulo"].ToString());
                    programas.id = Convert.ToInt32(lectorDatos["Id"].ToString());
                    programas.nombre = lectorDatos["Nombre"].ToString();
                    lista.Add(programas);
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
                string condicion = string.Empty;
                if (this.IdModulo >= 0)
                {
                    condicion += " WHERE P.IdModulo=@idModulo";
                }
                comando.CommandText = "SELECT P.IdModulo, M.Nombre, P.Id, P.Nombre FROM Programas AS P LEFT JOIN Modulos AS M ON P.IdModulo=M.Id " + condicion;
                comando.Parameters.AddWithValue("@idModulo", this.IdModulo);
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

        new public void Guardar()
        {

            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = BaseDatos.conexionConfiguracion;
                comando.CommandText = "INSERT INTO Programas (IdModulo, Id, Nombre) VALUES (@idModulo, @id, @nombre)"; 
                comando.Parameters.AddWithValue("@idModulo", this.IdModulo);
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
                comando.CommandText = "UPDATE Programas SET Nombre=@nombre WHERE IdModulo=@idModulo AND Id=@id";
                comando.Parameters.AddWithValue("@idModulo", this.IdModulo);
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
                if (this.IdModulo > 0)
                {
                    condiciones += " AND IdModulo=@idModulo ";
                }
                if (this.Id > 0)
                {
                    condiciones += " AND Id=@id ";
                }
                comando.CommandText = "DELETE FROM Programas WHERE 0=0 " + condiciones;
                comando.Parameters.AddWithValue("@idModulo", this.IdModulo);
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

        new public bool ValidarPorId()
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
                if (this.Id > 0)
                {
                    condiciones += " AND Id=@id ";
                }
                comando.CommandText = "SELECT * FROM Programas WHERE 0=0 " + condiciones;
                comando.Parameters.AddWithValue("@idModulo", this.IdModulo);
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

    }
}
