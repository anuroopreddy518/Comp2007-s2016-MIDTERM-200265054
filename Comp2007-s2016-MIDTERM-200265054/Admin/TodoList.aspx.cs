using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// using statements that are required to connect to EF DB
using Comp2007_s2016_MIDTERM_200265054.Models;
using System.Web.ModelBinding;
using System.Linq.Dynamic;

/**
 * @author: Anuroop Reddy
 * @student #: 200265054
 * @date: June 23, 2016
 * @version: 0.0.2 - Todo list
 */

namespace Comp2007_s2016_MIDTERM_200265054
{
    public partial class TodoList : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            // if loading the page for the first time, populate the student grid
            if (!IsPostBack)
            {
                Session["SortColumn"] = "TodoID"; // default sort column
                Session["SortDirection"] = "ASC";
                // Get the student data
                this.GetTodos();
            }
        }

        /**
         * <summary>
         * This method gets the todo data
         * </summary>
         * 
         * @method GetTodos
         * @returns {void}
         */
        protected void GetTodos()
        {
            // connect to EF
            using (TodoConnection db = new TodoConnection())
            {

                string SortString = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();                

                // query the Todo Table using EF and LINQ
                var Todo = (from alltodo in db.Todos
                            select alltodo);

                // bind the result to the GridView
                TodoGridView.DataSource = Todo.AsQueryable().OrderBy(SortString).ToList();
                TodoGridView.DataBind();
            }

        }


        /**
        * <summary>
        * This method delete the todo
        * </summary>
        * 
         * @method TodoGridView_RowDeleting
         * @param {object} sender
         * @param {GridViewPageEventArgs} e
         * @returns {void}
        */
        protected void TodoGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // store which row was clicked
            int selectedRow = e.RowIndex;

            // get the selected StudentID using the Grid's DataKey collection
            int TodoID = Convert.ToInt32(TodoGridView.DataKeys[selectedRow].Values["TodoID"]);

            // use EF to find the selected student in the DB and remove it
            using (TodoConnection db = new TodoConnection())
            {
                // create object of the Student class and store the query string inside of it
                Todo deleteTodo = (from todoRecords in db.Todos
                                    where todoRecords.TodoID == TodoID
                                    select todoRecords).FirstOrDefault();

                // remove the selected student from the db
                db.Todos.Remove(deleteTodo);

                // save my changes back to the database
                db.SaveChanges();

                // refresh the grid
                this.GetTodos();
            }
        }

        /**
        * <summary>
        * This method handle the paging event
        * </summary>
        * 
         * @method PageSizeDropDownList_SelectedIndexChanged
         * @param {object} sender
         * @param {GridViewPageEventArgs} e
         * @returns {void}
        */
        protected void PageSizeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set the new Page size
            TodoGridView.PageSize = Convert.ToInt32(PageSizeDropDownList.SelectedValue);

            // refresh the grid
            this.GetTodos();
        }

        /**
         * <summary>
         * This event handler allows pagination to occur for the Students page
         * </summary>
         * @method TodoGridView_PageIndexChanging
         * @param {object} sender
         * @param {GridViewPageEventArgs} e
         * @returns {void}
         */
        protected void TodoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the new page number
            TodoGridView.PageIndex = e.NewPageIndex;

            // refresh the grid
            this.GetTodos();
        }

        /**
         * <summary>
         * This event do the sorting
         * </summary>
         * @method TodoGridView_Sorting
         * @param {object} sender
         * @param {GridViewPageEventArgs} e
         * @returns {void}
         */
        protected void TodoGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            // get the column to sorty by
            Session["SortColumn"] = e.SortExpression;

            // Refresh the Grid
            this.GetTodos();

            // toggle the direction
            Session["SortDirection"] = Session["SortDirection"].ToString() == "ASC" ? "DESC" : "ASC";
        }



        protected void TodoGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header) // if header row has been clicked
                {
                    LinkButton linkbutton = new LinkButton();

                    for (int index = 0; index < TodoGridView.Columns.Count - 1; index++)
                    {
                        if (TodoGridView.Columns[index].SortExpression == Session["SortColumn"].ToString())
                        {
                            if (Session["SortDirection"].ToString() == "ASC")
                            {
                                linkbutton.Text = " <i class='fa fa-caret-up fa-lg'></i>";
                            }
                            else
                            {
                                linkbutton.Text = " <i class='fa fa-caret-down fa-lg'></i>";
                            }

                            e.Row.Cells[index].Controls.Add(linkbutton);
                        }
                    }
                }
            }
        }

        protected void Completedcheckbox_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}