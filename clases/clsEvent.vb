Option Explicit On
Option Strict On

Imports eProdService.cls.msora.DB_MSora
Imports MySql.Data.MySqlClient
Imports System.Data.SqlClient

Namespace cls.event
    Public Class EprodEvent
        Private lngId As Long
        Private strDesc As String
        Private intWorkstation As Integer
        Private intType As Integer
        Private blnEnabled As Boolean
        Private intTextIdBody As Integer
        Private intTextIdSubject As Integer
        Private blnMailToPartner As Boolean
        Private blnMailToDirector As Boolean
        Private blnMailToComercialist As Boolean
        Private blnMailToDeveloper As Boolean
        Private blnMailToProduction As Boolean
        Private blnMailToTechnology As Boolean
        Private blnMailToLogistic As Boolean
        Private strEventParameter As String
        Private intEventDays As Integer
        Private blnExcludeReclamation As Boolean
        Private blnCheckDelivery As Boolean
        Private blnCheckMontage As Boolean
        Private intMontageDays As Integer
        Private strEmailExtra As String
        Private strPartnerId As String

        

        Public Enum intMailType As Integer
            MailToPartner = 1
            MailToDirector = 2
            MailToComercialist = 3
            MailToDeveloper = 4
            MailToProduction = 5
            MailToTechnology = 6
            MailToLogistic = 7
            MialToExtra = 8
        End Enum

        Public Sub New(ByVal lngEventId As Long)
            Dim dt As DataTable
            Dim strSQL As String
            strSQL = "SELECT * FROM eprod_events WHERE event_id = @lngEventId"
            Using cmd As New SqlCommand(strSQL, GetConnection("KlaesTools"))
                cmd.Parameters.AddWithValue("@lngEventId", lngEventId)
                dt = GetData(cmd)
                If dt.Rows.Count > 0 Then
                    EventId = lngEventId
                    EventDescription = dt(0)("event_desc").ToString
                    EventWorkstation = DB2Int(dt(0)("event_workstation"))
                    EventDays = DB2IntZero(dt(0)("event_days"))
                    EventType = DB2Int(dt(0)("event_type"))
                    EventTextIdBody = DB2Int(dt(0)("event_text_id_body"))
                    EventTextIdSubject = DB2Int(dt(0)("event_text_id_subject"))
                    EventParameter = dt(0)("event_parameter").ToString

                    If DB2Int(dt(0)("event_mailto_commercial")) <> 0 Then
                        EventMailToCommerialist = True
                    Else
                        EventMailToCommerialist = False
                    End If

                    If DB2Int(dt(0)("event_mailto_developer")) <> 0 Then
                        EventMailToDeveloper = True
                    Else
                        EventMailToDeveloper = False
                    End If

                    If DB2Int(dt(0)("event_mailto_director")) <> 0 Then
                        EventMailToDirector = True
                    Else
                        EventMailToDirector = False
                    End If

                    If DB2Int(dt(0)("event_mailto_partner")) <> 0 Then
                        EventMailToPartner = True
                    Else
                        EventMailToPartner = False
                    End If

                    If DB2Int(dt(0)("event_mailto_production")) <> 0 Then
                        EventMailToProduction = True
                    Else
                        EventMailToProduction = False
                    End If

                    If DB2Int(dt(0)("event_mailto_technology")) <> 0 Then
                        EventMailToTechnology = True
                    Else
                        EventMailToTechnology = False
                    End If

                    If DB2Int(dt(0)("event_mailto_logistic")) <> 0 Then
                        EventMailToLogistic = True
                    Else
                        EventMailToLogistic = False
                    End If

                    If DB2Int(dt(0)("event_enabled")) <> 0 Then
                        EventEnabled = True
                    Else
                        EventEnabled = False
                    End If

                    If DB2Int(dt(0)("event_exclude_reclamation")) <> 0 Then
                        EventExcludeReclamation = True
                    Else
                        EventExcludeReclamation = False
                    End If

                    If DB2Int(dt(0)("event_check_delivery")) <> 0 Then
                        EventCheckDelivery = True
                    Else
                        EventCheckDelivery = False
                    End If

                    If DB2Int(dt(0)("event_check_montage")) <> 0 Then
                        EventCheckMontage = True
                        EventMontageDays = DB2Int(dt(0)("event_montage_days"))
                    Else
                        EventCheckMontage = False
                        EventMontageDays = 0
                    End If


                    strEmailExtra = dt(0)("event_emailto_extra").ToString
                    strPartnerId = dt(0)("event_emailto_partner_id").ToString

                Else

                    EventId = -1
                    EventDescription = ""
                    EventWorkstation = -1
                    EventParameter = ""
                    EventType = clsGlobal.EventType.event_not_defined
                    EventTextIdBody = -1
                    EventTextIdSubject = -1
                    EventMailToCommerialist = False
                    EventMailToDeveloper = False
                    EventMailToDirector = False
                    EventMailToLogistic = False
                    EventMailToPartner = False
                    EventMailToProduction = False
                    EventMailToTechnology = False
                    EventEnabled = False
                    EventDays = 0
                    EventExcludeReclamation = False
                    EventCheckDelivery = False
                    EventCheckMontage = False
                    EventMontageDays = 0
                    strEmailExtra = ""
                    strPartnerId = ""
                End If
            End Using

        End Sub

        Public Sub Dispose()

        End Sub

        Property EventId() As Long
            Get
                Return lngId
            End Get
            Set(ByVal Value As Long)
                lngId = Value
            End Set
        End Property

        Property EventDescription() As String
            Get
                Return strDesc
            End Get
            Set(ByVal Value As String)
                strDesc = Value
            End Set
        End Property
        Property EventParameter() As String
            Get
                Return strEventParameter
            End Get
            Set(ByVal Value As String)
                strEventParameter = Value
            End Set
        End Property

        Property EventWorkstation() As Integer
            Get
                Return intWorkstation
            End Get
            Set(ByVal Value As Integer)
                intWorkstation = Value
            End Set
        End Property

        Property EventDays() As Integer
            Get
                Return intEventDays
            End Get
            Set(ByVal Value As Integer)
                intEventDays = Value
            End Set
        End Property

        Property EventType() As Integer
            Get
                Return intType
            End Get
            Set(ByVal Value As Integer)
                intType = Value
            End Set
        End Property

        Property EventEnabled() As Boolean
            Get
                Return blnEnabled
            End Get
            Set(ByVal Value As Boolean)
                blnEnabled = Value
            End Set
        End Property

        Property EventTextIdBody() As Integer
            Get
                Return intTextIdBody
            End Get
            Set(ByVal Value As Integer)
                intTextIdBody = Value
            End Set
        End Property

        Property EventTextIdSubject() As Integer
            Get
                Return intTextIdSubject
            End Get
            Set(ByVal Value As Integer)
                intTextIdSubject = Value
            End Set
        End Property

        Property EventMailToPartner() As Boolean
            Get
                Return blnMailToPartner
            End Get
            Set(ByVal Value As Boolean)
                blnMailToPartner = Value
            End Set
        End Property

        Property EventMailToDirector() As Boolean
            Get
                Return blnMailToDirector
            End Get
            Set(ByVal Value As Boolean)
                blnMailToDirector = Value
            End Set
        End Property
        Property EventMailToCommerialist() As Boolean
            Get
                Return blnMailToComercialist
            End Get
            Set(ByVal Value As Boolean)
                blnMailToComercialist = Value
            End Set
        End Property
        Property EventMailToDeveloper() As Boolean
            Get
                Return blnMailToDeveloper
            End Get
            Set(ByVal Value As Boolean)
                blnMailToDeveloper = Value
            End Set
        End Property
        Property EventMailToProduction() As Boolean
            Get
                Return blnMailToProduction
            End Get
            Set(ByVal Value As Boolean)
                blnMailToProduction = Value
            End Set
        End Property
        Property EventMailToTechnology() As Boolean
            Get
                Return blnMailToTechnology
            End Get
            Set(ByVal Value As Boolean)
                blnMailToTechnology = Value
            End Set
        End Property
        Property EventMailToLogistic() As Boolean
            Get
                Return blnMailToLogistic
            End Get
            Set(ByVal Value As Boolean)
                blnMailToLogistic = Value
            End Set
        End Property

        Property EventExcludeReclamation() As Boolean
            Get
                Return blnExcludeReclamation
            End Get
            Set(ByVal Value As Boolean)
                blnExcludeReclamation = Value
            End Set
        End Property

        Property EventCheckDelivery() As Boolean
            Get
                Return blnCheckDelivery
            End Get
            Set(ByVal Value As Boolean)
                blnCheckDelivery = Value
            End Set
        End Property
        Property EventCheckMontage() As Boolean
            Get
                Return blnCheckMontage
            End Get
            Set(ByVal Value As Boolean)
                blnCheckMontage = Value
            End Set
        End Property

        Property EventMontageDays() As Integer
            Get
                Return intMontageDays
            End Get
            Set(ByVal Value As Integer)
                intMontageDays = Value
            End Set
        End Property
        Property EventEmailToExtra() As String
            Get
                Return strEmailExtra
            End Get
            Set(ByVal Value As String)
                strEmailExtra = Value
            End Set
        End Property

        Property EventPartnerId() As String
            Get
                Return strPartnerId
            End Get
            Set(ByVal Value As String)
                strPartnerId = Value
            End Set
        End Property
    End Class
End Namespace

