Imports System.ComponentModel
Imports System.Net.NetworkInformation
Imports CefSharp.WinForms
Imports CefSharp

Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Inicializa o CefSharp
        Cef.Initialize(New CefSettings())

        ' Inicializa os ChromiumWebBrowsers
        ChromiumWebBrowser1 = New ChromiumWebBrowser("http://192.168.29.1")
        ChromiumWebBrowser2 = New ChromiumWebBrowser("http://192.168.1.1")

        ' Adiciona os browsers ao formulário
        Panel1.Controls.Add(ChromiumWebBrowser1)
        ChromiumWebBrowser1.Dock = DockStyle.Fill

        Panel2.Controls.Add(ChromiumWebBrowser2)
        ChromiumWebBrowser2.Dock = DockStyle.Fill
    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        If BackgroundWorker1.IsBusy Then
            ' Se o BackgroundWorker estiver ocupado, cancela a operação
            BackgroundWorker1.CancelAsync()
        Else
            ' Inicia a execução do ping em uma thread separada
            BackgroundWorker1.RunWorkerAsync()
            Button1.Text = "Parar"

            ' Atualiza os ChromiumWebBrowsers
            ChromiumWebBrowser1.Load("http://192.168.29.1")
            ChromiumWebBrowser2.Load("http://192.168.1.1")
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        ' Abrir a interface web para o endereço 192.168.29.1 no navegador padrão
        Process.Start("http://192.168.29.1")
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        ' Abrir a interface web para o endereço 192.168.1.1 no navegador padrão
        Process.Start("http://192.168.1.1")
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        ' PC 1
        AtualizarStatusPc("108.139.10.22", PictureBox2)

        ' PC 2
        AtualizarStatusPc("34.107.153.189", PictureBox3)

        ' PC 3
        AtualizarStatusPc("142.250.189.206", PictureBox4)

        ' PC 4
        AtualizarStatusPc("192.168.29.1", PictureBox5)

        ' PC 5
        AtualizarStatusPc("192.168.1.1", PictureBox6)

        ' Atualiza o texto do botão quando a execução termina
        If Not BackgroundWorker1.CancellationPending Then
            Me.Invoke(Sub() Button1.Text = "Iniciar")
        End If
    End Sub

    Private Sub AtualizarStatusPc(ByVal ip As String, ByVal pictureBox As PictureBox)
        Dim ping As New Ping()
        Dim resposta As PingReply

        Try
            resposta = ping.Send(ip)

            If resposta.Status = IPStatus.Success Then
                pictureBox.Image = My.Resources.icone_du_bouton_en_ligne_vert__1_
            Else
                pictureBox.Image = My.Resources.Screenshot_51_removebg_preview_resized
            End If
        Catch ex As Exception
            pictureBox.Image = My.Resources.Screenshot_51_removebg_preview_resized
        End Try
    End Sub

    Private Sub Button9_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button9.Click
        ' Navegar para a página desejada quando o botão é clicado
        ChromiumWebBrowser1.Load("http://192.168.29.1/state/opt_power.asp")
    End Sub

    Private Sub Button10_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button10.Click
        ' Navegar para a página desejada quando o botão é clicado
        ChromiumWebBrowser1.Load("http://192.168.29.1/application/ping_diagnosis.asp")
    End Sub

    Private Sub Button11_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button11.Click
        ' Navegar para a página desejada quando o botão é clicado
        ChromiumWebBrowser2.Load("http://192.168.1.1/state/opt_power.asp")
    End Sub

    Private Sub Button12_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button12.Click
        ' Navegar para a página desejada quando o botão é clicado
        ChromiumWebBrowser2.Load("http://192.168.1.1/application/ping_diagnosis.asp")
    End Sub

    Private Sub PictureBox11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox11.Click

    End Sub
End Class
