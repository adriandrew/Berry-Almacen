Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Text
Imports System.ComponentModel

Public Class Usuarios
     
    Private id As Integer
    Private nombre As String
    Private contrasena As String
    Private nivel As Integer
    Private accesoTotal As Boolean 
     
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
    Public Property EContrasena() As String
        Get
            Return contrasena
        End Get
        Set(value As String)
            contrasena = value
        End Set
    End Property
    Public Property ENivel() As Integer
        Get
            Return nivel
        End Get
        Set(value As Integer)
            nivel = value
        End Set
    End Property
    Public Property EAccesoTotal() As Boolean
        Get
            Return accesoTotal
        End Get
        Set(value As Boolean)
            accesoTotal = value
        End Set
    End Property 

    Public Sub Guardar()

        Try
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionCatalogo
            comando.CommandText = "INSERT INTO Usuarios VALUES (@id, @nombre, @contrasena, @nivel, @accesoTotal)"
            comando.Parameters.AddWithValue("@id", Me.Id)
            comando.Parameters.AddWithValue("@nombre", Me.Nombre)
            comando.Parameters.AddWithValue("@contrasena", Me.Contrasena)
            comando.Parameters.AddWithValue("@nivel", Me.Nivel)
            comando.Parameters.AddWithValue("@accesoTotal", Me.AccesoTotal) 
            BaseDatos.conexionCatalogo.Open()
            comando.ExecuteNonQuery()
            BaseDatos.conexionCatalogo.Close()
        Catch ex As Exception
            Throw ex
        Finally
            BaseDatos.conexionCatalogo.Close()
        End Try

    End Sub

    Public Sub Editar()

        Try
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionCatalogo
            comando.CommandText = "UPDATE Usuarios SET Nombre=@nombre, Contrasena=@contrasena, Nivel=@nivel, AccesoTotal=@accesoTotal WHERE Id=@id" 
            comando.Parameters.AddWithValue("@id", Me.Id)
            comando.Parameters.AddWithValue("@nombre", Me.Nombre)
            comando.Parameters.AddWithValue("@contrasena", Me.Contrasena)
            comando.Parameters.AddWithValue("@nivel", Me.Nivel)
            comando.Parameters.AddWithValue("@accesoTotal", Me.accesoTotal)
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
            comando.CommandText = "DELETE FROM Usuarios " & condicion
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

    Public Function ValidarPorId() As Boolean

        Try
            Dim resultado As Boolean = False
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionCatalogo
            comando.CommandText = "SELECT * FROM Usuarios WHERE Id=@id"
            comando.Parameters.AddWithValue("@id", Me.Id)
            BaseDatos.conexionCatalogo.Open()
            Dim lectorDatos As SqlDataReader = comando.ExecuteReader()
            If (lectorDatos.HasRows) Then
                resultado = True
            Else
                resultado = False
            End If
            BaseDatos.conexionCatalogo.Close()
            Return resultado
        Catch ex As Exception
            Throw ex
        Finally
            BaseDatos.conexionCatalogo.Close()
        End Try

    End Function

    Public Function ObtenerListado() As List(Of Usuarios)

        Try
            Dim lista As New List(Of Usuarios)
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionCatalogo
            Dim condicion As String = String.Empty
            If (Me.EId > 0) Then
                condicion &= " WHERE Id=@id"
            End If
            comando.CommandText = "SELECT * FROM Usuarios " & condicion
            comando.Parameters.AddWithValue("@id", Me.id)
            BaseDatos.conexionCatalogo.Open()
            Dim lectorDatos As SqlDataReader = comando.ExecuteReader()
            Dim usuarios As Usuarios
            While lectorDatos.Read()
                usuarios = New Usuarios()
                usuarios.id = Convert.ToInt32(lectorDatos("Id").ToString())
                usuarios.nombre = lectorDatos("Nombre").ToString()
                usuarios.contrasena = lectorDatos("Contrasena").ToString()
                usuarios.nivel = Convert.ToInt32(lectorDatos("Nivel").ToString())
                usuarios.accesoTotal = Convert.ToBoolean(lectorDatos("AccesoTotal").ToString())
                lista.Add(usuarios)
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
            comando.CommandText = "SELECT Id, Nombre FROM Usuarios ORDER BY Id"
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

    Public Function ValidarActividadPorId() As Boolean

        Try
            Dim resultado As Boolean = False
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            comando.CommandText = "SELECT * FROM Actividades WHERE IdUsuario=@id OR IdUsuarioDestino=@id"
            comando.Parameters.AddWithValue("@id", Me.Id)
            BaseDatos.conexionAlmacen.Open()
            Dim lectorDatos As SqlDataReader = comando.ExecuteReader()
            If lectorDatos.HasRows Then
                resultado = True
            Else
                resultado = False
            End If
            BaseDatos.conexionAlmacen.Close()
            Return resultado
        Catch ex As Exception
            Throw ex
        Finally
            BaseDatos.conexionAlmacen.Close()
        End Try

    End Function

End Class
