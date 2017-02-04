/**
 * Excerpted from the book, "Pragmatic Unit Testing in C# with NUnit, 2nd Ed."
 * ISBN 0-9776166-7-3
 * Copyrights apply to this code. It may not be used to create training material, 
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose. 
 * Visit http://www.pragmaticprogrammer.com/titles/utc2 for more book information.
 */

using System;
using System.Data;
using System.Data.SqlClient;

public class AccessController1 {
  private ILogger       logger;
  private String        resource;
  private IDbConnection conn;
  
  public static readonly String CHECK_SQL =
    "select count(*) from access where " +
    "user=@user and password=@password " +
    "and resource=@resource";

  public AccessController1(String resource, 
                           ILogger logger,
                           IDbConnection conn) {
    this.logger   = logger;
    this.resource = resource;
    this.conn     = conn;
    logger.SetName("AccessControl");
  }

  public bool CanAccess(String user, String password) {
    logger.Log("Checking access for " + user + 
      " to " + resource);

    if (password == null || password.Length == 0) {
      logger.Log("Missing password. Access denied");
      return false;
    }

    IDbCommand cmd = conn.CreateCommand();
    cmd.CommandText = CHECK_SQL;
    cmd.Parameters.Add(
      new SqlParameter("@user",     user));
    cmd.Parameters.Add(
      new SqlParameter("@password", password));
    cmd.Parameters.Add(
      new SqlParameter("@resource", resource));
    IDataReader rdr = cmd.ExecuteReader();
    
    int rows = 0;

    if (rdr.Read())
      rows = rdr.GetInt32(0);

    cmd.Dispose();

    if (rows == 1) {
      logger.Log("Access granted");
      return true;
    }
    else {
      logger.Log("Access denied");
      return false;
    }
  }
}
