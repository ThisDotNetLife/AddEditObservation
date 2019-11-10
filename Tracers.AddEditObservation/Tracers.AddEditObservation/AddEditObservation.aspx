<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditObservation.aspx.cs" Inherits="DynamicTableCreation.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/2.0.3/jquery.min.js"></script>
    <link href="Content/Styles/AddEditObservation.css" rel="stylesheet" />
</head>
    
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 100%;">
                <tr><td colspan="3" class="pageTitle"><%=PageTitle%></td></tr>
                <tr><td colspan="3" style="color: red;">* = Required</td></tr>
            </table>

            <table style="width: 100%;" align="center" border="0">
                <tr valign="top">
                    <td>
                        <!-- Observation Header Title Bar containing Tracer Name and Category -->
                        <table align="center" style="border: 1px solid #74A3A3;" width="100%" border="0" cellpadding="2" cellspacing="2">
                            <tr class="gridHeader">
                                <td colspan="4" style="border: 1px solid #74A3A3;" height="17px"><span class="labelTitle" style="float: left;"><b>Tracer Name:</b>&nbsp;<%=TracerName %></span>
                                    <span class="labelTitle" style="float: right;"><b>Tracer Category:</b>&nbsp;<%=TracerCategory %></span>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="labelTitle">Observation Title:
                                    <span style="color:Red; font-weight:bold;"> * </span>
                                    <img id='errorObservationTitle' src='Content/warning.png' title='' style='width:16px; height:16px; padding-top:0px; padding-left: 6px; display:none;' alt='' /></td>
                                <td align="left"><asp:TextBox ID="txtRecordingTitle" runat="server" MaxLength="255" style="width:99%;" ClientIDMode="Static"></asp:TextBox></td>
                                <td align="left" class="labelTitle" style="padding-left: 8px;">Department Name:
                                    <span style="color:Red; font-weight:bold;"> * </span>
                                    <img id='errorDepartmentName' src='Content/warning.png' title='' style='width:16px; height:16px; padding-top:0px; padding-left: 6px; display:none;' alt='' /></td>
                                <td align="left"><asp:DropDownList ID="cmbDepartment" runat="server" Width="330px" ClientIDMode="Static"></asp:DropDownList></td>

                            </tr>                                
                            <tr>
                                <td align="left" valign="top" class="labelTitle">Survey Team:</td>
                                <td align="left" valign="top"><asp:TextBox ID="txtSurveyTeam" runat="server" TextMode="MultiLine" Rows="2" Columns="50" Style="width:99%;" MaxLength="1000"></asp:TextBox></td>
                                <td align="left" valign="top" class="labelTitle" style="padding-left: 8px;">Observation Date:
                                    <span style="color:Red; font-weight:bold;"> * </span>
                                    <img id='errorObservationDate' src='Content/warning.png' title='' style='width:16px; height:16px; padding-top:0px; padding-left: 6px; display:none;' alt='' />
                                </td>
                                <td align="left" valign="top">
                                    <div style="width: 100px;">

                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="labelTitle">Medical Staff Involved:</td>
                                <td align="left" valign="top"><asp:TextBox ID="txtMedicalStaff" runat="server" TextMode="MultiLine" Rows="2" style="width:99%;" MaxLength="1000"></asp:TextBox></td>
                                <td align="left" valign="top" class="labelTitle" style="padding-left: 8px;">Staff Interviewed:</td>
                                <td align="left" valign="top" ><asp:TextBox ID="txtStaffInterviewed" runat="server" TextMode="MultiLine" Rows="2" MaxLength="1000" style="width:99%;"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="labelTitle">Location:</td>
                                <td align="left" valign="top"><asp:TextBox ID="txtlocation" runat="server" MaxLength="100" style="width:99%;"></asp:TextBox></td>
                                <td align="left" valign="top" class="labelTitle" style="padding-left: 8px;">Aggregate Total:</td>
                                <td align="left" valign="top">Numerator : <asp:Label ID="Aggregate_Numerator" runat="server"></asp:Label>&nbsp; 
                                    Denominator : <asp:Label ID="Aggregate_Denominator" runat="server"></asp:Label>&nbsp;
                                    Percentage Compliant : <asp:Label ID="Aggregate_Total" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" rowspan="3" class="labelTitle" valign="top">Note:</td>
                                <td valign="top" rowspan="3" align="left" ><asp:TextBox ID="txtNotes" runat="server" Rows="5" TextMode="MultiLine" MaxLength="3000" style="width: 99%;" ></asp:TextBox></td>
                                <td align="left" class="labelTitle" valign="top" style="padding-left: 8px;">Medical Record No:</td>
                                <td align="left" valign="top"><asp:TextBox ID="txtMedicalRecordNo" runat="server" Width="99%" MaxLength="50" ClientIDMode="Static" /></td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" class="labelTitle" style="padding-left: 8px;">Equipment Observed:</td>
                                <td align="left" valign="top"><asp:TextBox ID="txtEquipmentObserved" runat="server" Width="99%" MaxLength="50" ClientIDMode="Static"/></td>
                            </tr>

                            <tr>
                                <td align="left" valign="top" class="labelTitle" style="padding-left: 8px;">Contracted Service:</td>
                                <td align="left" valign="top"><asp:TextBox ID="txtContractedService" runat="server" Width="99%" MaxLength="50" ClientIDMode="Static"/></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>

        <div><asp:Panel ID="Panel1" runat="server"></asp:Panel></div>

        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
        <asp:Button ID="btnMarkAsCompleted" runat="server" Text="Mark as Completed" OnClick="btnMarkAsCompleted_Click" />
        <asp:HiddenField ID="NoOfQuestions" runat="server" />
        <asp:HiddenField ID="ObservationID" runat="server" />

        <p>Time to Render this Page: <%=TimeToRenderPage %></p>
    </form>
    <script src="Content/Scripts/AddEditObservation.js"></script>
</body>
</html>
