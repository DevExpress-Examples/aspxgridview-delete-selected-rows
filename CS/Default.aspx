<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<!--region #Markup-->
<head runat="server">
    <script type="text/javascript">
        function OnClickButtonDel(s, e) {
            grid.PerformCallback('Delete');
        }
    </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <dx:ASPxGridView ID="grid" KeyFieldName="ID" runat="server" AutoGenerateColumns="False"
        OnCellEditorInitialize="gridView_CellEditorInitialize" OnRowInserting="gridView_RowInserting"
        OnRowUpdating="gridView_RowUpdating" ClientInstanceName="grid" 
        OnCustomCallback="gridView_CustomCallback" 
        ondatabinding="grid_DataBinding" >
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" ShowSelectCheckbox="True">
                <EditButton Visible="True">
                </EditButton>
                <NewButton Visible="True">
                </NewButton>
                <UpdateButton Visible="True">
                </UpdateButton>
                <FooterTemplate>
                    <dx:ASPxButton ID="buttonDel" AutoPostBack="false" runat="server" Text="Delete">
                        <ClientSideEvents Click="OnClickButtonDel" />
                    </dx:ASPxButton>
                </FooterTemplate>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="ID" ReadOnly="True" VisibleIndex="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Data" VisibleIndex="2">
            </dx:GridViewDataTextColumn>
        </Columns>
        <Settings ShowFooter="True" />
    </dx:ASPxGridView>
    </div>
    </form>
</body>
<!--endregion #Markup-->
</html>
