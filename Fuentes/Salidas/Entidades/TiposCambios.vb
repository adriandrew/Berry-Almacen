Imports System.Data.SqlClient

Public Class TiposCambios

    Private idMoneda As Integer
    Private fecha As Date
    Private valor As Double

    Public Property EIdMoneda() As Integer
        Get
            Return idMoneda
        End Get
        Set(value As Integer)
            idMoneda = value
        End Set
    End Property
    Public Property EFecha() As Date
        Get
            Return fecha
        End Get
        Set(value As Date)
            fecha = value
        End Set
    End Property
    Public Property EValor() As Double
        Get
            Return valor
        End Get
        Set(value As Double)
            valor = value
        End Set
    End Property

    Public Function ObtenerListado() As List(Of TiposCambios)

        Try
            Dim lista As New List(Of TiposCambios)
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionCatalogo
            Dim condicion As String = String.Empty
            If (Me.EIdMoneda > 0) Then
                condicion &= " AND IdMoneda=@idMoneda AND Fecha=@fecha"
            End If 
            comando.CommandText = "SELECT IdMoneda, Fecha, Valor FROM " & ALMLogicaSalidas.Programas.prefijoBaseDatosAlmacen & "TiposCambios WHERE 0=0 " & condicion & " ORDER BY Fecha, IdMoneda DESC"
            comando.Parameters.AddWithValue("@idMoneda", Me.idMoneda)
            comando.Parameters.AddWithValue("@fecha", ALMLogicaSalidas.Funciones.ValidarFechaAEstandar(Me.fecha))
            BaseDatos.conexionCatalogo.Open()
            Dim lectorDatos As SqlDataReader = comando.ExecuteReader()
            Dim tabla As TiposCambios
            While lectorDatos.Read()
                tabla = New TiposCambios()
                tabla.idMoneda = Convert.ToInt32(lectorDatos("IdMoneda").ToString())
                tabla.fecha = Convert.ToDateTime(lectorDatos("Fecha").ToString())
                tabla.valor = Convert.ToDouble(lectorDatos("Valor").ToString())
                lista.Add(tabla)
            End While
            BaseDatos.conexionCatalogo.Close()
            Return lista
        Catch ex As Exception
            Throw ex
        Finally
            BaseDatos.conexionCatalogo.Close()
        End Try

    End Function

    Public Function ObtenerListadoReporte() As DataTable

        Try
            Dim datos As New DataTable
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionCatalogo
            comando.CommandText = "SELECT IdMoneda, Fecha, Valor FROM " & ALMLogicaSalidas.Programas.prefijoBaseDatosAlmacen & "TiposCambios ORDER BY Fecha, IdMoneda DESC"
            BaseDatos.conexionCatalogo.Open()
            Dim lectorDatos As SqlDataReader
            lectorDatos = comando.ExecuteReader()
            datos.Load(lectorDatos)
            BaseDatos.conexionCatalogo.Close()
            Return datos
        Catch ex As Exception
            Throw ex
        Finally
            BaseDatos.conexionCatalogo.Close()
        End Try

    End Function

End Class
