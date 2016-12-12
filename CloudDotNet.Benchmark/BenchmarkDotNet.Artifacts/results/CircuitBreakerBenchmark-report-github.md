``` ini

BenchmarkDotNet=v0.10.1, OS=Windows
Processor=?, ProcessorCount=8
Frequency=2338338 Hz, Resolution=427.6542 ns, Timer=TSC
dotnet cli version=1.0.0-preview2-1-003177
  [Host]     : .NET Core 4.6.24628.01, 64bit RyuJITDEBUG
  DefaultJob : .NET Core 4.6.24628.01, 64bit RyuJIT

Gen 0=0.1111  Allocated=422 B  

```
  Method |        Mean |    StdDev |    StdErr |         Min |          Q1 |      Median |          Q3 |         Max |          P0 |         P25 |         P50 |         P80 |         P85 |         P90 |         P95 |        P100 |       Op/s |
-------- |------------ |---------- |---------- |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |------------ |----------- |
 Execute | 420.3001 ns | 2.5388 ns | 0.6785 ns | 416.8909 ns | 418.3885 ns | 420.8323 ns | 421.4081 ns | 426.0714 ns | 416.8909 ns | 418.5151 ns | 420.8323 ns | 421.5338 ns | 421.7843 ns | 422.5912 ns | 424.0514 ns | 426.0714 ns | 2379252.41 |
.06 |
