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

    Public Sub Guardar()

        Try
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionCatalogo
            comando.CommandText = String.Format("INSERT INTO {0}TiposCambios (IdMoneda, Fecha, Valor) VALUES (@idMoneda, @fecha, @valor)", ALMLogicaCatalogos.Programas.prefijoBaseDatosAlmacen)
            comando.Parameters.AddWithValue("@idMoneda", Me.EIdMoneda)
            comando.Parameters.AddWithValue("@fecha", ALMLogicaCatalogos.Funciones.ValidarFechaAEstandar(Me.fecha))
            comando.Parameters.AddWithValue("@valor", Me.EValor)
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
            If (Me.EIdMoneda > 0) Then
                condicion &= " AND IdMoneda=@idMoneda"
            End If
            If (IsDate(Me.EFecha)) Then
                condicion &= " AND Fecha=@fecha"
            End If
            comando.CommandText = String.Format("DELETE FROM {0}TiposCambios WHERE 0=0 {1}", ALMLogicaCatalogos.Programas.prefijoBaseDatosAlmacen, condicion)
            comando.Parameters.AddWithValue("@idMoneda", Me.EIdMoneda)
            comando.Parameters.AddWithValue("@fecha", ALMLogicaCatalogos.Funciones.ValidarFechaAEstandar(Me.fecha))
            comando.Parameters.AddWithValue("@valor", Me.EValor)
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
            If (Me.EIdMoneda > 0) Then
                condicion &= " AND IdMoneda=@idMoneda"
            End If
            'If (IsDate(Me.EFecha)) Then
            '    condicion &= " AND Fecha=@fecha"
            'End If
            comando.CommandText = String.Format("SELECT TC.IdMoneda, M.Nombre, TC.Fecha, TC.Valor FROM {0}TiposCambios AS TC LEFT JOIN {0}Monedas AS M ON TC.IdMoneda = M.Id WHERE 0=0 {1} ORDER BY Fecha, IdMoneda ASC", ALMLogicaCatalogos.Programas.prefijoBaseDatosAlmacen, condicion)
            comando.Parameters.AddWithValue("@idMoneda", Me.EIdMoneda)
            comando.Parameters.AddWithValue("@fecha", ALMLogicaCatalogos.Funciones.ValidarFechaAEstandar(Me.fecha))
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
