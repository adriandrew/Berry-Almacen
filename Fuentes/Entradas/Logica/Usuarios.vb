Public Class Usuarios

    Public Shared id As Integer
    Public Shared nombre As String
    Public Shared contrasena As String
    Public Shared nivel As Integer
    Public Shared accesoTotal As Boolean

    Public Shared Sub ObtenerParametros()

        Dim parametros() = Environment.GetCommandLineArgs().ToArray() 
        If (parametros.Length > 0) Then
            Dim numeracion As Integer = 10
            id = Convert.ToInt32(parametros(numeracion).Replace("|", " ")) : numeracion += 1
        End If

    End Sub

End Class
