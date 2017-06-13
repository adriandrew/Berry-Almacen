Imports System.Data.SqlClient

Public Class Salidas

    Private idAlmacen As Integer
    Private idFamilia As Integer
    Private idSubFamilia As Integer
    Private idArticulo As Integer
    Private id As Integer
    Private idExterno As String
    Private idTipoSalida As Integer
    Private idCliente As Integer
    Private idMoneda As Integer
    Private tipoCambio As Double
    Private fecha As Date
    Private cantidad As Integer
    Private precioUnitario As Double
    Private total As Double
    Private totalPesos As Double
    Private orden As Integer
    Private observaciones As String
    Private factura As String
    Private chofer As String
    Private camion As String
    Private noEconomico As String
    Private idLote As Integer
    Private idCultivo As Integer

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
    Public Property EIdTipoSalida() As Integer
        Get
            Return idTipoSalida
        End Get
        Set(value As Integer)
            idTipoSalida = value
        End Set
    End Property
    Public Property EIdCliente() As Integer
        Get
            Return idCliente
        End Get
        Set(value As Integer)
            idCliente = value
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
    Public Property EPrecioUnitario() As Double
        Get
            Return precioUnitario
        End Get
        Set(value As Double)
            precioUnitario = value
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
    Public Property EChofer() As String
        Get
            Return chofer
        End Get
        Set(value As String)
            chofer = value
        End Set
    End Property
    Public Property ECamion() As String
        Get
            Return camion
        End Get
        Set(value As String)
            camion = value
        End Set
    End Property
    Public Property ENoEconomico() As String
        Get
            Return noEconomico
        End Get
        Set(value As String)
            noEconomico = value
        End Set
    End Property
    Public Property EIdLote() As Integer
        Get
            Return idLote
        End Get
        Set(value As Integer)
            idLote = value
        End Set
    End Property
    Public Property EIdCultivo() As Integer
        Get
            Return idCultivo
        End Get
        Set(value As Integer)
            idCultivo = value
        End Set
    End Property

    Public Sub Guardar()

        Try
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            comando.CommandText = "INSERT INTO Salidas (IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo, Id, IdExterno, IdTipoSalida, IdCliente, IdMoneda, TipoCambio, Fecha, Cantidad, PrecioUnitario, Total, TotalPesos, Orden, Observaciones, Factura, Chofer, Camion, NoEconomico, IdLote, IdCultivo) VALUES (@idAlmacen, @idFamilia, @idSubFamilia, @idArticulo, @id, @idExterno, @idTipoSalida, @idCliente, @idMoneda, @tipoCambio, @fecha, @cantidad, @precioUnitario, @total, @totalPesos, @orden, @observaciones, @factura, @chofer, @camion, @noEconomico, @idLote, @idCultivo)"
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            comando.Parameters.AddWithValue("@idFamilia", Me.EIdFamilia)
            comando.Parameters.AddWithValue("@idSubFamilia", Me.EIdSubFamilia)
            comando.Parameters.AddWithValue("@idArticulo", Me.EIdArticulo)
            comando.Parameters.AddWithValue("@id", Me.EId)
            comando.Parameters.AddWithValue("@idExterno", Me.EIdExterno)
            comando.Parameters.AddWithValue("@idTipoSalida", Me.EIdTipoSalida)
            comando.Parameters.AddWithValue("@idCliente", Me.EIdCliente)
            comando.Parameters.AddWithValue("@idMoneda", Me.EIdMoneda)
            comando.Parameters.AddWithValue("@tipoCambio", Me.ETipoCambio)
            comando.Parameters.AddWithValue("@fecha", Me.EFecha)
            comando.Parameters.AddWithValue("@cantidad", Me.ECantidad)
            comando.Parameters.AddWithValue("@precioUnitario", Me.EPrecioUnitario)
            comando.Parameters.AddWithValue("@total", Me.ETotal)
            comando.Parameters.AddWithValue("@totalPesos", Me.ETotalPesos)
            comando.Parameters.AddWithValue("@orden", Me.EOrden)
            comando.Parameters.AddWithValue("@observaciones", Me.EObservaciones)
            comando.Parameters.AddWithValue("@factura", Me.EFactura)
            comando.Parameters.AddWithValue("@chofer", Me.EChofer)
            comando.Parameters.AddWithValue("@camion", Me.ECamion)
            comando.Parameters.AddWithValue("@noEconomico", Me.ENoEconomico)
            comando.Parameters.AddWithValue("@idLote", Me.EIdLote)
            comando.Parameters.AddWithValue("@idCultivo", Me.EIdCultivo)
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
            If (Me.EIdAlmacen > 0) Then
                condicion &= " AND IdAlmacen=@idAlmacen"
            End If
            If (Me.EId > 0) Then
                condicion &= " AND Id=@id"
            End If
            comando.CommandText = "DELETE FROM Salidas WHERE 0=0 " & condicion
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            comando.Parameters.AddWithValue("@id", Me.id)
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
            If (Me.EIdAlmacen > 0) Then
                condicion &= " WHERE IdAlmacen=@idAlmacen"
            End If
            comando.CommandText = "SELECT MAX(CAST (Id AS Int)) AS IdMaximo FROM Salidas" + condicion
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            BaseDatos.conexionAlmacen.Open()
            Dim lectorDatos As SqlDataReader = comando.ExecuteReader()
            Dim valor As Integer = 0
            While lectorDatos.Read()
                valor = LogicaSalidas.Funciones.ValidarNumeroACero(lectorDatos("IdMaximo").ToString()) + 1
            End While
            BaseDatos.conexionAlmacen.Close()
            Return valor
        Catch ex As Exception
            Throw ex
        Finally
            BaseDatos.conexionAlmacen.Close()
        End Try

    End Function

    Public Function ObtenerListadoReporte() As DataTable

        Try
            Dim datos As New DataTable
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            Dim condicion As String = String.Empty
            If (Me.EIdAlmacen > 0) Then
                condicion &= " AND S.IdAlmacen=@idAlmacen"
            End If
            If (Me.EId > 0) Then
                condicion &= " AND S.Id=@id"
            End If
            comando.CommandText = "SELECT 'TRUE', S.IdFamilia, F.Nombre, S.IdSubFamilia, SF.Nombre, S.IdArticulo, A.Nombre, UM.Nombre, S.Cantidad, S.PrecioUnitario, S.Total, S.TotalPesos, S.Observaciones, S.Factura, S.Chofer, S.Camion, S.NoEconomico " & _
            " FROM Salidas AS S " & _
            " LEFT JOIN " & LogicaSalidas.Programas.bdCatalogo & ".dbo." & LogicaSalidas.Programas.prefijoBaseDatosAlmacen & "Familias AS F ON S.IdFamilia = F.Id AND S.IdAlmacen = F.IdAlmacen" & _
            " LEFT JOIN " & LogicaSalidas.Programas.bdCatalogo & ".dbo." & LogicaSalidas.Programas.prefijoBaseDatosAlmacen & "SubFamilias AS SF ON S.IdSubFamilia = SF.Id AND S.IdFamilia = SF.IdFamilia AND S.IdAlmacen = SF.IdAlmacen" & _
            " LEFT JOIN " & LogicaSalidas.Programas.bdCatalogo & ".dbo." & LogicaSalidas.Programas.prefijoBaseDatosAlmacen & "Articulos AS A ON S.IdArticulo = A.Id AND S.IdSubFamilia = A.IdSubFamilia AND S.IdFamilia = A.IdFamilia AND S.IdAlmacen = A.IdAlmacen" & _
            " LEFT JOIN " & LogicaSalidas.Programas.bdCatalogo & ".dbo." & LogicaSalidas.Programas.prefijoBaseDatosAlmacen & "UnidadesMedidas AS UM ON A.IdUnidadMedida = UM.Id" & _
            " WHERE 0=0 " & condicion & " ORDER BY S.Orden ASC"
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

    Public Function ObtenerListado() As List(Of Salidas)

        Try
            Dim lista As New List(Of Salidas)
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            Dim condicion As String = String.Empty
            If (Me.EIdAlmacen > 0) Then
                condicion &= " AND IdAlmacen=@idAlmacen"
            End If 
            If (Me.EId > 0) Then
                condicion &= " AND Id=@id"
            End If
            comando.CommandText = "SELECT * FROM Salidas WHERE 0=0 " & condicion & " ORDER BY Orden ASC"
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            comando.Parameters.AddWithValue("@id", Me.EId)
            BaseDatos.conexionAlmacen.Open()
            Dim lectorDatos As SqlDataReader = comando.ExecuteReader()
            Dim salidas As Salidas
            While lectorDatos.Read()
                salidas = New Salidas()
                salidas.idAlmacen = Convert.ToInt32(lectorDatos("IdAlmacen").ToString())
                salidas.idFamilia = Convert.ToInt32(lectorDatos("IdFamilia").ToString())
                salidas.idSubFamilia = Convert.ToInt32(lectorDatos("IdSubFamilia").ToString())
                salidas.idArticulo = Convert.ToInt32(lectorDatos("IdArticulo").ToString())
                salidas.id = Convert.ToInt32(lectorDatos("Id").ToString())
                salidas.idExterno = lectorDatos("IdExterno").ToString()
                salidas.idTipoSalida = lectorDatos("IdTipoSalida").ToString()
                salidas.idCliente = Convert.ToInt32(lectorDatos("IdCliente").ToString())
                salidas.idMoneda = Convert.ToInt32(lectorDatos("IdMoneda").ToString())
                salidas.tipoCambio = Convert.ToDouble(lectorDatos("TipoCambio").ToString())
                salidas.fecha = Convert.ToDateTime(lectorDatos("Fecha").ToString())
                salidas.cantidad = Convert.ToInt32(lectorDatos("Cantidad").ToString())
                salidas.precioUnitario = Convert.ToDouble(lectorDatos("PrecioUnitario").ToString())
                salidas.total = Convert.ToDouble(lectorDatos("Total").ToString())
                salidas.totalPesos = Convert.ToDouble(lectorDatos("TotalPesos").ToString())
                salidas.orden = Convert.ToInt32(lectorDatos("Orden").ToString())
                salidas.observaciones = lectorDatos("Observaciones").ToString()
                salidas.factura = lectorDatos("Factura").ToString()
                salidas.chofer = lectorDatos("Chofer").ToString()
                salidas.camion = lectorDatos("Camion").ToString()
                salidas.noEconomico = lectorDatos("NoEconomico").ToString()
                salidas.idLote = Convert.ToInt32(lectorDatos("IdLote").ToString())
                salidas.idCultivo = Convert.ToInt32(lectorDatos("IdCultivo").ToString())
                lista.Add(salidas)
            End While
            BaseDatos.conexionAlmacen.Close()
            Return lista
        Catch ex As Exception
            Throw ex
        Finally
            BaseDatos.conexionAlmacen.Close()
        End Try

    End Function

    Public Function ObtenerSaldos() As DataTable

        Try
            Dim datos As New DataTable
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            Dim condicion As String = String.Empty
            If (Me.EIdAlmacen > 0) Then
                condicion &= " AND Saldos.IdAlmacen=@idAlmacen"
            End If
            If (Me.EIdFamilia > 0) Then
                condicion &= " AND Saldos.IdFamilia=@idFamilia"
            End If
            If (Me.EIdSubFamilia > 0) Then
                condicion &= " AND Saldos.IdSubFamilia=@idSubFamilia"
            End If
            If (Me.EIdArticulo > 0) Then
                condicion &= " AND Saldos.IdArticulo=@idArticulo"
            End If 
            comando.CommandText = "SELECT * FROM " & _
            "( " & _
                " SELECT E.IdAlmacen, E.IdFamilia, E.IdSubFamilia, E.IdArticulo, SUM(ISNULL(E.Cantidad, 0)) - SUM(ISNULL(S.Cantidad, 0)) AS Cantidad " & _
                " FROM " & _
                " ( " & _
                " SELECT IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo, SUM(ISNULL(Cantidad, 0)) AS Cantidad " & _
                " FROM Entradas " & _
                " GROUP BY IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo " & _
                " ) AS E LEFT JOIN " & _
                " ( " & _
                " SELECT IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo, SUM(ISNULL(Cantidad, 0)) AS Cantidad " & _
                " FROM Salidas " & _
                " GROUP BY IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo " & _
                " ) AS S ON E.IdAlmacen = S.IdAlmacen  AND E.IdFamilia = S.IdFamilia AND E.IdSubFamilia = S.IdSubFamilia AND E.IdArticulo = S.IdArticulo " & _
            " GROUP BY E.IdAlmacen, E.IdFamilia, E.IdSubFamilia, E.IdArticulo " & _
            " ) AS Saldos " & _
            " WHERE 0=0 " & condicion
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            comando.Parameters.AddWithValue("@idFamilia", Me.EIdFamilia)
            comando.Parameters.AddWithValue("@idSubFamilia", Me.EIdSubFamilia)
            comando.Parameters.AddWithValue("@idArticulo", Me.EIdArticulo)
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

    Public Function ValidarFechasPosteriores() As DataTable

        Try 
            Dim datos As New DataTable
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            Dim condicion As String = String.Empty
            If (Me.EIdAlmacen > 0) Then
                condicion &= " AND IdAlmacen=@idAlmacen"
            End If
            If (Me.EIdFamilia > 0) Then
                condicion &= " AND IdFamilia=@idFamilia"
            End If
            If (Me.EIdSubFamilia > 0) Then
                condicion &= " AND IdSubFamilia=@idSubFamilia"
            End If
            If (Me.EIdArticulo > 0) Then
                condicion &= " AND IdArticulo=@idArticulo"
            End If
            condicion &= " AND Fecha>@fecha"
            comando.CommandText = " SELECT Id, Fecha FROM Salidas WHERE 0=0 " & condicion
            comando.Parameters.AddWithValue("@idAlmacen", Me.EIdAlmacen)
            comando.Parameters.AddWithValue("@idFamilia", Me.EIdFamilia)
            comando.Parameters.AddWithValue("@idSubFamilia", Me.EIdSubFamilia)
            comando.Parameters.AddWithValue("@idArticulo", Me.EIdArticulo)
            comando.Parameters.AddWithValue("@fecha", LogicaSalidas.Funciones.ValidarFechaAEstandar(Me.EFecha))
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

End Class
