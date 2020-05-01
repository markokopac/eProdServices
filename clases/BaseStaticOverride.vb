Option Explicit On
Option Strict On

Namespace cls.base
    Public MustInherit Class BaseStaticOverride(Of T)
        Private Shared _instance As T
        Private Shared _lock As New Object()

        Public Shared ReadOnly Property Instance() As T
            Get
                If _instance Is Nothing Then
                    SyncLock _lock
                        If _instance Is Nothing Then
                            _instance = DirectCast(Activator.CreateInstance(GetType(T), True), T)
                        End If
                    End SyncLock
                End If

                Return _instance
            End Get
        End Property
    End Class
End Namespace