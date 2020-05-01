Option Explicit On
Option Strict On

Imports System.Data.SqlClient

Namespace cls.msora

    Public Class DB_MSora
        Inherits base.DBBase(Of DB_MSora)



        Protected Overrides Sub SetConnectionString()
            'm_strConnectionString = ""
            'm_strConnectionString = Config.GetConnectionStringeProd
        End Sub

    End Class
End Namespace