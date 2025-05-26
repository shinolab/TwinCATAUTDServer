# TwinCATAUTDServer

TwinCAT AUTD3 Server for `link::TwinCAT` and `link::RemoteTwinCAT`.

# Usage

```
  TwinCATAUTDServer.exe [options]
```

## Options
* -c, --client <client>                                                                              Client IP address. if empty, use localhost. []
* -s, --sync0 <sync0>                                                                                Sync0 cycle time in units of 500μs. [default: 2]
* -t, --task <task>                                                                                  Task cycle time in units of CPU base time. [default: 1]
* -b, --base <none|1ms|500us|333us|250us|200us|125us|100us|83.3us|76.9us|71.4us|66.6us|62.5us|50us>  CPU base time. [default: 1ms]
* -k, --keep                                                                                         Keep TwinCAT XAE Shell window open [default: False]
* --version                                                                                          Show version information
* -?, -h, --help                                                                                     Show help and usage information