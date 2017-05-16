using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logica
{
    public class Usuarios
    {
         
        public static int id;
        public static string nombre;
        public static string contrasena;
        public static int nivel;
        public static bool accesoTotal;

        public static void ObtenerParametros()
        {

            string[] parametros = Environment.GetCommandLineArgs().ToArray(); 
            if (parametros.Length > 1)
            {
                int numeracion = 10;
                id = Funciones.ValidarNumero(parametros[numeracion].Replace("|", " ")); numeracion += 1; 
            }

        }

    }
}
