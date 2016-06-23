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
    public partial class TodoDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                this.GetTodos();
            }
        }

        protected void GetTodos()
        {
            // populate form with existing data from the database
            int TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);

            // connect to the EF DB
            using (TodoConnection db = new TodoConnection())
            {
                // populate a tod object instance with the todoID from the URL Parameter
                Todo updatedTodo = (from todos in db.Todos
                                    where todos.TodoID == TodoID
                                    select todos).FirstOrDefault();

                // map the todo properties to the form controls
                if (updatedTodo != null)
                {
                    TodoNameTextBox.Text = updatedTodo.TodoName;
                    TodoNotesTextBox.Text = updatedTodo.TodoNotes;
                }
            }
        }

        /**
         * <summary>
         * This method save the data to db
         * </summary>
         * 
         * @method SaveButton_Click
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void}         
         */
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            // Use EF to connect to the server
            using (TodoConnection db = new TodoConnection())
            {
                // use the Student model to create a new student object and
                // save a new record
                Todo newTodo = new Todo();

                int TodoID = 0;

                if (Request.QueryString.Count > 0) // our URL has a StudentID in it
                {
                    // get the id from the URL
                    TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);

                    // get the current student from EF DB
                    newTodo = (from todos in db.Todos
                               where todos.TodoID == TodoID
                               select todos).FirstOrDefault();
                }

                // add form data to the new student record
                newTodo.TodoName = TodoNameTextBox.Text;
                newTodo.TodoNotes = TodoNotesTextBox.Text;


                // use LINQ to ADO.NET to add / insert new student into the database

                if (TodoID == 0)
                {
                    db.Todos.Add(newTodo);
                }


                // save our changes - also updates and inserts
                db.SaveChanges();

                // Redirect back to the updated students page
                Response.Redirect("~/TodoList.aspx");
            }
        }

        /**
         * <summary>
         * This method cancels and redirect back
         * </summary>
         * 
         * @method CancelButton_Click
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void}         
         */
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            //redirect
            Response.Redirect("~/TodoList.aspx");
        }
    }
}