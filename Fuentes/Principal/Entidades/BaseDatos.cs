using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlServerCe;

namespace Entidades
{
   public class BaseDatos
    {
        
        private string cadenaConexionPrincipal;
        private string cadenaConexionConfiguracion;
        private string cadenaConexionCatalogo;
        private string cadenaConexionAlmacen;
        public static SqlCeConnection conexionPrincipal = new SqlCeConnection();
        public static SqlConnection conexionConfiguracion = new SqlConnection();
        public static SqlConnection conexionCatalogo = new SqlConnection();
        public static SqlConnection conexionAlmacen = new SqlConnection(); 

        public string CadenaConexionPrincipal
        {
            get { return cadenaConexionPrincipal; }
            set { cadenaConexionPrincipal = value; }
        } 
        public string CadenaConexionConfiguracion
        {
            get { return cadenaConexionConfiguracion; }
            set { cadenaConexionConfiguracion = value; }
        }
        public string CadenaConexionCatalogo
        {
            get { return cadenaConexionCatalogo; }
            set { cadenaConexionCatalogo = value; }
        }
        public string CadenaConexionAlmacen
        {
            get { return cadenaConexionAlmacen; }
            set { cadenaConexionAlmacen = value; }
        } 
            
        public void ConfigurarConexionPrincipal()
        { 
             
            this.cadenaConexionPrincipal = string.Format("Data Source={0};Password={1}", this.cadenaConexionPrincipal, "@berry2017"); // Contraseña fija.
            conexionPrincipal.ConnectionString = this.cadenaConexionPrincipal; 

        }

        public void ConfigurarConexionConfiguracion()
        {
            
            this.cadenaConexionConfiguracion = string.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3}", Logica.Directorios.instanciaSql, this.cadenaConexionConfiguracion, Logica.Directorios.usuarioSql, Logica.Directorios.contrasenaSql);
            conexionConfiguracion.ConnectionString = this.cadenaConexionConfiguracion; 

        }

        public void ConfigurarConexionCatalogo()
        {

            this.cadenaConexionCatalogo = string.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3}", Logica.Directorios.instanciaSql, this.cadenaConexionCatalogo, Logica.Directorios.usuarioSql, Logica.Directorios.contrasenaSql);
            conexionCatalogo.ConnectionString = this.cadenaConexionCatalogo; 

        }

        public void ConfigurarConexionAlmacen()
        {

            this.cadenaConexionAlmacen = string.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3}", Logica.Directorios.instanciaSql, this.cadenaConexionAlmacen, Logica.Directorios.usuarioSql, Logica.Directorios.contrasenaSql);
            conexionAlmacen.ConnectionString = this.cadenaConexionAlmacen;

        }

        public string ProbarConexionCE(SqlCeConnection conexion)
        {

            try
            {
                SqlCeCommand comando = new SqlCeCommand();
                comando.Connection = conexion;
                conexion.Open();
                conexion.Close();
                return string.Empty;
            }
            catch (SqlCeException ex)
            {
                return ex.Message.ToString();
            }

        }

        public string ProbarConexion(SqlConnection conexion)
        {

            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = conexion;
                conexion.Open();
                conexion.Close();
                return string.Empty;
            }
            catch (SqlException ex)
            {
                return ex.Message.ToString();
            }

        }

    }
    
}
