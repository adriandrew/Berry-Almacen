using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 

namespace Logica
{
    public static class Funciones
    {

        public static int ValidarNumero(string valor)
        {

            int resultado = 0;
            if (int.TryParse(valor, out resultado))
            {
                return resultado;
            }
            else
            {
                return 0;
            }
                    
        }

        public static string ValidarLetra(object valor)
        { 
        
            if (valor == null)
            {
                return string.Empty;
            }
            else
            {
                return valor.ToString();
            }

        }
        
    }
}
