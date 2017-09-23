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
     
    Public Function ObtenerListado() As List(Of Usuarios)

        Try
            Dim lista As New List(Of Usuarios)
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionCatalogo
            Dim condicion As String = String.Empty
            If (Me.EId > 0) Then
                condicion &= " WHERE Id=@id"
            End If
            comando.CommandText = String.Format("SELECT * FROM Usuarios {0}", condicion)
            comando.Parameters.AddWithValue("@id", Me.EId)
            BaseDatos.conexionCatalogo.Open()
            Dim dataReader As SqlDataReader = comando.ExecuteReader()
            Dim tabla As Usuarios
            While dataReader.Read()
                tabla = New Usuarios()
                tabla.id = Convert.ToInt32(dataReader("Id").ToString())
                tabla.nombre = dataReader("Nombre").ToString()
                tabla.contrasena = dataReader("Contrasena").ToString()
                tabla.nivel = Convert.ToInt32(dataReader("Nivel").ToString())
                tabla.accesoTotal = Convert.ToBoolean(dataReader("AccesoTotal").ToString())
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
