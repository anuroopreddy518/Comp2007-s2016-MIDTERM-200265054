﻿<%@ Page Title="Todo List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TodoList.aspx.cs" Inherits="Comp2007_s2016_MIDTERM_200265054.TodoList" %>

<%--
    Author: Anuroop Reddy
    student #: 200265054
    date: June 23, 2016
    version: 0.0.2 - Todo list
    desc: todo list page which display all lists 
    --%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="container">
        <div class="row">
            <div class="col-md-offset-2 col-md-8">
                <h1>Todo List</h1>
                <a href="/Admin/TodoDetails.aspx" class="btn btn-success btn-sm"><i class="fa fa-plus"></i>Add Todo</a>

                <div>
                    <label for="PageSizeDropDownList">Records per Page: </label>
                    <asp:DropDownList ID="PageSizeDropDownList" runat="server"
                        AutoPostBack="true" CssClass="btn btn-default bt-sm dropdown-toggle"
                        OnSelectedIndexChanged="PageSizeDropDownList_SelectedIndexChanged">
                        <asp:ListItem Text="3" Value="3" />
                        <asp:ListItem Text="5" Value="5" />
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="All" Value="10000" />
                    </asp:DropDownList>
                </div>

                <asp:GridView runat="server" CssClass="table table-bordered table-striped table-hover"
                    ID="TodoGridView" AutoGenerateColumns="false" DataKeyNames="TodoID" OnRowDeleting="TodoGridView_RowDeleting"
                    AllowPaging="true" PageSize="3" OnPageIndexChanging="TodoGridView_PageIndexChanging" AllowSorting="true"
                    OnSorting="TodoGridView_Sorting" OnRowDataBound="TodoGridView_RowDataBound"
                    PagerStyle-CssClass="pagination-ys">
                    <Columns>
                        <asp:BoundField DataField="TodoID" HeaderText="Todo ID" Visible="true" SortExpression="TodoID" />
                        <asp:BoundField DataField="TodoName" HeaderText="Todo Name" Visible="true" SortExpression="TodoName" />
                        <asp:BoundField DataField="TodoNotes" HeaderText="Todo Notes" Visible="true" SortExpression="TodoNotes" />                        

                        <asp:TemplateField HeaderText="Completed"  SortExpression="Completed">
                            <ItemTemplate >
                                <asp:CheckBox ID="CompletedCheckBox" runat="server" AutoPostBack="true" EnableViewState="true" Checked='<%# Convert.ToBoolean(Eval("Completed")) %>'/>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:HyperLinkField HeaderText="Edit" Text="<i class='fa fa-pencil-square-o fa-lg'></i> Edit" 
                            NavigateUrl="~/TodoDetails.aspx.cs" ControlStyle-CssClass="btn btn-primary btn-sm" runat="server"
                            DataNavigateUrlFields="TodoID" DataNavigateUrlFormatString="TodoDetails.aspx?TodoID={0}" />

                        <asp:CommandField  HeaderText="Delete" DeleteText="<i class='fa fa-trash-o fa-lg'></i> Delete"
                            ShowDeleteButton="true" ButtonType="Link" ControlStyle-CssClass="btn btn-danger btn-sm" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
