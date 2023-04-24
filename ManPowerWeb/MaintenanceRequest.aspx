﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MaintenanceRequest.aspx.cs" Inherits="ManPowerWeb.MaintenanceRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server" ID="Scriptmanger1"></asp:ScriptManager>
    <div class="container">
        <div class="card p-4 mb-5">

            <h2>New Maintenance Request</h2>
            <br />
            <br />

            <%-- <div class="row">
                <div class="col-4">
                    <label>File Number : </label>
                </div>
                <div class="col-8">
                    <asp:TextBox ID="fielNo" runat="server" Width="250px" CssClass="form-control form-control-user"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="fielNo" ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator" ValidationGroup="1" ForeColor="Red">*</asp:RequiredFieldValidator>
                </div>
            </div>
            --%>

            <div class="row">
                <div class="col-4">
                    <label>Date : </label>
                </div>
                <div class="col-8">
                    <asp:TextBox ID="date" runat="server" name="date" Width="250px" CssClass="form-control form-control-user" TextMode="Date"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="date" ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ValidationGroup="1" ForeColor="Red">*</asp:RequiredFieldValidator>
                </div>
            </div>
            <br />

            <div class="row">
                <div class="col-4">
                    <label>Maintenance Category : </label>
                </div>
                <div class="col-8">
                    <asp:DropDownList ID="ddlCategory" Width="250px" runat="server" AutoPostBack="true" CssClass="dropdown-toggle form-control"></asp:DropDownList>
                </div>
            </div>
            <br />

            <div class="row">
                <div class="col-4">
                    <label>Requested By : </label>
                </div>
                <div class="col-8">
                    <asp:TextBox ID="requestedBy" runat="server" name="place" Width="250px" CssClass="form-control form-control-user" Enabled="false"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="requestedBy" ID="RequiredFieldValidator3" runat="server" ErrorMessage="RequiredFieldValidator" ValidationGroup="1" ForeColor="Red">*</asp:RequiredFieldValidator>
                </div>
            </div>
            <br />

            <div class="row">
                <div class="col-4">
                    <label>Vehicle Number :</label>
                </div>
                <div class="col-8">
                    <asp:TextBox ID="vNo" runat="server" Width="250px" CssClass="form-control form-control-user"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="vNo" ID="RequiredFieldValidator4" runat="server" ErrorMessage="RequiredFieldValidator" ValidationGroup="1" ForeColor="Red">*</asp:RequiredFieldValidator>
                </div>
            </div>
            <br />

            <asp:Label runat="server" ID="lbl"></asp:Label>

            <%if (ddlCategory.SelectedValue != "2")
                { %>

            <div class="row">
                <div class="col-4">
                    <label>Vehicle Meter :</label>
                </div>
                <div class="col-8">
                    <asp:TextBox ID="txtMeter" runat="server" Width="250px" CssClass="form-control form-control-user" TextMode="Number"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="txtMeter" ID="RequiredFieldValidator7" runat="server" ErrorMessage="RequiredFieldValidator" ValidationGroup="1" ForeColor="Red">*</asp:RequiredFieldValidator>
                </div>
            </div>

            <%} %>
            <br />

            <%if (ddlCategory.SelectedValue == "3")
                { %>
            <div class="row">
                <div class="col-4">
                    <label>Previous Vehicle Meter :</label>
                </div>
                <div class="col-8">
                    <asp:TextBox ID="txtPrevMeter" runat="server" Width="250px" CssClass="form-control form-control-user" TextMode="Number" AutoPostBack="true" OnTextChanged="txtPrevMeter_TextChanged"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="txtPrevMeter" ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator" ValidationGroup="1" ForeColor="Red">*</asp:RequiredFieldValidator>
                </div>
            </div>

            <%} %>

            <div class="row">
                <div class="col-4">
                    <label>Mileage :</label>
                </div>
                <div class="col-8">
                    <asp:TextBox ID="txtMiladge" runat="server" Width="250px" CssClass="form-control form-control-user"></asp:TextBox>
                    <%--                <asp:RequiredFieldValidator ControlToValidate="txtMiladge" ID="RequiredFieldValidator8" runat="server" ErrorMessage="RequiredFieldValidator" ValidationGroup="1" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                </div>
            </div>
            <br />

            <div class="row">
                <div class="col-4">
                    <label>Description : </label>
                </div>
                <div class="col-8">
                    <asp:TextBox ID="description" runat="server" name="place" Width="250px" CssClass="form-control form-control-user" TextMode="MultiLine"></asp:TextBox>
                    <asp:RequiredFieldValidator ControlToValidate="description" ID="RequiredFieldValidator5" runat="server" ErrorMessage="RequiredFieldValidator" ValidationGroup="1" ForeColor="Red">*</asp:RequiredFieldValidator>
                </div>
            </div>
            <br />

            <div class="row">
                <div class="col-4">
                    <label>Uploads : </label>
                </div>
                <div class="col-8">
                    <asp:FileUpload ID="Uploader" CssClass="btn" runat="server" AllowMultiple="true" />
                    <asp:Label ID="lblListOfUploadedFiles" runat="server" />
                    <asp:RequiredFieldValidator ControlToValidate="Uploader" ID="RequiredFieldValidator6" runat="server" ErrorMessage="RequiredFieldValidator" ValidationGroup="1" ForeColor="Red">*</asp:RequiredFieldValidator>
                </div>
            </div>
            <br />


            <br />

            <div class="row">
                <div class="col-2">
                    <asp:Button runat="server" ID="Button3" Text="Back" OnClick="isClicked" CssClass="btn btn-primary btn-user btn-block" BackColor="#212529" BorderColor="#212529" />
                </div>
                <div class="col-2">
                    <asp:Button runat="server" ID="btnSave" Text="Submit" CssClass="btn btn-success btn-user btn-block" OnClick="btnSave_Click" ValidationGroup="1" />
                </div>
                <div class="col-2">
                    <asp:Button runat="server" ID="Button1" Text="Clear" OnClick="btnClear_Click" CssClass="btn btn-facebook btn-user btn-block" />
                </div>
            </div>
            <br />
        </div>
    </div>
</asp:Content>
