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
            label1.Text = "Сумма ставки?";
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var bet = await CheckBet(tcpSocket, label1, textBox1);

            if (bet > 0)
            {
                await tcpSocket.SendAsync(Encoding.UTF8.GetBytes(bet.ToString()), SocketFlags.None);

                label1.Text = @$"Вы поставили: + {await GetAnswer(tcpSocket)}
    Выберите действие:
        1. Выберите 1 позицию
        2. Выберите цвет";
            }
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
                label.Text = "Введите число";
            }

            return resultBet;
        }

        static async Task CheckPrize(Socket tcpSocket)
        {
            var answer = new StringBuilder();
            var buffer = new byte[256];

            do
            {
                var size = await tcpSocket.ReceiveAsync(buffer, SocketFlags.None);
                answer.Append(Encoding.UTF8.GetString(buffer, 0, size));
            } while (tcpSocket.Available > 0);

            var resultOfGame = answer.ToString().Split("-");
            Console.WriteLine($"Выпала цифра - {resultOfGame[1]}");
            if (resultOfGame[0] == "0")
            {
                Console.WriteLine("Поражение");
                return;
            }
            Console.WriteLine($"Вы выиграли {resultOfGame[0]}");
        }

        static async Task CheckEnd(Socket tcpSocket, Label label)
        {
            await CheckPrize(tcpSocket);
            //tcpSocket.Shutdown(SocketShutdown.Both);
            label.Text = @"Желаете продолжить?
            1. Y 2. N";
/*            var resumeGame = Console.ReadLine();
            if (resumeGame == "1")
            {
                label.Text = "продолжаем";
            }
            else
            {
                label.Text = "До новых встреч";
                tcpSocket.Close();
                return;
            }*/
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            button6.Enabled = false;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            var choice = textBox1.Text;
            byte number = 0;
            if (!(byte.TryParse(choice, out number) && number is >= 1 and <= 152))
            {
                textBox1.Text = "Выбери число: 1 - 152";
                return;
            }

            await CheckNumber(number, tcpSocket);
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Выбери цвет: 1. Красный / 2. Черный";
            await CheckColour(2, tcpSocket);
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}