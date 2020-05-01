Option Explicit On
Option Strict On
Imports MySql.Data.MySqlClient
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Web
Imports System.Data.Common

Namespace cls.base


#Region "QueryCounter class"
    Public NotInheritable Class QueryCounter
        Implements System.IDisposable

        Public Sub New(ByVal sql As String)

        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose

        End Sub

    End Class
#End Region

    Public MustInherit Class DBBase(Of T As DBBase(Of T))
        Inherits BaseStaticOverride(Of T)

        Private Shared m_isoLevel As IsolationLevel = IsolationLevel.ReadUncommitted

        Protected Shared m_strConnectionString As String = Nothing
        Protected Shared m_strConnectionStringKlaes As String = Nothing
        Protected Shared m_strConnectionStringKlaesFenster As String = Nothing
        Protected Shared m_strConnectionStringKlaesFinestre As String = Nothing
        Protected Shared m_strConnectionStringKapa As String = Nothing
        Protected Shared m_strConnectionStringMawi As String = Nothing
        Protected Shared m_strConnectionStringKlaesTools As String = Nothing
        Protected Shared m_strConnectionStringeProd As String = Nothing
        Protected Shared m_strConnectionStringeProdMSora As String = Nothing
        Protected Shared m_strConnectionStringSpica As String = Nothing
        Protected Shared m_strConnectionStringVnosUr As String = Nothing
        Protected MustOverride Sub SetConnectionString()

#Region "DB Access Functions"

        Public Shared ReadOnly Property IsolationLevel() As IsolationLevel
            Get
                Return m_isoLevel
            End Get
        End Property

        Partial Private Shared Sub GetConnectionString()

        End Sub

        ''' <summary>
        ''' Gets Connection out of Web.config
        ''' </summary>
        ''' <returns>Returns SqlConnection</returns>
        Public Shared Function GetConnection(Optional ByVal strConn As String = "") As SqlConnection
            Select Case strConn.ToUpper
                Case "KLAES", "KLAESKBS"
                    If String.IsNullOrEmpty(m_strConnectionStringKlaes) Then
                        m_strConnectionStringKlaes = cls.Config.GetConnectionStringKlaes
                    End If

                    Dim conn As New SqlConnection(m_strConnectionStringKlaes)
                    conn.ConnectionString = m_strConnectionStringKlaes
                    conn.Open()
                    Return conn
                Case "KLAESFENSTER", "FENSTER"
                    If String.IsNullOrEmpty(m_strConnectionStringKlaesFenster) Then
                        m_strConnectionStringKlaesFenster = cls.Config.GetConnectionStringKlaesFenster
                    End If

                    Dim conn As New SqlConnection(m_strConnectionStringKlaesFenster)
                    conn.ConnectionString = m_strConnectionStringKlaesFenster
                    conn.Open()
                    Return conn

                Case "KLAESFINESTRE", "FINESTRE"
                    If String.IsNullOrEmpty(m_strConnectionStringKlaesFinestre) Then
                        m_strConnectionStringKlaesFinestre = cls.Config.GetConnectionStringKlaesFinestre
                    End If

                    Dim conn As New SqlConnection(m_strConnectionStringKlaesFinestre)
                    conn.ConnectionString = m_strConnectionStringKlaesFinestre
                    conn.Open()
                    Return conn
                Case "KAPA", "KLAESKAPA"
                    If String.IsNullOrEmpty(m_strConnectionStringKapa) Then
                        m_strConnectionStringKapa = cls.Config.GetConnectionStringKapa
                    End If

                    Dim conn As New SqlConnection(m_strConnectionStringKapa)
                    conn.ConnectionString = m_strConnectionStringKapa
                    conn.Open()
                    Return conn
                Case "MAWI", "KLAESMAWI"
                    If String.IsNullOrEmpty(m_strConnectionStringMawi) Then
                        m_strConnectionStringMawi = cls.Config.GetConnectionStringMAWI
                    End If

                    Dim conn As New SqlConnection(m_strConnectionStringMawi)
                    conn.ConnectionString = m_strConnectionStringMawi
                    conn.Open()
                    Return conn
                Case "KLAESTOOLS", "TOOLS"
                    If String.IsNullOrEmpty(m_strConnectionStringKlaesTools) Then
                        m_strConnectionStringKlaesTools = cls.Config.GetConnectionStringKlaesTools
                    End If

                    Dim conn As New SqlConnection(m_strConnectionStringKlaesTools)
                    conn.ConnectionString = m_strConnectionStringKlaesTools
                    conn.Open()
                    Return conn
                Case "SPICA"
                    If String.IsNullOrEmpty(m_strConnectionStringSpica) Then
                        m_strConnectionStringSpica = cls.Config.GetConnectionStringSpica
                    End If

                    Dim conn As New SqlConnection(m_strConnectionStringSpica)
                    conn.ConnectionString = m_strConnectionStringSpica
                    conn.Open()
                    Return conn
                Case "VNOSUR"
                    If String.IsNullOrEmpty(m_strConnectionStringVnosUr) Then
                        m_strConnectionStringVnosUr = cls.Config.GetConnectionStringVnosUr
                    End If

                    Dim conn As New SqlConnection(m_strConnectionStringVnosUr)
                    conn.ConnectionString = m_strConnectionStringVnosUr
                    conn.Open()
                    Return conn
            End Select
            Return Nothing
        End Function

        ''' <summary>
        ''' Gets data out of the database
        ''' </summary>
        ''' <param name="cmd">The SQL Command</param>
        ''' <returns>DataTable with the results</returns>
        '''
        Public Shared Function GetData(ByVal cmd As SqlCommand) As DataTable
            Return GetData(cmd, False)
        End Function

        Public Shared Function GetData(ByVal cmd As SqlCommand, ByVal prepare As Boolean) As DataTable
            Return GetData(cmd, DirectCast(Nothing, SqlTransaction), prepare)
        End Function

        Public Shared Function GetData(ByVal cmd As SqlCommand, ByRef SQLtrans As SqlTransaction) As DataTable
            Return GetData(cmd, SQLtrans, False)
        End Function

        Public Shared Function GetData(ByVal cmd As SqlCommand, ByRef SQLtrans As SqlTransaction, ByVal prepare As Boolean) As DataTable
            Dim qc As New QueryCounter(cmd.CommandText)
            Try
                If cmd.Connection IsNot Nothing Then
                    If SQLtrans Is Nothing Then
                        If prepare Then
                            cmd.Prepare()
                        End If

                        Using ds As New DataSet()
                            Using da As New SqlDataAdapter()
                                da.SelectCommand = cmd
                                da.Fill(ds)
                                Return ds.Tables(0)
                            End Using
                        End Using
                    Else

                        cmd.Transaction = SQLtrans

                        Using ds As New DataSet()
                            Using da As New SqlDataAdapter()
                                da.SelectCommand = cmd
                                da.SelectCommand.Connection = SQLtrans.Connection

                                If prepare Then
                                    da.SelectCommand.Prepare()
                                End If

                                da.Fill(ds)
                                Return ds.Tables(0)
                            End Using
                        End Using
                    End If

                Else

                    If SQLtrans Is Nothing Then
                        Using conn As SqlConnection = GetConnection()
                            Using trans As SqlTransaction = conn.BeginTransaction(m_isoLevel)
                                Try
                                    cmd.Transaction = trans
                                    Using ds As New DataSet()
                                        Using da As New SqlDataAdapter()
                                            da.SelectCommand = cmd
                                            da.SelectCommand.Connection = conn

                                            If prepare Then
                                                da.SelectCommand.Prepare()
                                            End If

                                            da.Fill(ds)
                                            Return ds.Tables(0)
                                        End Using
                                    End Using

                                    trans.Commit()
                                Catch ex As Exception
                                    trans.Rollback()
                                    Throw ex
                                End Try
                            End Using
                        End Using
                    Else
                        cmd.Connection = SQLtrans.Connection
                        cmd.Transaction = SQLtrans
                        Using ds As New DataSet()
                            Using da As New SqlDataAdapter()
                                da.SelectCommand = cmd
                                da.SelectCommand.Connection = SQLtrans.Connection

                                If prepare Then
                                    da.SelectCommand.Prepare()
                                End If

                                da.Fill(ds)
                                Return ds.Tables(0)
                            End Using
                        End Using
                    End If
                End If
            Finally
                qc.Dispose()
            End Try
        End Function

        Public Shared Function GetData(ByVal cmd As SqlCommand, ByRef da As SqlDataAdapter) As DataTable
            Return GetData(cmd, da, Nothing, False)
        End Function

        Public Shared Function GetData(ByVal cmd As SqlCommand, ByRef da As SqlDataAdapter, ByVal prepare As Boolean) As DataTable
            Return GetData(cmd, da, Nothing, prepare)
        End Function

        Public Shared Function GetData(ByVal cmd As SqlCommand, ByRef da As SqlDataAdapter, ByRef SQLtrans As SqlTransaction) As DataTable
            Return GetData(cmd, da, SQLtrans, False)
        End Function

        Public Shared Function GetData(ByVal cmd As SqlCommand, ByRef da As SqlDataAdapter, ByRef SQLtrans As SqlTransaction, ByVal prepare As Boolean) As DataTable
            Dim qc As New QueryCounter(cmd.CommandText)
            Try
                If da Is Nothing Then da = New SqlDataAdapter(cmd)
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey

                If cmd.Connection IsNot Nothing Then
                    If SQLtrans Is Nothing Then
                        If prepare Then
                            cmd.Prepare()
                        End If

                        Using ds As New DataSet()
                            da.SelectCommand = cmd
                            da.Fill(ds)
                            Return ds.Tables(0)
                        End Using
                    Else
                        cmd.Transaction = SQLtrans
                        Using ds As New DataSet()
                            da.SelectCommand = cmd
                            da.SelectCommand.Connection = SQLtrans.Connection

                            If prepare Then
                                da.SelectCommand.Prepare()
                            End If

                            da.Fill(ds)
                            Return ds.Tables(0)
                        End Using
                    End If
                Else
                    If SQLtrans Is Nothing Then
                        Using conn As SqlConnection = GetConnection()
                            cmd.Connection = conn
                            Using trans As SqlTransaction = conn.BeginTransaction(m_isoLevel)
                                Try
                                    cmd.Transaction = trans
                                    Using ds As New DataSet()
                                        da.SelectCommand = cmd
                                        da.SelectCommand.Connection = conn

                                        If prepare Then
                                            da.SelectCommand.Prepare()
                                        End If

                                        da.Fill(ds)
                                        Return ds.Tables(0)
                                    End Using

                                    trans.Commit()
                                Catch ex As Exception
                                    trans.Rollback()
                                    Throw ex
                                End Try
                            End Using
                        End Using
                    Else
                        cmd.Connection = SQLtrans.Connection
                        cmd.Transaction = SQLtrans
                        Using ds As New DataSet()
                            da.SelectCommand = cmd
                            da.SelectCommand.Connection = SQLtrans.Connection

                            If prepare Then
                                da.SelectCommand.Prepare()
                            End If

                            da.Fill(ds)
                            Return ds.Tables(0)
                        End Using
                    End If
                End If
            Finally
                qc.Dispose()
            End Try
        End Function

        Public Shared Sub UpdateData(ByRef da As SqlDataAdapter, ByRef dt As DataTable)
            UpdateData(da, dt, Nothing, Nothing)
        End Sub

        Public Shared Sub UpdateData(ByRef da As SqlDataAdapter, ByRef dt As DataTable, ByRef SQLconn As SqlConnection, ByRef SQLtrans As SqlTransaction)
            If SQLconn IsNot Nothing Then
                Using cb = New SqlCommandBuilder(da)
                    da.SelectCommand.Connection = SQLconn
                    da.SelectCommand.Transaction = SQLtrans

                    da.UpdateCommand = cb.GetUpdateCommand
                    da.UpdateCommand.Connection = SQLconn
                    da.UpdateCommand.Transaction = SQLtrans

                    da.InsertCommand = cb.GetInsertCommand
                    da.InsertCommand.Connection = SQLconn
                    da.InsertCommand.Transaction = SQLtrans

                    da.DeleteCommand = cb.GetDeleteCommand
                    da.DeleteCommand.Connection = SQLconn
                    da.DeleteCommand.Transaction = SQLtrans

                    da.Update(dt)
                End Using
            Else
                Using conn As SqlConnection = GetConnection()
                    Using cb = New SqlCommandBuilder(da)
                        da.SelectCommand.Connection = conn

                        da.UpdateCommand = cb.GetUpdateCommand
                        da.UpdateCommand.Connection = conn

                        da.InsertCommand = cb.GetInsertCommand
                        da.InsertCommand.Connection = conn

                        da.DeleteCommand = cb.GetDeleteCommand
                        da.DeleteCommand.Connection = conn

                        da.Update(dt)
                    End Using
                End Using
            End If
        End Sub

        ''' <summary>
        ''' Gets data out of database using a plain text string command
        ''' </summary>
        ''' <param name="sql">string command to be executed</param>
        ''' <returns>DataTable with results</returns>
        ''' 
        Public Shared Function GetData(ByVal sql As String) As DataTable
            Return GetData(sql, Nothing)
        End Function

        Public Shared Function GetData(ByVal sql As String, ByRef SQLtrans As SqlTransaction) As DataTable
            Dim qc As New QueryCounter(sql)
            Try
                Using conn As SqlConnection = GetConnection()
                    If SQLtrans Is Nothing Then
                        Using trans As SqlTransaction = conn.BeginTransaction(m_isoLevel)
                            Try
                                Using cmd As SqlCommand = conn.CreateCommand()
                                    cmd.Transaction = trans
                                    cmd.CommandType = CommandType.Text
                                    cmd.CommandText = sql
                                    Using ds As New DataSet()
                                        Using da As New SqlDataAdapter()
                                            da.SelectCommand = cmd
                                            da.SelectCommand.Connection = conn
                                            da.Fill(ds)
                                            Return ds.Tables(0)
                                        End Using
                                    End Using
                                End Using

                                trans.Commit()
                            Catch ex As Exception
                                trans.Rollback()
                                Throw ex
                            End Try
                        End Using
                    Else
                        Using cmd As SqlCommand = conn.CreateCommand()
                            cmd.Transaction = SQLtrans
                            cmd.CommandType = CommandType.Text
                            cmd.CommandText = sql
                            Using ds As New DataSet()
                                Using da As New SqlDataAdapter()
                                    da.SelectCommand = cmd
                                    da.SelectCommand.Connection = SQLtrans.Connection
                                    da.Fill(ds)
                                    Return ds.Tables(0)
                                End Using
                            End Using
                        End Using
                    End If
                End Using
            Finally
                qc.Dispose()
            End Try
        End Function

        Public Shared Function GetData(ByVal sql As String, ByRef da As SqlDataAdapter, ByRef ds As DataSet, ByRef SQLtrans As SqlTransaction) As DataTable
            Return GetData(sql, da, Nothing)
        End Function

        Public Shared Function GetData(ByVal sql As String, ByRef da As SqlDataAdapter, ByRef SQLtrans As SqlTransaction) As DataTable
            Dim qc As New QueryCounter(sql)
            Try
                If da Is Nothing Then da = New SqlDataAdapter()
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey

                Using conn As SqlConnection = GetConnection()
                    If SQLtrans Is Nothing Then
                        Using trans As SqlTransaction = conn.BeginTransaction(m_isoLevel)
                            Try
                                Using cmd As SqlCommand = conn.CreateCommand()
                                    cmd.Transaction = trans
                                    cmd.CommandType = CommandType.Text
                                    cmd.CommandText = sql
                                    Using ds As New DataSet()
                                        da.SelectCommand = cmd
                                        da.SelectCommand.Connection = conn
                                        da.Fill(ds)
                                        Return ds.Tables(0)
                                    End Using
                                End Using

                                trans.Commit()
                            Catch ex As Exception
                                trans.Rollback()
                                Throw ex
                            End Try
                        End Using
                    Else
                        Using cmd As SqlCommand = conn.CreateCommand()
                            cmd.Transaction = SQLtrans
                            cmd.CommandType = CommandType.Text
                            cmd.CommandText = sql
                            Using ds As New DataSet()
                                da.SelectCommand = cmd
                                da.SelectCommand.Connection = SQLtrans.Connection
                                da.Fill(ds)
                                Return ds.Tables(0)
                            End Using
                        End Using
                    End If
                End Using
            Finally
                qc.Dispose()
            End Try
        End Function

        ''' <summary>
        ''' Executes a NonQuery
        ''' </summary>
        ''' <param name="cmd">NonQuery to execute</param>
        Public Shared Function ExecuteNonQuery(ByVal cmd As SqlCommand) As Integer
            Return ExecuteNonQuery(cmd, False)
        End Function

        Public Shared Function ExecuteNonQuery(ByVal cmd As SqlCommand, ByVal prepare As Boolean) As Integer
            Return ExecuteNonQuery(cmd, Nothing, prepare)
        End Function

        Public Shared Function ExecuteNonQuery(ByVal cmd As SqlCommand, ByRef SQLtrans As SqlTransaction, ByVal prepare As Boolean) As Integer
            Dim qc As New QueryCounter(cmd.CommandText)
            Dim intReturn As Integer = -1

            Try
                If cmd.Connection IsNot Nothing Then
                    If prepare Then
                        cmd.Prepare()
                    End If

                    If SQLtrans Is Nothing Then
                        Using trans As SqlTransaction = cmd.Connection.BeginTransaction(m_isoLevel)
                            cmd.Transaction = trans
                            intReturn = cmd.ExecuteNonQuery()
                            trans.Commit()
                        End Using
                    Else
                        cmd.Transaction = SQLtrans
                        intReturn = cmd.ExecuteNonQuery()
                    End If

                Else
                    Using conn As SqlConnection = GetConnection()
                        If SQLtrans Is Nothing Then
                            Using trans As SqlTransaction = conn.BeginTransaction(m_isoLevel)
                                cmd.Connection = conn
                                cmd.Transaction = trans

                                If prepare Then
                                    cmd.Prepare()
                                End If

                                intReturn = cmd.ExecuteNonQuery()
                                trans.Commit()
                            End Using
                        Else
                            cmd.Connection = SQLtrans.Connection
                            cmd.Transaction = SQLtrans

                            If prepare Then
                                cmd.Prepare()
                            End If

                            intReturn = cmd.ExecuteNonQuery()
                        End If
                    End Using
                End If
            Finally
                qc.Dispose()
            End Try

            Return intReturn
        End Function


        Protected Shared Function ExecuteScalar(ByVal cmd As SqlCommand) As Object
            Dim qc As New QueryCounter(cmd.CommandText)
            Try
                Using conn As SqlConnection = GetConnection()
                    Using trans As SqlTransaction = conn.BeginTransaction(m_isoLevel)
                        cmd.Connection = conn
                        cmd.Transaction = trans
                        Dim res As Object = cmd.ExecuteScalar()
                        trans.Commit()
                        Return res
                    End Using
                End Using
            Finally
                qc.Dispose()
            End Try
        End Function

        ''' <summary>
        ''' Gets the database size
        ''' </summary>
        ''' <returns>intager value for database size</returns>
        Protected Shared Function DBSize() As Integer
            Using cmd As New SqlCommand("select sum(cast(size as integer))/128 from sysfiles")
                cmd.CommandType = CommandType.Text
                Return CInt(ExecuteScalar(cmd))
            End Using
        End Function


        Protected Shared Function GetColumnLength(ByRef strTable As String, ByRef strColumn As String) As Integer
            'delim z dva ker je unicode
            Using cmd As New SqlCommand("SELECT x=col_length(@table, @column)/2")
                cmd.Parameters.AddWithValue("@table", strTable)
                cmd.Parameters.AddWithValue("@column", strColumn)
                cmd.CommandType = CommandType.Text
                Return CInt(ExecuteScalar(cmd))
            End Using
        End Function
#End Region

#Region "MySQL"
        Public Shared Function GetMyConnection(Optional ByVal strConn As String = "") As MySqlConnection
            Select Case strConn.ToUpper
                Case "EPROD", ""
                    If String.IsNullOrEmpty(m_strConnectionStringeProd) Then
                        m_strConnectionStringeProd = cls.Config.GetConnectionStringeProd
                    End If

                    Dim conn As New MySqlConnection(m_strConnectionStringeProd)
                    conn.ConnectionString = m_strConnectionStringeProd
                    conn.Open()
                    Return conn
                Case "MSORA"
                    If String.IsNullOrEmpty(m_strConnectionStringeProdMSora) Then
                        m_strConnectionStringeProdMSora = cls.Config.GetConnectionStringeProdMSora
                    End If

                    Dim conn As New MySqlConnection(m_strConnectionStringeProdMSora)
                    conn.ConnectionString = m_strConnectionStringeProdMSora
                    conn.Open()
                    Return conn

            End Select
            Return Nothing
        End Function

        ''' <summary>
        ''' Gets data out of the database
        ''' </summary>
        ''' <param name="cmd">The SQL Command</param>
        ''' <returns>DataTable with the results</returns>
        '''
        Public Shared Function GetMyData(ByVal cmd As MySqlCommand) As DataTable
            Return GetMyData(cmd, False)
        End Function

        Public Shared Function GetMyData(ByVal cmd As MySqlCommand, ByVal prepare As Boolean) As DataTable
            Return GetMyData(cmd, DirectCast(Nothing, MySqlTransaction), prepare)
        End Function

        Public Shared Function GetMyData(ByVal cmd As MySqlCommand, ByRef SQLtrans As MySqlTransaction) As DataTable
            Return GetMyData(cmd, SQLtrans, False)
        End Function

        Public Shared Function GetMyData(ByVal cmd As MySqlCommand, ByRef SQLtrans As MySqlTransaction, ByVal prepare As Boolean) As DataTable
            Dim qc As New QueryCounter(cmd.CommandText)
            Try
                If cmd.Connection IsNot Nothing Then
                    If SQLtrans Is Nothing Then
                        If prepare Then
                            cmd.Prepare()
                        End If

                        Using ds As New DataSet()
                            Using da As New MySqlDataAdapter()
                                da.SelectCommand = cmd
                                da.Fill(ds)
                                Return ds.Tables(0)
                            End Using
                        End Using
                    Else

                        cmd.Transaction = SQLtrans

                        Using ds As New DataSet()
                            Using da As New MySqlDataAdapter()
                                da.SelectCommand = cmd
                                da.SelectCommand.Connection = SQLtrans.Connection

                                If prepare Then
                                    da.SelectCommand.Prepare()
                                End If

                                da.Fill(ds)
                                Return ds.Tables(0)
                            End Using
                        End Using
                    End If

                Else

                    If SQLtrans Is Nothing Then
                        Using conn As MySqlConnection = GetMyConnection()
                            Using trans As MySqlTransaction = conn.BeginTransaction(m_isoLevel)
                                Try
                                    cmd.Transaction = trans
                                    Using ds As New DataSet()
                                        Using da As New MySqlDataAdapter()
                                            da.SelectCommand = cmd
                                            da.SelectCommand.Connection = conn

                                            If prepare Then
                                                da.SelectCommand.Prepare()
                                            End If

                                            da.Fill(ds)
                                            Return ds.Tables(0)
                                        End Using
                                    End Using

                                    trans.Commit()
                                Catch ex As Exception
                                    trans.Rollback()
                                    Throw ex
                                End Try
                            End Using
                        End Using
                    Else
                        cmd.Connection = SQLtrans.Connection
                        cmd.Transaction = SQLtrans
                        Using ds As New DataSet()
                            Using da As New MySqlDataAdapter()
                                da.SelectCommand = cmd
                                da.SelectCommand.Connection = SQLtrans.Connection

                                If prepare Then
                                    da.SelectCommand.Prepare()
                                End If

                                da.Fill(ds)
                                Return ds.Tables(0)
                            End Using
                        End Using
                    End If
                End If
            Finally
                qc.Dispose()
            End Try
        End Function

        Public Shared Function GetMyData(ByVal cmd As MySqlCommand, ByRef da As MySqlDataAdapter) As DataTable
            Return GetMyData(cmd, da, Nothing, False)
        End Function

        Public Shared Function GetMyData(ByVal cmd As MySqlCommand, ByRef da As MySqlDataAdapter, ByVal prepare As Boolean) As DataTable
            Return GetMyData(cmd, da, Nothing, prepare)
        End Function

        Public Shared Function GetMyData(ByVal cmd As MySqlCommand, ByRef da As MySqlDataAdapter, ByRef SQLtrans As MySqlTransaction) As DataTable
            Return GetMyData(cmd, da, SQLtrans, False)
        End Function

        Public Shared Function GetMyData(ByVal cmd As MySqlCommand, ByRef da As MySqlDataAdapter, ByRef SQLtrans As MySqlTransaction, ByVal prepare As Boolean) As DataTable
            Dim qc As New QueryCounter(cmd.CommandText)
            Try
                If da Is Nothing Then da = New MySqlDataAdapter(cmd)
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey

                If cmd.Connection IsNot Nothing Then
                    If SQLtrans Is Nothing Then
                        If prepare Then
                            cmd.Prepare()
                        End If

                        Using ds As New DataSet()
                            da.SelectCommand = cmd
                            da.Fill(ds)
                            Return ds.Tables(0)
                        End Using
                    Else
                        cmd.Transaction = SQLtrans
                        Using ds As New DataSet()
                            da.SelectCommand = cmd
                            da.SelectCommand.Connection = SQLtrans.Connection

                            If prepare Then
                                da.SelectCommand.Prepare()
                            End If

                            da.Fill(ds)
                            Return ds.Tables(0)
                        End Using
                    End If
                Else
                    If SQLtrans Is Nothing Then
                        Using conn As MySqlConnection = GetMyConnection()
                            cmd.Connection = conn
                            Using trans As MySqlTransaction = conn.BeginTransaction(m_isoLevel)
                                Try
                                    cmd.Transaction = trans
                                    Using ds As New DataSet()
                                        da.SelectCommand = cmd
                                        da.SelectCommand.Connection = conn

                                        If prepare Then
                                            da.SelectCommand.Prepare()
                                        End If

                                        da.Fill(ds)
                                        Return ds.Tables(0)
                                    End Using

                                    trans.Commit()
                                Catch ex As Exception
                                    trans.Rollback()
                                    Throw ex
                                End Try
                            End Using
                        End Using
                    Else
                        cmd.Connection = SQLtrans.Connection
                        cmd.Transaction = SQLtrans
                        Using ds As New DataSet()
                            da.SelectCommand = cmd
                            da.SelectCommand.Connection = SQLtrans.Connection

                            If prepare Then
                                da.SelectCommand.Prepare()
                            End If

                            da.Fill(ds)
                            Return ds.Tables(0)
                        End Using
                    End If
                End If
            Finally
                qc.Dispose()
            End Try
        End Function

        Public Shared Sub UpdateMyData(ByRef da As MySqlDataAdapter, ByRef dt As DataTable)
            UpdateMyData(da, dt, Nothing, Nothing)
        End Sub

        Public Shared Sub UpdateMyData(ByRef da As MySqlDataAdapter, ByRef dt As DataTable, ByRef SQLconn As MySqlConnection, ByRef SQLtrans As MySqlTransaction)
            If SQLconn IsNot Nothing Then
                Using cb = New MySqlCommandBuilder(da)
                    da.SelectCommand.Connection = SQLconn
                    da.SelectCommand.Transaction = SQLtrans

                    da.UpdateCommand = cb.GetUpdateCommand
                    da.UpdateCommand.Connection = SQLconn
                    da.UpdateCommand.Transaction = SQLtrans

                    da.InsertCommand = cb.GetInsertCommand
                    da.InsertCommand.Connection = SQLconn
                    da.InsertCommand.Transaction = SQLtrans

                    da.DeleteCommand = cb.GetDeleteCommand
                    da.DeleteCommand.Connection = SQLconn
                    da.DeleteCommand.Transaction = SQLtrans

                    da.Update(dt)
                End Using
            Else
                Using conn As MySqlConnection = GetMyConnection()
                    Using cb = New MySql.Data.MySqlClient.MySqlCommandBuilder(da)
                        da.SelectCommand.Connection = conn

                        da.UpdateCommand = cb.GetUpdateCommand
                        da.UpdateCommand.Connection = conn

                        da.InsertCommand = cb.GetInsertCommand
                        da.InsertCommand.Connection = conn

                        da.DeleteCommand = cb.GetDeleteCommand
                        da.DeleteCommand.Connection = conn

                        da.Update(dt)
                    End Using
                End Using
            End If
        End Sub

        ''' <summary>
        ''' Gets data out of database using a plain text string command
        ''' </summary>
        ''' <param name="sql">string command to be executed</param>
        ''' <returns>DataTable with results</returns>
        ''' 
        Public Shared Function GetMyData(ByVal sql As String) As DataTable
            Return GetMyData(sql, Nothing)
        End Function

        Public Shared Function GetMyData(ByVal sql As String, ByRef SQLtrans As MySqlTransaction) As DataTable
            Dim qc As New QueryCounter(sql)
            Try
                Using conn As MySqlConnection = GetMyConnection()
                    If SQLtrans Is Nothing Then
                        Using trans As MySqlTransaction = conn.BeginTransaction(m_isoLevel)
                            Try
                                Using cmd As MySqlCommand = conn.CreateCommand()
                                    cmd.Transaction = trans
                                    cmd.CommandType = CommandType.Text
                                    cmd.CommandText = sql
                                    Using ds As New DataSet()
                                        Using da As New MySqlDataAdapter()
                                            da.SelectCommand = cmd
                                            da.SelectCommand.Connection = conn
                                            da.Fill(ds)
                                            Return ds.Tables(0)
                                        End Using
                                    End Using
                                End Using

                                trans.Commit()
                            Catch ex As Exception
                                trans.Rollback()
                                Throw ex
                            End Try
                        End Using
                    Else
                        Using cmd As MySqlCommand = conn.CreateCommand()
                            cmd.Transaction = SQLtrans
                            cmd.CommandType = CommandType.Text
                            cmd.CommandText = sql
                            Using ds As New DataSet()
                                Using da As New MySqlDataAdapter()
                                    da.SelectCommand = cmd
                                    da.SelectCommand.Connection = SQLtrans.Connection
                                    da.Fill(ds)
                                    Return ds.Tables(0)
                                End Using
                            End Using
                        End Using
                    End If
                End Using
            Finally
                qc.Dispose()
            End Try
        End Function

        Public Shared Function GetMyData(ByVal sql As String, ByRef da As MySqlDataAdapter, ByRef ds As DataSet, ByRef SQLtrans As MySqlTransaction) As DataTable
            Return GetMyData(sql, da, Nothing)
        End Function

        Public Shared Function GetMyData(ByVal sql As String, ByRef da As MySqlDataAdapter, ByRef SQLtrans As MySqlTransaction) As DataTable
            Dim qc As New QueryCounter(sql)
            Try
                If da Is Nothing Then da = New MySqlDataAdapter()
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey

                Using conn As MySqlConnection = GetMyConnection()
                    If SQLtrans Is Nothing Then
                        Using trans As MySqlTransaction = conn.BeginTransaction(m_isoLevel)
                            Try
                                Using cmd As MySqlCommand = conn.CreateCommand()
                                    cmd.Transaction = trans
                                    cmd.CommandType = CommandType.Text
                                    cmd.CommandText = sql
                                    Using ds As New DataSet()
                                        da.SelectCommand = cmd
                                        da.SelectCommand.Connection = conn
                                        da.Fill(ds)
                                        Return ds.Tables(0)
                                    End Using
                                End Using

                                trans.Commit()
                            Catch ex As Exception
                                trans.Rollback()
                                Throw ex
                            End Try
                        End Using
                    Else
                        Using cmd As MySqlCommand = conn.CreateCommand()
                            cmd.Transaction = SQLtrans
                            cmd.CommandType = CommandType.Text
                            cmd.CommandText = sql
                            Using ds As New DataSet()
                                da.SelectCommand = cmd
                                da.SelectCommand.Connection = SQLtrans.Connection
                                da.Fill(ds)
                                Return ds.Tables(0)
                            End Using
                        End Using
                    End If
                End Using
            Finally
                qc.Dispose()
            End Try
        End Function

        ''' <summary>
        ''' Executes a NonQuery
        ''' </summary>
        ''' <param name="cmd">NonQuery to execute</param>
        Public Shared Function ExecuteMyNonQuery(ByVal cmd As MySqlCommand) As Integer
            Return ExecuteMyNonQuery(cmd, False)
        End Function

        Public Shared Function ExecuteMyNonQuery(ByVal cmd As MySqlCommand, ByVal prepare As Boolean) As Integer
            Return ExecuteMyNonQuery(cmd, Nothing, prepare)
        End Function

        Public Shared Function ExecuteMyNonQuery(ByVal cmd As MySqlCommand, ByRef SQLtrans As MySqlTransaction, ByVal prepare As Boolean) As Integer
            Dim qc As New QueryCounter(cmd.CommandText)
            Dim intReturn As Integer = -1

            Try
                If cmd.Connection IsNot Nothing Then
                    If prepare Then
                        cmd.Prepare()
                    End If

                    If SQLtrans Is Nothing Then
                        Using trans As MySqlTransaction = cmd.Connection.BeginTransaction(m_isoLevel)
                            cmd.Transaction = trans
                            intReturn = cmd.ExecuteNonQuery()
                            trans.Commit()
                        End Using
                    Else
                        cmd.Transaction = SQLtrans
                        intReturn = cmd.ExecuteNonQuery()
                    End If

                Else
                    Using conn As MySqlConnection = GetMyConnection()
                        If SQLtrans Is Nothing Then
                            Using trans As MySqlTransaction = conn.BeginTransaction(m_isoLevel)
                                cmd.Connection = conn
                                cmd.Transaction = trans

                                If prepare Then
                                    cmd.Prepare()
                                End If

                                intReturn = cmd.ExecuteNonQuery()
                                trans.Commit()
                            End Using
                        Else
                            cmd.Connection = SQLtrans.Connection
                            cmd.Transaction = SQLtrans

                            If prepare Then
                                cmd.Prepare()
                            End If

                            intReturn = cmd.ExecuteNonQuery()
                        End If
                    End Using
                End If
            Finally
                qc.Dispose()
            End Try

            Return intReturn
        End Function


        Protected Shared Function ExecuteMyScalar(ByVal cmd As MySqlCommand) As Object
            Dim qc As New QueryCounter(cmd.CommandText)
            Try
                Using conn As MySqlConnection = GetMyConnection()
                    Using trans As MySqlTransaction = conn.BeginTransaction(m_isoLevel)
                        cmd.Connection = conn
                        cmd.Transaction = trans
                        Dim res As Object = cmd.ExecuteScalar()
                        trans.Commit()
                        Return res
                    End Using
                End Using
            Finally
                qc.Dispose()
            End Try
        End Function

        ''' <summary>
        ''' Gets the database size
        ''' </summary>
        ''' <returns>intager value for database size</returns>
        Protected Shared Function MyDBSize() As Integer
            Using cmd As New MySqlCommand("select sum(cast(size as integer))/128 from sysfiles")
                cmd.CommandType = CommandType.Text
                Return CInt(ExecuteMyScalar(cmd))
            End Using
        End Function


        Protected Shared Function GetMyColumnLength(ByRef strTable As String, ByRef strColumn As String) As Integer
            'delim z dva ker je unicode
            Using cmd As New MySqlCommand("SELECT x=col_length(@table, @column)/2")
                cmd.Parameters.AddWithValue("@table", strTable)
                cmd.Parameters.AddWithValue("@column", strColumn)
                cmd.CommandType = CommandType.Text
                Return CInt(ExecuteMyScalar(cmd))
            End Using
        End Function
#End Region




#Region "MiscFunctions"

        Public Shared Function Int2DB(ByVal i As Integer) As Object
            If i < 0 Then
                Return DBNull.Value
            End If

            Return i
        End Function

        Public Shared Function Str2DB(ByRef o As Object) As Object
            If o Is Nothing Then
                Return DBNull.Value
            End If

            Return Str2DB(o.ToString)
        End Function

        Public Shared Function Str2DB(ByRef str As String) As Object
            If String.IsNullOrEmpty(str) Then
                Return DBNull.Value
            End If

            Return str.Trim()
        End Function

        Public Shared Function Str2DB(ByRef o As Object, ByVal maxLen As Integer) As Object
            If o Is Nothing Then
                Return DBNull.Value
            End If

            Return Str2DB(o.ToString, maxLen)
        End Function

        Public Shared Function Str2DB(ByRef str As String, ByVal maxLen As Integer) As Object
            If String.IsNullOrEmpty(str) Then
                Return DBNull.Value
            End If

            If str.Length > maxLen Then
                Return str.Substring(0, maxLen)
            End If

            Return str.Trim()
        End Function

        Public Shared Function DB2Str(ByVal str As Object) As String
            If str Is Nothing OrElse DBNull.Value.Equals(str) Then
                Return String.Empty
            End If

            Return DirectCast(str, String).Trim()
        End Function

        Public Shared Function DB2Int(ByVal i As Object) As Integer
            If i Is Nothing OrElse DBNull.Value.Equals(i) Then
                Return -1
            End If

            If TypeOf i Is Integer Then
                Return DirectCast(i, Integer)
            Else
                Return Convert.ToInt32(i)
            End If
        End Function

        Public Shared Function DB2Lng(ByVal i As Object) As Long
            If i Is Nothing OrElse DBNull.Value.Equals(i) Then
                Return -1
            End If

            If TypeOf i Is Long Then
                Return DirectCast(i, Long)
            Else
                Return Convert.ToInt64(i)
            End If
        End Function


        Public Shared Function DB2Dbl(ByVal i As Object) As Double
            If i Is Nothing OrElse DBNull.Value.Equals(i) Then
                Return 0.0
            End If

            If TypeOf i Is Double Then
                Return DirectCast(i, Double)
            Else
                Return Convert.ToDouble(i)
            End If
        End Function

        Public Shared Function DB2IntZero(ByVal i As Object) As Integer
            If i Is Nothing OrElse DBNull.Value.Equals(i) Then
                Return 0
            End If

            If TypeOf i Is Integer Then
                Return DirectCast(i, Integer)
            Else
                Return Convert.ToInt32(i)
            End If
        End Function

        Public Shared Function DB2ShortZero(ByVal i As Object) As Short
            If i Is Nothing OrElse DBNull.Value.Equals(i) Then
                Return 0
            End If

            If TypeOf i Is Integer Then
                Return DirectCast(i, Short)
            Else
                Return Convert.ToInt16(i)
            End If
        End Function
        Public Shared Function SQL_SetDateFormat(ByVal dtmDate As Date, Optional ByVal strFormat As String = "", Optional ByVal blnSkipAppostrof As Boolean = False) As String

            strFormat = "yyyy-MM-dd"

            If blnSkipAppostrof Then
                SQL_SetDateFormat = Format$(dtmDate, strFormat)
            Else
                SQL_SetDateFormat = "'" & Format$(dtmDate, strFormat) & "'"
            End If

        End Function



#End Region
    End Class

End Namespace
