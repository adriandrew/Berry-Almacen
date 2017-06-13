Public Module Funciones

    Public Function ValidarNumeroACero(ByVal valor As String) As Double

        If (IsNumeric(valor)) Then
            Return valor
        Else
            Return 0
        End If

    End Function

    Public Function ValidarNumeroAUno(ByVal valor As String) As Double

        If (IsNumeric(valor)) Then
            Return valor
        Else
            Return 1
        End If

    End Function

    Public Function ValidarLetra(ByVal valor As String) As String

        If (valor = Nothing) Then
            Return String.Empty
        Else
            Return valor
        End If

    End Function

    Public Function ValidarFechaAEstandar(ByVal valor As Date) As String
 
        Return valor.ToString("yyyyMMdd")

    End Function

End Module
