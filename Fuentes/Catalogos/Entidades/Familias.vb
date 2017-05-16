﻿Imports System.Data.SqlClient

Public Class Familias

    Private idAlmacen As Integer
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
            comando.CommandText = "INSERT INTO " & LogicaCatalogos.Programas.prefijo & "Familias (IdAlmacen, Id, Nombre) VALUES (@idAlmacen, @id, @nombre)"
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
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
            If (Me.EIdAlmacen > 0) Then
                condicion &= " AND IdAlmacen=@idAlmacen"
            End If
            If (Me.EId > 0) Then
                condicion &= " AND Id=@id"
            End If
            comando.CommandText = "DELETE FROM " & LogicaCatalogos.Programas.prefijo & "Familias WHERE 0=0 " & condicion
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
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
            Dim condicion As String = String.Empty
            If Me.EIdAlmacen > 0 Then
                condicion &= " AND IdAlmacen=@idAlmacen"
            End If
            If Me.EId > 0 Then
                condicion &= " AND Id=@id"
            End If
            comando.CommandText = "SELECT Id, Nombre FROM " & LogicaCatalogos.Programas.prefijo & "Familias WHERE 0=0 " & condicion & " ORDER BY IdAlmacen, Id"
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            comando.Parameters.AddWithValue("@id", Me.EId)
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
