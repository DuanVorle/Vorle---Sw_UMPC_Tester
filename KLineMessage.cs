using System;

public class KLineMessage
{
    // Create an enum with positions: FORMAT_BYTE, DESTINATION_BYTE, SOURCE_BYTE
    private enum MessagePosition
    {
        FORMAT_BYTE = 0,
        DESTINATION_BYTE = 1,
        SOURCE_BYTE = 2
    }

    public static readonly int MINIMUM_SIZE = 4;


    private byte[] raw_data;
    private bool has_start_pulse;
    private byte format_byte;
    private byte destination_byte;
    private byte source_byte;
    private byte[] payload;

    // Create format_byte, destination_byte, source_byte, payload, raw_data, has_start_pulse getters
    public byte[] RawData
    {
        get
        {
            return raw_data;
        }
    }

    public bool HasStartPulse
    {
        get
        {
            return has_start_pulse;
        }
    }

    public byte FormatByte
    {
        get
        {
            return format_byte;
        }
    }

    public byte DestinationByte
    {
        get
        {
            return destination_byte;
        }
    }

    public byte SourceByte
    {
        get
        {
            return source_byte;
        }
    }

    public byte[] Payload
    {
        get
        {
            return payload;
        }
    }

    // Create a Equals method that takes in a KLineMessage object and returns a bool
    public bool Equals(KLineMessage message)
    {
        // Check if the raw_data is the same
        if (this.RawData != message.RawData)
        {
            return false;
        }

        // Check if the format_byte is the same
        if (this.FormatByte != message.FormatByte)
        {
            return false;
        }
        // Check if the destination_byte is the same
        if (this.DestinationByte != message.DestinationByte)
        {
            return false;
        }
        // Check if the source_byte is the same
        if (this.SourceByte != message.SourceByte)
        {
            return false;
        }
        // Check if the payload is the same
        if (this.Payload != message.Payload)
        {
            return false;
        }
        return true;
    }


    public KLineMessage(byte formatByte, byte destination, byte source, byte[] inputData, bool hasStartPulse)
    {
        raw_data = new byte[inputData.Length + 4];
        this.format_byte = formatByte;
        this.destination_byte = destination;
        this.source_byte = source;
        this.payload = inputData;
        raw_data[(int)MessagePosition.FORMAT_BYTE] = this.format_byte;
        raw_data[(int)MessagePosition.DESTINATION_BYTE] = this.destination_byte;
        raw_data[(int)MessagePosition.SOURCE_BYTE] = this.source_byte;
        Array.Copy(inputData, 0, raw_data, 3, inputData.Length);
        raw_data[raw_data.Length - 1] = calculateChecksum(raw_data, raw_data.Length - 1);
        has_start_pulse = hasStartPulse;
    }

    public KLineMessage(byte[] inputData, bool hasStartPulse)
    {
        raw_data = new byte[inputData.Length];
        Array.Copy(inputData, raw_data, inputData.Length);
        has_start_pulse = hasStartPulse;
        this.format_byte = raw_data[(int)MessagePosition.FORMAT_BYTE];
        this.destination_byte = raw_data[(int)MessagePosition.DESTINATION_BYTE];
        this.source_byte = raw_data[(int)MessagePosition.SOURCE_BYTE];
        this.payload = new byte[raw_data.Length - 4];
        Array.Copy(raw_data, 3, payload, 0, raw_data.Length - 4);
    }

    public KLineMessage()
    {
        this.raw_data = new byte[0];
        this.payload = new byte[0];
    }

    private byte calculateChecksum(byte[] data, int data_size)
    {
        byte checksum = 0;
        for (int i = 0; i < data_size; i++)
        {
            checksum += data[i];
        }
        return checksum;
    }

    public bool isValid()
    {
        int data_size = (int)this.RawData.Length;

        // Check if the checksum (last byte) is valid
        byte checksum = raw_data[data_size - 1];
        byte calculatedChecksum = calculateChecksum(raw_data, data_size - 1);
        Console.WriteLine("Checksum: " + checksum + " Calculated Checksum: " + calculatedChecksum);
        if (checksum != calculatedChecksum)
        {
            return false;
        }

        return true;
    }

}

