﻿using Nuts.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nuts
{
    public partial class frm_dbSetup : Form
    {
        public frm_dbSetup()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(nuts_session.config_path)) {
                fieldDisable();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lbl_user_Click(object sender, EventArgs e)
        {

        }

        private void lbl_db_Click(object sender, EventArgs e)
        {

        }

        private void fieldDisable(){
            in_db.Enabled = false;
            in_host.Enabled = false;
            in_user.Enabled = false;
            in_pass.Enabled = false;
            button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e){
            /////////////////// Validation ///////////////////////////////
            Boolean v = true;
            string db_host = in_host.Text; string db_db = in_db.Text;
            string db_user = in_user.Text; string db_pass = in_pass.Text;
            if (db_host == "") { v = inputCheck.noInfo(v, "Hostname"); }
            if (db_db == "") { v = inputCheck.noInfo(v, "Database"); }
            if (db_user == "") { v = inputCheck.noInfo(v, "Username"); }
            if (db_pass == "") { v = inputCheck.noInfo(v, "Password"); }

            if (v == true) { // Continue below if all valid //////////////
                nuts_session.db_host = db_host;
                nuts_session.db_db = db_db;
                nuts_session.db_user = db_user;
                nuts_session.db_pass = db_pass;
                nuts_session.db_timeout = 10;
                string r = db_setup.db_initialize();
                if (hj_tools.compareStr(r, String.Empty) == false) { hj_tools.msgBox(r, "Cannot Initialize DB", "ERROR", true);
                } else { //db setup successful
                    hj_tools.fileCreate(nuts_session.config_dir, nuts_session.config_name, nuts_session.config_ext, nuts_session.config_description); //Create config file
                    string[] strSettings = { "hostname", "database", "username", "password", "timeout" }; //Settings
                    string[] strValues = { db_host, db_db, db_user, db_pass, "10"}; //Values
                    hj_tools.fileAppend(nuts_session.config_path, hj_tools.writeConfig(strSettings,strValues)); //Add database info to config
                    db_setup.db_loadFile();
                    string hash = hj_tools.ext_hash_sha1("admin", "password");
                    string[] strIn = {"admin", "@", hash, "1", "0"}; //Column Values

                    try { db.db_insert("hj_users", db_setup.columns_hj_users, strIn); //Create admin account
                    }catch (Exception y) { Console.WriteLine(y); }
                    fieldDisable(); //Disable all fields
                    frm_login frm_login = new frm_login();
                    //form.Text = "test";
                    frm_login.Show();
                    this.Hide();
                }
            }

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }

        private void pic_dbSetup_Click(object sender, EventArgs e)
        {

        }
    }
}
