Imports System.Data.SqlClient

Public Class Empresas

    Private id As Integer
    Private nombre As String
    Private descripcion As String
    Private domicilio As String
    Private municipio As String
    Private estado As String
    Private pais As String
    Private rfc As String
    Private representanteLegal As String
    Private telefono As String
    Private logo As String

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
    Public Property EDescripcion() As String
        Get
            Return descripcion
        End Get
        Set(value As String)
            descripcion = value
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
    Public Property EPais() As String
        Get
            Return pais
        End Get
        Set(value As String)
            pais = value
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
    Public Property ERepresentanteLegal() As String
        Get
            Return representanteLegal
        End Get
        Set(value As String)
            representanteLegal = value
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
    Public Property ELogo() As String
        Get
            Return logo
        End Get
        Set(value As String)
            logo = value
        End Set
    End Property

    Public Function ObtenerListado(ByVal primerElemento As Boolean) As DataTable

        Try
            Dim datos As New DataTable
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionCatalogo
            Dim consulta As String = String.Empty : Dim condicion As String = String.Empty
            If (primerElemento) Then
                consulta = "SELECT TOP 1 * FROM Empresas ORDER BY Id ASC"
            Else
                If (Me.EId > 0) Then
                    condicion &= "WHERE Id = @id"
                End If
                consulta = String.Format("SELECT * FROM Empresas {0}", condicion)
            End If
            comando.CommandText = consulta
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

End Class
