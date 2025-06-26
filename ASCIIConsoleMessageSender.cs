
using System;
using System.Text;

public class ASCIIConsoleMessageSender : IMessageSenderReceiver
{
    private string _portName;
    private int _baudRate;
    public ASCIIConsoleMessageSender(string portName, int baudRate)
    {
        this._portName = portName;
        this._baudRate = baudRate;
    }

    public bool Setup()
    {
        return true;
    }

    public bool SendMessage(KLineMessage message)
    {
        Console.WriteLine("Sending message...");
        string hexString = GetHexBytesString(message.RawData, (int)message.RawData.Length);
        Console.WriteLine($"Hex representation: {hexString}");
        Console.WriteLine("Message sent.");
        return true;
    }

    public KLineMessage ReceiveMessage()
    {
        Console.WriteLine("Receiving message...");

        // Read a line from console
        string line = Console.ReadLine();

        // Convert the line to a byte array
        byte[] in_bytes = GetBytesFromHexString(line);


        // For the sake of this example, let's create a dummy message
        KLineMessage receivedMessage = new KLineMessage(in_bytes, false);

        string hexString = GetHexBytesString(receivedMessage.RawData, (int)receivedMessage.RawData.Length);
        Console.WriteLine($"Received hex data: {hexString}");
        Console.WriteLine("Message received.");

        return receivedMessage;
    }

    // Create a function to convert a hex string to a byte array
    private byte[] GetBytesFromHexString(string hexString)
    {
        int length = hexString.Length;
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
        throw new NotImplementedException();
    }

    public string GetRevisionHardwareUKC()
    {
        throw new NotImplementedException();
    }

    public string GetRevisionFirmwareUKC()
    {
        throw new NotImplementedException();
    }
}

