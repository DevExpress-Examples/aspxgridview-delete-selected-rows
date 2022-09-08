Imports System
Imports System.Data
Imports DevExpress.Web
Imports System.Collections.Generic
Partial Public Class _Default
    Inherits System.Web.UI.Page

    Private table As DataTable = Nothing
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        If (Not IsCallback) AndAlso (Not IsPostBack) Then
            grid.DataBind()
        End If
    End Sub
    Protected Sub grid_DataBinding(ByVal sender As Object, ByVal e As EventArgs)
        grid.DataSource = GetTable()
    End Sub
    Protected Sub gridView_RowInserting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInsertingEventArgs)
        table = GetTable()
        Dim gridView As ASPxGridView = DirectCast(sender, ASPxGridView)
        Dim row As DataRow = table.NewRow()
        row("ID") = e.NewValues("ID")
        row("Data") = e.NewValues("Data")
        gridView.CancelEdit()
        e.Cancel = True
        table.Rows.Add(row)
    End Sub
    Protected Sub gridView_RowUpdating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs)
        table = GetTable()
        Dim row As DataRow = table.Rows.Find(e.Keys(0))
        row("Data") = e.NewValues("Data")
        grid.CancelEdit()
        e.Cancel = True
    End Sub
    Protected Sub gridView_CellEditorInitialize(ByVal sender As Object, ByVal e As ASPxGridViewEditorEventArgs)
        Dim grid As ASPxGridView = DirectCast(sender, ASPxGridView)
        If e.Column.FieldName = "ID" Then
            Dim textBox As ASPxTextBox = CType(e.Editor, ASPxTextBox)
            textBox.ClientEnabled = False
            If grid.IsNewRowEditing Then
                table = GetTable()
                textBox.Text = GetNewId().ToString()
            End If
        End If
    End Sub
    #Region "#CustomCallbackMethod"
    Protected Sub gridView_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
        If e.Parameters = "Delete" Then
            table = GetTable()
            Dim selectItems As List(Of Object) = grid.GetSelectedFieldValues("ID")
            For Each selectItemId As Object In selectItems
                table.Rows.Remove(table.Rows.Find(selectItemId))
            Next selectItemId
            grid.DataBind()
            grid.Selection.UnselectAll()
        End If
    End Sub
    #End Region ' #CustomCallbackMethod
    Private Function GetNewId() As Integer
        table = GetTable()
        If table.Rows.Count = 0 Then
            Return 0
        End If
        Dim max As Integer = Convert.ToInt32(table.Rows(0)("ID"))
        For i As Integer = 1 To table.Rows.Count - 1
            If Convert.ToInt32(table.Rows(i)("ID")) > max Then
                max = Convert.ToInt32(table.Rows(i)("ID"))
            End If
        Next i
        Return max + 1
    End Function
    Private Function GetTable() As DataTable
        Dim table As DataTable
        If Session("table") Is Nothing Then
            table = New DataTable()
            table.Columns.Add("ID", GetType(Integer))
            table.Columns.Add("Data", GetType(String))
            table.PrimaryKey = New DataColumn() { table.Columns("ID") }
            For i As Integer = 0 To 9
                table.Rows.Add(New Object() { i, "row " & i.ToString() })
            Next i
            Session("table") = table

        Else

            table = DirectCast(Session("table"), DataTable)
        End If
        Return table
    End Function
End Class