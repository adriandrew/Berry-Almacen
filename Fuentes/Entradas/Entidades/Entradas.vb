Imports System.Data.SqlClient

Public Class Entradas

    Private idOrigen As Integer
    Private idAlmacen As Integer
    Private idFamilia As Integer
    Private idSubFamilia As Integer
    Private idArticulo As Integer
    Private id As Integer
    Private idExterno As String
    Private idTipoEntrada As Integer
    Private idProveedor As Integer
    Private idMoneda As Integer
    Private tipoCambio As Double
    Private fecha As Date
    Private cantidad As Integer
    Private precio As Double
    Private total As Double
    Private totalPesos As Double
    Private orden As Integer
    Private observaciones As String
    Private factura As String
    Private idSucursalOrigen As Integer

    Public Property EIdOrigen() As Integer
        Get
            Return idOrigen
        End Get
        Set(value As Integer)
            idOrigen = value
        End Set
    End Property
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
    Public Property EIdSubFamilia() As Integer
        Get
            Return idSubFamilia
        End Get
        Set(value As Integer)
            idSubFamilia = value
        End Set
    End Property
    Public Property EIdArticulo() As Integer
        Get
            Return idArticulo
        End Get
        Set(value As Integer)
            idArticulo = value
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
    Public Property EIdExterno() As String
        Get
            Return idExterno
        End Get
        Set(value As String)
            idExterno = value
        End Set
    End Property
    Public Property EIdTipoEntrada() As Integer
        Get
            Return idTipoEntrada
        End Get
        Set(value As Integer)
            idTipoEntrada = value
        End Set
    End Property
    Public Property EIdProveedor() As Integer
        Get
            Return idProveedor
        End Get
        Set(value As Integer)
            idProveedor = value
        End Set
    End Property
    Public Property EIdMoneda() As Integer
        Get
            Return idMoneda
        End Get
        Set(value As Integer)
            idMoneda = value
        End Set
    End Property
    Public Property ETipoCambio() As Double
        Get
            Return tipoCambio
        End Get
        Set(value As Double)
            tipoCambio = value
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
    Public Property ECantidad() As Integer
        Get
            Return cantidad
        End Get
        Set(value As Integer)
            cantidad = value
        End Set
    End Property
    Public Property EPrecio() As Double
        Get
            Return precio
        End Get
        Set(value As Double)
            precio = value
        End Set
    End Property
    Public Property ETotal() As Double
        Get
            Return total
        End Get
        Set(value As Double)
            total = value
        End Set
    End Property
    Public Property ETotalPesos() As Double
        Get
            Return totalPesos
        End Get
        Set(value As Double)
            totalPesos = value
        End Set
    End Property
    Public Property EOrden() As Integer
        Get
            Return orden
        End Get
        Set(value As Integer)
            orden = value
        End Set
    End Property
    Public Property EObservaciones() As String
        Get
            Return observaciones
        End Get
        Set(value As String)
            observaciones = value
        End Set
    End Property
    Public Property EFactura() As String
        Get
            Return factura
        End Get
        Set(value As String)
            factura = value
        End Set
    End Property
    Public Property EIdSucursalOrigen() As Integer
        Get
            Return idSucursalOrigen
        End Get
        Set(value As Integer)
            idSucursalOrigen = value
        End Set
    End Property

    Public Sub Guardar()

        Try
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            comando.CommandText = String.Format("INSERT INTO Entradas (IdOrigen, IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo, Id, IdExterno, IdTipoEntrada, IdProveedor, IdMoneda, TipoCambio, Fecha, Cantidad, Precio, Total, TotalPesos, Orden, Observaciones, Factura, IdSucursalOrigen) VALUES (@idOrigen, @idAlmacen, @idFamilia, @idSubFamilia, @idArticulo, @id, @idExterno, @idTipoEntrada, @idProveedor, @idMoneda, @tipoCambio, @fecha, @cantidad, @precio, @total, @totalPesos, @orden, @observaciones, @factura, @idSucursalOrigen)")
            comando.Parameters.AddWithValue("@idOrigen", Me.EIdOrigen)
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            comando.Parameters.AddWithValue("@idFamilia", Me.EIdFamilia)
            comando.Parameters.AddWithValue("@idSubFamilia", Me.EIdSubFamilia)
            comando.Parameters.AddWithValue("@idArticulo", Me.EIdArticulo)
            comando.Parameters.AddWithValue("@id", Me.EId)
            comando.Parameters.AddWithValue("@idExterno", Me.EIdExterno)
            comando.Parameters.AddWithValue("@idTipoEntrada", Me.EIdTipoEntrada)
            comando.Parameters.AddWithValue("@idProveedor", Me.EIdProveedor)
            comando.Parameters.AddWithValue("@idMoneda", Me.EIdMoneda)
            comando.Parameters.AddWithValue("@tipoCambio", Me.ETipoCambio)
            comando.Parameters.AddWithValue("@fecha", Me.EFecha)
            comando.Parameters.AddWithValue("@cantidad", Me.ECantidad)
            comando.Parameters.AddWithValue("@precio", Me.EPrecio)
            comando.Parameters.AddWithValue("@total", Me.ETotal)
            comando.Parameters.AddWithValue("@totalPesos", Me.ETotalPesos)
            comando.Parameters.AddWithValue("@orden", Me.EOrden)
            comando.Parameters.AddWithValue("@observaciones", Me.EObservaciones)
            comando.Parameters.AddWithValue("@factura", Me.EFactura)
            comando.Parameters.AddWithValue("@idSucursalOrigen", Me.EIdSucursalOrigen)
            BaseDatos.conexionAlmacen.Open()
            comando.ExecuteNonQuery()
            BaseDatos.conexionAlmacen.Close()
        Catch ex As Exception
            Throw ex
        Finally
            BaseDatos.conexionAlmacen.Close()
        End Try

    End Sub

    Public Sub Eliminar()

        Try
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            Dim condicion As String = String.Empty
            If (Me.EIdOrigen > 0) Then
                condicion &= " AND IdOrigen=@idOrigen"
            End If
            If (Me.EIdAlmacen > 0) Then
                condicion &= " AND IdAlmacen=@idAlmacen"
            End If
            If (Me.EId > 0) Then
                condicion &= " AND Id=@id"
            End If
            comando.CommandText = String.Format("DELETE FROM Entradas WHERE 0=0 {0}", condicion)
            comando.Parameters.AddWithValue("@idOrigen", Me.EIdOrigen)
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            comando.Parameters.AddWithValue("@id", Me.EId)
            BaseDatos.conexionAlmacen.Open()
            comando.ExecuteNonQuery()
            BaseDatos.conexionAlmacen.Close()
        Catch ex As Exception
            Throw ex
        Finally
            BaseDatos.conexionAlmacen.Close()
        End Try

    End Sub

    Public Function ObtenerMaximoId() As Integer

        Try
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            Dim condicion As String = String.Empty
            If (Me.EIdOrigen > 0) Then
                condicion &= " AND IdOrigen=@idOrigen"
            End If
            If (Me.EIdAlmacen > 0) Then
                condicion &= " AND IdAlmacen=@idAlmacen"
            End If
            comando.CommandText = String.Format("SELECT MAX(CAST (Id AS Int)) AS IdMaximo FROM Entradas WHERE 0=0 {0}", condicion)
            comando.Parameters.AddWithValue("@idOrigen", Me.EIdOrigen)
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            BaseDatos.conexionAlmacen.Open()
            Dim lectorDatos As SqlDataReader = comando.ExecuteReader()
            Dim valor As Integer = 0
            While lectorDatos.Read()
                valor = ALMLogicaEntradas.Funciones.ValidarNumeroACero(lectorDatos("IdMaximo").ToString()) + 1
            End While
            BaseDatos.conexionAlmacen.Close()
            Return valor
        Catch ex As Exception
            Throw ex
        Finally
            BaseDatos.conexionAlmacen.Close()
        End Try

    End Function

    Public Function ObtenerListadoGeneral() As DataTable

        Try
            Dim datos As New DataTable
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            Dim condicion As String = String.Empty
            If (Me.EIdOrigen > 0) Then
                condicion &= " AND IdOrigen=@idOrigen"
            End If
            If (Me.EIdAlmacen > 0) Then
                condicion &= " AND IdAlmacen=@idAlmacen"
            End If
            If (Me.EId > 0) Then
                condicion &= " AND Id=@id"
            End If
            comando.CommandText = String.Format("SELECT * FROM Entradas WHERE 0=0 {0} ORDER BY Orden ASC", condicion)
            comando.Parameters.AddWithValue("@idOrigen", Me.EIdOrigen)
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            comando.Parameters.AddWithValue("@id", Me.EId)
            BaseDatos.conexionAlmacen.Open()
            Dim lectorDatos As SqlDataReader
            lectorDatos = comando.ExecuteReader()
            datos.Load(lectorDatos)
            BaseDatos.conexionAlmacen.Close()
            Return datos
        Catch ex As Exception
            Throw ex
        Finally
            BaseDatos.conexionAlmacen.Close()
        End Try

    End Function

    Public Function ObtenerListadoDetallado() As DataTable

        Try
            Dim datos As New DataTable
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            Dim condicion As String = String.Empty
            If (Me.EIdOrigen > 0) Then
                condicion &= " AND E.IdOrigen=@idOrigen"
            End If
            If (Me.EIdAlmacen > 0) Then
                condicion &= " AND E.IdAlmacen=@idAlmacen"
            End If
            If (Me.EId > 0) Then
                condicion &= " AND E.Id=@id"
            End If
            comando.CommandText = String.Format("SELECT E.IdFamilia, F.Nombre, E.IdSubFamilia, SF.Nombre, E.IdArticulo, A.Nombre, UM.Nombre, E.Cantidad, E.Precio, E.Total, E.TotalPesos, E.Observaciones " & _
            " FROM Entradas AS E " & _
            " LEFT JOIN {0}Familias AS F ON E.IdFamilia = F.Id AND E.IdAlmacen = F.IdAlmacen" & _
            " LEFT JOIN {0}SubFamilias AS SF ON E.IdSubFamilia = SF.Id AND E.IdFamilia = SF.IdFamilia AND E.IdAlmacen = SF.IdAlmacen" & _
            " LEFT JOIN {0}Articulos AS A ON E.IdArticulo = A.Id AND E.IdSubFamilia = A.IdSubFamilia AND E.IdFamilia = A.IdFamilia AND E.IdAlmacen = A.IdAlmacen" & _
            " LEFT JOIN {0}UnidadesMedidas AS UM ON A.IdUnidadMedida = UM.Id" & _
            " WHERE 0=0 {1} ORDER BY E.Orden ASC", ALMLogicaEntradas.Programas.bdCatalogo & ".dbo." & ALMLogicaEntradas.Programas.prefijoBaseDatosAlmacen, condicion)
            comando.Parameters.AddWithValue("@idOrigen", Me.EIdOrigen)
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            comando.Parameters.AddWithValue("@id", Me.EId)
            BaseDatos.conexionAlmacen.Open()
            Dim lectorDatos As SqlDataReader
            lectorDatos = comando.ExecuteReader()
            datos.Load(lectorDatos)
            BaseDatos.conexionAlmacen.Close()
            Return datos
        Catch ex As Exception
            Throw ex
        Finally
            BaseDatos.conexionAlmacen.Close()
        End Try

    End Function

    Public Function ObtenerListado() As DataTable

        Try
            Dim datos As New DataTable
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            Dim condicion As String = String.Empty
            If (Me.EIdOrigen > 0) Then
                condicion &= " AND IdOrigen=@idOrigen"
            End If
            If (Me.EIdAlmacen > 0) Then
                condicion &= " AND IdAlmacen=@idAlmacen"
            End If
            If (Me.EId > 0) Then
                condicion &= " AND Id=@id"
            End If
            comando.CommandText = String.Format("SELECT E.IdOrigen, O.Nombre, E.IdAlmacen, A.Nombre, E.Id " & _
            " FROM (SELECT IdOrigen, IdAlmacen, Id FROM Entradas WHERE 0=0 {1} GROUP BY IdOrigen, IdAlmacen, Id) AS E " & _
            " LEFT JOIN {0}Origenes AS O ON E.IdOrigen = O.Id " & _
            " LEFT JOIN {0}Almacenes AS A ON E.IdAlmacen = A.Id " & _
            " ORDER BY IdOrigen, IdAlmacen, Id ASC", ALMLogicaEntradas.Programas.bdCatalogo & ".dbo." & ALMLogicaEntradas.Programas.prefijoBaseDatosAlmacen, condicion)
            comando.Parameters.AddWithValue("@idOrigen", Me.EIdOrigen)
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            comando.Parameters.AddWithValue("@id", Me.EId)
            BaseDatos.conexionAlmacen.Open()
            Dim lectorDatos As SqlDataReader
            lectorDatos = comando.ExecuteReader()
            datos.Load(lectorDatos)
            BaseDatos.conexionAlmacen.Close()
            Return datos
        Catch ex As Exception
            Throw ex
        Finally
            BaseDatos.conexionAlmacen.Close()
        End Try

    End Function

    Public Function ObtenerSaldos(ByVal soloId As Boolean) As Integer

        Try
            Dim datos As New DataTable
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            Dim condicion As String = String.Empty
            If (Not soloId) Then
                condicion &= " AND Id<>@id "
            End If
            comando.CommandText = String.Format("SELECT Actual.IdAlmacen, Actual.IdFamilia, Actual.IdSubFamilia, Actual.IdArticulo, ISNULL(SUM(Actual.SaldoEntradasActual), 0) - ISNULL(SUM(Actual.SaldoSalidasActual), 0) AS SaldoActual, ISNULL(SUM(Actual.CostoEntradasActual), 0) - ISNULL(SUM(Actual.CostoSalidasActual), 0) AS CostoActual " & _
                    " FROM " & _
                    " ( " & _
                        " ( SELECT IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo, ISNULL(SUM(Cantidad), 0) AS SaldoEntradasActual, ISNULL(SUM(Total), 0) AS CostoEntradasActual, 0 AS SaldoSalidasActual, 0 AS CostoSalidasActual FROM Entradas WHERE IdAlmacen=@idAlmacen AND IdFamilia=@idFamilia AND IdSubFamilia=@idSubFamilia AND IdArticulo=@idArticulo {0} GROUP BY IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo ) " & _
                        " UNION ALL " & _
                        " (  SELECT IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo, 0 AS SaldoEntradasActual, 0 AS CostoEntradasActual, ISNULL(SUM(Cantidad), 0) AS SaldoSalidasActual, ISNULL(SUM(Total), 0) AS CostoSalidasActual FROM Salidas WHERE IdAlmacen=@idAlmacen AND IdFamilia=@idFamilia AND IdSubFamilia=@idSubFamilia AND IdArticulo=@idArticulo GROUP BY IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo ) " & _
                    " ) AS Actual " & _
                    " GROUP BY Actual.IdAlmacen, Actual.IdFamilia, Actual.IdSubFamilia, Actual.IdArticulo ", condicion)
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            comando.Parameters.AddWithValue("@idFamilia", Me.EIdFamilia)
            comando.Parameters.AddWithValue("@idSubFamilia", Me.EIdSubFamilia)
            comando.Parameters.AddWithValue("@idArticulo", Me.EIdArticulo)
            comando.Parameters.AddWithValue("@id", Me.EId)
            BaseDatos.conexionAlmacen.Open()
            Dim lectorDatos As SqlDataReader
            lectorDatos = comando.ExecuteReader()
            datos.Load(lectorDatos)
            Dim saldo As Integer = 0
            If (datos.Rows.Count > 0) Then
                saldo = datos.Rows(0).Item("SaldoActual")
            End If
            BaseDatos.conexionAlmacen.Close()
            Return saldo
        Catch ex As Exception
            Throw ex
        Finally
            BaseDatos.conexionAlmacen.Close()
        End Try

    End Function

End Class
