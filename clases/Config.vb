Namespace cls

    Public Class Config

        Public Shared mstr_TOOLSName As String
        Public Shared iniFile As cIni = Nothing

        Public Shared ReadOnly Property ToolsDatabaseName() As String
            Get
                Return iniFile.ReadValue("Connection", "tools_database_name", "[KlaesTools].[dbo]")
            End Get
        End Property
        Public Shared ReadOnly Property GetCollation() As String
            Get
                Return iniFile.ReadValue("Connection", "colation", "collate Latin1_General_CI_AS_WS")
            End Get
        End Property

        Public Shared ReadOnly Property GetConnectionStringKlaes() As String
            Get
                Return iniFile.ReadValue("Connection", "ConnectionStringKlaes", "")
            End Get
        End Property

        Public Shared ReadOnly Property GetConnectionStringKlaesFenster() As String
            Get
                Return iniFile.ReadValue("Connection", "ConnectionStringKlaesFenster", "")
            End Get
        End Property

        Public Shared ReadOnly Property GetConnectionStringKlaesFinestre() As String
            Get
                Return iniFile.ReadValue("Connection", "ConnectionStringKlaesFinestre", "")
            End Get
        End Property

        Public Shared ReadOnly Property GetConnectionStringKapa() As String
            Get
                Return iniFile.ReadValue("Connection", "ConnectionStringKapa", "")
            End Get
        End Property
        Public Shared ReadOnly Property GetConnectionStringMAWI() As String
            Get
                Return iniFile.ReadValue("Connection", "ConnectionStringMawi", "")
            End Get
        End Property
        Public Shared ReadOnly Property GetConnectionStringKlaesTools() As String
            Get
                Return iniFile.ReadValue("Connection", "ConnectionStringKlaesTools", "")
            End Get
        End Property

        Public Shared ReadOnly Property GetConnectionStringeProd() As String
            Get
                Return iniFile.ReadValue("Connection", "ConnectionStringeProd", "")
            End Get
        End Property

        Public Shared ReadOnly Property GetConnectionStringSpica() As String
            Get
                Return iniFile.ReadValue("Connection", "ConnectionStringSpica", "")
            End Get
        End Property

        Public Shared ReadOnly Property GetConnectionStringVnosUr() As String
            Get
                Return iniFile.ReadValue("Connection", "ConnectionStringVnosUr", "")
            End Get
        End Property

        Public Shared ReadOnly Property GetConnectionStringeProdMSora() As String
            Get
                Return iniFile.ReadValue("Connection", "ConnectionStringeProdMSora", "")
            End Get
        End Property
        Public Shared ReadOnly Property GetConnectionStringDatabasePlan() As String
            Get
                Return iniFile.ReadValue("Connection", "connstring_plan", "")
            End Get
        End Property


        Public Shared ReadOnly Property ActionLogUpdateName() As String
            Get
                Return iniFile.ReadValue("UpdateName", "ActionLog", "ActionUpdateName.log")
            End Get
        End Property

        Public Shared ReadOnly Property ActionLogProductionText() As String
            Get
                Return iniFile.ReadValue("ProductionText", "ActionLog", "ActionProductionText.log")
            End Get
        End Property

        Public Shared ReadOnly Property ActionLogCutterSendFile() As String
            Get
                Return iniFile.ReadValue("CutterSendFile", "ActionLog", "ActionCutterSendFile.log")
            End Get
        End Property

        Public Shared ReadOnly Property ActionLogCutterArchive() As String
            Get
                Return iniFile.ReadValue("CutterArchive", "ActionLog", "ActionCutterArchive.log")
            End Get
        End Property

        Public Shared ReadOnly Property ActionLogUpdateSklic() As String
            Get
                Return iniFile.ReadValue("UpdateSklic", "ActionLog", "ActionUpdateSklic.log")
            End Get
        End Property
        Public Shared ReadOnly Property ActionLogUpdateRKoncano() As String
            Get
                Return iniFile.ReadValue("RKoncano", "ActionLog", "RKoncano.log")
            End Get
        End Property
        Public Shared ReadOnly Property ActionLogUpdateMonter() As String
            Get
                Return iniFile.ReadValue("Monter", "ActionLog", "ActionUpdateMonter.log")
            End Get
        End Property


        Public Shared ReadOnly Property ActionLogUpdateSlikaVrtanja() As String
            Get
                Return iniFile.ReadValue("SlikaVrtanja", "ActionLog", "ActionUpdateSlikaVrtanja.log")
            End Get
        End Property

        Public Shared ReadOnly Property ErrorLogName() As String
            Get
                Return iniFile.ReadValue("Settings", "ErrorLog", "EProdServiceLog.log")
            End Get
        End Property

        Public Shared ReadOnly Property GetNameNummerListe() As String
            Get
                Return iniFile.ReadValue("Name", "NummerListe", "81")
            End Get
        End Property


        Public Shared ReadOnly Property GetNameKennungMarke() As String
            Get
                Return iniFile.ReadValue("Name", "KennungMarke", "2006")
            End Get
        End Property

        Public Shared ReadOnly Property AutoStartMailinig() As Boolean
            Get
                Return iniFile.ReadValue("Settings", "Autostartmailing", 0)
            End Get
        End Property
        Public Shared ReadOnly Property AutoStartDeliveryDate() As Boolean
            Get
                Return iniFile.ReadValue("Settings", "Autostartdeliverydate", 0)
            End Get
        End Property
        Public Shared ReadOnly Property AutoStartMAWIDate() As Boolean
            Get
                Return iniFile.ReadValue("Settings", "AutostartMawidate", 0)
            End Get
        End Property
        Public Shared ReadOnly Property AutoStartName() As Boolean
            Get
                Return iniFile.ReadValue("Settings", "Autostartname", 0)
            End Get
        End Property

        Public Shared ReadOnly Property AutoStartCutterFile() As Boolean
            Get
                Return iniFile.ReadValue("Settings", "AutoStartCutterFile", 0)
            End Get
        End Property

        Public Shared ReadOnly Property AutoStartCutterArchive() As Boolean
            Get
                Return iniFile.ReadValue("Settings", "AutoStartCutterArchive", 0)
            End Get
        End Property

        Public Shared ReadOnly Property AutoStartSklic() As Boolean
            Get
                Return iniFile.ReadValue("Settings", "AutoStartCutterArchive", 0)
            End Get
        End Property

        Public Shared ReadOnly Property AutoStartMonter() As Boolean
            Get
                Return iniFile.ReadValue("Settings", "AutoStartMonter", 0)
            End Get
        End Property

        Public Shared ReadOnly Property AutoStartSlikaVrtanja() As Boolean
            Get
                Return iniFile.ReadValue("Settings", "AutoStartSlikaVrtanja", 0)
            End Get
        End Property

        Public Shared ReadOnly Property RunAtStart() As Boolean
            Get
                Return iniFile.ReadValue("Settings", "RunAtStart", 1)
            End Get
        End Property


        Public Shared ReadOnly Property NotRealizedOffersDayDelay() As Integer
            Get
                Return iniFile.ReadValue("Settings", "NotRealizedOffersDelay", 60)
            End Get
        End Property

        Public Shared ReadOnly Property GetUpdateNameCheckLastDays() As Integer
            Get
                Return iniFile.ReadValue("UpdateName", "CheckLastDays", 2)
            End Get
        End Property

        Public Shared ReadOnly Property GetUpdateNameCheckInterval() As Integer
            Get
                Return iniFile.ReadValue("UpdateName", "CheckInterval", 60)
            End Get
        End Property

        Public Shared ReadOnly Property GetUpdateNameStartTime() As String
            Get
                Return iniFile.ReadValue("UpdateName", "StartTime", "06:00")
            End Get
        End Property
        Public Shared ReadOnly Property GetUpdateNameEndTime() As String
            Get
                Return iniFile.ReadValue("UpdateName", "EndTime", "15:00")
            End Get
        End Property
        Public Shared ReadOnly Property GetUpdateNameDays() As String
            Get
                Return iniFile.ReadValue("UpdateName", "UpdateDays", "1,2,3,4,5")
            End Get
        End Property

        Public Shared ReadOnly Property CheckStatusForAllPositions() As String
            Get
                Return iniFile.ReadValue("Settings", "CheckStatusForAllPositions", "1")
            End Get
        End Property

        Public Shared ReadOnly Property UpdateKapaPCT() As Boolean
            Get
                Return iniFile.ReadValue("Settings", "UpdateKapaPCT", "1")
            End Get
        End Property
        Public Shared ReadOnly Property UpdateKapaPCTDaysBefore() As Integer
            Get
                Return iniFile.ReadValue("Settings", "UpdateKapaPCTDaysBefore", "7")
            End Get
        End Property

        Public Shared ReadOnly Property UpdateKapaPCTDaysAfter() As Integer
            Get
                Return iniFile.ReadValue("Settings", "UpdateKapaPCTDaysAfter", "7")
            End Get
        End Property

        Public Shared ReadOnly Property GetOrderStatusStartTime() As String
            Get
                Return iniFile.ReadValue("OrderStatus", "StartTime", "06:00")
            End Get
        End Property
        Public Shared ReadOnly Property GetOrderStatusEndTime() As String
            Get
                Return iniFile.ReadValue("OrderStatus", "EndTime", "15:00")
            End Get
        End Property
        Public Shared ReadOnly Property GetOrderStatusDays() As String
            Get
                Return iniFile.ReadValue("OrderStatus", "UpdateDays", "1,2,3,4,5")
            End Get
        End Property
#Region "Cutter"

        Public Shared ReadOnly Property GetCutterSourcePath() As String
            Get
                Return iniFile.ReadValue("Cutter", "source_path", "z:\Skupno\Liste")
            End Get
        End Property

        Public Shared ReadOnly Property GetCutterFileTypes() As String
            Get
                Return iniFile.ReadValue("Cutter", "file_types", "Z15,Z16")
            End Get
        End Property

        Public Shared ReadOnly Property GetCutterStation() As Integer
            Get
                Return iniFile.ReadValue("Cutter", "station", 5)
            End Get
        End Property

        Public Shared ReadOnly Property GetCutterDestPath() As String
            Get
                Return iniFile.ReadValue("Cutter", "dest_path", "z:\Skupno\Liste\Rezalnik")
            End Get
        End Property

        Public Shared ReadOnly Property ImportAddressFilePath() As String
            Get
                Return iniFile.ReadValue("ImportAddress", "file", "\\SZMZSV04\Network\Skupno\Daten\")
            End Get
        End Property

        Public Shared ReadOnly Property ImportGlassPath() As String
            Get
                Return iniFile.ReadValue("ImportGlass", "dir", "\\SZMZSV04\Network\Skupno\Liste")
            End Get
        End Property
        Public Shared ReadOnly Property RunImportGlass() As Integer
            Get
                Return iniFile.ReadValue("ImportGlass", "run", 1)
            End Get
        End Property
        Public Shared ReadOnly Property ImportGlassFileExtension() As String
            Get
                Return iniFile.ReadValue("ImportGlass", "extension", "G08")
            End Get
        End Property
        Public Shared ReadOnly Property ImportGlassActionLog() As String
            Get
                Return iniFile.ReadValue("ImportGlass", "action_log", "ImportGlass.log")
            End Get
        End Property

        Public Shared ReadOnly Property GetCutterSendFileCheckLastDays() As Integer
            Get
                Return iniFile.ReadValue("CutterSendFile", "CheckLastDays", 2)
            End Get
        End Property

        Public Shared ReadOnly Property GetCutterSendFileCheckInterval() As Integer
            Get
                Return iniFile.ReadValue("CutterSendFile", "CheckInterval", 60)
            End Get
        End Property

        Public Shared ReadOnly Property GetCutterSendFileStartTime() As String
            Get
                Return iniFile.ReadValue("CutterSendFile", "StartTime", "06:00")
            End Get
        End Property

        Public Shared ReadOnly Property GetCutterSendFileEndTime() As String
            Get
                Return iniFile.ReadValue("CutterSendFile", "EndTime", "15:00")
            End Get
        End Property

        Public Shared ReadOnly Property GetCutterSendFileDays() As String
            Get
                Return iniFile.ReadValue("CutterSendFile", "UpdateDays", "1,2,3,4,5")
            End Get
        End Property

        Public Shared ReadOnly Property GetCutterFilter() As String
            Get
                Return iniFile.ReadValue("CutterSendFile", "Filter", "NATURA")
            End Get
        End Property

        Public Shared ReadOnly Property GetCutterArchiveCheckLastDays() As Integer
            Get
                Return iniFile.ReadValue("CutterArchive", "CheckLastDays", 2)
            End Get
        End Property

        Public Shared ReadOnly Property GetSklicCheckLastDays() As Integer
            Get
                Return iniFile.ReadValue("UpdateSklic", "CheckLastDays", 2)
            End Get
        End Property

        Public Shared ReadOnly Property GetRKoncanoCheckLastDays() As Integer
            Get
                Return iniFile.ReadValue("RKoncano", "CheckLastDays", 2)
            End Get
        End Property

        Public Shared ReadOnly Property GetMonterCheckLastDays() As Integer
            Get
                Return iniFile.ReadValue("Montaza", "CheckLastDays", 2)
            End Get
        End Property

        Public Shared ReadOnly Property GetSlikaVrtanjaCheckLastDays() As Integer
            Get
                Return iniFile.ReadValue("SlikaVrtanja", "CheckLastDays", 2)
            End Get
        End Property

        Public Shared ReadOnly Property GetCutterArchiveCheckInterval() As Integer
            Get
                Return iniFile.ReadValue("CutterArchive", "CheckInterval", 60)
            End Get
        End Property

        Public Shared ReadOnly Property GetSklicUpdateInterval() As Integer
            Get
                Return iniFile.ReadValue("UpdateSklic", "CheckInterval", 60)
            End Get
        End Property

        Public Shared ReadOnly Property GetMonterUpdateInterval() As Integer
            Get
                Return iniFile.ReadValue("Monter", "CheckInterval", 60)
            End Get
        End Property

        Public Shared ReadOnly Property GetSlikaVrtanjaUpdateInterval() As Integer
            Get
                Return iniFile.ReadValue("SlikaVrtanja", "CheckInterval", 60)
            End Get
        End Property

        Public Shared ReadOnly Property GetCutterArchiveStartTime() As String
            Get
                Return iniFile.ReadValue("CutterArchive", "StartTime", "06:00")
            End Get
        End Property

        Public Shared ReadOnly Property GetCutterArchiveEndTime() As String
            Get
                Return iniFile.ReadValue("CutterArchive", "EndTime", "15:00")
            End Get
        End Property

        Public Shared ReadOnly Property GetCutterArchiveDays() As String
            Get
                Return iniFile.ReadValue("CutterArchive", "UpdateDays", "1,2,3,4,5")
            End Get
        End Property

        Public Shared ReadOnly Property GetRKoncanoStartTime() As String
            Get
                Return iniFile.ReadValue("RKoncano", "StartTime", "06:00")
            End Get
        End Property

        Public Shared ReadOnly Property GetRKoncanoEndTime() As String
            Get
                Return iniFile.ReadValue("RKoncano", "EndTime", "15:00")
            End Get
        End Property

        Public Shared ReadOnly Property GetRKoncanoDays() As String
            Get
                Return iniFile.ReadValue("RKoncano", "UpdateDays", "1,2,3,4,5")
            End Get
        End Property

        Public Shared ReadOnly Property GetSklicStartTime() As String
            Get
                Return iniFile.ReadValue("UpdateSklic", "StartTime", "06:00")
            End Get
        End Property

        Public Shared ReadOnly Property GetSklicEndTime() As String
            Get
                Return iniFile.ReadValue("UpdateSklic", "EndTime", "15:00")
            End Get
        End Property


        Public Shared ReadOnly Property GetSklicDays() As String
            Get
                Return iniFile.ReadValue("UpdateSklic", "UpdateDays", "1,2,3,4,5")
            End Get
        End Property

        Public Shared ReadOnly Property GetMonterStartTime() As String
            Get
                Return iniFile.ReadValue("Monter", "StartTime", "06:00")
            End Get
        End Property

        Public Shared ReadOnly Property GetMonterEndTime() As String
            Get
                Return iniFile.ReadValue("Monter", "EndTime", "15:00")
            End Get
        End Property

        Public Shared ReadOnly Property GetMonterDays() As String
            Get
                Return iniFile.ReadValue("Monter", "UpdateDays", "1,2,3,4,5")
            End Get
        End Property

        Public Shared ReadOnly Property GetSlikaVrtanjaStartTime() As String
            Get
                Return iniFile.ReadValue("SlikaVrtanja", "StartTime", "06:00")
            End Get
        End Property

        Public Shared ReadOnly Property GetSlikaVrtanjaEndTime() As String
            Get
                Return iniFile.ReadValue("SlikaVrtanja", "EndTime", "15:00")
            End Get
        End Property

        Public Shared ReadOnly Property GetSlikaVrtanjaDays() As String
            Get
                Return iniFile.ReadValue("SlikaVrtanja", "UpdateDays", "1,2,3,4,5")
            End Get
        End Property

#End Region



#Region "Mail"

        Public Shared ReadOnly Property LicenceKey() As String
            Get
                Return iniFile.ReadValue("Mail", "licence_key", "")
            End Get
        End Property
        Public Shared ReadOnly Property GetMailAddress() As String
            Get
                Return iniFile.ReadValue("Mail", "mail", "info.mizarstvo@m-sora.si")
            End Get
        End Property

        Public Shared ReadOnly Property GetMailSMTP() As String
            Get
                Return iniFile.ReadValue("Mail", "smtp", "vhost01.stelkom.eu")
            End Get
        End Property

        Public Shared ReadOnly Property GetMailUsername() As String
            Get
                Return iniFile.ReadValue("Mail", "username", "minfo@m-sora.si")
            End Get
        End Property

        Public Shared ReadOnly Property GetMailPassword() As String
            Get
                Return iniFile.ReadValue("Mail", "password", "minfo2012")
            End Get
        End Property

        Public Shared ReadOnly Property GetMailSMTPPort() As String
            Get
                Return iniFile.ReadValue("Mail", "port", "465")
            End Get
        End Property

        Public Shared ReadOnly Property DefaultLang() As String
            Get
                Return iniFile.ReadValue("Mail", "DefaultLang", "EN")
            End Get
        End Property

        Public Shared ReadOnly Property SendToTrade() As Boolean
            Get
                Return iniFile.ReadValue("Mail", "send_to_trade", 0)
            End Get
        End Property

        Public Shared ReadOnly Property GetSendMailCheckLastDays() As Integer
            Get
                Return iniFile.ReadValue("SendMail", "CheckLastDays", 2)
            End Get
        End Property

        Public Shared ReadOnly Property GetDeliveryDateCheckInterval() As Integer
            Get
                Return iniFile.ReadValue("DeliveryDate", "CheckInterval", 60)
            End Get
        End Property
        Public Shared ReadOnly Property GetMawiDateCheckInterval() As Integer
            Get
                Return iniFile.ReadValue("MAWIDate", "CheckInterval", 60)
            End Get
        End Property

        Public Shared ReadOnly Property GetSendMailCheckInterval() As Integer
            Get
                Return iniFile.ReadValue("SendMail", "CheckInterval", 60)
            End Get
        End Property
        Public Shared ReadOnly Property GetSendMailStartTime() As String
            Get
                Return iniFile.ReadValue("SendMail", "StartTime", "06:00")
            End Get
        End Property
        Public Shared ReadOnly Property GetDeliveryDateStartTime() As String
            Get
                Return iniFile.ReadValue("DeliveryDate", "StartTime", "06:00")
            End Get
        End Property
        Public Shared ReadOnly Property GetMawiDateStartTime() As String
            Get
                Return iniFile.ReadValue("MAWIDate", "StartTime", "06:00")
            End Get
        End Property
        Public Shared ReadOnly Property GetSendMailEndTime() As String
            Get
                Return iniFile.ReadValue("SendMail", "EndTime", "15:00")
            End Get
        End Property
        Public Shared ReadOnly Property GetDeliveryDateEndTime() As String
            Get
                Return iniFile.ReadValue("DeliveryDate", "EndTime", "15:00")
            End Get
        End Property
        Public Shared ReadOnly Property GetMawiDateEndTime() As String
            Get
                Return iniFile.ReadValue("MAWIDate", "EndTime", "15:00")
            End Get
        End Property
        Public Shared ReadOnly Property GetSendMailDays() As String
            Get
                Return iniFile.ReadValue("SendMail", "UpdateDays", "1,2,3,4,5")
            End Get
        End Property
        Public Shared ReadOnly Property GetMawiArticleTypes() As String
            Get
                Return iniFile.ReadValue("MAWIDate", "ArticleTypes", "'Komarnik','Zubehör'")
            End Get
        End Property
        Public Shared ReadOnly Property GetDeliveryDateDays() As String
            Get
                Return iniFile.ReadValue("DeliveryDate", "UpdateDays", "1,2,3,4,5")
            End Get
        End Property

        Public Shared ReadOnly Property GetMawiDateDays() As String
            Get
                Return iniFile.ReadValue("MAWIDate", "UpdateDays", "1,2,3,4,5")
            End Get
        End Property
        Public Shared ReadOnly Property ActionLogSendMail() As String
            Get
                Return iniFile.ReadValue("SendMail", "ActionLog", "ServiceEvents.log")
            End Get
        End Property

        Public Shared ReadOnly Property SummeNetto() As Long
            Get
                Return iniFile.ReadValue("SendMail", "SummeNetto", 3000)
            End Get
        End Property

        Public Shared ReadOnly Property ActionLogDeliveryDate() As String
            Get
                Return iniFile.ReadValue("DeliveryDate", "ActionLog", "ActionDeliveryDate.log")
            End Get
        End Property
        Public Shared ReadOnly Property ActionLogMAWIDate() As String
            Get
                Return iniFile.ReadValue("MAWIDate", "ActionLog", "ActionMAWIDate.log")
            End Get
        End Property

        Public Shared ReadOnly Property GetDeliveryDateCheckLastDays() As Integer
            Get
                Return iniFile.ReadValue("DeliveryDate", "CheckLastDays", 2)
            End Get
        End Property
        Public Shared ReadOnly Property GetMAWIDateCheckLastDays() As Integer
            Get
                Return iniFile.ReadValue("MawiDate", "CheckLastDays", 2)
            End Get
        End Property

        Public Shared ReadOnly Property GetDeveloperMail() As String
            Get
                Return iniFile.ReadValue("Mail", "developer", "")
            End Get
        End Property
        Public Shared ReadOnly Property GeteProdMail() As String
            Get
                Return iniFile.ReadValue("Mail", "eprod", "")
            End Get
        End Property
        Public Shared ReadOnly Property GetDirectorMail() As String
            Get
                Return iniFile.ReadValue("Mail", "director", "")
            End Get
        End Property
        Public Shared ReadOnly Property GetProductionMail() As String
            Get
                Return iniFile.ReadValue("Mail", "production", "")
            End Get
        End Property
        Public Shared ReadOnly Property GetLogisticMail() As String
            Get
                Return iniFile.ReadValue("Mail", "logistic", "")
            End Get
        End Property

        'KapaLog
        Public Shared ReadOnly Property AutoStartTechnicalLock() As Boolean
            Get
                Return iniFile.ReadValue("Settings", "AutoStartTechnicalLock", 0)
            End Get
        End Property

        Public Shared ReadOnly Property AutoStartOrderStatus() As Boolean
            Get
                Return iniFile.ReadValue("Settings", "AutoStartOrderStatus", 0)
            End Get
        End Property

        Public Shared ReadOnly Property AutoStartKapaLog() As Boolean
            Get
                Return iniFile.ReadValue("Settings", "AutoStartKapaLog", 0)
            End Get
        End Property

        Public Shared ReadOnly Property AutoStartSpicaEvents() As Boolean
            Get
                Return iniFile.ReadValue("Settings", "AutoStartSpicaEvents", 0)
            End Get
        End Property

        Public Shared ReadOnly Property GetKapaLogStartTime() As String
            Get
                Return iniFile.ReadValue("KapaLog", "StartTime", "06:00")
            End Get
        End Property

        Public Shared ReadOnly Property GetKapaLogEndTime() As String
            Get
                Return iniFile.ReadValue("KapaLog", "EndTime", "18:00")
            End Get
        End Property

        Public Shared ReadOnly Property GetKapaLogDays() As String
            Get
                Return iniFile.ReadValue("KapaLog", "UpdateDays", "1,2,3,4,5")
            End Get
        End Property

        Public Shared ReadOnly Property GetKapaLogCheckDaysBack() As Integer
            Get
                Return iniFile.ReadValue("KapaLog", "CheckDaysBack", 10)
            End Get
        End Property

        Public Shared ReadOnly Property GetKapaLogCheckDaysForward() As Integer
            Get
                Return iniFile.ReadValue("KapaLog", "CheckDaysForward", 30)
            End Get
        End Property

        Public Shared ReadOnly Property GetKapaLogUpdateInterval() As Integer
            Get
                Return iniFile.ReadValue("KapaLog", "CheckInterval", 60)
            End Get
        End Property

        Public Shared ReadOnly Property GetSpicaEventUpdateInterval() As Integer
            Get
                Return iniFile.ReadValue("Spica", "UpdateEventInterval", 600)
            End Get

        End Property

        Public Shared ReadOnly Property GetSpicaEventsCheckDaysBack() As Integer
            Get
                Return iniFile.ReadValue("Spica", "CheckDaysBack", 2)
            End Get
        End Property

        Public Shared ReadOnly Property ActionLogImportAddressLog() As String
            Get
                Return iniFile.ReadValue("ImportAddress", "ActionLog", "ImportAddressLog.log")
            End Get
        End Property

        Public Shared ReadOnly Property RunImportAddress() As Boolean
            Get
                Return iniFile.ReadValue("ImportAddress", "Run", 0)
            End Get
        End Property
        Public Shared ReadOnly Property ActionLogKapaLog() As String
            Get
                Return iniFile.ReadValue("KapaLog", "ActionLog", "ActionKapaLog.log")
            End Get
        End Property

        Public Shared ReadOnly Property ActionSpicaLog() As String
            Get
                Return iniFile.ReadValue("Spica", "ActionLog", "SpiceEventsLog.log")
            End Get
        End Property
        Public Shared ReadOnly Property GetKapaLinija() As String
            Get
                Return iniFile.ReadValue("KapaLog", "Linija", "00001")
            End Get
        End Property

        Public Shared ReadOnly Property ActionLogTechnicalLock() As String
            Get
                Return iniFile.ReadValue("LockTechnicalOrders", "ActionLog", "ActionKapaLog.log")
            End Get
        End Property

        Public Shared ReadOnly Property ActionLogOrderStatus() As String
            Get
                Return iniFile.ReadValue("OrderStatus", "ActionLog", "MSORAOrderStatus.log")
            End Get
        End Property

        Public Shared ReadOnly Property GetTechnicalLockUpdateInterval() As Integer
            Get
                Return iniFile.ReadValue("LockTechnicalOrders", "CheckInterval", 60)
            End Get
        End Property



        Public Shared ReadOnly Property GetTechnicalLockCheckDaysBack() As Integer
            Get
                Return iniFile.ReadValue("LockTechnicalOrders", "CheckDaysBack", 10)
            End Get
        End Property

        Public Shared ReadOnly Property GetStatusNarocilaInterval() As Integer
            Get
                Return iniFile.ReadValue("OrderStatus", "CheckInterval", 60)
            End Get
        End Property

        Public Shared ReadOnly Property GetOrderStatusCheckDaysBack1() As Integer
            Get
                Return iniFile.ReadValue("OrderStatus", "CheckDaysBack1", 10)
            End Get
        End Property

        Public Shared ReadOnly Property GetOrderStatusCheckDaysBack2() As Integer
            Get
                Return iniFile.ReadValue("OrderStatus", "CheckDaysBack2", 10)
            End Get
        End Property

        Public Shared ReadOnly Property GetOrderStatusCheckDaysBack3() As Integer
            Get
                Return iniFile.ReadValue("OrderStatus", "CheckDaysBack3", 10)
            End Get
        End Property

        Public Shared ReadOnly Property GetOrderStatusCheckDaysBack4() As Integer
            Get
                Return iniFile.ReadValue("OrderStatus", "CheckDaysBack4", 10)
            End Get
        End Property

        Public Shared ReadOnly Property GetOrderStatusCheckDaysBack5() As Integer
            Get
                Return iniFile.ReadValue("OrderStatus", "CheckDaysBack5", 10)
            End Get
        End Property

        Public Shared ReadOnly Property GetOrderStatusCheckDaysBack6() As Integer
            Get
                Return iniFile.ReadValue("OrderStatus", "CheckDaysBack6", 10)
            End Get
        End Property

        Public Shared ReadOnly Property GetMapaArhiv() As String
            Get
                Return iniFile.ReadValue("OrderStatus", "mapa_arhiv", "\\SZMZSV04\network\Skupno\Arhiv_Dokument\")
            End Get
        End Property

        Public Shared ReadOnly Property GetLeta() As String
            Get
                Return iniFile.ReadValue("OrderStatus", "Leta", "2019,2020")
            End Get
        End Property

        Public Shared ReadOnly Property FolderNames() As String
            Get
                Return iniFile.ReadValue("OrderStatus", "FolderNames", "")
            End Get
        End Property

        Public Shared ReadOnly Property CreateFolders() As Integer
            Get
                Return iniFile.ReadValue("OrderStatus", "CreateFolders", 0)
            End Get
        End Property
        Public Shared ReadOnly Property GetUnlockMin() As Integer
            Get
                Return iniFile.ReadValue("LockTechnicalOrders", "unlock_min", 60)
            End Get
        End Property

        Public Shared ReadOnly Property TechnicalLockUser() As Long
            Get
                Return iniFile.ReadValue("LockTechnicalOrders", "User", "-1")
            End Get
        End Property

        Public Shared ReadOnly Property FirebaseAuthSecret() As String
            Get
                Return iniFile.ReadValue("Firebase", "AuthSecret", "")
            End Get
        End Property

        Public Shared ReadOnly Property FirebaseBasePath() As String
            Get
                Return iniFile.ReadValue("Firebase", "BasePath", "")
            End Get
        End Property
#End Region
    End Class
End Namespace
