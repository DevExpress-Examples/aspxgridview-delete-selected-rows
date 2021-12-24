using System;
using System.Data;
using DevExpress.Web;
using System.Collections.Generic;
public partial class _Default : System.Web.UI.Page {
    DataTable table = null;
    protected void Page_Init(object sender, EventArgs e) {
       
    }
    protected void Page_Load(object sender, EventArgs e) {
        if(!IsCallback && !IsPostBack) grid.DataBind();
    }
    protected void grid_DataBinding(object sender, EventArgs e) {
        grid.DataSource = GetTable();
    }
    protected void gridView_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e) {
        table = GetTable();
        ASPxGridView gridView = (ASPxGridView)sender;
        DataRow row = table.NewRow();
        row["ID"] = e.NewValues["ID"];
        row["Data"] = e.NewValues["Data"];
        gridView.CancelEdit();
        e.Cancel = true;
        table.Rows.Add(row);
    }
    protected void gridView_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e) {
        table = GetTable();
        DataRow row = table.Rows.Find(e.Keys[0]);
        row["Data"] = e.NewValues["Data"];
        grid.CancelEdit();
        e.Cancel = true;
    }
    protected void gridView_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e) {
        ASPxGridView grid = (ASPxGridView)sender;
        if(e.Column.FieldName == "ID") {
            ASPxTextBox textBox = (ASPxTextBox)e.Editor;
            textBox.ClientEnabled = false;
            if(grid.IsNewRowEditing) {
                table = GetTable();
                textBox.Text = GetNewId().ToString();
            }
        }
    }
    #region #CustomCallbackMethod
    protected void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e) {
        if(e.Parameters == "Delete") {
            table = GetTable();
            List<Object> selectItems = grid.GetSelectedFieldValues("ID");
            foreach(object selectItemId in selectItems) {
                table.Rows.Remove(table.Rows.Find(selectItemId));
            }
            grid.DataBind();
            grid.Selection.UnselectAll();
        }
    }
    #endregion #CustomCallbackMethod
    private int GetNewId() {
        table = GetTable();
        if(table.Rows.Count == 0) return 0;
        int max = Convert.ToInt32(table.Rows[0]["ID"]);
        for(int i = 1; i < table.Rows.Count; i++) {
            if(Convert.ToInt32(table.Rows[i]["ID"]) > max)
                max = Convert.ToInt32(table.Rows[i]["ID"]);
        }
        return max + 1;
    }
    private DataTable GetTable(){
        DataTable table;
        if(Session["table"] == null) {
            table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Data", typeof(string));
            table.PrimaryKey = new DataColumn[] { table.Columns["ID"] };
            for(int i = 0; i < 10; i++) {
                table.Rows.Add(new object[] { i, "row " + i.ToString() });
            }
            Session["table"] = table;

        } else {

            table = (DataTable)Session["table"];
        }
        return table;
    }
}