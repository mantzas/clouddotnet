``` ini

BenchmarkDotNet=v0.10.1, OS=Windows
Processor=?, ProcessorCount=8
Frequency=2338334 Hz, Resolution=427.6549 ns, Timer=TSC
dotnet cli version=1.0.0-preview2-1-003177
  [Host]     : .NET Core 4.6.24628.01, 64bit RyuJIT
  DefaultJob : .NET Core 4.6.24628.01, 64bit RyuJIT

Gen 0=106.8359  Allocated=53.02 kB  

```
  Method |        Mean |    StdDev |    StdErr |         Min |          Q1 |      Median |          Q3 |         Max |          P0 |         P25 |         P50 |         P80 |         P85 |         P90 |         P95 |        P100 |    Op/s |
-------- |------------ |---------- |---------- |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |-------- |
 Execute | 355.3770 us | 6.2489 us | 1.7331 us | 346.8259 us | 351.5814 us | 353.3998 us | 358.4120 us | 370.4605 us | 346.8259 us | 351.7485 us | 353.3998 us | 358.8140 us | 361.0626 us | 362.9848 us | 366.3595 us | 370.4605 us | 2813.91 |

73 |

.06 |
