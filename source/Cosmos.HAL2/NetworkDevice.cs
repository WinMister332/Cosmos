using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cosmos.HAL;
using Cosmos.HAL.Network;

namespace Cosmos.HAL
{
    public delegate void DataReceivedHandler(byte[] packetData);

    public abstract class NetworkDevice : Device
    {
        public static List<NetworkDevice> Devices { get; private set; }
        public static List<PCIDevice> Card;

        public static void GetNetCard()
        {
            PCIDevice card = PCI.GetDeviceClass(0x02, 0x00);
            if (card == null)
            {
                Console.WriteLine("Not network card found!");
            }
            else
            {
                PCIDevice card_AMDPcnETTII = PCI.GetDevice(0x1022, 0x2000);
                //PCIDevice card_AMDPcnETTII = PCI.GetDevice(0x1022, 0x2000);
                Card.Add(card_AMDPcnETTII);
            }

            foreach (var netcard in Card)
            {
                Console.WriteLine(netcard);
            }
        }

        static NetworkDevice()
        {
            Devices = new List<NetworkDevice>();
        }

        public DataReceivedHandler DataReceived;

        protected NetworkDevice()
        {
            //mType = DeviceType.Network;
            Devices.Add(this);
        }

        public abstract MACAddress MACAddress
        {
            get;
        }

        public abstract bool Ready
        {
            get;
        }

        //public DataReceivedHandler DataReceived;

        public virtual bool QueueBytes(byte[] buffer)
        {
            return QueueBytes(buffer, 0, buffer.Length);
        }

        public abstract bool QueueBytes(byte[] buffer, int offset, int length);

        public abstract bool ReceiveBytes(byte[] buffer, int offset, int max);
        public abstract byte[] ReceivePacket();

        public abstract int BytesAvailable();

        public abstract bool Enable();

        public abstract bool IsSendBufferFull();
        public abstract bool IsReceiveBufferFull();
    }
}
