﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Aggregator.Framework.Contracts;
using Aggregator.Test.Helpers.Mocks;
using Material.Contracts;

namespace Quantfabric.Test.Application.Mocks
{
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;

    public class BluetoothAdapterMock : 
        MockBase<IBluetoothAdapter>, 
        IBluetoothAdapter
    {
        public void ListDevices(Action<BluetoothDevice> newDeviceFound)
        {
            throw new NotImplementedException();
        }

        Task<bool> IBluetoothAdapter.ConnectToDevice()
        {
            throw new NotImplementedException();
        }

        Task<bool> IBluetoothAdapter.ConnectToDevice(Guid address)
        {
            throw new NotImplementedException();
        }

        Task<Tuple<DateTimeOffset, JObject>> IBluetoothAdapter.GetCharacteristicValue(Guid deviceAddress, Guid serviceUuid, Guid characteristicUuid)
        {
            throw new NotImplementedException();
        }
    }
}
