﻿Imports System.Data.SqlClient
Public Class frmEnroll
    Protected db As New db
    Protected sid As Integer
    Public Sub New(ByVal id As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        sid = id
    End Sub
    Protected Sub fillEnroll()
        db.sql = "SELECT * FROM Enrollment Where SID = @id"
        db.bind("@id", sid)
        db.fill(dgvEnroll)
    End Sub

    Private Sub frmEnroll_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim connection As New SqlConnection With {.ConnectionString = "Server=essql1.walton.uark.edu;Database=arshephe; Trusted_Connection=yes"}
        Dim command As New SqlCommand("Select * FROM Enrollment where SID = @id", connection)
        command.Parameters.Add("@id", SqlDbType.VarChar).Value = sid


        Dim adapter As New SqlDataAdapter(command)
        Dim table As New DataTable()
        adapter.Fill(table)
        If table.Rows.Count() <= 0 Then
            dgvEnroll.Visible = False
            lblNoClass.Visible = True
        Else
            lblNoClass.Visible = False
            dgvEnroll.Visible = True
            fillEnroll()

        End If
    End Sub

    Private Sub btnEnroll_Click(sender As Object, e As EventArgs) Handles btnEnroll.Click
        Dim enroll As New frmAddClass(sid)
        enroll.ShowDialog()
        fillEnroll()

    End Sub
End Class