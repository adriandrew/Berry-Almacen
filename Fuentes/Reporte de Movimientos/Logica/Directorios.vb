Public Class Directorios

    Public Shared id As Integer
    Public Shared nombre As String
    Public Shared descripcion As String
    Public Shared rutaLogo As String
    Public Shared esPredeterminado As Boolean
    Public Shared instanciaSql As String
    Public Shared usuarioSql As String
    Public Shared contrasenaSql As String

    Public Shared Sub ObtenerParametros()

        Dim parametros As String() = Environment.GetCommandLineArgs().ToArray()
        'For i As Integer = 1 To parametros.Length - 1
        '    MsgBox("Parámetro no " & i & " valor: " & parametros(i))
        'Next
        If (parametros.Length > 1) Then
            Dim numeracion As Integer = 1
            id = Funciones.ValidarNumeroACero(parametros(numeracion).Replace("|", " ")) : numeracion += 1
            nombre = parametros(numeracion).Replace("|", " ") : numeracion += 1
            descripcion = Funciones.ValidarLetra(parametros(numeracion).Replace("|", " ")) : numeracion += 1
            rutaLogo = Funciones.ValidarLetra(parametros(numeracion).Replace("|", " ")) : numeracion += 1
            esPredeterminado = Convert.ToBoolean(Funciones.ValidarNumeroACero(parametros(numeracion).Replace("|", " "))) : numeracion += 1
            instanciaSql = Funciones.ValidarLetra(parametros(numeracion).Replace("|", " ")) : numeracion += 1
            usuarioSql = Funciones.ValidarLetra(parametros(numeracion).Replace("|", " ")) : numeracion += 1
            contrasenaSql = Funciones.ValidarLetra(parametros(numeracion).Replace("|", " ")) : numeracion += 1
        End If

    End Sub

End Class
