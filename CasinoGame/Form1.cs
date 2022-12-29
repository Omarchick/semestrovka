using System.Net;
using System.Net.Sockets;
using System.Text;



namespace CasinoGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Socket tcpSocket;

        private void Form1_Load(object sender, EventArgs e)
        {
            const string ip = "127.0.0.1";
            const int port = 8080;
            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            tcpSocket.Connect(tcpEndPoint);
            textArea.Text = "����� ������?";
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            textArea.Text = $"�� ������� ������";
            await tcpSocket.SendAsync(new byte[] { 2 }, SocketFlags.None);
            chooseRed.Visible = false;
            chooseBlack.Visible = false;
            await CheckPrize(tcpSocket, textArea, timerLabel);
            endGame.Visible = true;
            chooseBetMoney.Visible = true;
            inputMoney.Visible = true;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            endGame.Visible = false;
            var bet = await CheckBet(tcpSocket, textArea, inputMoney);
            if (bet > 0)
            {
                await tcpSocket.SendAsync(Encoding.UTF8.GetBytes(bet.ToString()), SocketFlags.None);

                textArea.Text = $"�� ���������: {await GetAnswer(tcpSocket)}\n�������� ��������";
            }
            else return;
            chooseRange.Visible = true;
            chooseColour.Visible = true;
            inputMoney.Visible = false;
            chooseBetMoney.Visible = false;
        }

        static async Task<string> GetAnswer(Socket tcpSocket)
        {
            var buffer = new byte[256];
            var answer = new StringBuilder();
            do
            {
                var size = await tcpSocket.ReceiveAsync(buffer, SocketFlags.None);
                answer.Append(Encoding.UTF8.GetString(buffer, 0, size));
            } while (tcpSocket.Available > 0);

            return answer.ToString();
        }

        static async Task CheckNumber(byte number, Socket tcpSocket)
        {
            await tcpSocket.SendAsync(new byte[] { number }, SocketFlags.None);
        }

        static async Task CheckColour(byte number, Socket tcpSocket)
        {
            await tcpSocket.SendAsync(new byte[] { number }, SocketFlags.None);
        }

        static async Task<int> CheckBet(Socket tcpSocket, Label label, TextBox textBox)
        {
            var resultBet = 0;
            var bet = textBox.Text;
            if (!(int.TryParse(bet, out resultBet) && resultBet > 0))
            {
                label.Text = "������� �����";
            }
            textBox.Text = string.Empty;
            return resultBet;
        }

        static async Task CheckPrize(Socket tcpSocket, Label label, Label timerLabel)
        {
            var answer = new StringBuilder();
            var buffer = new byte[256];

            await tcpSocket.ReceiveAsync(buffer, SocketFlags.None);
            var time = buffer[0];
            while (time > 0)
            {
                timerLabel.Text = $"���� ������� ����� {time}";
                time--;
                await Task.Delay(1000);
            }
            timerLabel.Text = string.Empty;
            do
            {
                var size = await tcpSocket.ReceiveAsync(buffer, SocketFlags.None);
                answer.Append(Encoding.UTF8.GetString(buffer, 0, size));
            } while (tcpSocket.Available > 0);
            var resultOfGame = answer.ToString().Split("-");
            answer.Clear();
            answer.Append($"������ ����� - {resultOfGame[1]}");
            if (resultOfGame[0] == "0")
            {
                answer.Append("\n���������");
            }
            else
            {
                answer.Append($"\n�� �������� {resultOfGame[0]}");
            }
            label.Text = answer.ToString();
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            start.Visible = false;
            const string ip = "127.0.0.1";
            const int port = 8080;
            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            tcpSocket.Connect(tcpEndPoint);
            textArea.Text = "����� ������?";
            chooseBetMoney.Visible = true;
            inputMoney.Visible = true;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await tcpSocket.SendAsync(new byte[] { 1 }, SocketFlags.None);
            textArea.Text = "������ �����: 1 - 152";
            chooseRange.Visible = false;
            chooseColour.Visible = false;
            chooseNumber.Visible = true;
            betButton.Visible = true;
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            textArea.Text = @"������ ����";
            await CheckColour(2, tcpSocket);
            chooseRange.Visible = false;
            chooseColour.Visible = false;
            chooseRed.Visible = true;
            chooseBlack.Visible = true;
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            textArea.Text = "�� ������� �������";
            await tcpSocket.SendAsync(new byte[] { 1 }, SocketFlags.None);
            chooseBlack.Visible = false;
            chooseRed.Visible = false;
            await CheckPrize(tcpSocket, textArea, timerLabel);
            endGame.Visible = true;
            chooseBetMoney.Visible = true;
            inputMoney.Visible = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button9_Click(object sender, EventArgs e)
        {
            var choice = chooseNumber.Text;
            byte number = 0;
            if (!(byte.TryParse(choice, out number) && number is >= 1 and <= 152))
            {
                textArea.Text = "������ ����� �� 1 �� 152";
                return;
            }
            textArea.Text = $"�� ������� {number}";
            await CheckNumber(number, tcpSocket);
            chooseRange.Visible = false;
            chooseNumber.Visible = false;
            betButton.Visible = false;
            await CheckPrize(tcpSocket, textArea, timerLabel);
            endGame.Visible = true;
            chooseBetMoney.Visible = true;
            inputMoney.Visible = true;

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            endGame.Visible = false;
            chooseBetMoney.Visible = false;
            inputMoney.Visible = false;
            tcpSocket.Shutdown(SocketShutdown.Both);
           tcpSocket.Close();
            start.Visible = true;
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}