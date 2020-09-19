﻿Imports System.IO
Imports System.Text
Public NotInheritable Class Settings
    Public MoveForm As Boolean
    Public MoveForm_MousePosition As Point
    Dim settingsdata As String
    Public parameters As String = ""
    Private Sub Form1_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown, Panel1.MouseDown, Label2.MouseDown
        If e.Button = MouseButtons.Left Then
            MoveForm = True
            'Me.Cursor = Cursors.NoMove2D
            MoveForm_MousePosition = e.Location
        End If
    End Sub

    Private Sub Form1_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove, Panel1.MouseMove, Label2.MouseMove
        If MoveForm Then
            Me.Location = Me.Location + (e.Location - MoveForm_MousePosition)
        End If
    End Sub

    Private Sub Form1_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp, Panel1.MouseUp, Label2.MouseUp
        If e.Button = MouseButtons.Left Then
            MoveForm = False
            'Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Computer.FileSystem.FileExists("config\parameterconfig.bin") Then 'loads parameters
            parameters = My.Computer.FileSystem.ReadAllText("config\parameterconfig.bin")
            TextBox3.Text = parameters
        End If
        TextBox1.Text = MainPage.sourcemodsfolder
        If Not My.Computer.FileSystem.FileExists("config\settingsconfig.bin") Then
            Dim parset As String
            If CheckBox1.Checked Then
                parset = "1"
            Else
                parset = "0"
            End If
            If CheckBox2.Checked Then
                parset = parset & 1
            Else
                parset = parset & 0
            End If
            If CheckBox3.Checked Then
                parset = parset & 1
            Else
                parset = parset & 0
            End If
            If CheckBox4.Checked Then
                parset = parset & 1
            Else
                parset = parset & 0
            End If
            If CheckBox5.Checked Then
                parset = parset & 1
            Else
                parset = parset & 0
            End If
            My.Computer.FileSystem.WriteAllText("config\settingsconfig.bin", parset, True, Encoding.ASCII)
        Else
            settingsdata = My.Computer.FileSystem.ReadAllText("config\settingsconfig.bin")
            If settingsdata(0) = "0" Then
                CheckBox1.Checked = False
            Else
                CheckBox1.Checked = True
            End If
            If settingsdata(1) = "0" Then
                CheckBox2.Checked = False
            Else
                CheckBox2.Checked = True
            End If
            If settingsdata(2) = "0" Then
                CheckBox3.Checked = False
            Else
                CheckBox3.Checked = True
            End If
            If settingsdata(3) = "0" Then
                CheckBox4.Checked = False
            Else
                CheckBox4.Checked = True
            End If
            If settingsdata(4) = "0" Then
                CheckBox5.Checked = False
            Else
                CheckBox5.Checked = True
            End If
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged, CheckBox2.CheckedChanged, CheckBox3.CheckedChanged, CheckBox4.CheckedChanged, CheckBox5.CheckedChanged
        Dim parset As String
        If CheckBox1.Checked Then
            parset = "1"
        Else
            parset = "0"
        End If
        If CheckBox2.Checked Then
            parset = parset & 1
        Else
            parset = parset & 0
        End If
        If CheckBox3.Checked Then
            parset = parset & 1
        Else
            parset = parset & 0
        End If
        If CheckBox4.Checked Then
            parset = parset & 1
        Else
            parset = parset & 0
        End If
        If CheckBox5.Checked Then
            parset = parset & 1
        Else
            parset = parset & 0
        End If
        If Not My.Computer.FileSystem.FileExists("config\settingsconfig.bin") Then
            My.Computer.FileSystem.WriteAllText("config\settingsconfig.bin", parset, True, Encoding.ASCII)
        Else
            My.Computer.FileSystem.DeleteFile("config\settingsconfig.bin")
            My.Computer.FileSystem.WriteAllText("config\settingsconfig.bin", parset, True, Encoding.ASCII)
        End If
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        parameters = TextBox3.Text
        If Not My.Computer.FileSystem.FileExists("config\parameterconfig.bin") Then
            My.Computer.FileSystem.WriteAllText("config\parameterconfig.bin", parameters, True, Encoding.ASCII)
        Else
            My.Computer.FileSystem.DeleteFile("config\parameterconfig.bin")
            My.Computer.FileSystem.WriteAllText("config\parameterconfig.bin", parameters, True, Encoding.ASCII)
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call MainPage.Sourcemodsfolderassign()
    End Sub
End Class
