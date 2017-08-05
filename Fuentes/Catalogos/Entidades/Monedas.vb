Imports System.Data.SqlClient

Public Class Monedas

    Private id As Integer
    Private nombre As String

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

    Public Sub Guardar()

        Try
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionCatalogo
            comando.CommandText = "INSERT INTO " & ALMLogicaCatalogos.Programas.prefijoBaseDatosAlmacen & "Monedas (Id, Nombre) VALUES (@id, @nombre)"
            comando.Parameters.AddWithValue("@id", Me.EId)
            comando.Parameters.AddWithValue("@nombre", Me.ENombre)
            BaseDatos.conexionCatalogo.Open()
            comando.ExecuteNonQuery()
            BaseDatos.conexionCatalogo.Close()
        Catch ex As Exception
            Throw ex
        Finally
            BaseDatos.conexionCatalogo.Close()
        End Try

    End Sub

    Public Sub Eliminar()

        Try
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionCatalogo
            Dim condicion As String = String.Empty
            If (Me.EId > 0) Then
                condicion &= " WHERE Id=@id"
            End If
            comando.CommandText = "DELETE FROM " & ALMLogicaCatalogos.Programas.prefijoBaseDatosAlmacen & "Monedas " & condicion
            comando.Parameters.AddWithValue("@id", Me.id)
            BaseDatos.conexionCatalogo.Open()
            comando.ExecuteNonQuery()
            BaseDatos.conexionCatalogo.Close()
        Catch ex As Exception
            Throw ex
        Finally
            BaseDatos.conexionCatalogo.Close()
        End Try

    End Sub

    Public Function ObtenerListadoReporte() As DataTable

        Try
            Dim datos As New DataTable
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionCatalogo
            comando.CommandText = "SELECT Id, Nombre FROM " & ALMLogicaCatalogos.Programas.prefijoBaseDatosAlmacen & "Monedas ORDER BY Id ASC"
            BaseDatos.conexionCatalogo.Open()
            Dim dataReader As SqlDataReader
            dataReader = comando.ExecuteReader()
            datos.Load(dataReader)
            BaseDatos.conexionCatalogo.Close()
            Return datos
        Catch ex As Exception
            Throw ex
        Finally
            BaseDatos.conexionCatalogo.Close()
        End Try

    End Function

    Public Function ObtenerListado() As List(Of Monedas)

        Try
            Dim lista As New List(Of Monedas)
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionCatalogo
            Dim condicion As String = String.Empty
            If (Me.EId > 0) Then
                condicion &= " AND Id=@id"
            End If
            comando.CommandText = "SELECT Id, Nombre FROM " & ALMLogicaCatalogos.Programas.prefijoBaseDatosAlmacen & "Monedas WHERE 0=0 " & condicion
            comando.Parameters.AddWithValue("@id", Me.id)
            BaseDatos.conexionCatalogo.Open()
            Dim dataReader As SqlDataReader = comando.ExecuteReader()
            Dim tabla As Monedas
            While dataReader.Read()
                tabla = New Monedas()
                tabla.id = Convert.ToInt32(dataReader("Id").ToString())
                tabla.nombre = dataReader("Nombre").ToString()
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

End Class
