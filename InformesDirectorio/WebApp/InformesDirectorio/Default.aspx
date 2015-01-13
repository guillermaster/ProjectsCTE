<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="css/DocsArea.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveNewDocument" />
        </Triggers>
        <ContentTemplate>
            <div id="mainDocArea">
                <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="error">
                    <asp:Label ID="lblError" runat="server" Text="" />
                </asp:Panel>
                <asp:Panel ID="pnlSuccess" runat="server" Visible="false" CssClass="success">
                    <asp:Label ID="lblSuccess" runat="server" Text="" />
                </asp:Panel>             
                <div id="toolbar" style="height: 30px">
                    <asp:ImageButton ID="btnNewFolder" runat="server" ImageUrl="~/images/icoNvoDir.png"
                        OnClick="btnNewFolder_Click" ClientIDMode="Static" Visible="false" />
                    <asp:ImageButton ID="btnNewDocument" runat="server" ImageUrl="~/images/icoNvoDoc.png"
                        OnClick="btnNewDocument_Click" ClientIDMode="Static" />
                    <asp:Panel ID="pnlNewFolder" runat="server" Style="padding: 10px; text-align: center;
                        border: solid thin #000000" BackColor="AntiqueWhite" ScrollBars="None">
                        <div>
                            <h3>
                                Nueva Carpeta</h3>
                            <asp:Label ID="lblFolderName" runat="server" Text="Nombre:" />
                            <asp:TextBox ID="txtFolderName" runat="server"></asp:TextBox>
                            <asp:Button ID="btnSaveNewFolder" runat="server" Text="Crear carpeta" OnClick="btnSaveNewFolder_Click" />
                            <asp:Button ID="btnCancelNewFolder" runat="server" Text="Cancelar" OnClick="btnCancelNewFolder_Click" />
                        </div>
                    </asp:Panel>
                    <asp:ModalPopupExtender ID="mpeNewFolder" runat="server" TargetControlID="ButtonNewFolder"
                        PopupControlID="pnlNewFolder" BackgroundCssClass="modalBackground" DropShadow="false"
                        CancelControlID="btnCancelNewFolder" />
                    <asp:Panel ID="pnlNewDocument" runat="server" Style="padding: 10px; text-align: center;
                        border: solid thin #000000" BackColor="AntiqueWhite" ScrollBars="None">
                        <div>
                            <h3>Nuevo Documento</h3>
                            <div style="margin: 3px">
                                <asp:Label ID="lblNewDocFile" runat="server" Text="Archivo:" />
                                <asp:FileUpload ID="FileUpload1" runat="server" ClientIDMode="Static" />
                            </div>
                            <div style="margin: 3px">
                                <asp:Label ID="lblNewDocTitle" runat="server" Text="Título:" />
                                <asp:TextBox ID="txtNewDocTitle" runat="server" Width="220px"></asp:TextBox>
                            </div>
                            <div style="margin: 3px">
                                <asp:Button ID="btnSaveNewDocument" runat="server" Text="Publicar" OnClick="btnSaveNewDocument_Click" OnClientClick="javascript:showWaitNewDocUpload()" />
                                <asp:Button ID="btnCancelNewDocument" runat="server" Text="Cancelar" />
                            </div>
                            <asp:UpdateProgress ID="upprogNewDoc" runat="server" AssociatedUpdatePanelID="UpdatePanel1" ClientIDMode="Static">
                                <ProgressTemplate>
                                    <asp:Label ID="lblWait" runat="server" BackColor="#507CD1" Font-Bold="True" ForeColor="White"
                                        Text="Publicando archivo... Por favor espere"></asp:Label>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                    </asp:Panel>
                    <asp:ModalPopupExtender ID="mpeNewDocument" runat="server" TargetControlID="ButtonNewDocument"
                        PopupControlID="pnlNewDocument" BackgroundCssClass="modalBackground" DropShadow="false"
                        CancelControlID="btnCancelNewDocument" />
                    <asp:Button ID="ButtonNewFolder" runat="server" Style="display: none" />
                    <asp:Button ID="ButtonNewDocument" runat="server" Style="display: none" />
                </div>
                <div id="docsListArea">
                    
                    <asp:Panel ID="pnlSharing" runat="server" Style="padding: 10px; text-align: center;
                        border: solid thin #000000" BackColor="AntiqueWhite" ScrollBars="None">
                        <div>
                            <h3>Compartir Documento</h3>
                            <div style="margin: 3px">
                                <asp:Label ID="lblCompartido" runat="server" Text="Este documento se encuentra compartido con " />
                                
                            </div>
                            <div style="margin: 3px">
                                <asp:Label ID="lblCompartir" runat="server" Text="Compartir documento con:" />
                                <asp:CheckBoxList ID="chkListUsersToShare" runat="server">
                                </asp:CheckBoxList>
                            </div>
                            <div style="margin: 3px">
                                <asp:Button ID="btnShare" runat="server" Text="Compartir" OnClick="btnShare_Click" OnClientClick="javascript:showWaitSharing()" />
                                <asp:Button ID="btnShareCancel" runat="server" Text="Cancelar" />
                            </div>
                            <asp:UpdateProgress ID="upprogSharing" runat="server" AssociatedUpdatePanelID="UpdatePanel1" ClientIDMode="Static">
                                <ProgressTemplate>
                                    <asp:Label ID="lblWaitSharing" runat="server" BackColor="#507CD1" Font-Bold="True" ForeColor="White"
                                        Text="Compartiendo archivo... Por favor espere"></asp:Label>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                    </asp:Panel>
                    <asp:ModalPopupExtender ID="mpeSharing" runat="server" TargetControlID="ButtonSharing"
                        PopupControlID="pnlSharing" BackgroundCssClass="modalBackground" DropShadow="false"
                        CancelControlID="btnShareCancel" />
                    <asp:Button ID="ButtonSharing" runat="server" Style="display: none" />

                    <table style="width: 100%; min-width: 680px" border="0">
                        <tr>
                            <td style="width: 100%">
                                <asp:GridView ID="gvFolderList" runat="server" Width="100%" 
                                    AutoGenerateColumns="False" Visible="false">
                                    <Columns>
                                        <asp:BoundField DataField="IdCarpeta" Visible="False" />
                                        <asp:ImageField DataImageUrlField="ICONO">
                                        </asp:ImageField>
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="Fecha de creación" HeaderText="Fecha de creación" />
                                        <asp:BoundField DataField="Creado por" HeaderText="Creado por" />
                                    </Columns>
                                </asp:GridView>  
                                <asp:GridView ID="gvDocsList" runat="server" Width="100%" 
                                    AutoGenerateColumns="False" 
                                    onselectedindexchanged="gvDocsList_SelectedIndexChanged" OnRowDataBound="gvDocumentsRowDataBound">
                                    <Columns>
                                        <asp:ImageField DataImageUrlField="icono">
                                        </asp:ImageField>
                                        <asp:BoundField DataField="idDocumento" />
                                        <asp:HyperLinkField DataNavigateUrlFields="url" DataTextField="Nombre" 
                                            HeaderText="Nombre" Target="_blank" />
                                        <asp:BoundField DataField="Título" HeaderText="Título" />
                                        <asp:BoundField DataField="Fecha de creación" HeaderText="Fecha de creación" />
                                        <asp:BoundField DataField="Creado por" HeaderText="Creado por" />
                                        <asp:CommandField ButtonType="Image" SelectImageUrl="~/images/sharing.png" 
                                            ShowSelectButton="True"  />
                                        <asp:BoundField DataField="id_carpeta" SortExpression="id_carpeta" 
                                            Visible="False" />
                                        <asp:BoundField DataField="IdPermiso" />
                                    </Columns>
                                </asp:GridView>                                
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
