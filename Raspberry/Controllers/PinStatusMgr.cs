using System.Collections.Generic;
using System.Linq;
using Unosquare.RaspberryIO.Gpio;

namespace RaspberryAPI.Controllers
{
    public class PinStatusMgr
    {
        public List<PinStatus> Pins { get; private set; }

        private static PinStatusMgr _instance;
        public static PinStatusMgr Manager
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PinStatusMgr();
                }

                return _instance;
            }
        }

        public PinStatusMgr()
        {
            this.setPins();
        }

        public PinStatus getPinByNumber(ushort pinNumber)
        {
            if(pinNumber > this.Pins.Count)
            {
                return null;
            }

            return this.Pins[pinNumber];
        }

        private void setPins()
        {
            this.Pins = new List<PinStatus>();

            GpioController.Instance.Pins.ToList().ForEach(pin =>
            {
                this.Pins.Add(new PinStatus(pin));
            });
        }
    }

    public class PinStatus
    {
        public PinStatus(GpioPin pin)
        {
            this.Pin = pin;
            this.Pin.PinMode = GpioPinDriveMode.Output;
            this.PinNumber = (ushort)pin.PinNumber;
        }

        public void on()
        {
            if (this.IsOn)
            {
                return;
            }
            
            this.IsOn = true;
            this.Pin.Write(true);
        }

        public void off()
        {
            if (!this.IsOn)
            {
                return;
            }
            
            this.IsOn = false;
            this.Pin.Write(false);
        }

        public ushort PinNumber { get; private set; }
        public GpioPin Pin { get; private set; }
        public bool IsOn { get; private set; }
    }
}
