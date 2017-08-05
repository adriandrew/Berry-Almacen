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
            Dim condicion As String = String.Empty
            Dim condicionFechaRango As String = String.Empty
            Dim condicionFechaAnterior As String = String.Empty
            Dim condicionFechaFinal As String = String.Empty
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
                condicionFechaRango &= " AND Fecha BETWEEN @fecha AND @fecha2 "
                condicionFechaAnterior &= " AND Fecha < @fecha "
                condicionFechaFinal &= " AND Fecha <= @fecha2 "
            End If 
            comando.CommandText = "SELECT Catalogo.*, SaldosFinales.SaldoAnterior, SaldosFinales.CostoAnterior, SaldosFinales.SaldoEntradasRango, SaldosFinales.CostoEntradasRango, SaldosFinales.SaldoSalidasRango, SaldosFinales.CostoSalidasRango, SaldosFinales.SaldoActual, SaldosFinales.CostoActual FROM " & _
            " ( " & _
                "SELECT PreSaldos.IdAlmacen, PreSaldos.IdFamilia, PreSaldos.IdSubFamilia, PreSaldos.IdArticulo, SUM(PreSaldos.SaldoAnterior) AS SaldoAnterior, SUM(PreSaldos.CostoAnterior) AS CostoAnterior, SUM(PreSaldos.SaldoEntradasRango) AS SaldoEntradasRango, SUM(PreSaldos.CostoEntradasRango) AS CostoEntradasRango, SUM(PreSaldos.SaldoSalidasRango) AS SaldoSalidasRango, SUM(PreSaldos.CostoSalidasRango) AS CostoSalidasRango, SUM(PreSaldos.SaldoActual) AS SaldoActual, SUM(PreSaldos.CostoActual) AS CostoActual FROM " & _
                    " ( " & _
                    " SELECT Actual.IdAlmacen, Actual.IdFamilia, Actual.IdSubFamilia, Actual.IdArticulo, 0 AS SaldoAnterior, 0 AS CostoAnterior, 0 AS SaldoEntradasRango, 0 AS CostoEntradasRango, 0 AS SaldoSalidasRango, 0 AS CostoSalidasRango, SUM(ISNULL(Actual.SaldoEntradasActual, 0)) - SUM(ISNULL(Actual.SaldoSalidasActual, 0)) AS SaldoActual, SUM(ISNULL(Actual.CostoEntradasActual, 0)) - SUM(ISNULL(Actual.CostoSalidasActual, 0)) AS CostoActual " & _
                    " FROM " & _
                    " ( " & _
                        " ( SELECT IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo, SUM(ISNULL(Cantidad, 0)) AS SaldoEntradasActual, SUM(ISNULL(Total, 0)) AS CostoEntradasActual, 0 AS SaldoSalidasActual, 0 AS CostoSalidasActual FROM Entradas WHERE 0=0 " & condicion & condicionFechaFinal & " GROUP BY IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo ) " & _
                        " UNION ALL " & _
                        " (  SELECT IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo, 0 AS SaldoEntradasActual, 0 AS CostoEntradasActual, SUM(ISNULL(Cantidad, 0)) AS SaldoSalidasActual, SUM(ISNULL(Total, 0)) AS CostoSalidasActual FROM Salidas WHERE 0=0 " & condicion & condicionFechaFinal & " GROUP BY IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo ) " & _
                    " ) AS Actual " & _
                    " GROUP BY Actual.IdAlmacen, Actual.IdFamilia, Actual.IdSubFamilia, Actual.IdArticulo " & _
                " UNION ALL " & _
                    " ( SELECT IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo, 0 AS SaldoAnterior, 0 AS CostoAnterior, SUM(ISNULL(Cantidad, 0)) AS SaldoEntradasRango, SUM(ISNULL(Total, 0)) AS CostoEntradasRango, 0 AS SaldoSalidasRango, 0 AS CostoSalidasRango, 0 AS SaldoActual, 0 AS CostoActual FROM Entradas WHERE 0=0 " & condicion & condicionFechaRango & " GROUP BY IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo ) " & _
                    " UNION ALL " & _
                    " ( SELECT IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo, 0 AS SaldoAnterior, 0 AS CostoAnterior, 0 AS SaldoEntradasRango, 0 AS CostoEntradasRango, SUM(ISNULL(Cantidad, 0)) AS SaldoSalidasRango, SUM(ISNULL(Total, 0)) AS CostoSalidasRango, 0 AS SaldoActual, 0 AS CostoActual FROM Salidas WHERE 0=0 " & condicion & condicionFechaRango & " GROUP BY IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo ) " & _
                " UNION ALL " & _
                    " SELECT Anterior.IdAlmacen, Anterior.IdFamilia, Anterior.IdSubFamilia, Anterior.IdArticulo, SUM(ISNULL(Anterior.SaldoEntradasAnterior, 0)) - SUM(ISNULL(Anterior.SaldoSalidasAnterior, 0)) AS SaldoAnterior, SUM(ISNULL(Anterior.CostoEntradasAnterior, 0)) - SUM(ISNULL(Anterior.CostoSalidasAnterior, 0)) AS CostoAnterior, 0 AS SaldoEntradasRango, 0 AS CostoEntradasRango, 0 AS SaldoSalidasRango, 0 AS CostoSalidasRango, 0 AS SaldoActual, 0 AS CostoActual " & _
                    " FROM " & _
                    " ( " & _
                        " ( SELECT IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo, SUM(ISNULL(Cantidad, 0)) AS SaldoEntradasAnterior, SUM(ISNULL(Total, 0)) AS CostoEntradasAnterior, 0 AS SaldoSalidasAnterior, 0 AS CostoSalidasAnterior FROM Entradas WHERE 0=0 " & condicion & condicionFechaAnterior & " GROUP BY IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo ) " & _
                        " UNION ALL " & _
                        " ( SELECT IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo, 0 AS SaldoEntradasAnterior, 0 AS CostoEntradasAnterior, SUM(ISNULL(Cantidad, 0)) AS SaldoSalidasAnterior, SUM(ISNULL(Total, 0)) AS CostoSalidasAnterior FROM Salidas WHERE 0=0 " & condicion & condicionFechaAnterior & " GROUP BY IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo ) " & _
                    " ) AS Anterior " & _
                    " GROUP BY Anterior.IdAlmacen, Anterior.IdFamilia, Anterior.IdSubFamilia, Anterior.IdArticulo " & _
                    " ) AS PreSaldos " & _
                " GROUP BY PreSaldos.IdAlmacen, PreSaldos.IdFamilia, PreSaldos.IdSubFamilia, PreSaldos.IdArticulo " & _
            " ) AS SaldosFinales " & _
            " LEFT JOIN " & _
                "( SELECT A.Id AS IdAlmacen, A.Nombre AS NombreAlmacen, F.Id AS IdFamilia, F.Nombre AS NombreFamilia, SF.Id AS IdSubFamilia, SF.Nombre AS NombreSubFamilia, AR.Id AS IdArticulo, AR.Nombre AS NombreArticulo FROM " & ALMLogicaReporteSaldos.Programas.bdCatalogo & ".dbo.ALM_Almacenes AS A LEFT JOIN " & ALMLogicaReporteSaldos.Programas.bdCatalogo & ".dbo.ALM_Familias AS F ON A.Id = F.IdAlmacen LEFT JOIN " & ALMLogicaReporteSaldos.Programas.bdCatalogo & ".dbo.ALM_SubFamilias AS SF ON A.Id = SF.IdAlmacen AND F.Id = SF.IdFamilia LEFT JOIN " & ALMLogicaReporteSaldos.Programas.bdCatalogo & ".dbo.ALM_Articulos AS AR ON A.Id = AR.IdAlmacen AND F.Id = AR.IdFamilia AND SF.Id = AR.IdSubFamilia " & _
                " ) AS Catalogo " & _
            " ON SaldosFinales.IdAlmacen = Catalogo.IdAlmacen AND SaldosFinales.IdFamilia = Catalogo.IdFamilia AND SaldosFinales.IdSubFamilia = Catalogo.IdSubFamilia AND SaldosFinales.IdArticulo = Catalogo.IdArticulo"
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            comando.Parameters.AddWithValue("@idFamilia", Me.EIdFamilia)
            comando.Parameters.AddWithValue("@idSubFamilia", Me.EIdSubFamilia)
            comando.Parameters.AddWithValue("@idArticulo", Me.EIdArticulo)
            comando.Parameters.AddWithValue("@fecha", ALMLogicaReporteSaldos.Funciones.ValidarFechaAEstandar(Me.EFecha))
            comando.Parameters.AddWithValue("@fecha2", ALMLogicaReporteSaldos.Funciones.ValidarFechaAEstandar(Me.EFecha2))
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
