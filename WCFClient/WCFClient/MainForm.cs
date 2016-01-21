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
            //basicHttpBinding绑定方式的通讯 单工  

            //Service1Client host = new ServiceReference1.Service1Client();
            //       string htime = host.GetSvrTime();//调用GetSvrTime获取到wcf服务器上的时间  
            //      MessageBox.Show("basicHttpBinding" + "@" + htime);


            //         //NetTcpBinding绑定方式的通讯 双工  

            //IService1 m_Innerclient;
            //ChannelFactory<IService1> m_ChannelFactory;
            //NetTcpBinding binding = new NetTcpBinding();
            //Uri baseAddress = new Uri(string.Format("net.tcp://{0}:{1}/WCFHostServer/Service1", textBox1.Text, textBox2.Text));
            //m_ChannelFactory = new ChannelFactory<IService1>(binding, new EndpointAddress(baseAddress));
            //m_Innerclient = m_ChannelFactory.CreateChannel();

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
