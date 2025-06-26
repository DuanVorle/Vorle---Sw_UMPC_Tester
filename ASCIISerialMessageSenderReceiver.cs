using System;
using System.IO.Ports;
using System.Text;
using System.Linq;
using System.Windows.Forms;


public class ASCIISerialMessageSenderReceiver : IMessageSenderReceiver
{
    private string _portName;
    private int _baudRate;
    private SerialPort _serialPort;

    // Flags to avoid sending and receiving messages at the same time
    private bool _isSending;
    private bool _isReceiving;
    public ASCIISerialMessageSenderReceiver(string portName, int baudRate)
    {
        this._portName = portName;
        this._baudRate = baudRate;
        this._serialPort = new SerialPort(portName, baudRate);
        // Set the read timeouts
        // TODO reduce to 3000
        this._serialPort.ReadTimeout = 10;
        // Set Dtr and Rts to true to ensure that the serial port is ready to receive data
        // NOTE: This avoids that the serial port hangs when trying to receive data
        this._serialPort.DtrEnable = true; //enable Data Terminal Ready
        this._serialPort.RtsEnable = true; //enable Request to send
        this._isSending = false; // Clear the sending flag
        this._isReceiving = false; // Clear the receiving flag
    }

    // Create a function that lists all avaliable serial ports
    public static void ListAvailablePorts()
    {
        Console.WriteLine("Available Ports:");
        foreach (string portName in SerialPort.GetPortNames())
        {
            Console.WriteLine(portName);
        }
    }

    public bool Setup()
    {
        try
        {
            _serialPort.Open();
            _serialPort.DiscardInBuffer();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    public bool Disconnect()
    {
        try
        {
            _serialPort.Close();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    public bool SendMessage(KLineMessage message)
    {
        // Wait until the serial port is ready to send data.
        // We also need to check if the serial port is receiving data to avoid sending data while receiving data
        while (_isSending || _isReceiving) { }
        // Set the sending flag to true to assure that no other message is sent while this message is being sent
        _isSending = true;
        Console.WriteLine("Sending message...");
        string hexString = GetHexBytesString(message.RawData, (int)message.RawData.Length);
        Console.WriteLine($"Hex representation: {hexString}");
        _serialPort.WriteLine(hexString);
        Console.WriteLine("Message sent.");
        // Sleep for 1 second to allow the message to be sent
        //System.Threading.Thread.Sleep(1000);
        KLineMessage echo = ReceiveMessage();
        // Clear the sending flag
        _isSending = false;
        //return true;
        return echo.Equals(message);
    }

    public byte[] SendMessage(byte[] message, short length)
    {
        // Wait until the serial port is ready to send data.
        // We also need to check if the serial port is receiving data to avoid sending data while receiving data
        while (_isSending || _isReceiving) { }
        // Set the sending flag to true to assure that no other message is sent while this message is being sent
        _isSending = true;
        Console.WriteLine("Sending message...");
        string hexString = GetHexBytesString(message, length);
        Console.WriteLine($"Hex representation: {hexString}");
        _serialPort.WriteLine(hexString);
        Console.WriteLine("Message sent.");
        // Sleep for 1 second to allow the message to be sent
        //System.Threading.Thread.Sleep(1000);
        byte[] echo = ReceiveMessage(30);
        // Clear the sending flag
        _isSending = false;
        //return true;
        return echo;
    }

    public KLineMessage ReceiveMessage()
    {
        const UInt16 SERIAL_TIMEOUT = 6000;
        const UInt16 DEFAULT_DELAY = 10;
        const UInt16 KLINE_OVERHEAD = 4;
        KLineMessage receivedMessage;
        // Set the receiving flag to true to assure that no other message is received while this message is being received
        _isReceiving = true;
        // Array to store the received data
        byte[] sumData = { };
        string line = "";
        for (int i = 0; i < 3; i++)
        {
            try
            {
                Console.WriteLine("Receiving message...");

                // Receive packet size
                string packet_size_str = "";

                while(packet_size_str.Length != 2)
                {
                    char rChar = (char)_serialPort.ReadChar();
                    // Ignore line breakers
                    if (rChar == '\r' || rChar == '\n')
                    {
                        continue;
                    }
                    if (rChar >= '0' || rChar <= '9' || rChar >= 'A' || rChar <= 'F' || rChar >= 'a' || rChar <= 'f')
                    {
                    }
                    else
                    {
                        Console.Write("fuck");
                    }
                    // Append char
                    packet_size_str += rChar;
                }

                // Convert the size to a byte array
                byte[] in_bytes_packet = GetBytesFromHexString(packet_size_str);

                UInt16 packet_size = (ushort)((ushort)(in_bytes_packet[0] & 0x3F) + KLINE_OVERHEAD);

                // Read a line from serial port
                line = packet_size_str;

                // Try to read until timeout
                for (UInt16 current_delay = 0; current_delay < SERIAL_TIMEOUT; current_delay += DEFAULT_DELAY)
                {
                    // While has data to read
                    while (_serialPort.BytesToRead > 0)
                    {
                        int rChar = _serialPort.ReadChar();
                        // Ignore line breakers
                        if (rChar == '\r' || rChar == '\n')
                        {
                            continue;
                        }

                        if (rChar >= '0' || rChar <= '9' || rChar >= 'A' || rChar <= 'F' || rChar >= 'a' || rChar <= 'f')
                        {
                        }
                        else
                        {
                            Console.WriteLine("fuck");
                        }
                        // Append char
                        line += (char)rChar;
                        //Console.WriteLine("rChar: " + rChar);
                        //Console.WriteLine("Line: " + line);

                        // If has read enough bytes (packet_size * 2), get out
                        if (line.Length >= packet_size * 2)
                        {
                            break;
                        }
                    }
                    // If has read enough bytes (packet_size * 2), get out
                    if (line.Length >= packet_size * 2)
                    {
                        break;
                    }
                    // Wait new data
                    System.Threading.Thread.Sleep(DEFAULT_DELAY);
                }
                
                

                // Convert the line to a byte array
                byte[] in_bytes = GetBytesFromHexString(line);

                // Concatenate the received data to the sumData array
                sumData = sumData.Concat(in_bytes).ToArray();

                Console.WriteLine($"Received Size: " + sumData.Length);

                // Check if the received data is a valid KLineMessage
                if (sumData.Length < KLineMessage.MINIMUM_SIZE )
                {
                    continue;
                }

                // Create a KLineMessage object with the received data
                receivedMessage = new KLineMessage(sumData, false);
                // Clear the sumData array
                sumData = new byte[] { };

                string hexString = GetHexBytesString(receivedMessage.RawData, (int)receivedMessage.RawData.Length);
                Console.WriteLine($"Received hex data: {hexString}");
                Console.WriteLine("Message received.");
                // Clear the receiving flag
                _isReceiving = false;
                return receivedMessage;
            }
            catch (Exception e)
            {
                Console.WriteLine("Linha: " + line);
                Console.WriteLine(e.Message);
            }
        }
        // If the message is not received, return an empty message
        receivedMessage = new KLineMessage();
        // Clear the receiving flag
        _isReceiving = false;
        return receivedMessage;
    }

    public byte[] ReceiveMessage(UInt16 timeout)
    {
        UInt16 SERIAL_TIMEOUT = timeout;
        const UInt16 DEFAULT_DELAY = 10;
        const UInt16 KLINE_OVERHEAD = 4;
        // Set the receiving flag to true to assure that no other message is received while this message is being received
        _isReceiving = true;
        // Array to store the received data
        byte[] sumData = { };
        string line = "";
        for (int i = 0; i < 3; i++)
        {
            try
            {
                Console.WriteLine("Receiving message...");

                // Receive packet size
                string packet_size_str = "";

                while (packet_size_str.Length != 2)
                {
                    char rChar = (char)_serialPort.ReadChar();
                    // Ignore line breakers
                    if (rChar == '\r' || rChar == '\n')
                    {
                        continue;
                    }
                    if (rChar >= '0' || rChar <= '9' || rChar >= 'A' || rChar <= 'F' || rChar >= 'a' || rChar <= 'f')
                    {
                    }
                    else
                    {
                        Console.Write("fuck");
                    }
                    // Append char
                    packet_size_str += rChar;
                }

                // Convert the size to a byte array
                byte[] in_bytes_packet = GetBytesFromHexString(packet_size_str);

                UInt16 packet_size = (ushort)((ushort)(in_bytes_packet[0] & 0x3F) + KLINE_OVERHEAD);

                // Read a line from serial port
                line = packet_size_str;

                // Try to read until timeout
                for (UInt16 current_delay = 0; current_delay < SERIAL_TIMEOUT; current_delay += DEFAULT_DELAY)
                {
                    // While has data to read
                    while (_serialPort.BytesToRead > 0)
                    {
                        int rChar = _serialPort.ReadChar();
                        // Ignore line breakers
                        if (rChar == '\r' || rChar == '\n')
                        {
                            continue;
                        }

                        if (rChar >= '0' || rChar <= '9' || rChar >= 'A' || rChar <= 'F' || rChar >= 'a' || rChar <= 'f')
                        {
                        }
                        else
                        {
                            Console.WriteLine("fuck");
                        }
                        // Append char
                        line += (char)rChar;
                        //Console.WriteLine("rChar: " + rChar);
                        //Console.WriteLine("Line: " + line);

                        // If has read enough bytes (packet_size * 2), get out
                        if (line.Length >= packet_size * 2)
                        {
                            break;
                        }
                    }
                    // If has read enough bytes (packet_size * 2), get out
                    if (line.Length >= packet_size * 2)
                    {
                        break;
                    }
                    // Wait new data
                    System.Threading.Thread.Sleep(DEFAULT_DELAY);
                }



                // Convert the line to a byte array
                byte[] in_bytes = GetBytesFromHexString(line);

                // Concatenate the received data to the sumData array
                sumData = sumData.Concat(in_bytes).ToArray();

                Console.WriteLine($"Received Size: " + sumData.Length);

                // Check if the received data is a valid KLineMessage
                if (sumData.Length < KLineMessage.MINIMUM_SIZE)
                {
                    continue;
                }

                _isReceiving = false;
                return sumData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Linha: " + line);
                Console.WriteLine(e.Message);
                byte[] retorno = { 0};
                //return retorno;
            }
        }
        _isReceiving = false;

        if(sumData.Length == 0 || sumData == null)
        {
            Console.WriteLine("No data received.");
            byte[] retorno = { 0 };
            return retorno;
        }
        return sumData;
    }

    // Create a function to convert a hex string to a byte array
    private byte[] GetBytesFromHexString(string hexString)
    {
        int length = hexString.Length;
        // If length is even, subtract 1
        if((length % 2) == 1)
        {
            length--;
        }
        byte[] bytes = new byte[length / 2];
        for (int i = 0; i < length; i += 2)
        {
            bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
        }
        return bytes;
    }

    // Create a function to convert a byte array to a hex string
    private string GetHexBytesString(byte[] bytes, int length)
    {
        StringBuilder hexBuilder = new StringBuilder();
        for (int i = 0; i < length; i++)
        {
            hexBuilder.Append(bytes[i].ToString("X2"));
        }
        return hexBuilder.ToString();
    }

    public void SetTransmissionBaudRate(uint baudRate)
    {
        String cmd = "$B" + baudRate;
        Console.WriteLine("Setting baudrate: " + cmd);
        // Set the sending flag to true to assure that no other message is sent while this message is being sent
        _isSending = true;
        _serialPort.WriteLine(cmd);
        Console.WriteLine("Message sent.");
        // Wait serial port write all data
        do
        {
            // Sleep for 100 miliseconds to wait message be sent
            System.Threading.Thread.Sleep(1000);
        } while (_serialPort.BytesToWrite > 0);
        // Clear the sending flag
        _isSending = false;
    }



    public string GetRevisionHardwareUKC()
    {
        String cmd = "$VH";
        // Set the sending flag to true to assure that no other message is sent while this message is being sent
        _isSending = true;
        _serialPort.WriteLine(cmd);
        Console.WriteLine("Message sent.");
        // Wait serial port write all data
        while (_serialPort.BytesToWrite > 0)
        {
            // Sleep for 100 miliseconds to wait message be sent
            System.Threading.Thread.Sleep(100);
        }
        // Clear the sending flag
        _isSending = false;

        string messege = _serialPort.ReadLine();
        Console.WriteLine("Hardware Version " + messege);
        return messege;
    }

    public string GetRevisionFirmwareUKC()
    {
        String cmd = "$VF";
        // Set the sending flag to true to assure that no other message is sent while this message is being sent
        _isSending = true;
        _serialPort.WriteLine(cmd);
        Console.WriteLine("Message sent.");
        // Wait serial port write all data
        while (_serialPort.BytesToWrite > 0)
        {
            // Sleep for 10 miliseconds to wait message be sent
            System.Threading.Thread.Sleep(10);
        }
        // Clear the sending flag
        _isSending = false;

        string messege = _serialPort.ReadLine();
        Console.WriteLine("Firmware Version " + messege);
        return messege;

    }
}

