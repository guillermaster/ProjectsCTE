/* The ConnectionSpy is has a finalizer, and it disables the finalizer on the OracleConnection. 
 * So if the OracleConnection is Garbage Collected without being closed first, the ConnectionSpy's finalizer will fire. 
 * If the Connection is closed properly, the ConnectionSpy will detach itself.
 * So the ConnectionSpy's finalizer will only run if the connection was leaked. It will then print out a warning message to Trace if its finalizer is running and the connection has not been closed. The message will contain the stack trace of the method which leaked the connection.
^*/

using System;
using System.Collections.Generic;
using System.Text;
using Oracle.DataAccess.Client;
using System.Diagnostics;
using System.Data;


namespace AccesoDatos
{
    class ConnectionSpy
    {
        OracleConnection con;
        StackTrace st;
        StateChangeEventHandler handler;


        public ConnectionSpy(OracleConnection con, StackTrace st)
        {
            this.st = st;
            //latch on to the connection
            this.con = con;
            handler = new StateChangeEventHandler(StateChange);
            con.StateChange += handler;
            //substitute the spy's finalizer for the 
            //connection's
            GC.SuppressFinalize(con);
        }

        public void StateChange(Object sender, StateChangeEventArgs args)
        {
            if (args.CurrentState == ConnectionState.Closed)
            {
                //detach the spy object and let it float away into space
                //if the connection and the spy are already in the FReachable queue
                //GC.SuppressFinalize doesn't do anyting.
                GC.SuppressFinalize(this);
                con.StateChange -= handler;
                con = null;
                st = null;
            }
        }

        ~ConnectionSpy()
        {
            //if we got here then the connection was not closed.
            //Trace.WriteLine("WARNING: Open Connection is being Garbage Collected");
            //Trace.WriteLine("The connection was initially opened " + st.ToString());
            con.StateChange -= handler;
            //clean up the connection
            con.Close();
            con.Dispose();
        }
    }
}
