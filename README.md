# TwinCATAUTDServer

This program is used to set up TwinCAT for AUTD3.

This program is built for .Net Framework 4.8 to reduce binary size.
(The dependencies do not support Native AOT and trimming.)

# Usage

```
  -?, -h, --help                                                                                     Show help and usage information
  --version                                                                                          Show version information
  -c, --client                                                                                       Client IP address. If empty, use localhost. []
  --device_name                                                                                      Ethernet device name. If empty, use the first device found. []
  -s, --sync0                                                                                        Sync0 cycle time in units of 500μs. [default: 2]
  -t, --task                                                                                         Task cycle time in units of CPU base time. [default: 1]
  -b, --base <100us|125us|1ms|200us|250us|333us|500us|50us|62.5us|66.6us|71.4us|76.9us|83.3us|none>  CPU base time. [default: T_1ms]
  --twincat <4024|4026>                                                                              TwinCAT version [default: Build4026]
  -k, --keep                                                                                         Keep TwinCAT XAE Shell window open. [default: False]
  -d, --debug                                                                                        Enable debug mode. [default: False]
```

# Author

Shun Suzuki, 2023-2025
