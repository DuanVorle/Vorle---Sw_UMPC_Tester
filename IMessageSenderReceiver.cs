using System;

public interface IMessageSenderReceiver
{
    bool Setup();
    bool SendMessage(KLineMessage message);
    KLineMessage ReceiveMessage();

    void SetTransmissionBaudRate(UInt32 baudRate);
    string GetRevisionHardwareUKC();
    string GetRevisionFirmwareUKC();

}
