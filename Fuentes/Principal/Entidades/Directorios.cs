using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class Directorios
    {

        private int id;
        private string nombre;
        private string descripcion; 
        private string rutaLogo; 
        private bool esPredeterminado;
        private string instanciaSql; 
        private string usuarioSql; 
        private string contrasenaSql;

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        } 
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        public string RutaLogo
        {
            get { return rutaLogo; }
            set { rutaLogo = value; }
        }  
        public bool EsPredeterminado
        {
            get { return this.esPredeterminado; }
            set { this.esPredeterminado = value; }
        }
        public string InstanciaSql
        {
            get { return this.instanciaSql; }
            set { this.instanciaSql = value; }
        } 
        public string UsuarioSql
        {
            get { return usuarioSql; }
            set { usuarioSql = value; }
        }
        public string ContrasenaSql
        {
            get { return contrasenaSql; }
            set { contrasenaSql = value; }
        }

        public List<Directorios> ObtenerPredeterminado()
        {

            List<Directorios> lista = new List<Directorios>();
            bool conexionCorrecta = false;
            while (!conexionCorrecta)
            { 
                try
                {
                    SqlCeCommand comando = new SqlCeCommand();
                    comando.Connection = BaseDatos.conexionPrincipal;
                    comando.CommandText = "SELECT * FROM Directorios WHERE EsPredeterminado='TRUE'";
                    BaseDatos.conexionPrincipal.Open(); 
                    SqlCeDataReader lectorDatos = default(SqlCeDataReader);
                    lectorDatos = comando.ExecuteReader();
                    Directorios directorios;
                    while ((lectorDatos.Read()))
                    {
                        directorios = new Directorios();
                        directorios.Id = Convert.ToInt32(lectorDatos["Id"].ToString());
                        directorios.Nombre = lectorDatos["Nombre"].ToString();
                        directorios.Descripcion = lectorDatos["Descripcion"].ToString();
                        directorios.RutaLogo = lectorDatos["RutaLogo"].ToString(); 
                        directorios.EsPredeterminado = Convert.ToBoolean(lectorDatos["EsPredeterminado"].ToString());
                        directorios.InstanciaSql = lectorDatos["InstanciaSql"].ToString(); 
                        directorios.UsuarioSql = lectorDatos["UsuarioSql"].ToString();
                        directorios.ContrasenaSql = lectorDatos["ContrasenaSql"].ToString();
                        lista.Add(directorios);
                    }
                    BaseDatos.conexionPrincipal.Close();
                    conexionCorrecta = true;
                    //return lista;
                }
                catch (SqlCeException ex)
                {
                    if (ex.NativeError == 25035)
                    {
                        conexionCorrecta = false;
                    }
                    else
                    {
                        conexionCorrecta = true;
                        throw ex;
                    }
                }
                catch(Exception ex)
                {
                    conexionCorrecta = true;
                    throw ex; 
                }
                finally
                {
                    BaseDatos.conexionPrincipal.Close();
                }
            }
            return lista;

        }

        public List<Directorios> ObtenerListadoPorId()
        {

            List<Directorios> lista = new List<Directorios>();
            bool conexionCorrecta = false;
            while (!conexionCorrecta)
            {
                try
                {
                    SqlCeCommand comando = new SqlCeCommand();
                    comando.Connection = BaseDatos.conexionPrincipal;
                    comando.CommandText = "SELECT * FROM Directorios WHERE Id=@id";
                    comando.Parameters.AddWithValue("@id", this.Id);
                    BaseDatos.conexionPrincipal.Open();
                    SqlCeDataReader lectorDatos = default(SqlCeDataReader);
                    lectorDatos = comando.ExecuteReader();
                    Directorios directorios;
                    while ((lectorDatos.Read()))
                    {
                        directorios = new Directorios();
                        directorios.Id = Convert.ToInt32(lectorDatos["Id"].ToString());
                        directorios.Nombre = lectorDatos["Nombre"].ToString();
                        directorios.Descripcion = lectorDatos["Descripcion"].ToString();
                        directorios.RutaLogo = lectorDatos["RutaLogo"].ToString(); 
                        directorios.EsPredeterminado = Convert.ToBoolean(lectorDatos["EsPredeterminado"].ToString());
                        directorios.InstanciaSql = lectorDatos["InstanciaSql"].ToString();
                        directorios.UsuarioSql = lectorDatos["UsuarioSql"].ToString();
                        directorios.ContrasenaSql = lectorDatos["ContrasenaSql"].ToString();
                        lista.Add(directorios);
                    }
                    BaseDatos.conexionPrincipal.Close();
                    conexionCorrecta = true;
                    //return lista;
                }
                catch (SqlCeException ex)
                {
                    if (ex.NativeError == 25035)
                    {
                        conexionCorrecta = false;
                    }
                    else
                    {
                        conexionCorrecta = true;
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    conexionCorrecta = true;
                    throw ex;
                }
                finally
                {
                    BaseDatos.conexionPrincipal.Close();
                }
            }
            return lista;

        }

        public DataTable ObtenerListadoBasicoReporte()
        {
            
            DataTable datos = new DataTable();
            bool conexionCorrecta = false;
            while (!conexionCorrecta)
            {
                try
                {
                    SqlCeCommand comando = new SqlCeCommand();
                    comando.Connection = BaseDatos.conexionPrincipal;
                    comando.CommandText = "SELECT Id, Nombre, Descripcion, EsPredeterminado FROM Directorios";
                    BaseDatos.conexionPrincipal.Open();
                    SqlCeDataReader lectorDatos = default(SqlCeDataReader);
                    lectorDatos = comando.ExecuteReader();
                    datos.Load(lectorDatos);
                    BaseDatos.conexionPrincipal.Close();
                    conexionCorrecta = true;
                    //return lista;
                }
                catch (SqlCeException ex)
                {
                    if (ex.NativeError == 25035)
                    {
                        conexionCorrecta = false;
                    }
                    else
                    {
                        conexionCorrecta = true;
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    conexionCorrecta = true;
                    throw ex;
                }
                finally
                {
                    BaseDatos.conexionPrincipal.Close();
                }
            }
            return datos;

        }

        public DataTable ObtenerListadoCompletoReporte()
        {

            DataTable datos = new DataTable();
            bool conexionCorrecta = false;
            while (!conexionCorrecta)
            {
                try
                {
                    SqlCeCommand comando = new SqlCeCommand();
                    comando.Connection = BaseDatos.conexionPrincipal;
                    comando.CommandText = "SELECT Id, Nombre, Descripcion, RutaLogo, EsPredeterminado, InstanciaSql, UsuarioSql, ContrasenaSql FROM Directorios";
                    BaseDatos.conexionPrincipal.Open();
                    SqlCeDataReader lectorDatos = default(SqlCeDataReader);
                    lectorDatos = comando.ExecuteReader();
                    datos.Load(lectorDatos);
                    BaseDatos.conexionPrincipal.Close();
                    conexionCorrecta = true;
                    //return lista;
                }
                catch (SqlCeException ex)
                {
                    if (ex.NativeError == 25035)
                    {
                        conexionCorrecta = false;
                    }
                    else
                    {
                        conexionCorrecta = true;
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    conexionCorrecta = true;
                    throw ex;
                }
                finally
                {
                    BaseDatos.conexionPrincipal.Close();
                }
            }
            return datos;

        }

        public void Predeterminar()
        {

            try
            {
                SqlCeCommand comando = new SqlCeCommand();
                comando.Connection = BaseDatos.conexionPrincipal;
                comando.CommandText = "UPDATE Directorios SET EsPredeterminado='TRUE' WHERE Id=@id";
                comando.Parameters.AddWithValue("@id", this.Id);
                BaseDatos.conexionPrincipal.Open();
                comando.ExecuteNonQuery();
                comando.CommandText = "UPDATE Directorios SET EsPredeterminado='FALSE' WHERE Id<>@id";
                comando.ExecuteNonQuery();
                BaseDatos.conexionPrincipal.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                BaseDatos.conexionPrincipal.Close();
            }

        }

        public void Guardar()
        {

            try
            {
                SqlCeCommand comando = new SqlCeCommand();
                comando.Connection = BaseDatos.conexionPrincipal;
                comando.CommandText = "INSERT INTO Directorios (Id, Nombre, Descripcion, RutaLogo, EsPredeterminado, InstanciaSql, UsuarioSql, ContrasenaSql) VALUES (@id, @nombre, @descripcion, @rutaLogo, @esPredeterminado, @instanciaSql, @usuarioSql, @contrasenaSql)";
                comando.Parameters.AddWithValue("@id", this.Id);
                comando.Parameters.AddWithValue("@nombre", this.Nombre);
                comando.Parameters.AddWithValue("@descripcion", this.Descripcion);
                comando.Parameters.AddWithValue("@rutaLogo", this.RutaLogo);
                comando.Parameters.AddWithValue("@esPredeterminado", this.EsPredeterminado);
                comando.Parameters.AddWithValue("@instanciaSql", this.InstanciaSql);
                comando.Parameters.AddWithValue("@usuarioSql", this.UsuarioSql);
                comando.Parameters.AddWithValue("@contrasenaSql", this.ContrasenaSql);
                BaseDatos.conexionPrincipal.Open();
                comando.ExecuteNonQuery();
                BaseDatos.conexionPrincipal.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                BaseDatos.conexionPrincipal.Close();
            }

        }

        public void Eliminar()
        {

            try
            {
                SqlCeCommand comando = new SqlCeCommand();
                comando.Connection = BaseDatos.conexionPrincipal;
                string condiciones = string.Empty;
                if (this.Id > 0)
                    condiciones += " WHERE Id=@id";
                comando.CommandText = "DELETE FROM Directorios";
                comando.Parameters.AddWithValue("@id", this.Id);
                BaseDatos.conexionPrincipal.Open();
                comando.ExecuteNonQuery();
                BaseDatos.conexionPrincipal.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                BaseDatos.conexionPrincipal.Close();
            }

        }

    }
}
