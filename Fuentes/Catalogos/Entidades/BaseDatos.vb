Imports System.Data.SqlClient

Public Module BaseDatos

    Private cadenaConexionConfiguracion As String
    Private cadenaConexionCatalogo As String
    Private cadenaConexionAlmacen As String
    Public conexionConfiguracion As New SqlConnection()
    Public conexionCatalogo As New SqlConnection()
    Public conexionAgenda As New SqlConnection()

    Public Property ECadenaConexionConfiguracion() As String
        Get
            Return BaseDatos.cadenaConexionConfiguracion
        End Get
        Set(value As String)
            BaseDatos.cadenaConexionConfiguracion = value
        End Set
    End Property
    Public Property ECadenaConexionCatalogo() As String
        Get
            Return BaseDatos.cadenaConexionCatalogo
        End Get
        Set(value As String)
            BaseDatos.cadenaConexionCatalogo = value
        End Set
    End Property
    Public Property ECadenaConexionAlmacen() As String
        Get
            Return BaseDatos.cadenaConexionAlmacen
        End Get
        Set(value As String)
            BaseDatos.cadenaConexionAlmacen = value
        End Set
    End Property

    Public Sub AbrirConexionConfiguracion()

        BaseDatos.ECadenaConexionConfiguracion = String.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3}", LogicaCatalogos.Directorios.instanciaSql, BaseDatos.ECadenaConexionConfiguracion, LogicaCatalogos.Directorios.usuarioSql, LogicaCatalogos.Directorios.contrasenaSql)
        conexionConfiguracion.ConnectionString = BaseDatos.ECadenaConexionConfiguracion

    End Sub

    Public Sub AbrirConexionCatalogo()

        BaseDatos.ECadenaConexionCatalogo = String.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3}", LogicaCatalogos.Directorios.instanciaSql, BaseDatos.ECadenaConexionCatalogo, LogicaCatalogos.Directorios.usuarioSql, LogicaCatalogos.Directorios.contrasenaSql)
        conexionCatalogo.ConnectionString = BaseDatos.ECadenaConexionCatalogo

    End Sub

    Public Sub AbrirConexionAlmacen()

        BaseDatos.ECadenaConexionAlmacen = String.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3}", LogicaCatalogos.Directorios.instanciaSql, BaseDatos.ECadenaConexionAlmacen, LogicaCatalogos.Directorios.usuarioSql, LogicaCatalogos.Directorios.contrasenaSql)
        conexionAgenda.ConnectionString = BaseDatos.ECadenaConexionAlmacen

    End Sub

End Module
