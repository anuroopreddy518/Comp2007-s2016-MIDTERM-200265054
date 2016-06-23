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


        protected void TodoGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}