using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ObjectSyncTool
{
    public partial class frmMain : Form
    {
        string strSourceConn = "Data Source=192.168.1.166;Initial Catalog=BSYA_WebSite;User ID=sa;PassWord=Think123;";
        string strStoreConn = "Data Source=192.168.1.166;Initial Catalog=BSYA_WebSite;User ID=sa;PassWord=Think123;";
        
        public frmMain()
        {
            InitializeComponent();
        }
        public class ComboboxItem
        {
            public ComboboxItem(string AText,object AValue)
            {
                Text = AText;
                Value = AValue;
            }
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add(new ComboboxItem("人员信息","User"));
            comboBox1.SelectedIndex = 0;

        }

        private void btnStartSync_Click(object sender, EventArgs e)
        {
            btnStartSync.Enabled = false;
            
            TF.YA.Soft.DBSyncUser uSource = new TF.YA.Soft.DBSyncUser(strSourceConn);
            TF.YA.Soft.DBSyncObject uStore = new TF.YA.Soft.DBSyncObject(strStoreConn);
            TF.YA.Soft.SyncUtils.Update((comboBox1.SelectedItem as ComboboxItem).Value.ToString(), uSource, uStore);
            MessageBox.Show("更新完毕");
            btnStartSync.Enabled = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnViewSum_Click(object sender, EventArgs e)
        {
            TF.YA.Soft.DBSyncObject uStore = new TF.YA.Soft.DBSyncObject(strStoreConn);
            List<TF.YA.Soft.SyncObject> objs = new List<TF.YA.Soft.SyncObject>();
            List<TF.YA.Soft.SyncObjectSum> objsSum = new List<TF.YA.Soft.SyncObjectSum>();
            List<TF.YA.Soft.SyncUser> users = new List<TF.YA.Soft.SyncUser>();
            List<TF.YA.Soft.SyncUserSum> usersSum = new List<TF.YA.Soft.SyncUserSum>();
            uStore.GetObjects(objs);
            uStore.GetObjectsSum(objsSum);
            uStore.GetUsers(users);
            uStore.GetUsersSum(usersSum);

            lstObject.Items.Clear();
            for (int i = 0; i < objs.Count; i++)
            {
                ListViewItem item = lstObject.Items.Add("");
                item.Text = objs[i].ObjectName;
                item.SubItems.Add(objs[i].ObjectVersion);
                TF.YA.Soft.SyncObjectSum sum=  objsSum.Find((TF.YA.Soft.SyncObjectSum s) => s.ObjectName == objs[i].ObjectName);
                if (sum != null)
                {
                    item.SubItems.Add(sum.Count.ToString());
                }
                else { item.SubItems.Add("0"); }
            }
            lstUser.Items.Clear();
             for (int i = 0; i < users.Count; i++)
            {
                ListViewItem item = lstUser.Items.Add("");
                item.Text = users[i].UserID;
                item.SubItems.Add(users[i].UserName);
                item.SubItems.Add(users[i].ObjectName);
                TF.YA.Soft.SyncUserSum sum = usersSum.Find((TF.YA.Soft.SyncUserSum s) => (s.ObjectName == users[i].ObjectName) && (s.UserID == users[i].UserID));
                if (sum != null)
                {
                    item.SubItems.Add(sum.Count.ToString());
                }
                else { item.SubItems.Add("0"); }
            }


        }

        #region 采用Lib方式调用更新
        private void btnGetUpdate_Click(object sender, EventArgs e)
        {
            TF.YA.Soft.DBSyncObject uStore = new TF.YA.Soft.DBSyncObject(strStoreConn);
            List<TF.YA.Soft.UserIndex> userIndexes = new List<TF.YA.Soft.UserIndex>();
            if (! uStore.GetUserIndex(tbUserID.Text, tb_UserObjectName.Text,userIndexes))
            {
                MessageBox.Show("当前客户端未注册此更新对象");
                return;
            }
            tbUpdateLog.Text = "";

            tbUpdateLog.AppendText(string.Format("本次共获得{0}条更新" + "\r\n", userIndexes.Count));
            progressBar1.Maximum = userIndexes.Count;
            progressBar1.Value = 0;//设置当前值 
            foreach (var item in userIndexes)
            {
                
                tbUpdateLog.AppendText(string.Format("开始下载{0}" + "\r\n", item.Key));
                TF.YA.Soft.SyncData d = new TF.YA.Soft.SyncData();
                uStore.GetObjectData(tb_UserObjectName.Text, item.Key, d);
                tbUpdateLog.AppendText(string.Format("成功获取{0}的值:{1}" + "\r\n", item.Key,d.Json));

                uStore.CommitUserIndex(tbUserID.Text, tb_UserObjectName.Text, item.Key);
                tbUpdateLog.AppendText(string.Format("成功提交{0}" + "\r\n", item.Key));
                progressBar1.Value += 1;
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TF.YA.Soft.DBSyncObject uStore = new TF.YA.Soft.DBSyncObject(strStoreConn);
            uStore.RegUser(tbUserID.Text, tbUserName.Text, tb_UserObjectName.Text);
            MessageBox.Show("注册成功");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TF.YA.Soft.DBSyncObject uStore = new TF.YA.Soft.DBSyncObject(strStoreConn);
            uStore.UnRegUser(tbUserID.Text, tb_UserObjectName.Text);
            MessageBox.Show("注消成功");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            TF.YA.Soft.DBSyncObject uStore = new TF.YA.Soft.DBSyncObject(strStoreConn);
            List<TF.YA.Soft.SyncDataOP> userIndexes = new List<TF.YA.Soft.SyncDataOP>();
            uStore.GetObjectDatas(tb_UserObjectName.Text, userIndexes);

            tbUpdateLog.Text = "";

            tbUpdateLog.AppendText(string.Format("本次共获得{0}条更新" + "\r\n", userIndexes.Count));
            progressBar1.Maximum = userIndexes.Count;
            progressBar1.Value = 0;//设置当前值 
            foreach (var item in userIndexes)
            {

                tbUpdateLog.AppendText(string.Format("开始下载{0}" + "\r\n", item.Key));
                TF.YA.Soft.SyncData d = new TF.YA.Soft.SyncData();
                uStore.GetObjectData(tb_UserObjectName.Text, item.Key, d);
                tbUpdateLog.AppendText(string.Format("成功获取{0}的值:{1}" + "\r\n", item.Key, d.Json));

                uStore.CommitUserIndex(tbUserID.Text, tb_UserObjectName.Text, item.Key);
                tbUpdateLog.AppendText(string.Format("成功提交{0}" + "\r\n", item.Key));
                progressBar1.Value += 1;

            }
        }
        #endregion 采用Lib方式调用更新

        private void button5_Click(object sender, EventArgs e)
        {

            List<TF.YA.Soft.Demo.UserIndex> userIndexes = new List<TF.YA.Soft.Demo.UserIndex>();
            if (!TF.YA.Soft.Demo.SyncUtils.GetUserIndex(tbUserIDWweb.Text, tbObjectNameWeb.Text, ref userIndexes))
            {
                MessageBox.Show("当前客户端未注册此更新对象");
                return;
            }
            tbUpdateLogWeb.Text = "";

            tbUpdateLogWeb.AppendText(string.Format("本次共获得{0}条更新" + "\r\n", userIndexes.Count));
            pbWeb.Maximum = userIndexes.Count;
            pbWeb.Value = 0;//设置当前值 
            foreach (var item in userIndexes)
            {

                tbUpdateLogWeb.AppendText(string.Format("开始下载{0}" + "\r\n", item.Key));
                TF.YA.Soft.Demo.SyncData d = new TF.YA.Soft.Demo.SyncData();
                TF.YA.Soft.Demo.SyncUtils.GetObjectData(tbObjectNameWeb.Text, item.Key,ref d);
                tbUpdateLogWeb.AppendText(string.Format("成功获取{0}的值:{1}" + "\r\n", item.Key, d.Json));

                TF.YA.Soft.Demo.SyncUtils.CommitUserIndex(tbUserIDWweb.Text, tbObjectNameWeb.Text, item.Key);
                tbUpdateLogWeb.AppendText(string.Format("成功提交{0}" + "\r\n", item.Key));
                pbWeb.Value += 1;

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TF.YA.Soft.Demo.SyncUtils.RegUser(tbUserIDWweb.Text, tbUserNameWeb.Text, tbObjectNameWeb.Text);
            MessageBox.Show("注册成功");
        }

        private void button3_Click(object sender, EventArgs e)
        {

            TF.YA.Soft.Demo.SyncUtils.UnRegUser(tbUserIDWweb.Text, tbObjectNameWeb.Text);
            MessageBox.Show("注消成功");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            
            List<TF.YA.Soft.Demo.SyncDataOP> userIndexes = new List<TF.YA.Soft.Demo.SyncDataOP>();
            TF.YA.Soft.Demo.SyncUtils.GetObjectDatas(tb_UserObjectName.Text, userIndexes);

            tbUpdateLogWeb.Text = "";

            tbUpdateLogWeb.AppendText(string.Format("本次共获得{0}条更新" + "\r\n", userIndexes.Count));
            pbWeb.Maximum = userIndexes.Count;
            pbWeb.Value = 0;//设置当前值 
            foreach (var item in userIndexes)
            {

                tbUpdateLogWeb.AppendText(string.Format("开始下载{0}" + "\r\n", item.Key));
                TF.YA.Soft.Demo.SyncData d = new TF.YA.Soft.Demo.SyncData();
                TF.YA.Soft.Demo.SyncUtils.GetObjectData(tbObjectNameWeb.Text, item.Key, ref d);
                tbUpdateLogWeb.AppendText(string.Format("成功获取{0}的值:{1}" + "\r\n", item.Key, d.Json));

                TF.YA.Soft.Demo.SyncUtils.CommitUserIndex(tbUserIDWweb.Text, tbObjectNameWeb.Text, item.Key);
                tbUpdateLogWeb.AppendText(string.Format("成功提交{0}" + "\r\n", item.Key));
                pbWeb.Value += 1;

            }
        }
    }
}
