Imports System.IO
Imports System.Text
Public Class MainPage
    Dim modsfolder As DirectoryInfo
    Public sourcemodsfolder As String 'sourcemods folder
    Dim client As New Net.WebClient 'WEB
    Dim areweonline As Boolean = False
    Public MoveForm As Boolean
    Public MoveForm_MousePosition As Point
    Dim drive As String
    Dim names As New List(Of String)
    Dim ids As New List(Of String)
    Dim links As New List(Of String)
    Dim folders As New List(Of String)
    Dim codecd As String = ":
cd "
    'PLAY
    Dim code1 As String = "start """" ""steam://rungameid/"
    Dim code2 As String = "//"
    Dim code3 As String = "/""
md endplay
exit"
    'RESET
    'git
    Dim reset1 As String = "
title Reset [DO NOT CLOSE THIS UNTIL IT IS DONE]
cls
@echo off
setlocal EnableDelayedExpansion  
echo Resetting
echo.
color 2f
git reset --hard
md  endreset
Pause"
    'INSTALL
    'GIT
    Dim ins1 As String = "
title Installer [DO NOT CLOSE THIS UNTIL IT IS DONE]
cls
@echo off
setlocal EnableDelayedExpansion  
echo Mod Installer
echo.
echo This might take some time, don't run the game or don't close the window while this is happening
echo If you have any issues on installation, make sure to reset or reinstall
echo Installing
echo.
color 2f
git clone --depth 1 --progress """
    Dim ins2 As String = """
md endinstall
echo Restart Steam after the installation is complete
Pause"
    Dim ins22 As String = """
md endinstall
echo Restart Steam after the installation is complete
Pause"
    'UPDATE
    'GIT
    Dim upd1 As String = "
title Updater [DO NOT CLOSE THIS UNTIL IT IS DONE]
cls
@echo off
setlocal EnableDelayedExpansion
color 2f  
echo Mod Updater
echo.
git fetch --depth 1
echo Checking for Updates
FOR /f %%i IN ('git rev-parse origin/master') DO set LatestRevision=%%i
git reset --hard !LatestRevision!
echo This might take some time, don't run the game while this is happening
echo.
echo Updating existing install
git pull --depth 1 --progress --force """
    Dim upd2 As String = """
md endupdate
Pause"
    Dim upd22 As String = """
md endupdate
Exit"
    Private Sub MainPage_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown, Panel1.MouseDown, NewsAndChangelog.MouseDown, Panel2.MouseDown, Label1.MouseDown
        If e.Button = MouseButtons.Left Then
            MoveForm = True
            'Me.Cursor = Cursors.NoMove2D
            MoveForm_MousePosition = e.Location
        End If
    End Sub

    Private Sub MainPage_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove, Panel1.MouseMove, NewsAndChangelog.MouseMove, Panel2.MouseMove, Label1.MouseMove
        If MoveForm Then
            Me.Location = Me.Location + (e.Location - MoveForm_MousePosition)
        End If
    End Sub

    Private Sub MainPage_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp, Panel1.MouseUp, NewsAndChangelog.MouseUp, Panel2.MouseUp, Label1.MouseUp
        If e.Button = MouseButtons.Left Then
            MoveForm = False
            ' Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'config name
        If Not My.Computer.FileSystem.DirectoryExists("config") Then
            My.Computer.FileSystem.CreateDirectory("config")
            MsgBox("Install git scm if you did'nt already, it is required for app to work: https://git-scm.com/download/win")
        End If
        If My.Computer.FileSystem.FileExists("config\parameterconfig.bin") Then 'loads parameters
            Settings.parameters = My.Computer.FileSystem.ReadAllText("config\parameterconfig.bin")
            Settings.TextBox3.Text = Settings.parameters
        End If

        '[MAIN]Check if sourcemods folder is defined, if not open a openfolder for it else just assign it to a variable and move on
        If Not My.Computer.FileSystem.FileExists("config\launcherconfig.bin") Then
            '[MAIN] Check if there is a sourcemods folder on places like C:\Program Files\Steam\steamapps\sourcemods
            If My.Computer.FileSystem.DirectoryExists("C:\Program Files\Steam\steamapps\sourcemods") Then
                sourcemodsfolder = "C:\Program Files\Steam\steamapps\sourcemods"
                My.Computer.FileSystem.WriteAllText("config\launcherconfig.bin", sourcemodsfolder, True)
                modsfolder = New DirectoryInfo(sourcemodsfolder)
                Settings.TextBox1.Text = sourcemodsfolder
            ElseIf My.Computer.FileSystem.DirectoryExists("D:\Program Files\Steam\steamapps\sourcemods") Then
                sourcemodsfolder = "D:\Program Files\Steam\steamapps\sourcemods"
                My.Computer.FileSystem.WriteAllText("config\launcherconfig.bin", sourcemodsfolder, True)
                modsfolder = New DirectoryInfo(sourcemodsfolder)
                Settings.TextBox1.Text = sourcemodsfolder
            ElseIf My.Computer.FileSystem.DirectoryExists("C:\Program Files (x86)\Steam\steamapps\sourcemods") Then
                sourcemodsfolder = "C:\Program Files (x86)\Steam\steamapps\sourcemods"
                My.Computer.FileSystem.WriteAllText("config\launcherconfig.bin", sourcemodsfolder, True)
                modsfolder = New DirectoryInfo(sourcemodsfolder)
                Settings.TextBox1.Text = sourcemodsfolder
            ElseIf My.Computer.FileSystem.DirectoryExists("D:\Program Files (x86)\Steam\steamapps\sourcemods") Then
                sourcemodsfolder = "D:\Program Files (x86)\Steam\steamapps\sourcemods"
                My.Computer.FileSystem.WriteAllText("config\launcherconfig.bin", sourcemodsfolder, True)
                modsfolder = New DirectoryInfo(sourcemodsfolder)
                Settings.TextBox1.Text = sourcemodsfolder
            Else
                'if it is unknown location
browser:
                Dim result As DialogResult = FolderBrowserDialog1.ShowDialog()
                If result = Windows.Forms.DialogResult.OK And My.Computer.FileSystem.DirectoryExists(FolderBrowserDialog1.SelectedPath) Then
                    Dim path As String = FolderBrowserDialog1.SelectedPath
                    sourcemodsfolder = FolderBrowserDialog1.SelectedPath
                    My.Computer.FileSystem.WriteAllText("config\launcherconfig.bin", sourcemodsfolder, True)
                    modsfolder = New DirectoryInfo(sourcemodsfolder)
                    Settings.TextBox1.Text = sourcemodsfolder
                Else
                    GoTo browser
                End If
            End If
        ElseIf Not My.Computer.FileSystem.ReadAllText("config\launcherconfig.bin") = Nothing Then
            sourcemodsfolder = My.Computer.FileSystem.ReadAllText("config\launcherconfig.bin")
            modsfolder = New DirectoryInfo(sourcemodsfolder)
            Settings.TextBox1.Text = sourcemodsfolder
        Else
            MsgBox("If you are seeing this, something fishy is going on. Make sure to contact Doruk.")
        End If
        '[MAIN] find all the mods, and list them
        Call Listing()
        Try
            areweonline = True
            PictureBox1.BackgroundImage = doglauncher.My.Resources.Resource1.online
            NewsAndChangelog.Text = client.DownloadString("https://raw.githubusercontent.com/DorukSega/launcherdatabed/master/dognews.txt")

        Catch ex As Exception
            areweonline = False
            MsgBox("Can't connect to net, please restart after system is online")
            Label4.Visible = True
        End Try
    End Sub
    Public Sub Listing()
        If Not My.Computer.FileSystem.DirectoryExists("config") Then
            My.Computer.FileSystem.CreateDirectory("config")
        End If
        drive = sourcemodsfolder.First
        ComboBox1.Items.Clear()
        Dim databed As String
        Try
            databed = client.DownloadString("https://raw.githubusercontent.com/DorukSega/launcherdatabed/master/launchdatadog.txt")
            areweonline = True
            PictureBox1.BackgroundImage = doglauncher.My.Resources.Resource1.online
            Dim id As String = ""
            Dim name As String = ""
            Dim link As String = ""
            Dim folder As String = ""
            databed = databed.Replace(vbCrLf, "")
            Do While databed.Contains("FOLD")
                Dim b As Integer
                Dim a As Integer
                'Folder
                a = databed.IndexOf("FOLD") + 4
                If a >= 0 Then
                    folder = databed.Substring(a)
                End If
                b = folder.IndexOf("NAME")
                If b >= 0 Then
                    folder = folder.Remove(b)
                End If
                folders.Add(folder)
                'NAME
                a = databed.IndexOf("NAME") + 4
                If a >= 0 Then
                    name = databed.Substring(a)
                End If
                b = name.IndexOf("ID")
                If b >= 0 Then
                    name = name.Remove(b)
                End If
                ComboBox1.Items.Add(name)
                names.Add(name)
                'ID
                a = databed.IndexOf("ID") + 2
                If a >= 0 Then
                    id = databed.Substring(a)
                End If
                b = id.IndexOf("LINK")
                If b >= 0 Then
                    id = id.Remove(b)
                End If
                ids.Add(id)
                'LINK
                a = databed.IndexOf("LINK") + 4
                If a >= 0 Then
                    link = databed.Substring(a)
                End If
                b = link.IndexOf("END")
                If b >= 0 Then
                    link = link.Remove(b)
                End If
                links.Add(link)
                databed = databed.Substring(databed.IndexOf("END") + 2)
            Loop
        Catch ex As Exception
            areweonline = False
            MsgBox("Can't connect to net, please restart after system is online")
            'put a offline sign
            'this is preset offline stuff, just for launching I hope
            folders.Add("Dog")
            names.Add("Day of Glory: Offline")
            ComboBox1.Items.Add("Day of Glory: Offline")
            ids.Add("10904300306699630630") 'Don't forget to update this
            Label4.Visible = True
        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.Text <> "" Then
            If My.Computer.FileSystem.DirectoryExists(sourcemodsfolder & "\" & folders.Item(ComboBox1.SelectedIndex)) Then
                Button4.Text = "Reset"
                Button5.Enabled = True
            Else
                Button4.Text = "Install"
                Button5.Enabled = False
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If names.Contains(ComboBox1.Text) Then
            If Settings.CheckBox1.CheckState = CheckState.Checked Then
                Call updatemod()
            End If
            If My.Computer.FileSystem.FileExists("gamelaunch.bat") Then
                My.Computer.FileSystem.DeleteFile("gamelaunch.bat")
            Else
            End If
            My.Computer.FileSystem.WriteAllText("gamelaunch.bat", code1 & ids.Item(ComboBox1.SelectedIndex) & code2 & Settings.parameters & code3, True, Encoding.ASCII)
            Shell("gamelaunch.bat", AppWinStyle.Hide)
            Do Until My.Computer.FileSystem.DirectoryExists("endplay")
                Threading.Thread.Sleep(100)
            Loop
            My.Computer.FileSystem.DeleteDirectory("endplay", FileIO.DeleteDirectoryOption.DeleteAllContents)
            My.Computer.FileSystem.DeleteFile("gamelaunch.bat")
            If Settings.CheckBox5.CheckState = CheckState.Checked Then
                Me.Close()
            End If
        Else
            MsgBox("There is no mod picked", 6, "Launch")
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If names.Contains(ComboBox1.Text) Then
            If Button4.Text = "Install" Then
                Button4.Enabled = False
                Button2.Enabled = False
                Button5.Enabled = False
                Dim curlink As String = links.Item(ComboBox1.SelectedIndex)
                If My.Computer.FileSystem.FileExists("install.bat") Then
                    My.Computer.FileSystem.DeleteFile("install.bat")
                Else
                End If
                'add a option to change to hidden
                My.Computer.FileSystem.WriteAllText("install.bat", drive & codecd & sourcemodsfolder & ins1 & curlink & ins2, True, Encoding.ASCII)
                If Settings.CheckBox3.CheckState = CheckState.Checked Then
                    Shell("install.bat", AppWinStyle.Hide)
                Else
                    Shell("install.bat", AppWinStyle.NormalFocus)
                End If

                Do Until My.Computer.FileSystem.DirectoryExists(sourcemodsfolder & "\endinstall")
                    Threading.Thread.Sleep(100)
                Loop
                Button4.Enabled = True
                Button2.Enabled = True
                Button5.Enabled = True
                If My.Computer.FileSystem.DirectoryExists(sourcemodsfolder & "\" & folders.Item(ComboBox1.SelectedIndex)) Then
                    Button4.Text = "Reset"
                    Button5.Enabled = True
                Else
                    Button4.Text = "Install"
                    Button5.Enabled = False
                End If
                My.Computer.FileSystem.DeleteDirectory(sourcemodsfolder & "\endinstall", FileIO.DeleteDirectoryOption.DeleteAllContents)
                My.Computer.FileSystem.DeleteFile("install.bat")
                MsgBox("Restart Steam before launching")
            ElseIf Button4.Text = "Reset" Then
                Dim curlink As String = links.Item(ComboBox1.SelectedIndex)
                Dim curdirectory As String = sourcemodsfolder & "\" & folders.Item(ComboBox1.SelectedIndex)
                Button4.Enabled = False
                Button2.Enabled = False
                Button5.Enabled = False
                'add bash code here
                If My.Computer.FileSystem.FileExists("reset.bat") Then
                    My.Computer.FileSystem.DeleteFile("reset.bat")
                Else
                End If
                My.Computer.FileSystem.WriteAllText("reset.bat", drive & codecd & curdirectory & reset1, True, Encoding.ASCII)
                'add a option to change to hidden
                If Settings.CheckBox4.CheckState = CheckState.Checked Then
                    Shell("reset.bat", AppWinStyle.Hide)
                Else
                    Shell("reset.bat", AppWinStyle.NormalFocus)
                End If
                Do Until My.Computer.FileSystem.DirectoryExists(curdirectory & "\endreset")
                    Threading.Thread.Sleep(100)
                Loop
                My.Computer.FileSystem.DeleteDirectory(curdirectory & "\endreset", FileIO.DeleteDirectoryOption.DeleteAllContents)
                My.Computer.FileSystem.DeleteFile("reset.bat")
                Button4.Enabled = True
                Button2.Enabled = True
                Button5.Enabled = True
                If My.Computer.FileSystem.DirectoryExists(sourcemodsfolder & "\" & folders.Item(ComboBox1.SelectedIndex)) Then
                    Button4.Text = "Reset"
                    Button5.Enabled = True
                Else
                    Button4.Text = "Install"
                    Button5.Enabled = False
                End If
            End If
        Else
            MsgBox("There is no mod picked", 6, "Launch")
        End If

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If names.Contains(ComboBox1.Text) And areweonline = True Then
            Call updatemod()
        End If
    End Sub
    Public Sub Sourcemodsfolderassign()
        If Not My.Computer.FileSystem.DirectoryExists("config") Then
            My.Computer.FileSystem.CreateDirectory("config")
        End If
        If My.Computer.FileSystem.FileExists("config\launcherconfig.bin") Then
            My.Computer.FileSystem.DeleteFile("config\launcherconfig.bin")
        End If
browser:
        Dim result As DialogResult = FolderBrowserDialog1.ShowDialog()
        If result = Windows.Forms.DialogResult.OK And My.Computer.FileSystem.DirectoryExists(FolderBrowserDialog1.SelectedPath) Then
            Dim path As String = FolderBrowserDialog1.SelectedPath
            sourcemodsfolder = FolderBrowserDialog1.SelectedPath
            Settings.TextBox1.Text = sourcemodsfolder
            My.Computer.FileSystem.WriteAllText("config\launcherconfig.bin", sourcemodsfolder, True)
            modsfolder = New DirectoryInfo(sourcemodsfolder)
        Else
            GoTo browser
        End If
        Call Listing()
    End Sub
    Public Sub updatemod()
        If names.Contains(ComboBox1.Text) Then
            Dim curlink As String = links.Item(ComboBox1.SelectedIndex)
            Dim curdirectory As String = sourcemodsfolder & "\" & folders.Item(ComboBox1.SelectedIndex)
            Button4.Enabled = False
            Button2.Enabled = False
            Button5.Enabled = False
            'add bash code here
            If My.Computer.FileSystem.FileExists("update.bat") Then
                My.Computer.FileSystem.DeleteFile("update.bat")
            End If
            'add a option to change to hidden
            My.Computer.FileSystem.WriteAllText("update.bat", drive & codecd & curdirectory & upd1 & curlink & upd2, True, Encoding.ASCII)
            If Settings.CheckBox2.CheckState = CheckState.Checked Then
                Shell("update.bat", AppWinStyle.Hide)
            Else
                Shell("update.bat", AppWinStyle.NormalFocus)
            End If
            Do Until My.Computer.FileSystem.DirectoryExists(curdirectory & "\endupdate")
                Threading.Thread.Sleep(100)
            Loop
            My.Computer.FileSystem.DeleteDirectory(curdirectory & "\endupdate", FileIO.DeleteDirectoryOption.DeleteAllContents)
            My.Computer.FileSystem.DeleteFile("update.bat")
            Button4.Enabled = True
            Button2.Enabled = True
            Button5.Enabled = True
            If My.Computer.FileSystem.DirectoryExists(sourcemodsfolder & "\" & folders.Item(ComboBox1.SelectedIndex)) Then
                Button4.Text = "Reset"
                Button5.Enabled = True
            Else
                Button4.Text = "Install"
                Button5.Enabled = False
            End If
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If Settings.Visible = False Then
            Settings.Show()
            Settings.Location = New Point(MainPage.MousePosition.X - 400, MainPage.MousePosition.Y + 50)
        Else
            Settings.Close()
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        If areweonline = False Then
            Call Listing()
            Label4.Visible = False
        End If
    End Sub
End Class
