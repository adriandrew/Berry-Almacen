Imports System.Data.SqlClient

Public Class Saldos

    Private idAlmacen As Integer
    Private idFamilia As Integer
    Private idSubFamilia As Integer
    Private idArticulo As Integer
    Private fecha As Date
    Private fecha2 As Date

    Public Property EIdAlmacen() As Integer
        Get
            Return Me.idAlmacen
        End Get
        Set(value As Integer)
            Me.idAlmacen = value
        End Set
    End Property
    Public Property EIdFamilia() As Integer
        Get
            Return Me.idFamilia
        End Get
        Set(value As Integer)
            Me.idFamilia = value
        End Set
    End Property
    Public Property EIdSubFamilia() As Integer
        Get
            Return Me.idSubFamilia
        End Get
        Set(value As Integer)
            Me.idSubFamilia = value
        End Set
    End Property
    Public Property EIdArticulo() As Integer
        Get
            Return Me.idArticulo
        End Get
        Set(value As Integer)
            Me.idArticulo = value
        End Set
    End Property
    Public Property EFecha() As String
        Get
            Return Me.fecha
        End Get
        Set(value As String)
            Me.fecha = value
        End Set
    End Property
    Public Property EFecha2() As String
        Get
            Return Me.fecha2
        End Get
        Set(value As String)
            Me.fecha2 = value
        End Set
    End Property

    Public Function ObtenerListadoReporte(ByVal aplicaFecha As Boolean) As DataTable

        Dim datos As New DataTable
        Try
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            Dim condicion As String = String.Empty : Dim condicionInterna As String = String.Empty
            If (Me.EIdAlmacen > 0) Then
                condicion &= " AND IdAlmacen=@idAlmacen "
            End If
            If (Me.EIdFamilia > 0) Then
                condicion &= " AND IdFamilia=@idFamilia "
            End If
            If (Me.EIdSubFamilia > 0) Then
                condicion &= " AND IdSubFamilia=@idSubFamilia "
            End If
            If (Me.EIdArticulo > 0) Then
                condicion &= " AND IdArticulo=@idArticulo "
            End If
            If (aplicaFecha) Then
                condicionInterna &= " AND Fecha BETWEEN @fecha AND @fecha2 "
            End If
            comando.CommandText = "SELECT ESA.IdAlmacen, NULL AS NombreAlmacen, ESA.IdFamilia, NULL AS NombreFamilia, ESA.IdSubFamilia, NULL AS NombreSubFamilia, ESA.IdArticulo, NULL AS NombreArticulo, 0 AS SaldoAnterior, 0 AS Entradas, 0 AS Salidas, ESA.SaldoActual FROM " & _
            "( " & _
                " SELECT EA.IdAlmacen, EA.IdFamilia, EA.IdSubFamilia, EA.IdArticulo, SUM(ISNULL(EA.Cantidad, 0)) - SUM(ISNULL(SA.Cantidad, 0)) AS SaldoActual " & _
                " FROM " & _
                " ( " & _
                " SELECT IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo, SUM(ISNULL(Cantidad, 0)) AS Cantidad " & _
                " FROM Entradas " & _
                " WHERE 0=0 " & condicion & _
                " GROUP BY IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo " & _
                " ) AS EA LEFT JOIN " & _
                " ( " & _
                " SELECT IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo, SUM(ISNULL(Cantidad, 0)) AS Cantidad " & _
                " FROM Salidas " & _
                " WHERE 0=0 " & condicion & _
                " GROUP BY IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo " & _
                " ) AS SA ON EA.IdAlmacen = SA.IdAlmacen AND EA.IdFamilia = SA.IdFamilia AND EA.IdSubFamilia = SA.IdSubFamilia AND EA.IdArticulo = SA.IdArticulo " & _
            " GROUP BY EA.IdAlmacen, EA.IdFamilia, EA.IdSubFamilia, EA.IdArticulo " & _
            " ) AS ESA " & _
            " WHERE 0=0 " & condicion
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            comando.Parameters.AddWithValue("@idFamilia", Me.EIdFamilia)
            comando.Parameters.AddWithValue("@idSubFamilia", Me.EIdSubFamilia)
            comando.Parameters.AddWithValue("@idArticulo", Me.EIdArticulo) 
            comando.Parameters.AddWithValue("@fecha", LogicaReporteSaldos.Funciones.ValidarFechaAEstandar(Me.EFecha))
            comando.Parameters.AddWithValue("@fecha2", LogicaReporteSaldos.Funciones.ValidarFechaAEstandar(Me.EFecha2))
            BaseDatos.conexionAlmacen.Open()
            Dim dataReader As SqlDataReader
            dataReader = comando.ExecuteReader()
            datos.Load(dataReader)
            BaseDatos.conexionAlmacen.Close()
            Return datos
        Catch ex As Exception
            Throw ex
        Finally
            BaseDatos.conexionAlmacen.Close()
        End Try

    End Function

End Class
