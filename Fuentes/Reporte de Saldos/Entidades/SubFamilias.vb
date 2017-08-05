Imports System.Data.SqlClient

Public Class SubFamilias

    Private idAlmacen As Integer
    Private idFamilia As Integer
    Private id As Integer
    Private nombre As String

    Public Property EIdAlmacen() As Integer
        Get
            Return idAlmacen
        End Get
        Set(value As Integer)
            idAlmacen = value
        End Set
    End Property
    Public Property EIdFamilia() As Integer
        Get
            Return idFamilia
        End Get
        Set(value As Integer)
            idFamilia = value
        End Set
    End Property
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
     
    Public Function ObtenerListadoReporte() As DataTable

        Try
            Dim datos As New DataTable
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionCatalogo
            Dim condicion As String = String.Empty
            If Me.EIdAlmacen > 0 Then
                condicion &= " AND IdAlmacen=@idAlmacen"
            End If
            If Me.EIdFamilia > 0 Then
                condicion &= " AND IdFamilia=@idFamilia"
            End If
            If Me.EId > 0 Then
                condicion &= " AND Id=@id"
            End If
            comando.CommandText = "SELECT -1 AS Id, 'Todos' AS Nombre FROM " & ALMLogicaReporteSaldos.Programas.prefijoBaseDatosAlmacen & "SubFamilias " & _
            " UNION SELECT Id, Nombre FROM " & ALMLogicaReporteSaldos.Programas.prefijoBaseDatosAlmacen & "SubFamilias " & _
            " WHERE 0=0 " & condicion
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            comando.Parameters.AddWithValue("@idFamilia", Me.EIdFamilia)
            comando.Parameters.AddWithValue("@id", Me.EId)
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

    Public Function ObtenerListado() As List(Of SubFamilias)

        Try
            Dim lista As New List(Of SubFamilias)
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionCatalogo
            Dim condicion As String = String.Empty
            If (Me.EIdAlmacen > 0) Then
                condicion &= " AND IdAlmacen=@idAlmacen"
            End If
            If (Me.EIdFamilia > 0) Then
                condicion &= " AND IdFamilia=@idFamilia"
            End If
            If (Me.EId > 0) Then
                condicion &= " AND Id=@id"
            End If
            comando.CommandText = "SELECT IdAlmacen, IdFamilia, Id, Nombre FROM " & ALMLogicaReporteSaldos.Programas.prefijoBaseDatosAlmacen & "SubFamilias WHERE 0=0 " & condicion
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            comando.Parameters.AddWithValue("@idFamilia", Me.EIdFamilia)
            comando.Parameters.AddWithValue("@id", Me.EId)
            BaseDatos.conexionCatalogo.Open()
            Dim lectorDatos As SqlDataReader = comando.ExecuteReader()
            Dim tabla As SubFamilias
            While lectorDatos.Read()
                tabla = New SubFamilias()
                tabla.idAlmacen = Convert.ToInt32(lectorDatos("IdAlmacen").ToString())
                tabla.idFamilia = Convert.ToInt32(lectorDatos("idFamilia").ToString())
                tabla.id = Convert.ToInt32(lectorDatos("Id").ToString())
                tabla.nombre = lectorDatos("Nombre").ToString()
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
