Imports System.Data.SqlClient

Public Class Entradas

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
    Private precioUnitario As Double
    Private total As Double
    Private totalPesos As Double
    Private orden As Integer
    Private observaciones As String
    Private factura As String
    Private chofer As String
    Private camion As String
    Private noEconomico As String

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

    Public Sub Guardar()

        Try
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            comando.CommandText = String.Format("INSERT INTO Entradas (IdAlmacen, IdFamilia, IdSubFamilia, IdArticulo, Id, IdExterno, IdTipoEntrada, IdProveedor, IdMoneda, TipoCambio, Fecha, Cantidad, PrecioUnitario, Total, TotalPesos, Orden, Observaciones, Factura, Chofer, Camion, NoEconomico) VALUES (@idAlmacen, @idFamilia, @idSubFamilia, @idArticulo, @id, @idExterno, @idTipoEntrada, @idProveedor, @idMoneda, @tipoCambio, @fecha, @cantidad, @precioUnitario, @total, @totalPesos, @orden, @observaciones, @factura, @chofer, @camion, @noEconomico)")
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
            comando.Parameters.AddWithValue("@precioUnitario", Me.EPrecioUnitario)
            comando.Parameters.AddWithValue("@total", Me.ETotal)
            comando.Parameters.AddWithValue("@totalPesos", Me.ETotalPesos)
            comando.Parameters.AddWithValue("@orden", Me.EOrden)
            comando.Parameters.AddWithValue("@observaciones", Me.EObservaciones)
            comando.Parameters.AddWithValue("@factura", Me.EFactura)
            comando.Parameters.AddWithValue("@chofer", Me.EChofer)
            comando.Parameters.AddWithValue("@camion", Me.ECamion)
            comando.Parameters.AddWithValue("@noEconomico", Me.ENoEconomico)
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
            comando.CommandText = String.Format("DELETE FROM Entradas WHERE 0=0 {0}", condicion)
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
            If (Me.EIdAlmacen > 0) Then
                condicion &= " WHERE IdAlmacen=@idAlmacen"
            End If
            comando.CommandText = String.Format("SELECT MAX(CAST (Id AS Int)) AS IdMaximo FROM Entradas {0}", condicion)
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

    Public Function ObtenerListadoReporte() As DataTable

        Try
            Dim datos As New DataTable
            Dim comando As New SqlCommand()
            comando.Connection = BaseDatos.conexionAlmacen
            Dim condicion As String = String.Empty
            If (Me.EIdAlmacen > 0) Then
                condicion &= " AND E.IdAlmacen=@idAlmacen"
            End If
            If (Me.EId > 0) Then
                condicion &= " AND E.Id=@id"
            End If
            comando.CommandText = String.Format("SELECT E.IdFamilia, F.Nombre, E.IdSubFamilia, SF.Nombre, E.IdArticulo, A.Nombre, UM.Nombre, E.Cantidad, E.PrecioUnitario, E.Total, E.TotalPesos, E.Observaciones, E.Factura, E.Chofer, E.Camion, E.NoEconomico " & _
            " FROM Entradas AS E " & _
            " LEFT JOIN {0}Familias AS F ON E.IdFamilia = F.Id AND E.IdAlmacen = F.IdAlmacen" & _
            " LEFT JOIN {0}SubFamilias AS SF ON E.IdSubFamilia = SF.Id AND E.IdFamilia = SF.IdFamilia AND E.IdAlmacen = SF.IdAlmacen" & _
            " LEFT JOIN {0}Articulos AS A ON E.IdArticulo = A.Id AND E.IdSubFamilia = A.IdSubFamilia AND E.IdFamilia = A.IdFamilia AND E.IdAlmacen = A.IdAlmacen" & _
            " LEFT JOIN {0}UnidadesMedidas AS UM ON A.IdUnidadMedida = UM.Id" & _
            " WHERE 0=0 {1} ORDER BY E.Orden ASC", ALMLogicaEntradas.Programas.bdCatalogo & ".dbo." & ALMLogicaEntradas.Programas.prefijoBaseDatosAlmacen, condicion)
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
            If (Me.EIdAlmacen > 0) Then
                condicion &= " AND IdAlmacen=@idAlmacen"
            End If
            If (Me.EId > 0) Then
                condicion &= " AND Id=@id"
            End If
            comando.CommandText = String.Format("SELECT IdExterno, Fecha, IdMoneda, TipoCambio, IdTipoEntrada, IdProveedor FROM Entradas WHERE 0=0 {0} ORDER BY Orden ASC", condicion)
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

End Class
