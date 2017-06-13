using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class Estilos
    {
         
        private int idUsuario;
        private string colorFondoMenu;
        private string colorLetraMenu;
        private string colorFondo; 

        public int IdUsuario
        {
            get { return idUsuario; }
            set { idUsuario = value; }
        }
        public string ColorFondoMenu
        {
            get { return colorFondoMenu; }
            set { colorFondoMenu = value; }
        }
        public string ColorLetraMenu
        {
            get { return colorLetraMenu; }
            set { colorLetraMenu = value; }
        }
        public string ColorFondo
        {
            get { return colorFondo; }
            set { colorFondo = value; }
        } 

        public List<Estilos> ObtenerListado()
        {

            try
            {
                List<Estilos> lista = new List<Estilos>();
                SqlCommand comando = new SqlCommand();
                comando.Connection = BaseDatos.conexionConfiguracion;
                string condicion = string.Empty;
                if (this.IdUsuario > 0)
                    condicion += " AND IdUsuario = @idUsuario";
                comando.CommandText = "SELECT * FROM Estilos WHERE 0=0 " + condicion;
                comando.Parameters.AddWithValue("@idUsuario", this.IdUsuario);
                BaseDatos.conexionConfiguracion.Open();
                SqlDataReader lectorDatos = comando.ExecuteReader();
                Estilos estilos;
                while (lectorDatos.Read())
                {
                    estilos = new Estilos();
                    estilos.IdUsuario = Convert.ToInt32(lectorDatos["idUsuario"].ToString());
                    estilos.ColorFondoMenu = lectorDatos["colorFondoMenu"].ToString();
                    estilos.ColorLetraMenu = lectorDatos["colorLetraMenu"].ToString();
                    estilos.ColorFondo = lectorDatos["colorFondo"].ToString(); 
                    lista.Add(estilos);
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
