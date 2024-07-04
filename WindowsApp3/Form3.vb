Imports MySql.Data.MySqlClient

Public Class Form3

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call koneksi()

        ' Muat data ke DataGridView saat form dimuat
        Try
            myDataAdapter = New MySqlDataAdapter("SELECT * FROM tb_parkir", conn)
            myDataSet = New DataSet()
            myDataAdapter.Fill(myDataSet, "tb_parkir")
            DataGridView1.DataSource = myDataSet.Tables("tb_parkir")
        Catch ex As Exception
            MsgBox("Terjadi kesalahan saat memuat data: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            ' Mengambil nilai dari form
            Dim id As String = TextBox1.Text
            Dim plat As String = TextBox2.Text
            Dim jenis As String = If(ComboBox1.SelectedItem IsNot Nothing, ComboBox1.SelectedItem.ToString(), String.Empty)
            Dim merk As String = TextBox3.Text
            Dim tanggal As String = DateTimePicker1.Value.ToString("yyyy-MM-dd")

            ' Validasi input
            If String.IsNullOrEmpty(id) OrElse String.IsNullOrEmpty(plat) OrElse String.IsNullOrEmpty(jenis) OrElse String.IsNullOrEmpty(merk) Then
                MsgBox("Semua kolom harus diisi", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

            ' Menggunakan parameterized query untuk menghindari SQL injection
            Dim insertCmd As New MySqlCommand("INSERT INTO tb_parkir (id, plat, jenis, merk, tanggal) VALUES (@id, @plat, @jenis, @merk, @tanggal)", conn)
            insertCmd.Parameters.AddWithValue("@id", id)
            insertCmd.Parameters.AddWithValue("@plat", plat)
            insertCmd.Parameters.AddWithValue("@jenis", jenis)
            insertCmd.Parameters.AddWithValue("@merk", merk)
            insertCmd.Parameters.AddWithValue("@tanggal", tanggal)

            ' Eksekusi perintah SQL
            insertCmd.ExecuteNonQuery()
            MsgBox("Data berhasil disimpan", MsgBoxStyle.Information)

            ' Memperbarui DataGridView
            myDataAdapter = New MySqlDataAdapter("SELECT * FROM tb_parkir", conn)
            myDataSet = New DataSet()
            myDataAdapter.Fill(myDataSet, "tb_parkir")
            DataGridView1.DataSource = myDataSet.Tables("tb_parkir")

        Catch ex As Exception
            MsgBox("Terjadi kesalahan: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            ' Pastikan koneksi ditutup
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Call koneksi() ' Pastikan koneksi dibuka

        Try
            ' Mengambil nilai dari form
            Dim id As String = TextBox1.Text
            Dim plat As String = TextBox2.Text
            Dim jenis As String = If(ComboBox1.SelectedItem IsNot Nothing, ComboBox1.SelectedItem.ToString(), String.Empty)
            Dim merk As String = TextBox3.Text
            Dim tanggal As String = DateTimePicker1.Value.ToString("yyyy-MM-dd")

            ' Validasi input
            If String.IsNullOrEmpty(id) OrElse String.IsNullOrEmpty(plat) OrElse String.IsNullOrEmpty(jenis) OrElse String.IsNullOrEmpty(merk) Then
                MsgBox("Semua kolom harus diisi", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

            ' Menggunakan parameterized query untuk menghindari SQL injection
            Dim updateCmd As New MySqlCommand("UPDATE tb_parkir SET 
                                          plat = @plat, 
                                          jenis = @jenis,  
                                          merk = @merk,
                                          tanggal = @tanggal 
                                          WHERE id = @id", conn)
            updateCmd.Parameters.AddWithValue("@id", id)
            updateCmd.Parameters.AddWithValue("@plat", plat)
            updateCmd.Parameters.AddWithValue("@jenis", jenis)
            updateCmd.Parameters.AddWithValue("@merk", merk)
            updateCmd.Parameters.AddWithValue("@tanggal", tanggal)

            ' Eksekusi perintah SQL
            updateCmd.ExecuteNonQuery()
            MsgBox("Data berhasil diperbarui", MsgBoxStyle.Information)

            ' Memperbarui DataGridView
            myDataAdapter = New MySqlDataAdapter("SELECT * FROM tb_parkir", conn)
            myDataSet = New DataSet()
            myDataAdapter.Fill(myDataSet, "tb_parkir")
            DataGridView1.DataSource = myDataSet.Tables("tb_parkir")

        Catch ex As Exception
            MsgBox("Terjadi kesalahan: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            ' Pastikan koneksi ditutup
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.RowIndex >= 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            ' Isi form input dengan data dari baris yang diklik
            TextBox1.Text = selectedRow.Cells("id").Value.ToString()
            TextBox2.Text = selectedRow.Cells("plat").Value.ToString()
            ComboBox1.SelectedItem = selectedRow.Cells("jenis").Value.ToString()
            TextBox3.Text = selectedRow.Cells("merk").Value.ToString()

            ' Penanganan konversi tanggal
            Dim tanggalString As String = selectedRow.Cells("tanggal").Value.ToString()
            Dim tanggal As DateTime
            If DateTime.TryParse(tanggalString, tanggal) Then
                DateTimePicker1.Value = tanggal
            Else
                ' Atur tanggal default jika konversi gagal
                DateTimePicker1.Value = DateTime.Now
                MsgBox("Format tanggal tidak valid. Menggunakan tanggal saat ini sebagai default.", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call koneksi() ' Pastikan koneksi dibuka

        Try
            ' Mengambil nilai id yang dipilih
            Dim id As String = TextBox1.Text

            ' Validasi input
            If String.IsNullOrEmpty(id) Then
                MsgBox("Pilih data yang akan dihapus terlebih dahulu", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

            ' Menggunakan parameterized query untuk menghindari SQL injection
            Dim deleteCmd As New MySqlCommand("DELETE FROM tb_parkir WHERE id = @id", conn)
            deleteCmd.Parameters.AddWithValue("@id", id)

            ' Eksekusi perintah SQL
            deleteCmd.ExecuteNonQuery()
            MsgBox("Data berhasil dihapus", MsgBoxStyle.Information)

            ' Clear form input setelah hapus
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            ComboBox1.SelectedIndex = -1
            DateTimePicker1.Value = DateTime.Now

            ' Memperbarui DataGridView
            myDataAdapter = New MySqlDataAdapter("SELECT * FROM tb_parkir", conn)
            myDataSet = New DataSet()
            myDataAdapter.Fill(myDataSet, "tb_parkir")
            DataGridView1.DataSource = myDataSet.Tables("tb_parkir")

        Catch ex As Exception
            MsgBox("Terjadi kesalahan: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            ' Pastikan koneksi ditutup
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim form1 As New Form1()
        form1.ShowDialog()
        Me.Close()
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Call koneksi()

        Try

            Dim id As String = TextBox1.Text


            If String.IsNullOrEmpty(id) Then
                MsgBox("Pilih data yang akan dihapus terlebih dahulu", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

            Dim deleteCmd As New MySqlCommand("DELETE FROM tb_parkir WHERE id = @id", conn)
            deleteCmd.Parameters.AddWithValue("@id", id)

            deleteCmd.ExecuteNonQuery()
            MsgBox("Data berhasil dihapus", MsgBoxStyle.Information)

            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            ComboBox1.SelectedIndex = -1
            DateTimePicker1.Value = DateTime.Now

            myDataAdapter = New MySqlDataAdapter("SELECT * FROM tb_parkir", conn)
            myDataSet = New DataSet()
            myDataAdapter.Fill(myDataSet, "tb_parkir")
            DataGridView1.DataSource = myDataSet.Tables("tb_parkir")

        Catch ex As Exception
            MsgBox("Terjadi kesalahan: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub
End Class
