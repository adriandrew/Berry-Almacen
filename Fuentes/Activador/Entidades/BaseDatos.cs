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
        public static SqlCeConnection conexionPrincipal = new SqlCeConnection(); 

        public string CadenaConexionPrincipal
        {
            get { return cadenaConexionPrincipal; }
            set { cadenaConexionPrincipal = value; }
        }  
            
        public void AbrirConexionPrincipal()
        { 

            //this.cadenaConexionPrincipal = string.Format("Data Source=.\\SQLEXPRESS;AttachDbFilename={0};Integrated Security=True;Connect Timeout=1", this.cadenaConexionPrincipal); Es para los que sean mdf, que se necesiten adjuntar.
            this.cadenaConexionPrincipal = string.Format("Data Source={0};Password={1}", this.cadenaConexionPrincipal, "@berry2017"); 
            conexionPrincipal.ConnectionString = this.cadenaConexionPrincipal; 

        }

    }
    
}
