using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logica
{

    public class Directorios
    {

        public static int id;
        public static string nombre;
        public static string descripcion;
        public static string rutaLogo;
        public static bool esPredeterminado;
        public static string instanciaSql; 
        public static string usuarioSql;
        public static string contrasenaSql;  

        public static void ObtenerParametros()
        {

            string[] parametros = Environment.GetCommandLineArgs().ToArray();
            //for (int i = 0; i < parametros.Length; i++)
            //{
            //    //MessageBox.Show("Parámetro " + parametros[i]);
            //} 
	        if (parametros.Length > 1) 
            {
		        int numeracion = 1;
                id = Funciones.ValidarNumero(parametros[numeracion].Replace("|", " ")); numeracion += 1;
                nombre = parametros[numeracion].Replace("|", " "); numeracion += 1;
                descripcion = Funciones.ValidarLetra(parametros[numeracion].Replace("|", " ")); numeracion += 1;
                rutaLogo = Funciones.ValidarLetra(parametros[numeracion].Replace("|", " ")); numeracion += 1;
		        esPredeterminado = Convert.ToBoolean(Funciones.ValidarNumero(parametros[numeracion].Replace("|", " "))); numeracion += 1;
		        instanciaSql = Funciones.ValidarLetra(parametros[numeracion].Replace("|", " ")); numeracion += 1; 
		        usuarioSql = Funciones.ValidarLetra(parametros[numeracion].Replace("|", " ")); numeracion += 1;
		        contrasenaSql = Funciones.ValidarLetra(parametros[numeracion].Replace("|", " ")); numeracion += 1;
	        }

        }

    }

}
