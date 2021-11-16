using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Configuration;

namespace DB_Users
{

    public partial class Form1 : Form
    {
        public static string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:/Users/Александр/source/repos/DataBases/Users_DB1.accdb;";
        public OleDbConnection myConnection;

        public bool flag = false;

        TextBox login_txt;
        TextBox pass_txt;
        TextBox passconf_txt;
        TextBox name_txt;
        TextBox lastname_txt;
        TextBox age_txt;
        Label lbl;
        Button reg;
        Button log;
        Button search;
        DataGridView data_out;
        TextBox search_login;
        TextBox search_name;
        TextBox search_lastname;
        TextBox search_age;

        public Form1()
        {
            InitializeComponent();
            myConnection = new OleDbConnection(connectionString);
            myConnection.Open();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            myConnection.Close();
        }

        private void результатToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void search_Click(object sender, EventArgs e)
        {
            (data_out.DataSource as DataTable).DefaultView.RowFilter = $"login LIKE '%{search_login.Text}%' AND user_name LIKE '%{search_name.Text}%' AND user_lastname LIKE '%{search_lastname.Text}%' AND age LIKE '%{search_age.Text}%' ";
        }
        
        private void отриманняДанихToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!flag)
            {
                MessageBox.Show("Вы не авторизовались! Авторизуйтесь!");
            }
            else 
            {
                panel1.Controls.Clear();
                data_out = new DataGridView();
                data_out.Visible = true;
                data_out.Location = new Point(10, 10);
                data_out.Width = 500;
                data_out.Height = 300;
                /*data_out.Location = new Point(100, 50);
                data_out.Columns.Add("id", "ID");
                data_out.Columns.Add("login", "Логін");
                data_out.Columns.Add("name", "Ім'я");
                data_out.Columns.Add("lastname", "Прізвище");
                data_out.Columns.Add("age", "Вік");*/
                panel1.Controls.Add(data_out);

                generate_Search();
                /*// Filter
                lbl = new Label();
                lbl.Text = "Filter Login:";
                lbl.Visible = true;
                lbl.Location = new Point(550, 10);
                panel1.Controls.Add(lbl);
                //
                search_login = new TextBox();
                search_login.Visible = true;
                search_login.Location = new Point(550, 40);
                panel1.Controls.Add(search_login);
                //
                search = new Button();
                search.Text = "Enter";
                search.Visible = true;
                search.Location = new Point(580, 70);
                panel1.Controls.Add(search);
                search.Click += new EventHandler(search_Click);
                // END Filter*/

                data_out.Rows.Clear();
                string query = "SELECT id, login, user_name, user_lastname, age FROM users";
                //OleDbCommand command = new OleDbCommand(query, myConnection);
                //OleDbDataReader dbReader = command.ExecuteReader();

                DataSet ds = new DataSet();
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, myConnection);
                adapter.Fill(ds);
                data_out.DataSource = ds.Tables[0];

                //(data_out.DataSource as DataTable).DefaultView.RowFilter = $"login LIKE '%adm%'";
                

                /* if (dbReader.HasRows == false)
                 {
                     MessageBox.Show("Ошибка!");
                 }
                 else
                 {
                     while (dbReader.Read())
                     {
                         data_out.Rows.Add(dbReader["id"], dbReader["login"], dbReader["user_name"], dbReader["user_lastname"], dbReader["age"]);
                     }

                 }*/

            }
            

        }

        private void вхідToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            lbl = new Label();
            lbl.Text = "Login:";
            lbl.Visible = true;
            lbl.Location = new Point(100, 50);
            panel1.Controls.Add(lbl);
            //
            login_txt = new TextBox();
            login_txt.Visible = true;
            login_txt.Location = new Point(100, 80);
            panel1.Controls.Add(login_txt);
            //
            lbl = new Label();
            lbl.Text = "Pass:";
            lbl.Visible = true;
            lbl.Location = new Point(100, 120);
            panel1.Controls.Add(lbl);
            //
            pass_txt = new TextBox();
            pass_txt.Visible = true;
            pass_txt.PasswordChar = '*';
            pass_txt.Location = new Point(100, 160);
            panel1.Controls.Add(pass_txt);
            //
            log = new Button();
            log.Text = "Enter!";
            log.Visible = true;
            log.Location = new Point(100, 200);
            panel1.Controls.Add(log);
            log.Click += new EventHandler(log_Click);

            
        }
        private void log_Click(object sender, EventArgs e)
        {

            
            string query = "SELECT login, pass FROM users";
            OleDbCommand command = new OleDbCommand(query, myConnection);
            OleDbDataReader dbReader = command.ExecuteReader();
            if (dbReader.HasRows == false)
            {
                MessageBox.Show("Ошибка, попробуйте позже!");
            }
            else
            {
                while (dbReader.Read())
                {
                    if (login_txt.Text == dbReader["login"].ToString() && pass_txt.Text == dbReader["pass"].ToString())
                    {
                        flag = true;
                        отриманняДанихToolStripMenuItem_Click(null, null);
                    }
                }
                if (!flag)
                {
                    MessageBox.Show("Вы ввели не верный логин или пароль!");
                }
            }
        }

        private void вихідToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myConnection.Close();
            Close();
        }

        private void реєстраціяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            lbl = new Label();
            lbl.Text = "Login:";
            lbl.Visible = true;
            lbl.Location = new Point(100, 50);
            panel1.Controls.Add(lbl);
            //
            login_txt = new TextBox();
            login_txt.Visible = true;
            login_txt.Location = new Point(250, 50);
            login_txt.Width = 250;
            panel1.Controls.Add(login_txt);
            // Pass Lb
            lbl = new Label();
            lbl.Text = "Password:";
            lbl.Visible = true;
            lbl.Location = new Point(100, 100);
            panel1.Controls.Add(lbl);
            // Pass txt
            pass_txt = new TextBox();
            pass_txt.Visible = true;
            pass_txt.PasswordChar = '*';
            pass_txt.Location = new Point(250, 100);
            pass_txt.Width = 250;
            panel1.Controls.Add(pass_txt);

            // Conf pass lb
            lbl = new Label();
            lbl.Text = "Confirm Password:";
            lbl.Visible = true;
            lbl.Location = new Point(100, 150);
            panel1.Controls.Add(lbl);
            // Conf pass txt
            passconf_txt = new TextBox();
            passconf_txt.Visible = true;
            passconf_txt.PasswordChar = '*';
            passconf_txt.Location = new Point(250, 150);
            passconf_txt.Width = 250;
            panel1.Controls.Add(passconf_txt);
            
            // Name lbl
            lbl = new Label();
            lbl.Text = "Your name:";
            lbl.Visible = true;
            lbl.Location = new Point(100, 200);
            panel1.Controls.Add(lbl);
            // Name txt
            name_txt = new TextBox();
            name_txt.Visible = true;
            name_txt.Location = new Point(250, 200);
            name_txt.Width = 250;
            panel1.Controls.Add(name_txt);
            // Lastname lbl
            lbl = new Label();
            lbl.Text = "Your lasname:";
            lbl.Visible = true;
            lbl.Location = new Point(100, 250);
            panel1.Controls.Add(lbl);
            // lasname txt
            lastname_txt = new TextBox();
            lastname_txt.Visible = true;
            lastname_txt.Location = new Point(250, 250);
            lastname_txt.Width = 250;
            panel1.Controls.Add(lastname_txt);
            // Age lbl
            lbl = new Label();
            lbl.Text = "Your Age:";
            lbl.Visible = true;
            lbl.Location = new Point(100, 300);
            panel1.Controls.Add(lbl);
            // age txt
            age_txt = new TextBox();
            age_txt.Visible = true;
            age_txt.Location = new Point(250, 300);
            age_txt.Width = 250;
            panel1.Controls.Add(age_txt);
            //
            reg = new Button();
            reg.Text = "Registration!";
            reg.Visible = true;
            reg.Location = new Point(210, 350);
            reg.Width = 175;
            panel1.Controls.Add(reg);
            reg.Click += new EventHandler(reg_Click);
        }

        private void reg_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(login_txt.Text) && !string.IsNullOrWhiteSpace(pass_txt.Text) && pass_txt.Text == passconf_txt.Text && !string.IsNullOrWhiteSpace(name_txt.Text) && !string.IsNullOrWhiteSpace(lastname_txt.Text) && !string.IsNullOrWhiteSpace(age_txt.Text))
            {
                string query = "SELECT login FROM users";
                OleDbCommand command = new OleDbCommand(query, myConnection);
                OleDbDataReader dbReader = command.ExecuteReader();
                bool login_isset = false;
                while (dbReader.Read())
                {
                    if (login_txt.Text == dbReader["login"].ToString())
                    {
                        login_isset = true;
                    }
                }

                if (login_isset)
                {
                    MessageBox.Show("Пользователь с таким Логином уже зарегистрирован!");
                    реєстраціяToolStripMenuItem_Click(null, null);
                }
                else
                {
                    string query_ins = $"INSERT INTO users (login, pass, user_name, user_lastname, age) VALUES ('{login_txt.Text}', '{pass_txt.Text}', '{name_txt.Text}', '{lastname_txt.Text}', '{age_txt.Text}')";
                    OleDbCommand command_ins = new OleDbCommand(query_ins, myConnection);
                    int result = command_ins.ExecuteNonQuery();
                    if (result != 0)
                    {
                        MessageBox.Show("Вы успешно зарегистрировались!");
                        вхідToolStripMenuItem_Click(null, null);

                    }
                    else 
                    {
                        MessageBox.Show("Возникла ошибка при регистрации, повторите попытку!");
                    }
                }


            }
            else
            {
                MessageBox.Show("Заполните ВСЕ поля в форме!");
            }
            
        }

        private void generate_Search()
        {
            lbl = new Label();
            lbl.Text = "Filter Login:";
            lbl.Visible = true;
            lbl.Location = new Point(550, 10);
            panel1.Controls.Add(lbl);
            //
            search_login = new TextBox();
            search_login.Visible = true;
            search_login.Location = new Point(550, 30);
            panel1.Controls.Add(search_login);
            //
            lbl = new Label();
            lbl.Text = "Filter Name:";
            lbl.Visible = true;
            lbl.Location = new Point(550, 50);
            panel1.Controls.Add(lbl);
            //
            search_name = new TextBox();
            search_name.Visible = true;
            search_name.Location = new Point(550, 70);
            panel1.Controls.Add(search_name);
            //
            lbl = new Label();
            lbl.Text = "Filter Lastname:";
            lbl.Visible = true;
            lbl.Location = new Point(550, 90);
            panel1.Controls.Add(lbl);
            //
            search_lastname = new TextBox();
            search_lastname.Visible = true;
            search_lastname.Location = new Point(550, 110);
            panel1.Controls.Add(search_lastname);
            //
            lbl = new Label();
            lbl.Text = "Filter Age:";
            lbl.Visible = true;
            lbl.Location = new Point(550, 130);
            panel1.Controls.Add(lbl);
            //
            search_age = new TextBox();
            search_age.Visible = true;
            search_age.Location = new Point(550, 150);
            panel1.Controls.Add(search_age);
            //
            search = new Button();
            search.Text = "Filter!";
            search.Visible = true;
            search.Location = new Point(600, 170);
            panel1.Controls.Add(search);
            search.Click += new EventHandler(search_Click);
            // END Filter
        }
        private void проДодатокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Это приложение было разработано в последний день перед сдачей в тяжелый трудовые будни! Просьба отнестись к мелким недочетам с пониманием. Спасибо!");
        }

    }
}
