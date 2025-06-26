using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
// Add Serial Port handler


namespace BrexScanner.KLine
{
    internal class KLineHandler
    {
        private byte sourceAddress;
        private byte destinationAddress;

        // Create a constructor that takes in a IMessageSenderReceiver object
        // and assign it to a private field
        private IMessageSenderReceiver _messageSenderReceiver;
        public KLineHandler(IMessageSenderReceiver messageSenderReceiver, byte sourceAddress, byte destinationAddress)
        {
            this._messageSenderReceiver = messageSenderReceiver;
            this.sourceAddress = sourceAddress;
            this.destinationAddress = destinationAddress;
        }

        public void SendKLineMessage(KLineMessage message)
        {
            // Send the message using the IMessageSenderReceiver object
            this._messageSenderReceiver.SendMessage(message);
        }

        public void SendKLineMessage(bool hasStartPulse, byte format_byte, byte[] data)
        {
            // Create a new message object
            KLineMessage message = new KLineMessage(format_byte, this.destinationAddress, this.sourceAddress, data, hasStartPulse);

            // Send the message using the IMessageSenderReceiver object
            this._messageSenderReceiver.SendMessage(message);
            
        }

        
        public KLineMessage ReceiveKLineMessage()
        {
            // Receive the message using the IMessageSenderReceiver object
            return this._messageSenderReceiver.ReceiveMessage();

        }

        public void SetTransmissionBaudRate(UInt32 baudRate)
        {
            this._messageSenderReceiver.SetTransmissionBaudRate(baudRate);
        }

        public void GetRevisionHardwareUKC()
        {
            this._messageSenderReceiver.GetRevisionHardwareUKC();
        }

        public void GetRevisionFirmwareUKC()
        {
            this._messageSenderReceiver.GetRevisionFirmwareUKC();
        }

        public bool isReceivedKLineMessageValid(KLineMessage message)
        {
            // Check if remoteAddress is the same as the source byte
            Console.WriteLine("Source byte: " + message.SourceByte + " Destination address: " + this.destinationAddress);
            if (message.SourceByte != this.destinationAddress)
            {
                return false;
            }
            // Check if localAddress is the same as the destination byte
            Console.WriteLine("Destination byte: " + message.DestinationByte + " Source address: " + this.sourceAddress);
            if (message.DestinationByte != this.sourceAddress)
            {
                return false;
            }

            // Check if the checksum is valid
            if (!message.isValid())
            {
                return false;
            }
            return true;

        }
    }
}
