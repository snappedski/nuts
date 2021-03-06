﻿using MySQLDriverCS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nuts
{
    class db_setup
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        // Table Columns

        //Complete column names per table
        public static string[] columns_hj_users = { "username", "email", "password", "privilege", "status" };
        public static string[] columns_nuts_settings = { "setting", "value", "description" };
        public static string[] columns_nuts_inventory = { "sku", "name", "amt", "sold", "price", "description" };

        //Complete list of data types per column per table
        ///////////// MAY NOT BE NEEDED IN C# AS IT WAS IN JAVA ///////////////
        //public static string[] dt_hj_users = { "str", "str", "str", "int", "int" };
        //public static string[] dt_hj_settings = { "str", "str", "str" };
        //public static string[] dt_nuts_inventory = { "int", "str", "int", "int", "dec", "str" };

        ///////////////////////////////////////////////////////////////////////////////////////////
        // Create Tables

        public static void db_loadFile(){ //Saves Database information to memory
            nuts_session.db_host = hj_tools.fileGetLine(nuts_session.config_path, "hostname");
            nuts_session.db_db = hj_tools.fileGetLine(nuts_session.config_path, "database");
            nuts_session.db_user = hj_tools.fileGetLine(nuts_session.config_path, "username");
            nuts_session.db_pass = hj_tools.fileGetLine(nuts_session.config_path, "password");
            nuts_session.db_timeout = hj_tools.stringToInt(hj_tools.fileGetLine(nuts_session.config_path, "timeout"));
    }

        public static string db_initialize(){ //Creates all tables
            string r = string.Empty;
            try
            {
                if (db.connect() == true)
                {
                    Console.WriteLine("- Creating database tables.....");
                    try
                    {
                        string coll = "'utf8_unicode_ci'";

                        //CREATE hj_users table
                        string cTbl_hj_users = "CREATE TABLE IF NOT EXISTS `hj_users` (" +
                        "`username` varchar(32) NOT NULL," +
                        "`email` varchar(32) collate " + coll + " NOT NULL," +
                        "`password` varchar(32) collate " + coll + " NOT NULL," +
                        "`privilege` int(11) collate " + coll + " NOT NULL," +
                        "`status` int(11) collate " + coll + " NOT NULL," +
                        "PRIMARY KEY  (`username`)) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=" + coll + ";";
                        MySQLCommand sql_hj_users = new MySQLCommand(cTbl_hj_users, db.con);
                        MySQLDataReader reader_hj_users = sql_hj_users.ExecuteReaderEx();

                        //CREATE nuts_settings table
                        string cTbl_nuts_settings = "CREATE TABLE IF NOT EXISTS `nuts_settings` (" +
                        "`setting` varchar(32) NOT NULL," +
                        "`value` varchar(32) collate " + coll + " NOT NULL," +
                        "`description` varchar(64) collate " + coll + " NOT NULL," +
                        "PRIMARY KEY  (`setting`)) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=" + coll + ";";
                        MySQLCommand sql_nuts_settings = new MySQLCommand(cTbl_nuts_settings, db.con);
                        MySQLDataReader reader_nuts_settings = sql_nuts_settings.ExecuteReaderEx();

                        //CREATE nuts_inventory table
                        string cTbl_nuts_inventory = "CREATE TABLE IF NOT EXISTS `nuts_inventory` (" +
                        "`sku` int(11) NOT NULL," +
                        "`name` varchar(32) collate " + coll + " NOT NULL," +
                        "`amt` int(11) collate " + coll + " NOT NULL," +
                        "`sold` int(11) collate " + coll + " NOT NULL," +
                        "`price` decimal(65,2) collate " + coll + " NOT NULL," +
                        "`description` varchar(255) collate " + coll + " NOT NULL," +
                        "PRIMARY KEY  (`sku`)) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=" + coll + ";";
                        MySQLCommand sql_nuts_inventory = new MySQLCommand(cTbl_nuts_inventory, db.con);
                        MySQLDataReader reader_nuts_inventory = sql_nuts_inventory.ExecuteReaderEx();

                    }
                    catch (Exception e)
                    {
                        hj_tools.msgBox(e.ToString(), "Database Error", "ERROR", true);
                        r = e.ToString();
                    }
                    finally
                    {
                        db.disconnect();
                    }
                }
            }
            catch (Exception e) { Console.WriteLine(e); }
            return r;
        }
    }
}
