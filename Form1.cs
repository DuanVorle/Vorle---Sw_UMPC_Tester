using Ascci2BinBootloaderInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UMPC_Production
{
    public partial class frmHome : Form
    {
        ASCIISerialMessageSenderReceiver asciiCOM_Testador;
        ASCIISerialMessageSenderReceiver asciiCOM_EmTeste;

        BrexTs Testador;
        BrexTs EmTeste;

        short Testados = 0, Aprovados = 0, Reprovados = 0;

        const byte testbyte1 = 0x55;
        const byte testbyte2 = 0xAA;

        public frmHome()
        {
            InitializeComponent();
        }

        public static bool PortaSerialExiste(string portName)
        {
            return SerialPort.GetPortNames().Contains(portName);
        }

        private void btnTeste_Click(object sender, EventArgs e)
        {

            pbxStatus.Image = Properties.Resources.TESTANDO;
            rtbDebug.Clear();

            if (cbxTestador.Text == "" || cbxTestador.Text == null)
            {
                MessageBox.Show("Selecione uma porta de comunicação");
                pbxStatus.Image = Properties.Resources.Aguardando;
                return;

            }
            if (!PortaSerialExiste(cbxTestador.Text))
            {
                MessageBox.Show("Prolemas com Testador");
                pbxStatus.Image = Properties.Resources.Aguardando;
                return;
            }

            if (cbxEmTeste.Text == "" || cbxEmTeste.Text == null)
            {
                MessageBox.Show("Selecione uma porta de comunicação");
                pbxStatus.Image = Properties.Resources.Aguardando;
                return;
            }
            if (!PortaSerialExiste(cbxEmTeste.Text))
            {
                Reprovados++;
                pbxStatus.Image = Properties.Resources.Reprovado;
                rtbDebug.Text = "Prolemas com o dispositivo Em Teste. Porta não encontrada.";
                goto EndTest;
            }

            rtbDebug.Text = "Iniciando teste...\n" +
                            "Testando dispositivo: " + cbxEmTeste.Text + "\n" +
                            "Testador: " + cbxTestador.Text + "\n";

            if (asciiCOM_Testador == null) asciiCOM_Testador = new ASCIISerialMessageSenderReceiver(cbxTestador.Text, 115200);
            if(asciiCOM_EmTeste == null) asciiCOM_EmTeste = new ASCIISerialMessageSenderReceiver(cbxEmTeste.Text, 115200);

            if(Testador == null) Testador = new BrexTs(asciiCOM_Testador);
            if(EmTeste == null) EmTeste = new BrexTs(asciiCOM_EmTeste); 

            asciiCOM_Testador.Setup();
            asciiCOM_EmTeste.Setup();


            if(!Testador.SetBaudRateUKC(115200))
            {
                MessageBox.Show("Falha ao configurar a taxa de transmissão do Testador.");
                return;
            }

            if(!EmTeste.SetBaudRateUKC(115200))
            {
                MessageBox.Show("Falha ao configurar a taxa de transmissão do dispositivo Em Teste.");
                return;
            }

            rtbDebug.Text += "Taxa de transmissão configurada com sucesso.\n";

            byte[] mensage = { testbyte1 };
            byte[] dado = asciiCOM_Testador.SendMessage(mensage, 1);

            if (dado[0] != mensage[0])
            {

                rtbDebug.Text += "Falha ao enviar byte de teste. Echo: " + dado[0].ToString("X2") + "\n";
                Reprovados++;
                pbxStatus.Image = Properties.Resources.Reprovado;

                goto EndTest;
            }      

            byte[] dado2 = asciiCOM_EmTeste.ReceiveMessage(30);

            rtbDebug.Text += "Enviando byte de teste: " + testbyte1.ToString("X2") + "\n";

            if (dado2[0] != testbyte1)
            {
                Reprovados++;
                pbxStatus.Image = Properties.Resources.Reprovado;
                rtbDebug.Text += "Teste falhou. Byte recebido: " + dado2[0].ToString("X2") + "\n";

                goto EndTest;
            }


            mensage[0] = testbyte2;
            dado = asciiCOM_EmTeste.SendMessage(mensage, 1);

            if (dado[0] != mensage[0])
            {

                rtbDebug.Text += "Falha ao enviar byte de teste. Echo: " + dado[0].ToString("X2") + "\n";
                Reprovados++;
                pbxStatus.Image = Properties.Resources.Reprovado;

                goto EndTest;
            }

            dado2 = asciiCOM_Testador.ReceiveMessage(30);
            rtbDebug.Text += "Enviando byte de teste: " + testbyte2.ToString("X2") + "\n";

            if (dado2[0] != testbyte2)
            {
                rtbDebug.Text += "Teste falhou. Byte recebido: " + dado2[0].ToString("X2") + "\n";
                Reprovados++;
                pbxStatus.Image = Properties.Resources.Reprovado;

                goto EndTest;
            }            
            
            rtbDebug.Text += "Teste bem-sucedido. Byte recebido: " + dado2[0].ToString("X2") + "\n";
            Aprovados++;
            pbxStatus.Image = Properties.Resources.Aprovado;  

        EndTest:

            Testados++;
            UpdateIndicator();
            rtbDebug.Text += "Teste concluído.\n" +
                        "Total de dispositivos testados: " + Testados + "\n" +
                        "Dispositivos aprovados: " + Aprovados + "\n" +
                        "Dispositivos reprovados: " + Reprovados + "\n";

            asciiCOM_EmTeste.Disconnect();
            asciiCOM_Testador.Disconnect();

            rtbDebug.Text += "Conexões encerradas.\n";

        }

        private void UpdateIndicator()
        {
            lblTestados.Text = Testados.ToString();
            lblAprovados.Text = Aprovados.ToString();
            lblReprovados.Text = Reprovados.ToString();
        }

        private void cbxTestador_DropDown(object sender, EventArgs e)
        {
            string[] portas = SerialPort.GetPortNames();
            cbxTestador.Items.Clear(); // Limpa as opções anteriores
            cbxTestador.Items.AddRange(portas); // Adiciona as portas encontradas

            if (cbxTestador.Items.Count > 0)
            {
                cbxTestador.SelectedIndex = 0; // Seleciona a primeira porta por padrão
            }
            else
            {
                MessageBox.Show("Nenhuma porta serial foi encontrada.");
            }

        }

        private void cbxEmTeste_DropDown(object sender, EventArgs e)
        {
            string[] portas = SerialPort.GetPortNames();
            cbxEmTeste.Items.Clear(); // Limpa as opções anteriores
            cbxEmTeste.Items.AddRange(portas); // Adiciona as portas encontradas

            if (cbxEmTeste.Items.Count > 0)
            {
                cbxEmTeste.SelectedIndex = 0; // Seleciona a primeira porta por padrão
            }
            else
            {
                MessageBox.Show("Nenhuma porta serial foi encontrada.");
            }
        }
    }
}
