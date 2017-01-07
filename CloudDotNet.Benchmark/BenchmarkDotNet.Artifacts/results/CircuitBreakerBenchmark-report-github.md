``` ini

BenchmarkDotNet=v0.10.1, OS=Windows
Processor=?, ProcessorCount=8
Frequency=2338334 Hz, Resolution=427.6549 ns, Timer=TSC
dotnet cli version=1.0.0-preview2-1-003177
  [Host]     : .NET Core 4.6.24628.01, 64bit RyuJIT
  DefaultJob : .NET Core 4.6.24628.01, 64bit RyuJIT

Gen 0=138.6068  Allocated=51.79 kB  

```
  Method |        Mean |    StdDev |    StdErr |         Min |          Q1 |      Median |          Q3 |         Max |          P0 |         P25 |         P50 |         P80 |         P85 |         P90 |         P95 |        P100 |    Op/s |
-------- |------------ |---------- |---------- |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |-------- |
 Execute | 234.7074 us | 5.3413 us | 1.3791 us | 223.6322 us | 231.3217 us | 234.8197 us | 237.8083 us | 243.1051 us | 223.6322 us | 231.7731 us | 234.8197 us | 238.4468 us | 240.6815 us | 242.1669 us | 242.9926 us | 243.1051 us | 4260.62 |
 |
1 |
.06 |
