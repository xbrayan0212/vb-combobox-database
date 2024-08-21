Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ListView
Imports MySql.Data.MySqlClient

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LlenarComboBox("SELECT * FROM provincia", ComboBox2, "nombre_provincia", "codigo_provincia")
     
    End Sub

    Private Sub ComboBox2_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedValueChanged

       If ComboBox2.SelectedValue IsNot Nothing Then
            LlenarComboBox("SELECT * FROM distrito WHERE codigo_provincia = @codigo", ComboBox3, "nombre_distrito", "codigo_distrito", ComboBox2.SelectedValue)
        End If

    End Sub

    Private Sub ComboBox3_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedValueChanged
        If ComboBox3.SelectedValue IsNot Nothing Then
            LlenarComboBox("SELECT * FROM corregimiento WHERE codigo_distrito = @codigo", ComboBox4, "nombre_corregimiento", "codigo_corregimiento", ComboBox3.SelectedValue)
        End If
    End Sub

    Private Sub LlenarComboBox(consulta As String, comboBox As ComboBox, DisplayMember As String, valueMember As String, Optional parametro As Object = Nothing)
        Dim cm As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim ds1 As DataSet
        Dim conexion As String = "Server=localhost;database=bddesarrollo8;user id=brayan;password=12345678;"
        Dim miconexion As New MySqlConnection(conexion)

        Try
            miconexion.Open()

            cm = New MySqlCommand(consulta, miconexion)
            cm.CommandType = CommandType.Text

            If parametro IsNot Nothing Then
                cm.Parameters.AddWithValue("@codigo", parametro)
            End If

            da = New MySqlDataAdapter(cm)
            ds1 = New DataSet()
            da.Fill(ds1)

            comboBox.DataSource = ds1.Tables(0)
            comboBox.DisplayMember = DisplayMember
            comboBox.ValueMember = valueMember


        Catch ex As Exception
            MsgBox("Error de Conexión: " & ex.Message)

        Finally
            miconexion.Close()
        End Try
    End Sub
End Class
