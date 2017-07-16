Imports System.Data.SqlClient

Public Class Almacenes

    Private id As Integer
    Private nombre As String
    Private abreviatura As String

    Public Property EId() As Integer
        Get
            Return id
        End Get
        Set(value As Integer)
            id = value
        End Set
    End Property
    Public Property ENombre() As String
        Get
            Return nombre
        End Get
        Set(value As String)
            nombre = value
        End Set
    End Property
    Public Property EAbreviatura() As String
        Get
            Return abreviatura
        End Get
        Set(value As String)
            abreviatura = value
        End Set
    End Property
     
    Public Function ObtenerListadoReporte() As DataTable

        Try
            Dim datos As New DataTable
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionCatalogo
            comando.CommandText = "SELECT Id, Nombre FROM " & ALMLogicaSalidas.Programas.prefijoBaseDatosAlmacen & "Almacenes " & _
            " UNION SELECT -1 AS Id, NULL AS Nombre FROM " & ALMLogicaSalidas.Programas.prefijoBaseDatosAlmacen & "Almacenes " & _
            " ORDER BY Id ASC"
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

    Public Function ObtenerListado() As List(Of Almacenes)

        Try
            Dim lista As New List(Of Almacenes)
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionCatalogo
            Dim condicion As String = String.Empty
            If (Me.EId > 0) Then
                condicion &= " WHERE Id=@id"
            End If
            comando.CommandText = "SELECT * FROM " & ALMLogicaSalidas.Programas.prefijoBaseDatosAlmacen & "Almacenes " & condicion
            comando.Parameters.AddWithValue("@id", Me.id)
            BaseDatos.conexionCatalogo.Open()
            Dim lectorDatos As SqlDataReader = comando.ExecuteReader()
            Dim almacenes As Almacenes
            While lectorDatos.Read()
                almacenes = New Almacenes()
                almacenes.id = Convert.ToInt32(lectorDatos("Id").ToString())
                almacenes.nombre = lectorDatos("Nombre").ToString()
                almacenes.abreviatura = lectorDatos("Abreviatura").ToString()
                lista.Add(almacenes)
            End While
            BaseDatos.conexionCatalogo.Close()
            Return lista
        Catch ex As Exception
            Throw ex
        Finally
            BaseDatos.conexionCatalogo.Close()
        End Try

    End Function

End Class
