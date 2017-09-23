Imports System.Data.SqlClient

Public Class Clientes

    Private id As Integer
    Private nombre As String
    Private rfc As String
    Private domicilio As String
    Private municipio As String
    Private estado As String
    Private telefono As String
    Private correo As String

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
    Public Property ERfc() As String
        Get
            Return rfc
        End Get
        Set(value As String)
            rfc = value
        End Set
    End Property
    Public Property EDomicilio() As String
        Get
            Return domicilio
        End Get
        Set(value As String)
            domicilio = value
        End Set
    End Property
    Public Property EMunicipio() As String
        Get
            Return municipio
        End Get
        Set(value As String)
            municipio = value
        End Set
    End Property
    Public Property EEstado() As String
        Get
            Return estado
        End Get
        Set(value As String)
            estado = value
        End Set
    End Property
    Public Property ETelefono() As String
        Get
            Return telefono
        End Get
        Set(value As String)
            telefono = value
        End Set
    End Property
    Public Property ECorreo() As String
        Get
            Return correo
        End Get
        Set(value As String)
            correo = value
        End Set
    End Property

    Public Function ObtenerListadoReporte() As DataTable

        Try
            Dim datos As New DataTable
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionCatalogo
            comando.CommandText = String.Format("SELECT Id, Nombre, (CAST(Id AS Varchar)+' - '+Nombre) AS IdNombre FROM {0}Clientes " & _
            " UNION SELECT -1 AS Id, NULL AS Nombre, NULL AS IdNombre FROM {0}Clientes " & _
            " ORDER BY Id ASC", ALMLogicaSalidas.Programas.prefijoBaseDatosAlmacen)
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

    Public Function ObtenerListadoReporteCatalogo() As DataTable

        Try
            Dim datos As New DataTable
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionCatalogo
            comando.CommandText = String.Format("SELECT Id, Nombre FROM {0}Clientes ORDER BY Id ASC", ALMLogicaSalidas.Programas.prefijoBaseDatosAlmacen)
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
