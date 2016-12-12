``` ini

BenchmarkDotNet=v0.10.1, OS=Windows
Processor=?, ProcessorCount=8
Frequency=2338338 Hz, Resolution=427.6542 ns, Timer=TSC
dotnet cli version=1.0.0-preview2-1-003177
  [Host]     : .NET Core 4.6.24628.01, 64bit RyuJIT
  DefaultJob : .NET Core 4.6.24628.01, 64bit RyuJIT

Gen 0=0.1096  Allocated=422 B  

```
  Method |        Mean |    StdDev |    StdErr |         Min |          Q1 |      Median |          Q3 |         Max |          P0 |         P25 |         P50 |         P80 |         P85 |         P90 |         P95 |        P100 |       Op/s |
-------- |------------ |---------- |---------- |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |----------- |
 Execute | 414.9457 ns | 1.0921 ns | 0.2919 ns | 412.7136 ns | 414.4885 ns | 414.7646 ns | 415.5391 ns | 416.9976 ns | 412.7136 ns | 414.5105 ns | 414.7646 ns | 415.6390 ns | 415.8282 ns | 416.3414 ns | 416.7250 ns | 416.9976 ns | 2409953.83 |
1 |
.06 |
