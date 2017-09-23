Imports System.Data.SqlClient

Public Class Proveedores

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
    Public Property Erfc() As String
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

    Public Sub Guardar()

        Try
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionCatalogo
            comando.CommandText = String.Format("INSERT INTO {0}Proveedores (Id, Nombre, Rfc, Domicilio, Municipio, Estado, Telefono, Correo) VALUES (@id, @nombre, @rfc, @domicilio, @municipio, @estado,@telefono, @correo)", ALMLogicaCatalogos.Programas.prefijoBaseDatosAlmacen)
            comando.Parameters.AddWithValue("@id", Me.EId)
            comando.Parameters.AddWithValue("@nombre", Me.ENombre)
            comando.Parameters.AddWithValue("@rfc", Me.Erfc)
            comando.Parameters.AddWithValue("@domicilio", Me.EDomicilio)
            comando.Parameters.AddWithValue("@municipio", Me.EMunicipio)
            comando.Parameters.AddWithValue("@estado", Me.EEstado)
            comando.Parameters.AddWithValue("@telefono", Me.ETelefono)
            comando.Parameters.AddWithValue("@correo", Me.ECorreo)
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
            comando.CommandText = String.Format("DELETE FROM {0}Proveedores ", ALMLogicaCatalogos.Programas.prefijoBaseDatosAlmacen, condicion)
            comando.Parameters.AddWithValue("@id", Me.EId)
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
            comando.CommandText = String.Format("SELECT Id, Nombre, Rfc, Domicilio, Municipio, Estado, Telefono, Correo FROM {0}Proveedores ORDER BY Id ASC", ALMLogicaCatalogos.Programas.prefijoBaseDatosAlmacen)
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

End Class
