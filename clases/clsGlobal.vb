
Public Class clsGlobal
    Public Enum EditMode As Integer
        record_insert = 1
        record_edit = 2
    End Enum
    Public Enum EventType As Integer
        event_not_defined = 0
        event_kommission_production_start = 1
        event_kommission_production_end = 2
        event_order_production_start = 3
        event_order_production_end = 4
        event_order_planning_start = 5
        event_order_technical_end = 6
        event_order_production_end_check_delivery_OK = 7
        event_order_production_end_check_delivery_FAIL = 8
        event_order_already_delivered = 9
        event_kommission_send_to_cutter = 10
        event_kommission_cutter_move_to_archive = 11
        event_delivery_note_created = 12
        event_invoice_created = 13
        event_invoice_printed = 14
        event_avans_created = 15
        event_order_not_planed = 16
        event_order_kapa_glass = 17
        event_montaza_slepci = 18
        event_offer_not_realized = 19
        event_task_assigned = 20
        event_glass_not_ordered = 21
        event_MAWI_subsequent_delivery = 22
    End Enum
End Class
