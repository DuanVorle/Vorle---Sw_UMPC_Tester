using BrexScanner.KLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Ascci2BinBootloaderInterface
{

    public class SensorsActuators
    {
        public enum Gyro
        {
            Roll,
            Pitch,
            Yaw,
            Length
        }

        public enum Aceletrometer
        {
            Gravity,
            Laterial,
            Longitudinal,
            Length
        }

        public enum Speed
        {
            Left,
            Rigth,
            Length
        }

        public enum Valves
        {
            Plus,
            Minus,
            Length
        }

        public enum Lamp
        {
            Alert,
            Length
        }

        public enum Pressure
        {
            Channel_1,
            Channel_2,
            Length
        }

        private Gyro gyro;
        private Aceletrometer aceletrometer;
        private Speed speed;    
        private Valves valves;  
        private Lamp lamp;  
        private Pressure pressure;



    }

    internal class BrexTsStaticDetection
    {
        public byte modelID;
        public string modelName;
        public float batteryVoltage;
        public SensorsActuators[] Gyro = new SensorsActuators[(int)SensorsActuators.Gyro.Length];
        public SensorsActuators[] Aceletrometer = new SensorsActuators[(int)SensorsActuators.Aceletrometer.Length];
        public SensorsActuators[] Speed = new SensorsActuators[(int)SensorsActuators.Speed.Length];
        public SensorsActuators[] Valves = new SensorsActuators[(int)SensorsActuators.Valves.Length];
        public SensorsActuators[] Lamp = new SensorsActuators[(int)SensorsActuators.Lamp.Length];
        public SensorsActuators[] Pressure = new SensorsActuators[(int)SensorsActuators.Pressure.Length];

        public BrexTsStaticDetection(byte[] data) 
        {

            // Assert that the data length is 23
            if (data.Length != 23)
            {
                throw new Exception("Data length is not 23");
            }
            this.modelID = data[0];
            switch (this.modelID)
            {
                case 0x22:
                    this.modelName = "TsABS";
                    break;
                case 0x42:
                    this.modelName = "Ts";
                    break;
                default:
                    throw new Exception("Model ID not supported: " + this.modelID);
                    break;
            }


            this.batteryVoltage = (float)(data[1] * 0.2);

        }

    }




    internal class BrexTs
    {
        public enum TestValveEnum
        {
            PLUS = 0X05,
            MINUS = 0X04
        }

        const byte TS_ADDR = 0x66;
        const byte SCANNER_ADDR = 0xF1;

        private bool _isCommunicationStarted = false;
        const bool START_PULSE_NEEDED = false;

        // Create a timer that will be used to check if the communication is still alive
        private System.Timers.Timer _communicationTimer = new System.Timers.Timer(2000);
        private KLineHandler _kLineHandler;

        public BrexTs(IMessageSenderReceiver messageSenderReceiver)
        {
            this._kLineHandler = new KLineHandler(messageSenderReceiver, SCANNER_ADDR, TS_ADDR);
            // this._communicationTimer.Elapsed += _communicationTimer_Elapsed;
            this._communicationTimer.Enabled = false;
            // Set baudrate to default
            //this._kLineHandler.SetTransmissionBaudRate(10400);
            /*
            this._kLineHandler.GetRevisionFirmwareUKC();
            this._kLineHandler.GetRevisionHardwareUKC();
            */
        }

        private void _communicationTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // If communication is not started, stop timer and return
            if (!this._isCommunicationStarted)
            {
                this._communicationTimer.Enabled = false;
                return;
            }
            // Send Tester present
            // Send a message with the format byte 0x82 and data byte 0x82 0x03
            this._kLineHandler.SendKLineMessage(START_PULSE_NEEDED, 0x82, new byte[] { 0x82, 0x03 });
            // Receive Tester present
            KLineMessage message = this._kLineHandler.ReceiveKLineMessage();
            // Assert that message is valid
            if (!this._kLineHandler.isReceivedKLineMessageValid(message))
            {
                this._isCommunicationStarted = false;
                this._communicationTimer.Enabled = false;
                return;
            }
            // Assert that the format byte is 0x81
            if (message.FormatByte != 0x81)
            {
                this._isCommunicationStarted = false;
                this._communicationTimer.Enabled = false;
                return;
            }
            // Assert that first data byte is 0x00
            if (message.Payload[0] != 0x00)
            {
                this._isCommunicationStarted = false;
                this._communicationTimer.Enabled = false;
                return;
            }
        }

        private bool StartCommunicationRaw()
        {
            Console.WriteLine("Starting communication");
            // Send a message with the format byte 0x82 and data byte 0x80 0x03
            this._kLineHandler.SendKLineMessage(START_PULSE_NEEDED, 0x82, new byte[] { 0x80, 0x03 });
            Console.WriteLine("Message sent. Receiving response");
            KLineMessage message = this._kLineHandler.ReceiveKLineMessage();
            Console.WriteLine("Message received");
            // Assert that message is valid
            if (!this._kLineHandler.isReceivedKLineMessageValid(message))
            {
                Console.WriteLine("Message is invalid");
                return false;
            }
            // Assert that the format byte is 0x84
            if (message.FormatByte != 0x84)
            {
                Console.WriteLine("Format byte is invalid: " + message.FormatByte);
                return false;
            }
         
            // Assert that payload length is 4
            if (message.Payload.Length != 4)
            {
                Console.WriteLine("Payload length is invalid");
                return false;
            }

            // Create an uint32 variable and assign it the value of the seed
            uint seed = (uint)((message.Payload[3] << 24) | (message.Payload[2] << 16) | (message.Payload[1] << 8) | message.Payload[0]);
            Console.WriteLine($"Seed HEX: {seed:X}");
            uint key = (seed + 0x041E3140) ^ 0x1CA4DA1A;
            Console.WriteLine($"Key HEX: {key:X}");
            // Get bytes of key (Big Endian)
            byte[] keyBytes = new byte[4];
            keyBytes[0] = (byte)((key >> 24) & 0xFF);
            keyBytes[1] = (byte)((key >> 16) & 0xFF);
            keyBytes[2] = (byte)((key >> 8) & 0xFF);
            keyBytes[3] = (byte)(key & 0xFF);
            // Adds 0x27 and 0x02 before the key bytes
            byte[] keyBytesWithPrefix = new byte[] { 0x81, 0x03, keyBytes[3], keyBytes[2], keyBytes[1], keyBytes[0] };
            Console.WriteLine("Request #2 Data");
            this._kLineHandler.SendKLineMessage(START_PULSE_NEEDED, 0x86, keyBytesWithPrefix);
            Console.WriteLine("Message sent. Receiving response");
            message = this._kLineHandler.ReceiveKLineMessage();
            Console.WriteLine("Message received");
            // Assert that message is valid
            if (!this._kLineHandler.isReceivedKLineMessageValid(message))
            {
                Console.WriteLine("Message is invalid");
                return false;
            }
            // Assert that the format byte is 0x83
            if (message.FormatByte != 0x81)
            {
                Console.WriteLine("Format byte is invalid");
                return false;
            }
            // Assert that payload length is 3
            if (message.Payload.Length != 1)
            {
                Console.WriteLine("Payload length is invalid");
                return false;
            }
            // Assert that payload is 0x00
            if (message.Payload[0] != 0x00)
            {
               Console.WriteLine("Pwd is invalid: " + message.Payload[0]);
                return false;
            }
            Console.WriteLine("Initialization complete");

            return true;
        }

        /*public uint GetRevHwUKC()
        {
            return 
        }*/

        public bool StartCommunication()
        {
            // Stop timer
            this._communicationTimer.Enabled = false;
            this._isCommunicationStarted = this.StartCommunicationRaw();
            // If communication is started, start the timer
            if (this._isCommunicationStarted)
            {
                this._communicationTimer.Enabled = true;
            }

            return _isCommunicationStarted;
        }

        public bool SetBaudRateUKC(uint baudrate)
        {
            this._kLineHandler.SetTransmissionBaudRate(baudrate);
            return true;
        }

        public bool getStatusCommunication()
        {
            if (this._isCommunicationStarted == null)
            {
                this._isCommunicationStarted = false;
            }
            return this._isCommunicationStarted;
        }

        public bool SetBaudrateTs(UInt32 baudrate)
        {
            // Stop timer
            this._communicationTimer.Enabled = false;
            bool setBaudrateStatus = this.SetBaudrateTsRaw(baudrate);
            // If communication is started, start the timer
            if (!this._isCommunicationStarted)
            {
                this._communicationTimer.Enabled = true;
            } else {
                
            }

            return setBaudrateStatus;
        }
        private bool SetBaudrateTsRaw(UInt32 baudrate)
        {
            Console.WriteLine("Set baud rate TS:" + baudrate.ToString());

            UInt32 baudrate2 = baudrate;

            // Split baudrate in 4 bytes
            byte[] baud_bytes = new byte[4];
            baud_bytes[0] = (byte)(baudrate & 0xFF);
            baudrate >>= 8;
            baud_bytes[1] = (byte)(baudrate & 0xFF);
            baudrate >>= 8;
            baud_bytes[2] = (byte)(baudrate & 0xFF);
            baudrate >>= 8;
            baud_bytes[3] = (byte)(baudrate & 0xFF);


            // Send a message with the format byte 0x86 and data byte 0x93, 0x03, and baudrate
            this._kLineHandler.SendKLineMessage(START_PULSE_NEEDED, 0x86, new byte[] { 0x93, 0x02, baud_bytes[0], baud_bytes[1], baud_bytes[2], baud_bytes[3] });
            //this._kLineHandler.SetTransmissionBaudRate(baudrate2);
            Console.WriteLine("Message sent. Receiving response");
            KLineMessage message = this._kLineHandler.ReceiveKLineMessage();
            Console.WriteLine("Message received");
            // Assert that message is valid
            if (!this._kLineHandler.isReceivedKLineMessageValid(message))
            {
                Console.WriteLine("Message is invalid");
                return false;
            }
            // Assert that the format byte is 0x81
            if (message.FormatByte != 0x81)
            {
                Console.WriteLine("Format byte is invalid");
                return false;
            }
            // Assert that payload length is 1
            if (message.Payload.Length != 1)
            {
                Console.WriteLine("Payload length is invalid");
                return false;
            }

            // Assert that the payload is 0x00
            if (message.Payload[0] != 0x00)
            {
                Console.WriteLine("Payload is invalid");
                return false;
            }
            return true;
        }

        public bool SendDataBootloader(byte[] data, uint sizedata, UInt32 address)
        {
            // Stop timer
            this._communicationTimer.Enabled = false;
            bool SendDataBootloaderStatus = SendDataBootloaderRaw(data, sizedata, address);
            if (SendDataBootloaderStatus)
            {
                return true;// sucess
            }
            else
            {
                return false;// failed
            }
        }

        private bool SendDataBootloaderRaw(byte[] data, uint sizedata, UInt32 address)
        {
            byte xordata = 0;

            for (int k = 0; k < sizedata; k++)
            {
                xordata ^= data[k];
            }

            byte[] packed = new byte[sizedata + 6];

            byte sizepacked = (byte)packed.Length;

            byte[] i8address = BitConverter.GetBytes(address);

            packed[0] = 0x91;
            packed[1] = 0x02;
            packed[2] = i8address[0];
            packed[3] = i8address[1];
            packed[4] = i8address[2];
            uint i = 5;
            for ( i = 5; i < sizepacked-1; i++)
            {
                packed[i] = data[i - 5];
            }
            packed[i] = xordata;

            Console.WriteLine("Sending Packed Data: Size: " + sizedata + "\t XOR: " + xordata + "\t Address: " + address);
           

            this._kLineHandler.SendKLineMessage(START_PULSE_NEEDED, (byte)(sizepacked | 0x80), packed);

            KLineMessage message = this._kLineHandler.ReceiveKLineMessage();
            Console.WriteLine("Message received");
            // Assert that message is valid
            if (!this._kLineHandler.isReceivedKLineMessageValid(message))
            {
                Console.WriteLine("Message is invalid");
                return false;
            }
            // Assert that the format byte is 0x85
            if (message.FormatByte != 0x85)
            {
                Console.WriteLine("Format byte is invalid");
                return false;
            }
            // Assert that payload length is 5
            if (message.Payload.Length != 5)
            {
                Console.WriteLine("Payload length is invalid");
                return false;
            }

            // Assert that the payload is 0x00
            if (message.Payload[0] != 0x00)
            {
                Console.WriteLine("Payload is invalid");
                return false;
            }
            return true;

        }

        public bool StartUpdate(byte FwToBeUpdate,uint SizeUpdate)
        {
            // Stop timer
            this._communicationTimer.Enabled = false;
            bool StartUpdateStatus = StartUpdateRaw(FwToBeUpdate, SizeUpdate);
            if (StartUpdateStatus)
            {
                return true;// sucess
            }
            else
            {
                return false;// failed
            }
        }


        public bool StartUpdateRaw(byte FwToBeUpdate, uint SizeUpdate)
        {

            byte[] i8SizeUpdate = BitConverter.GetBytes(SizeUpdate);

            byte[] packed = { 0x90, 0x02, FwToBeUpdate, i8SizeUpdate[0], i8SizeUpdate[1], i8SizeUpdate[2], i8SizeUpdate[3] };
            int sizepacked = packed.Length;

            this._kLineHandler.SendKLineMessage(START_PULSE_NEEDED, (byte)(sizepacked | 0x80), packed);

            KLineMessage message = this._kLineHandler.ReceiveKLineMessage();
            Console.WriteLine("Message received");
            // Assert that message is valid
            if (!this._kLineHandler.isReceivedKLineMessageValid(message))
            {
                Console.WriteLine("Message is invalid");
                return false;
            }
            // Assert that the format byte is 0x81
            if (message.FormatByte != 0x81)
            {
                Console.WriteLine("Format byte is invalid");
                return false;
            }
            // Assert that payload length is 1
            if (message.Payload.Length != 1)
            {
                Console.WriteLine("Payload length is invalid");
                return false;
            }

            // Assert that the payload is 0x00
            if (message.Payload[0] != 0x00)
            {
                Console.WriteLine("Payload is invalid");
                return false;
            }
            return true;
        }

        public byte SendTestUKC(byte testID)
        {            // Stop timer
            this._communicationTimer.Enabled = false;
            byte SendTestUKCMessege = SendTestUKCRaw(testID);
            return SendTestUKCMessege;
        }

        public byte SendTestUKCRaw(byte testID)
        {
            byte[] packed = { 0xAA, 0x00, testID };
            int sizepacked = 3;

            this._kLineHandler.SendKLineMessage(START_PULSE_NEEDED, (byte)(sizepacked | 0x80), packed);

            KLineMessage message = this._kLineHandler.ReceiveKLineMessage();
            Console.WriteLine("Message received");
            // Assert that message is valid
            if (!this._kLineHandler.isReceivedKLineMessageValid(message))
            {
                Console.WriteLine("Message is invalid");
                return 00;
            }

            return message.Payload[2];        
        }

        public byte ReadTestUKC()
        {            // Stop timer
            this._communicationTimer.Enabled = false;
            byte SendTestUKCMessege = ReadTestUKCRaw();
            return SendTestUKCMessege;
        }

        public byte ReadTestUKCRaw()
        {
            KLineMessage message = this._kLineHandler.ReceiveKLineMessage();
            Console.WriteLine("Message received");
            // Assert that message is valid
            if (!this._kLineHandler.isReceivedKLineMessageValid(message))
            {
                Console.WriteLine("Message is invalid");
                return 00;
            }

            return message.Payload[2];
        }

        public bool ResetMCU(byte FwToBeUpdate)
        {
            // Stop timer
            this._communicationTimer.Enabled = false;
            bool ResetMCUStatus = ResetMCURaw(FwToBeUpdate);
            if (ResetMCUStatus)
            {
                return true;// sucess
            }
            else
            {
                return false;// failed
            }
        }

        private bool ResetMCURaw(byte FwToBeUpdate)
        {          

            if(FwToBeUpdate == 0x01)
            {
                Console.WriteLine("Send Command for Firmware Update for Application");
            }
            else if (FwToBeUpdate == 0x07)
            {
                Console.WriteLine("Send Command for Firmware Update for Bootloader");
            }
            else
            {
                Console.WriteLine("Toogle command invalid");
            }


            byte[] packed = {0x95, 0x02, FwToBeUpdate};
            int sizepacked = 3;

            this._kLineHandler.SendKLineMessage(START_PULSE_NEEDED, (byte)(sizepacked | 0x80), packed);

            KLineMessage message = this._kLineHandler.ReceiveKLineMessage();
            Console.WriteLine("Message received");
            // Assert that message is valid
            if (!this._kLineHandler.isReceivedKLineMessageValid(message))
            {
                Console.WriteLine("Message is invalid");
                return false;
            }
            // Assert that the format byte is 0x81
            if (message.FormatByte != 0x81)
            {
                Console.WriteLine("Format byte is invalid");
                return false;
            }
            // Assert that payload length is 1
            if (message.Payload.Length != 1)
            {
                Console.WriteLine("Payload length is invalid");
                return false;
            }

            // Assert that the payload is 0x00
            if (message.Payload[0] != 0x00)
            {
                Console.WriteLine("Payload is invalid");
                return false;
            }
            return true;
        }


        public bool CommitFirmware(UInt32 u32crc)
        {
            // Stop timer
            this._communicationTimer.Enabled = false;
            bool CommitFirmwareStatus = CommitFirmwareRaw(u32crc);
            if (CommitFirmwareStatus)
            {
                return true;// sucess
            }
            else
            {
                return false;// failed
            }
        }

        private bool CommitFirmwareRaw(UInt32 u32crc)
        {            
            Console.WriteLine("Send CRC for checking, CRC: " + u32crc);

            byte[] crc = BitConverter.GetBytes(u32crc);

            byte[] packed = { 0x92, 0x02, crc[0], crc[1], crc[2], crc[3] };
            int sizepacked = 6;

            this._kLineHandler.SendKLineMessage(START_PULSE_NEEDED, (byte)(sizepacked | 0x80), packed);

            KLineMessage message = this._kLineHandler.ReceiveKLineMessage();
            Console.WriteLine("Message received");
            // Assert that message is valid
            if (!this._kLineHandler.isReceivedKLineMessageValid(message))
            {
                Console.WriteLine("Message is invalid");
                return false;
            }
            // Assert that the format byte is 0x85
            if (message.FormatByte != 0x85)
            {
                Console.WriteLine("Format byte is invalid");
                return false;
            }
            // Assert that payload length is 5
            if (message.Payload.Length != 5)
            {
                Console.WriteLine("Payload length is invalid");
                return false;
            }

            // Assert that the payload is 0x00
            if (message.Payload[0] != 0x00)
            {
                Console.WriteLine("Payload is invalid");
                return false;
            }
            return true;
        }

        public bool CheckCRCUpdate(UInt32 u32crc, uint addressEnd)
        {
            // Stop timer
            this._communicationTimer.Enabled = false;
            bool CheckCRCUpdateStatus = CheckCRCUpdateRaw(u32crc, addressEnd);
            if (CheckCRCUpdateStatus)
            {
                return true;// sucess
            }
            else
            {
                return false;// failed
            }
        }

        private bool CheckCRCUpdateRaw(UInt32 u32crc, uint addressEnd)
        {
            Console.WriteLine("Check Memory CRC: " + u32crc.ToString("X"));

            byte[] crc = BitConverter.GetBytes(u32crc);
            byte[] address = BitConverter.GetBytes(addressEnd);

            byte[] packed = { 0x94, 0x02, crc[0], crc[1], crc[2], crc[3], address[0], address[1], address[2] };
            int sizepacked = 9;

            this._kLineHandler.SendKLineMessage(START_PULSE_NEEDED, (byte)(sizepacked | 0x80), packed);

            KLineMessage message = this._kLineHandler.ReceiveKLineMessage();
            Console.WriteLine("Message received");
            // Assert that message is valid
            if (!this._kLineHandler.isReceivedKLineMessageValid(message))
            {
                Console.WriteLine("Message is invalid");
                return false;
            }
            // Assert that the format byte is 0x85
            if (message.FormatByte != 0x85)
            {
                Console.WriteLine("Format byte is invalid");
                return false;
            }
            // Assert that payload length is 5
            if (message.Payload.Length != 5)
            {
                Console.WriteLine("Payload length is invalid");
                return false;
            }

            // Assert that the payload is 0x00
            if (message.Payload[0] != 0x00)
            {
                Console.WriteLine("Payload is invalid");
                return false;
            }
            return true;
        }
    }
}
