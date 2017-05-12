using System;
using System.Collections.Generic;
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

    }
}
