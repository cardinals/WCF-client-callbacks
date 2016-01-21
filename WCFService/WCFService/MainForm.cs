using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WCFService
{
    public partial class MainForm : Form
    {
        ServiceHost m_ServiceHost;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //NetTcpBinding方式启动wcf服务  
            m_ServiceHost = new ServiceHost(typeof(Service1));//Service1是wcf服务的类名称  
            NetTcpBinding binding = new NetTcpBinding();
            Uri baseAddress = new Uri(string.Format("net.tcp://localhost:10086/WCFHostServer/Service1"));
            m_ServiceHost.AddServiceEndpoint(typeof(IService1), binding, baseAddress);
            //BasicHttpBinding方式启动wcf服务  

            ServiceMetadataBehavior metadataBehavior;
            metadataBehavior = m_ServiceHost.Description.Behaviors.Find<ServiceMetadataBehavior>();
            if (metadataBehavior == null)
            {
                metadataBehavior = new ServiceMetadataBehavior();
                metadataBehavior.HttpGetEnabled = true;
                metadataBehavior.HttpGetUrl = new Uri(string.Format("http://localhost:10085/WCFHostServer/Service1"));
                m_ServiceHost.Description.Behaviors.Add(metadataBehavior);
            }
            m_ServiceHost.Open();
            label2.Text = m_ServiceHost.State.ToString();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Service1.userCallBack.NotifyClientMsg("服务端给客户端通知：" + textBox1.Text);
        }
    }
}
