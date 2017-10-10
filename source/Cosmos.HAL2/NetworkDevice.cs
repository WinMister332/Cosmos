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

        public static void GetNetCards()
        {
            PCIDevice card = PCI.GetDeviceClass(0x02, 0x00);
            if (card == null)
            {
                Console.WriteLine("Not network card found!");
            }
            else
            {
                Console.WriteLine("Network cards found!");

                //Check for AMDPCNETII
                PCIDevice card_AMDPCNETII = PCI.GetDevice(0x1022, 0x2000);
                if (card_AMDPCNETII != null)
                {
                    Console.WriteLine("AMDPCNETII card found!");
                }

                //Check for RTL8139
                PCIDevice card_RTL8139 = PCI.GetDevice(0x10EC, 0x8139);
                if (card_RTL8139 != null)
                {
                    Console.WriteLine("RTL8139 card found!");
                }
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
