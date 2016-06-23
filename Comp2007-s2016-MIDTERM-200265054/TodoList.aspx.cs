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
            this.GetTodos();
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

                // query the Todo Table using EF and LINQ
                var Todo = (from alltodo in db.Todos
                            select alltodo);

                // bind the result to the GridView
                TodoGridView.DataSource = Todo.AsQueryable().ToList();
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
    }
}