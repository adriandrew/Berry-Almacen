using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class Usuarios
    {
         
        private int id;
        private string nombre;
        private string contrasena;
        private int nivel;
        private bool accesoTotal; 
         
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
        public string Contrasena
        {
            get { return contrasena; }
            set { contrasena = value; }
        }
        public int Nivel
        {
            get { return nivel; }
            set { nivel = value; }
        }
        public bool AccesoTotal
        {
            get { return accesoTotal; }
            set { accesoTotal = value; }
        } 

        public void Guardar()
        {

            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = BaseDatos.conexionCatalogo;
                comando.CommandText = "INSERT INTO Usuarios VALUES (@id, @nombre, @contrasena, @nivel, @accesoTotal)"; 
                comando.Parameters.AddWithValue("@id", this.Id);
                comando.Parameters.AddWithValue("@nombre", this.Nombre);
                comando.Parameters.AddWithValue("@contrasena", this.Contrasena);
                comando.Parameters.AddWithValue("@nivel", this.Nivel);
                comando.Parameters.AddWithValue("@accesoTotal", this.AccesoTotal); 
                BaseDatos.conexionCatalogo.Open();
                comando.ExecuteNonQuery();
                BaseDatos.conexionCatalogo.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                BaseDatos.conexionCatalogo.Close();
            }

        }

        public void Editar()
        {

            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = BaseDatos.conexionCatalogo;
                comando.CommandText = "UPDATE Usuarios SET Nombre=@nombre, Contrasena=@contrasena, Nivel=@nivel, AccesoTotal=@accesoTotal WHERE Id=@id"; 
                comando.Parameters.AddWithValue("@id", this.Id);
                comando.Parameters.AddWithValue("@nombre", this.Nombre);
                comando.Parameters.AddWithValue("@contrasena", this.Contrasena);
                comando.Parameters.AddWithValue("@nivel", this.Nivel);
                comando.Parameters.AddWithValue("@accesoTotal", this.AccesoTotal);
                BaseDatos.conexionCatalogo.Open();
                comando.ExecuteNonQuery();
                BaseDatos.conexionCatalogo.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                BaseDatos.conexionCatalogo.Close();
            }

        }

        public void Eliminar()
        {

            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = BaseDatos.conexionCatalogo;
                string condiciones = string.Empty;
                if (this.Id > 0)
                {
                    condiciones += " WHERE Id=@id";
                }
                comando.CommandText = "DELETE FROM Usuarios";
                comando.Parameters.AddWithValue("@id", this.Id);
                BaseDatos.conexionCatalogo.Open();
                comando.ExecuteNonQuery();
                BaseDatos.conexionCatalogo.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                BaseDatos.conexionCatalogo.Close();
            }

        }
         
        public List<Usuarios> ObtenerListadoPorNombre()
        {

            List<Usuarios> lista = new List<Usuarios>();
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = BaseDatos.conexionCatalogo;
                comando.CommandText = "SELECT * FROM Usuarios WHERE Nombre=@nombre"; 
                comando.Parameters.AddWithValue("@nombre", this.Nombre);
                BaseDatos.conexionCatalogo.Open();
                SqlDataReader lectorDatos = comando.ExecuteReader();
                Usuarios usuarios = default(Usuarios);
                while (lectorDatos.Read())
                {
                    usuarios = new Usuarios(); 
                    usuarios.id = Convert.ToInt32(lectorDatos["id"].ToString());
                    usuarios.nombre = lectorDatos["nombre"].ToString();
                    usuarios.contrasena = lectorDatos["contrasena"].ToString();
                    usuarios.nivel = Convert.ToInt32(lectorDatos["nivel"].ToString());
                    usuarios.accesoTotal = Convert.ToBoolean(lectorDatos["accesoTotal"].ToString()); 
                    lista.Add(usuarios);
                }
                BaseDatos.conexionCatalogo.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                BaseDatos.conexionCatalogo.Close();
            }

        }

        public bool ValidarPorId()
        {

            try
            {
                bool resultado = false;
                SqlCommand comando = new SqlCommand();
                comando.Connection = BaseDatos.conexionCatalogo;
                comando.CommandText = "SELECT * FROM Usuarios WHERE Id=@id"; 
                comando.Parameters.AddWithValue("@id", this.Id);
                BaseDatos.conexionCatalogo.Open();
                SqlDataReader lectorDatos = comando.ExecuteReader();
                if (lectorDatos.HasRows)
                {
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }                
                BaseDatos.conexionCatalogo.Close();
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                BaseDatos.conexionCatalogo.Close();
            }

        }

        public List<Usuarios> ObtenerListado()
        {

            List<Usuarios> lista = new List<Usuarios>();
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = BaseDatos.conexionCatalogo;
                comando.CommandText = "SELECT * FROM Usuarios";
                BaseDatos.conexionCatalogo.Open();
                SqlDataReader lectorDatos = comando.ExecuteReader();
                Usuarios usuarios;
                while (lectorDatos.Read())
                {
                    usuarios = new Usuarios();
                    usuarios.Id = Convert.ToInt32(lectorDatos["id"].ToString());
                    usuarios.Nombre = lectorDatos["nombre"].ToString();
                    usuarios.Contrasena = lectorDatos["contrasena"].ToString();
                    usuarios.Nivel = Convert.ToInt32(lectorDatos["nivel"].ToString());
                    usuarios.AccesoTotal = Convert.ToBoolean(lectorDatos["accesoTotal"].ToString()); 
                    lista.Add(usuarios);
                }
                BaseDatos.conexionCatalogo.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                BaseDatos.conexionCatalogo.Close();
            }

        }

        public List<Usuarios> ObtenerListadoPorId()
        {

            List<Usuarios> lista = new List<Usuarios>();
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = BaseDatos.conexionCatalogo;
                comando.CommandText = "SELECT * FROM Usuarios WHERE Id=@id";
                comando.Parameters.AddWithValue("@id", this.id);
                BaseDatos.conexionCatalogo.Open();
                SqlDataReader lectorDatos = comando.ExecuteReader();
                Usuarios usuarios = default(Usuarios);
                while (lectorDatos.Read())
                {
                    usuarios = new Usuarios();
                    usuarios.id = Convert.ToInt32(lectorDatos["id"].ToString());
                    usuarios.nombre = lectorDatos["nombre"].ToString();
                    usuarios.contrasena = lectorDatos["contrasena"].ToString();
                    usuarios.nivel = Convert.ToInt32(lectorDatos["nivel"].ToString());
                    usuarios.accesoTotal = Convert.ToBoolean(lectorDatos["accesoTotal"].ToString()); 
                    lista.Add(usuarios);
                }
                BaseDatos.conexionCatalogo.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                BaseDatos.conexionCatalogo.Close();
            }

        }

    }
}
