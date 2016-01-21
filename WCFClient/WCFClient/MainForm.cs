using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WCFClient.ServiceReference1;

namespace WCFClient
{
    public partial class MainForm : Form, WCFClient.ServiceReference1.IService1Callback
    {
        IService1 m_Innerclient;
        public MainForm()
        {
            InitializeComponent();
            InstanceContext m_CallBackContext;
            m_CallBackContext = new InstanceContext(this);
            DuplexChannelFactory<IService1> m_ChannelFactory;
            NetTcpBinding binding = new NetTcpBinding();
            Uri baseAddress = new Uri("net.tcp://localhost:10086/WCFHostServer/Service1");
            m_ChannelFactory = new DuplexChannelFactory<IService1>(m_CallBackContext, binding, new EndpointAddress(baseAddress));
            m_Innerclient = m_ChannelFactory.CreateChannel();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        public void NotifyClientMsg(string msg)
        {
            label2.Text = msg;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //向wcf服务注册客户端操作的实例
            m_Innerclient.GetSvrTime();
        }
    }
}
