``` ini

BenchmarkDotNet=v0.10.1, OS=Windows
Processor=?, ProcessorCount=8
Frequency=2338334 Hz, Resolution=427.6549 ns, Timer=TSC
dotnet cli version=1.0.0-preview2-1-003177
  [Host]     : .NET Core 4.6.24628.01, 64bit RyuJIT
  DefaultJob : .NET Core 4.6.24628.01, 64bit RyuJIT

Gen 0=0.1106  Allocated=422 B  

```
  Method |        Mean |    StdDev |    StdErr |         Min |          Q1 |      Median |          Q3 |         Max |          P0 |         P25 |         P50 |         P80 |         P85 |         P90 |         P95 |        P100 |      Op/s |
-------- |------------ |---------- |---------- |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |---------- |
 Execute | 406.4898 ns | 5.3738 ns | 1.3875 ns | 398.8744 ns | 401.7374 ns | 405.6030 ns | 412.0575 ns | 414.6237 ns | 398.8744 ns | 401.8104 ns | 405.6030 ns | 412.1957 ns | 412.6793 ns | 412.8654 ns | 413.4474 ns | 414.6237 ns | 2460086.1 |
73 |

.06 |
