Option Explicit On
Option Strict On

Imports System
Imports System.Text
Imports System.Runtime.InteropServices

    Public Class cIni
        <DllImport("kernel32", SetLastError:=True)> _
        Private Shared Function WritePrivateProfileString(ByVal psSection As String, ByVal psKey As String, ByVal psValue As String, ByVal psFile As String) As Integer
        End Function
        <DllImport("kernel32", SetLastError:=True)> _
        Private Shared Function GetPrivateProfileString(ByVal psSection As String, ByVal psKey As String, ByVal psDefault As String, ByVal psrReturn As Byte(), ByVal piBufferLen As Integer, ByVal psFile As String) As Integer
        End Function

        Private lsIniFilename As String
        Private liBufferLen As Integer = 255

        ''' <summary>
        ''' Declare INI Class
        ''' </summary>
        Public Sub New(ByVal psIniFilename As String)
        lsIniFilename = psIniFilename
        End Sub

        ''' <summary>
        ''' INI Path and Filename
        ''' </summary>
        Public Property IniFile() As String
            Get
                Return lsIniFilename
            End Get
            Set(ByVal value As String)
                lsIniFilename = value
            End Set
        End Property

        ''' <summary>
        ''' Max return length when reading data
        ''' </summary>
        Public Property BufferLen() As Integer
            Get
                Return liBufferLen
            End Get
            Set(ByVal value As Integer)
                liBufferLen = value
            End Set
        End Property

        ''' <summary>
        ''' Read value from INI File
        ''' </summary>
        Public Function ReadValue(ByVal psSection As String, ByVal psKey As String, ByVal psDefault As String) As String
            Dim sGetBuffer As Byte() = New Byte(Me.liBufferLen - 1) {}
            Dim oAscii As New ASCIIEncoding()
            Dim i As Integer = GetPrivateProfileString(psSection, psKey, psDefault, sGetBuffer, Me.liBufferLen, Me.lsIniFilename)
            Return oAscii.GetString(sGetBuffer, 0, i)
        End Function

        ''' <summary>
        ''' Write value to INI File
        ''' </summary>
        Public Sub WriteValue(ByVal psSection As String, ByVal psKey As String, ByVal psValue As String)
            WritePrivateProfileString(psSection, psKey, psValue, Me.lsIniFilename)
        End Sub

        ''' <summary>
        ''' Remove value from INI File
        ''' </summary>
        Public Sub RemoveValue(ByVal psSection As String, ByVal psKey As String)
            WritePrivateProfileString(psSection, psKey, Nothing, Me.lsIniFilename)
        End Sub

        ''' <summary>
        ''' Read values in a section from INI File
        ''' </summary>
        Public Sub ReadValues(ByVal psSection As String, ByRef poValues As Array)
            Dim sGetBuffer As Byte() = New Byte(Me.liBufferLen - 1) {}
            Dim i As Integer = GetPrivateProfileString(psSection, Nothing, Nothing, sGetBuffer, Me.liBufferLen, Me.lsIniFilename)
            If i <> 0 Then
                Dim oAscii As New ASCIIEncoding()
                poValues = oAscii.GetString(sGetBuffer, 0, i - 1).Split(CChar(ChrW(0)))
            End If
        End Sub

        ''' <summary>
        ''' Read sections from INI File
        ''' </summary>
        Public Sub ReadSections(ByRef poSections As Array)
            Dim sGetBuffer As Byte() = New Byte(Me.liBufferLen - 1) {}
            Dim i As Integer = GetPrivateProfileString(Nothing, Nothing, Nothing, sGetBuffer, Me.liBufferLen, Me.lsIniFilename)
            If i <> 0 Then
                Dim oAscii As New ASCIIEncoding()
                poSections = oAscii.GetString(sGetBuffer, 0, i - 1).Split(CChar(ChrW(0)))
            End If
        End Sub

        ''' <summary>
        ''' Remove section from INI File
        ''' </summary>
        Public Sub RemoveSection(ByVal psSection As String)
            WritePrivateProfileString(psSection, Nothing, Nothing, Me.lsIniFilename)
        End Sub

    End Class


