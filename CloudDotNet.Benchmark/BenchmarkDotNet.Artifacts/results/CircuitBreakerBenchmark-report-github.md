``` ini

BenchmarkDotNet=v0.10.1, OS=Windows
Processor=?, ProcessorCount=8
Frequency=2338335 Hz, Resolution=427.6547 ns, Timer=TSC
dotnet cli version=1.0.0-preview2-1-003177
  [Host]     : .NET Core 4.6.24628.01, 64bit RyuJIT
  DefaultJob : .NET Core 4.6.24628.01, 64bit RyuJIT

Gen 0=0.1116  Allocated=422 B  

```
  Method |        Mean |    StdDev |    StdErr |         Min |          Q1 |      Median |          Q3 |         Max |          P0 |         P25 |         P50 |         P80 |         P85 |         P90 |         P95 |        P100 |       Op/s |
-------- |------------ |---------- |---------- |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |----------- |
 Execute | 355.9789 ns | 2.9496 ns | 0.7616 ns | 352.2151 ns | 354.0430 ns | 355.5863 ns | 358.4314 ns | 361.2643 ns | 352.2151 ns | 354.0553 ns | 355.5863 ns | 358.5810 ns | 359.1046 ns | 360.3095 ns | 361.1233 ns | 361.2643 ns | 2809155.53 |
1 |
.06 |
