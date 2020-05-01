Imports System.Text.RegularExpressions


Namespace cls

    Public Class Utils
        'decimal pretvorba z decimalno piko
        Private Shared numPrint As System.Globalization.NumberFormatInfo = Init_NumberFormatInfo_Print()
        Private Shared numSys As System.Globalization.NumberFormatInfo = Init_NumberFormatInfo_Sys()
        Private Shared datetimeSys As System.Globalization.DateTimeFormatInfo = Init_DateTimeFormatInfo_Sys()

        Private Shared Function Init_DateTimeFormatInfo_Sys() As System.Globalization.DateTimeFormatInfo
            Dim dtf As New System.Globalization.DateTimeFormatInfo()
            dtf.TimeSeparator = ":"
            dtf.DateSeparator = "-"

            Return dtf
        End Function
        Private Shared Function Init_NumberFormatInfo_Sys() As System.Globalization.NumberFormatInfo
            Dim numf As New System.Globalization.NumberFormatInfo()
            numf.CurrencyDecimalSeparator = "."
            numf.CurrencyGroupSeparator = ","

            numf.NumberDecimalSeparator = "."
            numf.NumberGroupSeparator = ","

            Return numf
        End Function

        Private Shared Function Init_NumberFormatInfo_Print() As System.Globalization.NumberFormatInfo
            Dim numf As New System.Globalization.NumberFormatInfo()
            numf.CurrencyDecimalSeparator = ","
            numf.CurrencyGroupSeparator = "."
            numf.CurrencyDecimalDigits = 2
            numf.CurrencySymbol = "EUR"

            numf.NumberDecimalSeparator = ","
            numf.NumberGroupSeparator = "."

            Return numf
        End Function


        Public Shared Function CountCharacter(ByVal value As String, ByVal ch As Char) As Integer
            Dim cnt As Integer = 0
            For Each c As Char In value
                If c = ch Then cnt += 1
            Next
            Return cnt
        End Function

        Public Shared Function StrToNumStr(ByVal strNum As String) As String


            Dim blnWrite As Boolean
            Dim tmpStr As String = String.Empty
            Dim strDecimal As String = String.Empty
            Dim strSeparator As String = String.Empty
            Dim i As Integer
            Dim strZnak As String = String.Empty
            Dim strNumDecimal As String = String.Empty
            Dim strNumSeparator As String = String.Empty

            blnWrite = False

            strDecimal = numSys.NumberDecimalSeparator

            strDecimal = System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator
            strSeparator = System.Globalization.NumberFormatInfo.CurrentInfo.NumberGroupSeparator

            tmpStr = strNum

            If Not IsNumeric(strNum) Then
                Return strNum
            Else
                'pike in vejice zamenjam z gstrDecima
                's tem brisem morebitne oznake za tisocice

                'pogledam, kaj je zadnji znak (. ali ,)
                For i = Len(strNum) To 1 Step -1
                    strZnak = Mid(strNum, i, 1)
                    If strZnak = "." Or strZnak = "," Then
                        If strZnak = "." Then
                            strNumDecimal = "."
                            strNumSeparator = ","
                            Exit For
                        End If
                    End If
                Next
                'pobrisemo separatorje tisočic
                tmpStr = tmpStr.Replace(strNumSeparator, "")
                'zamenjamo decimalni znak
                tmpStr = tmpStr.Replace(strNumDecimal, strDecimal)
                Return tmpStr
            End If


        End Function


        'pretvorba 100000000.56 -> double
        Public Shared Function SysStr2Double(ByVal str As String) As Double
            If String.IsNullOrEmpty(str) Then
                Return Nothing
            End If

            Return Double.Parse(str, numSys)
        End Function

        'double -> 100000000.56 
        Public Shared Function Double2SysStr(ByVal dbl As Double) As String

            Return dbl.ToString(numSys)
        End Function

        'pretvorba 100.000.000,56 -> 100000000.56
        Public Shared Function String2Double(ByVal str As String) As Double
            If String.IsNullOrEmpty(str) Then
                Return Nothing
            End If

            Return Double.Parse(str, numPrint)
        End Function

        'pretvorba 100,000,000.56 -> 100.000.000,56
        Public Shared Function Double2String(ByVal dbl As Double) As String
            'decimal pretvorba z decimalno piko

            Return dbl.ToString(numPrint)
        End Function

        Public Shared Function Double2Price(ByVal price As Double) As String
            'decimal pretvorba z decimalno piko

            Return price.ToString("#,#0.00", numPrint)
        End Function

        'pretvorba 100,000,000.56 -> 100.000.000,56
        Public Shared Function ConvertPriceDecimal(ByVal price As String) As String
            If String.IsNullOrEmpty(price) Then
                Return Nothing
            End If

            Dim tmp As String = price.Trim().Replace("."c, "#"c)
            tmp = tmp.Replace(","c, "."c)

            Return tmp.Replace("#"c, ","c)
        End Function

        Public Shared Function IsValidEmail(ByVal email As String, ByVal strRegularExpression As String) As Boolean
            'string strEmailRegex = @"^([0-9a-z]+[-._+&])*[0-9a-z]+@([-0-9a-z]+[.])+[a-z]{2,6}$";

            If String.IsNullOrEmpty(email) Then
                Return False
            End If

            Return Regex.IsMatch(email.Trim, strRegularExpression, RegexOptions.IgnoreCase)
        End Function

        Public Shared Function IsValidPhone(ByVal s As String) As Boolean
            If s Is Nothing Then
                Return False
            End If

            Dim tmp As String = s.Trim()
            If tmp.Length = 0 Then
                Return True
            ElseIf tmp.Length > 6 Then
                Dim ca As Char() = tmp.ToCharArray()
                For i As Integer = 0 To ca.Length - 1

                    If [Char].IsDigit(ca(i)) OrElse ca(i) = "("c Or ca(i) = ")"c OrElse ca(i) = "+"c OrElse ca(i) = " "c OrElse ca(i) = " "c OrElse ca(i) = "-"c OrElse ca(i) = "/"c Then
                    Else
                        Return False
                    End If
                Next

                Return True
            End If

            Return False
        End Function

        Public Shared Function IsValidFax(ByVal s As String) As Boolean
            Return IsValidPhone(s)
        End Function
        Public Shared Function DodajBack(ByVal s As String) As String

            If Right$(s, 1) <> "\" Then
                Return s & "\"
            Else
                Return s
            End If

        End Function
        Public Shared Function GetParameter(ByVal strParameter As String, ByVal intArgument As Integer, Optional ByVal strChar As String = ",") As String
            Dim c As String
            Dim CmdLine As String
            Dim CmdLnLen As Integer
            Dim InArg As Boolean
            Dim i As Integer
            Dim NumArgs As Integer
            Dim intLenStrChar As Integer
            Dim intArgStart As Integer

            GetParameter = ""

            NumArgs = 0
            InArg = False
            intArgStart = 0

            CmdLine = strChar & strParameter
            CmdLnLen = Len(CmdLine)

            intLenStrChar = Len(strChar)

            For i = 1 To CmdLnLen

                c = Mid$(CmdLine, i, intLenStrChar)

                If c = strChar Then
                    NumArgs = NumArgs + 1

                    If NumArgs = intArgument Then
                        intArgStart = i + intLenStrChar - 1
                    End If

                    If NumArgs = intArgument + 1 Then
                        GetParameter = Mid$(strParameter, intArgStart, i - intArgStart - 1)
                        Exit Function
                    End If
                End If
            Next

            If intArgStart > 0 Then
                GetParameter = Mid$(strParameter, intArgStart, CmdLnLen - intArgStart)
            Else
                GetParameter = ""
            End If

        End Function

        Public Shared Function MaxDate(ByVal dtm1 As Date, ByVal dtm2 As Date) As Date
            If dtm1 > dtm2 Then
                Return dtm1
            Else
                Return dtm2
            End If

        End Function

        Public Shared Function MinDate(ByVal dtm1 As Date, ByVal dtm2 As Date) As Date
            If dtm1 < dtm2 Then
                Return dtm1
            Else
                Return dtm2
            End If

        End Function


        Public Shared Function FindFirstDateDataTable(ByVal dtDate As DataTable, _
                      ByVal SearchDate As Date) As Long
            Dim lRow As Integer
            Dim lRowFound As Integer

            lRowFound = -1

            For lRow = 0 To dtDate.Rows.Count - 1


                If CDate(dtDate(lRow)(0)) = SearchDate Then
                    lRowFound = lRow
                    Exit For
                End If

            Next

            Return lRowFound

        End Function


        Public Shared Function FindFirstArrayDate(ByVal vArray() As Date, _
                              ByVal SearchDate As Date) As Long
            Dim lRow As Integer
            Dim lRowFound As Integer

            lRowFound = -1

            For lRow = 0 To UBound(vArray)


                If vArray(lRow) = SearchDate Then
                    lRowFound = lRow
                    Exit For
                End If

            Next

            Return lRowFound

        End Function

        Public Shared Function FindFirstArrayString(ByVal vArray() As String, _
                      ByVal SearchString As String, _
                      Optional ByVal blnCaseSensitive As Boolean = True) As Long
            Dim lRow As Integer
            Dim lRowFound As Integer

            lRowFound = -1

            For lRow = 0 To UBound(vArray)

                If blnCaseSensitive Then

                    If Mid$(vArray(lRow), 1, Len(SearchString)) = SearchString Then
                        lRowFound = lRow
                        Exit For
                    End If

                Else

                    If UCase$(Mid$(vArray(lRow), 1, Len(SearchString))) = UCase$(SearchString) Then
                        lRowFound = lRow
                        Exit For
                    End If

                End If

            Next

            Return lRowFound

        End Function

        Public Shared Function MadeForIn(ByVal sqlTemp As String, Optional ByVal blnString As Boolean = True) As String

            Dim strTemp As String
            Dim intPos As Integer
            Dim sqlTemp2 As String

            strTemp = ""
            sqlTemp2 = sqlTemp

            If Len(Trim$(sqlTemp2)) = 0 Then
                sqlTemp2 = ""
                MadeForIn = sqlTemp2
                Exit Function
            End If

            If Mid$(sqlTemp2, Len(sqlTemp2)) <> "," Then sqlTemp2 = sqlTemp2 & ","

            Do While Len(Trim$(sqlTemp2)) > 0
                intPos = InStr(sqlTemp2, ",")

                If Mid$(sqlTemp2, 1, intPos - 1) <> "" Then

                    If blnString Then
                        strTemp = strTemp & "'" & Mid$(sqlTemp2, 1, intPos - 1) & "',"
                    Else
                        strTemp = strTemp & Mid$(sqlTemp2, 1, intPos - 1) & ","
                    End If

                End If

                sqlTemp2 = Mid$(sqlTemp2, intPos + 1)
            Loop

            If Len(strTemp) > 0 Then

                If Right(strTemp, 1) = "," Then strTemp = Mid$(strTemp, 1, Len(strTemp) - 1)
            End If

            Return strTemp


        End Function
        Public Shared Function StrToArray(ByVal strString1 As String, ByVal strSeparator As String, ByRef avarArray1() As String) As Long
            Dim i, ipos, imax As Integer

            If IsDBNull(strString1) Then
                StrToArray = -1
                Exit Function
            End If

            imax = CountStrInStr(strString1, strSeparator)
            ReDim avarArray1(imax)
            For i = 0 To imax
                ipos = InStr(1, strString1, strSeparator)
                If ipos <> 0 Then
                    avarArray1(i) = Mid$(strString1, 1, ipos - 1)
                    strString1 = Mid$(strString1, ipos + Len(strSeparator))
                Else
                    avarArray1(i) = strString1
                    strString1 = ""
                End If
            Next
            StrToArray = imax + 1

        End Function

        Public Shared Function CountStrInStr(ByVal strString1 As String, ByVal strString2 As String) As Integer
            Dim intCount As Integer
            Dim intCurrPos As Integer

            intCount = 0
            intCurrPos = 0
            If Not IsDBNull(strString1 + strString2) Then
                Do While True
                    intCurrPos = InStr(intCurrPos + 1, strString1, strString2)
                    If intCurrPos = 0 Then
                        Exit Do
                    End If
                    intCount = intCount + 1
                Loop
            End If
            Return intCount

        End Function

        Public Shared Function RemoveLastChar(ByVal strString As String, Optional ByVal strChar As String = "") As String
            If strChar <> "" Then
                If Mid(strString, Len(strString) - Len(strChar) + 1, Len(strChar)) = strChar Then
                    strString = Mid(strString, 1, Len(strString) - Len(strChar))
                End If
            Else
                strString = Mid(strString, 1, Len(strString) - Len(strChar))
            End If
            Return strString
        End Function
        Public Shared Function StringLen(ByVal strTmp As String, ByVal intLen As Integer, Optional ByVal intJustified As Integer = 0) As String
            ' justified:   0-left    1-right
            Dim tmpString As String = ""
            If intJustified = 0 Then
                strTmp = strTmp & Space$(intLen)
                tmpString = Mid$(strTmp, 1, intLen)
            Else
                If intLen - Len(strTmp) < 0 Then
                    tmpString = Mid$(strTmp, 1, intLen)
                Else
                    tmpString = Space$(intLen - Len(strTmp)) & Trim$(strTmp)
                End If
            End If
            Return tmpString
        End Function

        Public Shared Function RemoveFileExtension(ByVal strFileName As String) As String

            Return Left$(strFileName, LastChar(strFileName, ".") - 1)

        End Function

        Public Shared Function SamoFileName(ByVal path As String) As String

            Return Right$(path, Len(path) - LastChar(path, "\") + 0)

        End Function

        Public Shared Function LastChar(ByVal s As String, ByVal c As String) As Integer
            Dim i, j As Integer

            i = 0
            j = 0
            Do
                i = InStr(i + 1, s, c, vbTextCompare)
                If i = 0 Or IsDBNull(i) Then Exit Do
                j = i
            Loop
            Return j

        End Function

        Public Shared Function SamoFileExt(ByVal path As String) As String

            Return Right$(path, Len(path) - LastChar(path, ".") + 0)

        End Function

        Public Shared Function MultiplyString(ByVal strString As String, ByVal n As Integer) As String
            Dim i As Integer
            Dim strReturn As String = ""

            strReturn = strString
            For i = 1 To n - 1
                strReturn = strReturn & strString

            Next
            Return strReturn
        End Function


        Public Shared Sub KillFile(ByVal strFileName As String)
            On Error Resume Next

            Call Kill(strFileName)
            
        End Sub


        Public Shared Function HTNGDateToFidelity(ByVal strDate As String) As DateTime
            If Right(strDate, 1) = "Z" Then
                Return DateTime.ParseExact(strDate, "yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'", Nothing)
            Else
                Return DateTime.ParseExact(strDate, "yyyy'-'MM'-'dd'T'HH':'mm':'ss", Nothing)
            End If

        End Function

        Public Shared Function FidelityDateToSytemDate(ByVal strDate As String) As String
            'iz datuma dd.mm.yyyy moram dobiti sistemski format datuma
            Dim strDay As String = ""
            Dim strMonth As String = ""
            Dim strYear As String = ""
            Dim i As Integer
            Dim j As Integer

            j = 1


            i = InStr(j, strDate, ".")
            If i > j Then
                strDay = Mid(strDate, j, i - j)
            End If

            j = i + 1
            i = InStr(j, strDate, ".")
            If i > j Then
                strMonth = Mid(strDate, j, i - j)
            End If

            j = i + 1
            strYear = Mid(strDate, j)

            Return Format(strYear & "-" & strMonth & "-" & strDay, "Short Date")

        End Function

        'pretvorba 2011-01-31 -> DateTime
        Public Shared Function SysStr2Date(ByVal str As String) As DateTime
            If String.IsNullOrEmpty(str) Then
                Return Nothing
            End If

            Return DateTime.ParseExact(str, "yyyy-MM-dd", datetimeSys)
        End Function

        'pretvorba 12:30 -> DateTime
        Public Shared Function SysStr2Time(ByVal str As String) As DateTime
            If String.IsNullOrEmpty(str) Then
                Return Nothing
            End If

            Return DateTime.ParseExact(str, "HH:mm", datetimeSys)
        End Function

        'pretvorba 2011-01-31 12:30 -> DateTime
        Public Shared Function SysStr2DateTime(ByVal str As String) As DateTime
            If String.IsNullOrEmpty(str) Then
                Return Nothing
            End If

            Return DateTime.ParseExact(str, "yyyy-MM-ddTHH:mm", datetimeSys)
        End Function

        'DateTime -> 2011-01-31
        Public Shared Function Date2SysStr(ByVal dt As DateTime) As String

            Return dt.ToString("yyyy-MM-dd", datetimeSys)
        End Function

        'DateTime -> 12:30
        Public Shared Function Time2SysStr(ByVal dt As DateTime) As String

            Return dt.ToString("HH:mm", datetimeSys)
        End Function

        'DateTime -> 2011-01-31 12:30
        Public Shared Function DateTime2SysStr(ByVal dt As DateTime) As String

            Return dt.ToString("yyyy-MM-ddTHH:mm", datetimeSys)
        End Function

        'pretvorba culture date format -> DateTime
        Public Shared Function String2Date(ByVal str As String) As DateTime
            If String.IsNullOrEmpty(str) Then
                Return Nothing
            End If

            Return DateTime.Parse(str)
        End Function

        'pretvorba culture time format -> DateTime
        Public Shared Function String2Time(ByVal str As String) As DateTime
            If String.IsNullOrEmpty(str) Then
                Return Nothing
            End If

            Return DateTime.ParseExact(str, "HH" & Globalization.CultureInfo.CurrentCulture.DateTimeFormat.TimeSeparator & "mm", Nothing)
        End Function

        'pretvorba culture datetime format -> DateTime
        Public Shared Function String2DateTime(ByVal str As String) As DateTime
            If String.IsNullOrEmpty(str) Then
                Return Nothing
            End If

            Return DateTime.ParseExact(str, Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern & " HH" & Globalization.CultureInfo.CurrentCulture.DateTimeFormat.TimeSeparator & "mm", Nothing)
        End Function

        'DateTime -> culture date format
        Public Shared Function Date2String(ByVal dt As DateTime) As String

            Return dt.ToString("d")
        End Function

        'DateTime -> culture time format
        Public Shared Function Time2String(ByVal dt As DateTime) As String

            Return dt.ToString("HH" & Globalization.CultureInfo.CurrentCulture.DateTimeFormat.TimeSeparator & "mm")
        End Function

        'DateTime -> culture datetime format
        Public Shared Function DateTime2String(ByVal dt As DateTime) As String

            Return dt.ToString(Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern & " HH" & Globalization.CultureInfo.CurrentCulture.DateTimeFormat.TimeSeparator & "mm")
        End Function

        Public Shared Function FormatDate(ByVal dtmDate As Date) As String
            Return "#" & dtmDate.ToString("dd") & "/" & dtmDate.ToString("MM") & "/" & dtmDate.ToString("yyyy") & "#"
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

        Public Shared Function DB2Lng(ByVal i As Object) As Long
            If i Is Nothing OrElse DBNull.Value.Equals(i) Then
                Return 0
            End If

            If TypeOf i Is Double Then
                Return DirectCast(i, Long)
            Else
                Return Convert.ToInt64(i)
            End If
        End Function
        Public Shared Function BlankToZero(ByVal i As String) As Double
            If i = "" Then
                Return 0.0
            Else
                If IsNumeric(i) Then
                    Return CDbl(i)
                Else
                    Return 0.0
                End If
            End If

        End Function

        Public Shared Function ZeroToBlank(ByVal i As Double) As String
            If i = 0 Then
                Return ""
            Else
                If IsNumeric(i) Then
                    Return CDbl(i)
                Else
                    Return ""
                End If
            End If

        End Function

        Public Shared Function GetWeek(ByVal dtmDate As Date) As Integer
            Return DatePart("ww", dtmDate)
        End Function

        Public Shared Function FirstDayOfMonthFromDateTime(ByVal DateTime As Date) As Date

            Return New DateTime(DateTime.Year, DateTime.Month, 1)

        End Function

        Public Shared Function LastDayOfMonthFromDateTime(ByVal DateTime As Date) As Date
            Dim firstDayOfTheMonth As Date = New DateTime(DateTime.Year, DateTime.Month, 1)
            Return firstDayOfTheMonth.AddMonths(1).AddDays(-1)
        End Function

        Public Shared Function FirstDayOfYear(ByVal DateTime As Date) As Date
            Return New DateTime(DateTime.Year, 1, 1)
        End Function

        Public Shared Function LastDayOfYear(ByVal DateTime As Date) As Date
            Dim n As Date = New DateTime(DateTime.Year + 1, 1, 1)
            Return n.AddDays(-1)
        End Function

        Public Shared Function FirstDayOfWeek(ByVal DateTime As Date) As Date
            Dim candidateDate As Date
            Do While (candidateDate.DayOfWeek <> DayOfWeek.Monday)
                candidateDate = candidateDate.AddDays(-1)
            Loop
            Return candidateDate
        End Function

        Public Shared Function FirstDayOfCurrentWeek() As Date
            Return FirstDayOfWeek(DateTime.Today)
        End Function

        Public Shared Function LastDayOfWeek(ByVal DateTime As Date) As Date
            Dim candidateDate As Date
            Do While (candidateDate.DayOfWeek <> DayOfWeek.Monday)

                candidateDate = candidateDate.AddDays(-1)

            Loop
            Return candidateDate.AddDays(7)
        End Function

        Public Shared Function LastDayOfCurrentWeek() As Date
            Dim candidateDate As Date = DateTime.Today
            Do While (candidateDate.DayOfWeek <> DayOfWeek.Monday)

                candidateDate = candidateDate.AddDays(-1)

            Loop
            Return candidateDate.AddDays(7)
        End Function

        Public Shared Function GetUnixTimestampMinutes(ByVal currDate As DateTime) As String
            'create Timespan by subtracting the value provided from the Unix Epoch
            Dim span As TimeSpan = (currDate - New DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime())
            'return the total seconds (which is a UNIX timestamp)
            Return span.TotalMinutes
        End Function
        Public Shared Function GetFileNameOnly(ByVal strFileName As String) As String
            Dim slashPosition As Integer = strFileName.LastIndexOf("\")
            Return strFileName.Substring(slashPosition + 1)
        End Function
        Public Shared Function UnixTime() As Integer
            Dim uTime As Integer
            uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
        End Function

        Public Shared Function UnixTimestampToDateTime(ByVal _UnixTimeStamp As Long) As DateTime
            Return (New DateTime(1970, 1, 1, 0, 0, 0)).AddSeconds(_UnixTimeStamp)
        End Function

        Public Shared Function UnixTimestampToDateTime(ByVal _UnixTimeStamp As String) As DateTime
            Return (New DateTime(1970, 1, 1, 0, 0, 0)).AddSeconds(Val(_UnixTimeStamp))
        End Function

        Public Shared Function DateTimeToUnixTimestamp(ByVal _DateTime As DateTime) As Long
            Dim _UnixTimeSpan As TimeSpan = (_DateTime.Subtract(New DateTime(1970, 1, 1, 0, 0, 0)))
            Return CLng(Fix(_UnixTimeSpan.TotalSeconds))
        End Function

        Public Shared Function DateTimeToUnixDateTimestamp(ByVal _DateTime As DateTime) As Long
            Dim _UnixTimeSpan As TimeSpan = (_DateTime.Subtract(New DateTime(1970, 1, 1, 0, 0, 0)))
            Return CLng(Fix(_UnixTimeSpan.TotalSeconds))
        End Function

        Public Shared Function UnixTimeToDate(ByVal strUnixTime As String) As Date

            Try

                UnixTimeToDate = DateAdd(DateInterval.Second, Val(Mid(strUnixTime, 1, 10)), #1/1/1970#)

                Return UnixTimeToDate

            Catch ex As Exception
                modLog.AddToErrorLog(ex.ToString)
            End Try

        End Function

    End Class





    Public Class EnumUtils(Of T)
        Public Shared Function NumToEnum(ByVal number As Integer) As T
            Return DirectCast([Enum].ToObject(GetType(T), number), T)
        End Function

        Public Shared Function NumToEnum(ByVal number As Object) As T
            If TypeOf number Is Integer Then
                Return DirectCast([Enum].ToObject(GetType(T), DirectCast(number, Integer)), T)
            Else
                Return DirectCast([Enum].ToObject(GetType(T), Convert.ToInt32(number)), T)
            End If

        End Function

    End Class

    
End Namespace