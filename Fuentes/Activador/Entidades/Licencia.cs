using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class Licencia
    {

        private bool esPrueba;
        private DateTime fechaRegistro;
        private DateTime fechaVencimiento;
        
        public bool EsPrueba
        {
            get { return esPrueba; }
            set { esPrueba = value; }
        }
        public DateTime FechaRegistro
        {
            get { return fechaRegistro; }
            set { fechaRegistro = value; }
        }
        public DateTime FechaVencimiento
        {
            get { return fechaVencimiento; }
            set { fechaVencimiento = value; }
        }

        public List<Licencia> ObtenerListado()
        {

            List<Licencia> lista = new List<Licencia>();
            try
            {
                SqlCeCommand comando = new SqlCeCommand();
                comando.Connection = BaseDatos.conexionPrincipal;
                comando.CommandText = "SELECT * FROM Licencia"; 
                BaseDatos.conexionPrincipal.Open();
                SqlCeDataReader dataReader = default(SqlCeDataReader);
                dataReader = comando.ExecuteReader();
                Licencia licencia = new Licencia();
                while ((dataReader.Read()))
                {
                    licencia = new Licencia();
                    licencia.esPrueba = Convert.ToBoolean(dataReader["EsPrueba"].ToString());
                    licencia.fechaRegistro = Convert.ToDateTime(dataReader["FechaRegistro"].ToString());
                    licencia.fechaVencimiento = Convert.ToDateTime(dataReader["FechaVencimiento"].ToString());
                    lista.Add(licencia);
                }
                BaseDatos.conexionPrincipal.Close();
                return lista;
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
                comando.CommandText = "INSERT INTO Licencia (EsPrueba, FechaRegistro, FechaVencimiento) VALUES (@esPrueba, @fechaRegistro, @fechaVencimiento)";
                comando.Parameters.AddWithValue("@esPrueba", this.EsPrueba);
                comando.Parameters.AddWithValue("@fechaRegistro", this.FechaRegistro);
                comando.Parameters.AddWithValue("@fechaVencimiento", this.FechaVencimiento);
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

        public void Editar()
        {

            try
            {
                SqlCeCommand comando = new SqlCeCommand();
                comando.Connection = BaseDatos.conexionPrincipal;
                comando.CommandText = "UPDATE Licencia SET EsPrueba=@esPrueba, FechaRegistro=@fechaRegistro, FechaVencimiento=@fechaVencimiento";
                comando.Parameters.AddWithValue("@esPrueba", this.EsPrueba);
                comando.Parameters.AddWithValue("@fechaRegistro", this.FechaRegistro);
                comando.Parameters.AddWithValue("@fechaVencimiento", this.FechaVencimiento); 
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
                comando.CommandText = "DELETE FROM Licencia"; 
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
