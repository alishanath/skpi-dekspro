Imports MySql.Data.MySqlClient

Module Module1
    Public conn As MySqlConnection
    Public myDataReader As MySqlDataReader
    Public myDataAdapter As MySqlDataAdapter
    Public myCommand As MySqlCommand
    Public myDataSet As DataSet

    Public Sub koneksi()
        Try
            Dim sqlconn = "server = localhost;
                            username = root;
                            password = ;
                            database = parkir1;
                            Allow Zero Datetime=True;
                           Convert Zero Datetime=True;"
            conn = New MySqlConnection(sqlconn)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        End Sub
End Module
