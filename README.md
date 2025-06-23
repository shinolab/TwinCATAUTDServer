# TwinCATAUTDServer

This program is used to set up TwinCAT for AUTD3.

This program is built for .Net Framework 4.8 to reduce binary size.
(The dependencies do not support Native AOT and trimming.)

# Usage

```
TwinCATAUTDServer [options]

Options:
  -?, -h, --help            Show help and usage information
  --version                 Show version information
  -c, --client <IP_ADDR>    Client IP address. If empty, use localhost. []
  --device_name <DEV_NAME>  Ethernet device name. If empty, use the first device found. []
  -s, --sync0 <CYCLE_TIME>  Sync0 cycle time in units of 500μs. [default: 2]
  -t, --task <CYCLE_TIME>   Task cycle time in units of CPU base time. [default: 1]
  -b, --base <TIME>         CPU base time. [default: 1ms]
  --twincat <4024|4026>     TwinCAT version [default: 4026]
  -k, --keep                Keep TwinCAT XAE Shell window open. [default: False]
  -d, --debug               Enable debug mode. [default: False]
```

# Author

Shun Suzuki, 2023-2025
